using Backend_Case_Study.Contexts;
using Backend_Case_Study.Exceptions;
using Backend_Case_Study.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Case_Study.Helpers
{
    public class DbHelper : IHostedService
    {
        private readonly IDbContextFactory<TableContext> _contextFactory;
        private readonly ILogger _logger;

        public DbHelper(ILogger<DbHelper> logger,
            IDbContextFactory<TableContext> contextFactory) {
            _logger = logger;
            _contextFactory = contextFactory;
        }

        public User? GetUserById(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            User? user = context.Users.SingleOrDefault(user => user.Id == id);
            if (user == null) {
                string errorMsg = $"User with ID {id} was not registered.";
                _logger.LogWarning(errorMsg);
                throw new BadHttpRequestException(errorMsg);
            }

            return user;
        }

        public Share? GetShareBySymbol(Portfolio source, string symbol)
        {
            using var context = _contextFactory.CreateDbContext();
            Share? share = null;
            try
            {
                var shares = context.Shares.Where(share => share.Symbol == symbol).ToList();
                if (!shares.Any()) {
                    throw new ShareNotRegistered($"{symbol} share has not been registered in the system");
                }

                share = shares.SingleOrDefault(sh => sh.PortfolioId.Equals(source.Id));
                if (share == null)
                {
                    string errorMsg = $"{symbol} shares not found in {source.Name}";
                    _logger.LogWarning(errorMsg);
                }
            }
            catch (ShareNotRegistered ex)
            {
                throw new BadHttpRequestException(ex.errorMessage);
            }

            return share;
        }

        public Portfolio GetPortfolioById(int? id)
        {
            using var context = _contextFactory.CreateDbContext();
            Portfolio? portfolio = context.Portfolios.SingleOrDefault(portfolio => portfolio.Id == id);

            if (portfolio == null) {
                string errorMsg = $"Portfolio with ID {id} was not registered.";
                _logger.LogWarning(errorMsg);
                throw new BadHttpRequestException(errorMsg);
            }
            return portfolio;
        }

        public Portfolio? GetCommonPool(string Domain)
        {
            using var context = _contextFactory.CreateDbContext();
            IEnumerable<Portfolio> portfolios;
            try
            {
                portfolios = context.Portfolios
                                .Where(pf => pf.Domain == Domain && pf.Type == PortfolioType.Public);
                if (portfolios.Count() > 1) {
                    throw new MultiplePoolException("A domain cannot have more than one public pool.");
                } else if (!portfolios.Any())
                {
                    string errorMsg = $"Public pool is not found for {Domain}";
                    _logger.LogError(errorMsg);
                    throw new NotFoundException(errorMsg);
                }
            }
            catch (MultiplePoolException ex)
            {
                _logger.LogError(ex.Message);
                throw new BadHttpRequestException(ex.Message);
            }

            return portfolios.ElementAt(0);
        }

        public async Task<bool> UpdateShare(UpdateRequest request)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            Share? share = GetShareBySymbol(request.Source, request.ShareSymbol);
            if (share == null)
            {
                string errorMsg = $"{request.ShareSymbol} is not found in {request.Source.Name}";
                _logger.LogError(errorMsg);
                throw new NotFoundException(errorMsg);
            }

            string soldOrBought = request.processType == ProcessType.Buy ? "Buy" : "Sell";

            _logger.LogInformation($"{soldOrBought} process is being done on {request.ShareSymbol}");

            if (request.Volume > share.Volume)
            {
                return false;
            } 
            else if (request.Volume == share.Volume)
            {
                _logger.LogInformation($"There are just enough shares of " +
                    $"{request.ShareSymbol} to {soldOrBought.ToLowerInvariant()} {request.Volume} volumes.");
                context.Entry(share).State = EntityState.Deleted;
            } 
            else
            {
                _logger.LogInformation($"There are more than enough shares of " +
                    $"{request.ShareSymbol} to {soldOrBought.ToLowerInvariant()} {request.Volume} volumes.");
                share.Volume -= request.Volume;
                context.Update(share);
            }

            await UpdateNewShare(request.Target, request.Source, share, request.Volume);
            await context.SaveChangesAsync();
            return true;
        }

        private async Task UpdateNewShare(Portfolio target, Portfolio source, Share share, int volume)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var newShare = GetShareBySymbol(target, share.Symbol);

            string soldOrBought = target.Type == PortfolioType.Public ? "sold" : "bought";

            if (newShare != null)
            {
                _logger.LogInformation($"{volume} volume has been {soldOrBought} of {share.Symbol}");
                newShare.Volume += volume;
                context.Update(newShare);
            }
            else
            {
                _logger.LogInformation(target.Type == PortfolioType.Public
                    ? $"All volumes of the share {share.Symbol} has been sold."
                    : $"{volume} volumes of a new share called {share.Symbol} has been bought.");
                context.Shares.Add(new Share()
                {
                    Id = (share.Id % 2 == 0 ? share.Id * 15 + 1 : share.Id * 8) ,
                    Symbol = share.Symbol,
                    PortfolioId = target.Id,
                    Volume = volume,
                    LastUpdate = DateTime.Now.ToString(),
                    Rate = share.Rate,
                    Description = share.Description,
                    Title = share.Title
                });
            }

            var transactions = context.Transactions.ToList().Count;
            await context.Transactions.AddAsync(new Transaction
            {
                Id = transactions + 1,
                OperationType = soldOrBought,
                Date = DateTime.Now.ToString(),
                ShareSymbol = share.Symbol,
                Volume = volume,
                Name = soldOrBought == "sold" ? source.Name : target.Name
            });

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Transactions.ToListAsync();
        }
        public void DeleteData()
        {
            using var context = _contextFactory.CreateDbContext();

            var tableNames = context.Model.GetEntityTypes()
                .Select(t => t.GetTableName())
                .Distinct()
                .ToList();
            context.Database.ExecuteSqlRaw("SET session_replication_role = 'replica';");
            foreach(var table in tableNames) {
                context.Database.ExecuteSqlRaw($"TRUNCATE TABLE \"{table}\"");
            }
            context.Database.ExecuteSqlRaw("SET session_replication_role = 'origin';");
        }

        public void BulkInsert()
        {
            DeleteData();

            var UserList = BulkInsertData.GetUsers().ToList();
            var ShareList = BulkInsertData.GetShares().ToList();
            var PortfolioList = BulkInsertData.GetPortfolios().ToList();

            using var context = _contextFactory.CreateDbContext();
            context.Portfolios.AddRange(PortfolioList);
            context.Users.AddRange(UserList);
            context.Shares.AddRange(ShareList);
            context.SaveChanges();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            await context.Database.EnsureCreatedAsync(cancellationToken);
            BulkInsert();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
