using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace desert.deer.Api.Security
{
public class HasScopeHandler : AuthorizationHandler<HasScopeRequirements>
{
protected override Task HandleRequirementAsync(AuthorizationHandlerContext
context, HasScopeRequirements requirement)
{
var scopeClaim = context.User.FindFirst(c => c.Type == "scope" && c.Issuer ==
requirement.Issuer);
if (scopeClaim == null)
return Task.CompletedTask;
var scopes = scopeClaim.Value.Split(' ');
if (scopes.Any(s => s == requirement.Scope))
context.Succeed((IAuthorizationRequirement)requirement);
return Task.CompletedTask;
}
}
}