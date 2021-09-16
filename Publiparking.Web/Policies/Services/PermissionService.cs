namespace Publiparking.Web.Policies.Services
{
    public interface IPermissionService
    {
        bool IsDiscountAllowed(string subject, int customerId, string token);
    }

    public class PermissionService : IPermissionService
    {
        public bool IsDiscountAllowed(string subject, int customerId, string token)
        {
            return (string.IsNullOrEmpty(token));
        }
    }
}