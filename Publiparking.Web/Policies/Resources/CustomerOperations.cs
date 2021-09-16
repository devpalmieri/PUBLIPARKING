using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Publiparking.Web.Policies.Resources
{
    public static class CustomerOperations
    {
        public static OperationAuthorizationRequirement Manage =
            new OperationAuthorizationRequirement { Name = "Manage" };
        public static OperationAuthorizationRequirement SendMail =
            new OperationAuthorizationRequirement { Name = "SendMail" };

        public static OperationAuthorizationRequirement CheckToken(string token)
        {
            return new CheckOperationAuthorizationRequirement
            {
                Name = "CheckToken",
                Token = token
            };
        }
    }

    public class CheckOperationAuthorizationRequirement : OperationAuthorizationRequirement
    {
        public string Token { get; set; }
    }
}