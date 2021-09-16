using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Publiparking.Web.Policies.Models;
using Publiparking.Web.Policies.Services;
using System.Threading.Tasks;

namespace Publiparking.Web.Policies.Resources
{
    public class CustomerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, PolicyUser>
    {
        private readonly IPermissionService _permissions;

        public CustomerAuthorizationHandler(IPermissionService permissions)
        {
            _permissions = permissions;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, PolicyUser resource)
        {
            var nop = Task.CompletedTask;

            // L'utente deve essere autenticato
            if (!context.User.HasClaim("authenticated", "1"))
            {
                return nop;
            }

            //Da applicare per geolocalizzazione utente
            //if (!context.User.HasClaim("region", resource.Region))
            //{
            //    return nop;
            //}

            // Se si tratta di tipo utente=2
            //if (resource.TipoUser)
            //{
            //    if (!context.User.HasClaim("tipoutente", "2"))
            //    {
            //        return nop;
            //    }
            //}

            if (requirement.Name == "CheckToken")
            {
                HandleDiscountOperation(context, requirement, resource);
                return nop;
            }

            context.Succeed(requirement);
            return nop;
        }

        private void HandleDiscountOperation(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, PolicyUser resource)
        {
            var discountOperation = requirement as CheckOperationAuthorizationRequirement;
            var salesRep = context.User.FindFirst("Token").Value;

            var result = _permissions.IsDiscountAllowed(
                salesRep,
                resource.Id,
                discountOperation.Token);

            if (result)
            {
                context.Succeed(requirement);
            }
        }
    }
}