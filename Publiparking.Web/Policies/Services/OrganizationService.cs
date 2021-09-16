using Publiparking.Web.Policies.Requirements;
using System.Security.Claims;

namespace Publiparking.Web.Policies.Services
{
    public interface IOrganizationService
    {
        JobLevel GetJobLevel(ClaimsPrincipal user);
    }

    public class OrganizationService : IOrganizationService
    {
        public JobLevel GetJobLevel(ClaimsPrincipal user)
        {
            return JobLevel.Developer;
        }
    }
}