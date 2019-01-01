using System.Threading.Tasks;
using IdentityServer4.Validation;

namespace OneZero.IdentityServerCenter.CustomerService
{
    internal class RescourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}