using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Security.Claims;

namespace hairDresser.IntegrationTests
{
    public class FakePolicyEvaluator : IPolicyEvaluator
    {
        public virtual async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            var testScheme = "FakeScheme";
            var principal = new ClaimsPrincipal();
            principal.AddIdentity(new ClaimsIdentity(new[]
            {
                // No need to add "correct" Claims like this: new Claim(ClaimTypes.Role, "admin"). You just need to add a random Claim for the fake evaluator to bypass [Authorize] Attribute.
                new Claim("Permission", "CanViewPage"),
            }, testScheme));

            return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, new AuthenticationProperties(), testScheme)));
        }

        public virtual async Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
        {
            return await Task.FromResult(PolicyAuthorizationResult.Success());
        }
    }
}
