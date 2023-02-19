using Backend_Case_Study.Models;
using System.Reflection.Metadata.Ecma335;

namespace Backend_Case_Study
{
    public static class BulkInsertData
    {
        public static IEnumerable<User> GetUsers()
        {
            return new List<User>
            {
                new User() 
                { 
                    Id = 1, 
                    Email = "user1@emailaddress.com", 
                    Name = "User1", 
                    PortfolioId = 1 
                },
                new User() 
                { 
                    Id = 2, 
                    Email = "user2@emailaddress.com", 
                    Name = "User2", 
                    PortfolioId = 2 
                },
                new User() 
                { 
                    Id = 3, 
                    Email = "user3@emailaddress.com", 
                    Name = "User3", 
                    PortfolioId = 3 
                },
                new User() 
                { 
                    Id = 4, 
                    Email = "user4@emailaddress.com", 
                    Name = "User4" 
                },
                new User() 
                { 
                    Id = 5, 
                    Email = "user5@emailaddress.com", 
                    Name = "User5", 
                    PortfolioId = 5 
                },
                new User() 
                { 
                    Id = 6, 
                    Email = "user6@emailaddress.com", 
                    Name = "User6", 
                    PortfolioId = -1 
                },
            };
        }

        public static IEnumerable<Share> GetShares()
        {
            return new List<Share> 
            {
                new Share() 
                { 
                    Id= 1, 
                    LastUpdate = DateTime.Now.ToString(), 
                    Symbol="ABC", 
                    Rate = 10, 
                    Volume = 100, 
                    PortfolioId= 4
                },
                new Share() 
                { 
                    Id= 2, 
                    LastUpdate = DateTime.Now.ToString(), 
                    Symbol="ABD", 
                    Rate = 15, 
                    Volume = 5, 
                    PortfolioId= 4
                },
                new Share() 
                { 
                    Id= 3, 
                    LastUpdate = DateTime.Now.ToString(), 
                    Symbol="ABE", 
                    Rate = 12, 
                    Volume = 100, 
                    PortfolioId= 4 
                },
                new Share() 
                { 
                    Id= 4, 
                    LastUpdate = DateTime.Now.ToString(), 
                    Symbol="ABF", 
                    Rate = 90, 
                    Volume = 20, 
                    PortfolioId= 4 
                },
                new Share() 
                { 
                    Id= 5, 
                    LastUpdate = DateTime.Now.ToString(), 
                    Symbol="ABC", 
                    Rate = 10, 
                    Volume = 10, 
                    PortfolioId= 1
                },
            };
        }

        public static IEnumerable<Portfolio> GetPortfolios()
        {
            return new List<Portfolio> 
            {
                new Portfolio() 
                { 
                    Id= 1, 
                    Name = "User1 Portfolio", 
                    Domain = "Test", 
                    Type = PortfolioType.Private 
                },
                new Portfolio() 
                { 
                    Id= 2, 
                    Name = "User2 Portfolio", 
                    Domain = "Test", 
                    Type = PortfolioType.Private 
                },
                new Portfolio() 
                { 
                    Id= 3, 
                    Name = "User3 Portfolio", 
                    Domain = "Test", 
                    Type = PortfolioType.Private 
                },
                new Portfolio() 
                { 
                    Id= 4, 
                    Name = "Common", 
                    Domain = "Test", 
                    Type = PortfolioType.Public 
                },
                new Portfolio() 
                { 
                    Id= 5, 
                    Name = "User5 Portfolio", 
                    Domain = "Test", 
                    Type = PortfolioType.Private 
                }
            };
        }
    }
}
