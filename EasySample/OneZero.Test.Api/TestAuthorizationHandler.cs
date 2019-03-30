using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace OneZero.Test.Api
{
    public class TestAuthorizationHandler : AuthorizationHandler<TestRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TestRequirement requirement)
        {
            var con = context;
            context.
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}