using Backend_Case_Study.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Case_Study.Helpers
{
    public static class ModelHelper
    {
        public static void ValidateUser(User user)
        {
            if (user.PortfolioId == null)
            {
                throw new BadHttpRequestException($"{user.Name} does not have a portfolio yet.");
            }
        }
    }
}
