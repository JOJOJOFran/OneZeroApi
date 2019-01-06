using System.Threading.Tasks;
using IdentityServer4.Validation;

namespace OneZero.IdentityServerCenter.CustomerService
{
    internal class RescourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            context.Result = new GrantValidationResult("1", "2");
            return Task.CompletedTask;
        }
    }
}