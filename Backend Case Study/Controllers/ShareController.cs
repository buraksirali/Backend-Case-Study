using Backend_Case_Study.Exceptions;
using Backend_Case_Study.GeneratedControllers;
using Backend_Case_Study.Helpers;
using Backend_Case_Study.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Case_Study.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShareController : SharesControllerBase
    {
        private readonly DbHelper databaseHelper;
        private readonly ILogger<ShareController> _logger;
        public ShareController(DbHelper dbHelper, ILogger<ShareController> logger)
        {
            databaseHelper = dbHelper;
            _logger = logger;
        }

        public override async Task<IActionResult> SharesBuy(BuyRequest request, CancellationToken cancellationToken = default)
        {
            Portfolio? portfolio;
            Portfolio? commonPool;
            bool result;
            try
            {
                var user = databaseHelper.GetUserById(request.UserId);
                _logger.LogInformation($"Buy process is being done for user {user.Name}");
                
                ModelHelper.ValidateUser(user);

                portfolio = databaseHelper.GetPortfolioById(user.PortfolioId);

                _logger.LogInformation($"Current domain that buy process is being done: {portfolio.Domain}");
                commonPool = databaseHelper.GetCommonPool(portfolio.Domain);

                result = await databaseHelper.UpdateShare(new UpdateRequest()
                {
                    Source = commonPool,
                    Target = portfolio,
                    ShareSymbol = request.ShareSymbol,
                    Volume = request.Volume,
                    processType = ProcessType.Buy
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.errorMessage);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }

            return result ? Ok("Buy process was successful.") : BadRequest("There are not enough shares for buy process.");
        }

        public override async Task<IActionResult> SharesSell(SellRequest request, CancellationToken cancellationToken = default)
        {
            Portfolio? portfolio;
            Portfolio? commonPool;
            bool result;
            try
            {
                var user = databaseHelper.GetUserById(request.UserId);
                _logger.LogInformation($"Sell process is being done for user {user.Name}");

                ModelHelper.ValidateUser(user);

                portfolio = databaseHelper.GetPortfolioById(user.PortfolioId);

                _logger.LogInformation($"Current domain that sell process is being done: {portfolio.Domain}");
                commonPool = databaseHelper.GetCommonPool(portfolio.Domain);

                result = await databaseHelper.UpdateShare(new UpdateRequest()
                {
                    Source = portfolio,
                    Target = commonPool,
                    ShareSymbol = request.ShareSymbol,
                    Volume = request.Volume,
                    processType = ProcessType.Sell
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.errorMessage);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }

            return result ? Ok("Sell process was successful.") : BadRequest("There are not enough shares for sell process.");
        }
    }
}
