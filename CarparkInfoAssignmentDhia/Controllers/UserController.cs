using CarparkInfoAssignmentDhia.CarparkInfo;
using CarparkInfoAssignmentDhia.CarparkInfo.Queries;
using CarparkInfoAssignmentDhia.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarparkInfoAssignmentDhia.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IDbContextFactory<CarparkContext> dbContextFactory;

    public UserController(IDbContextFactory<CarparkContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!int.TryParse(request.Username, out var userId))
        {
            return BadRequest("Invalid username.");
        }

        var cancellationToken = HttpContext.RequestAborted;

        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        var isUserExists = await new UserGetById(userId).Query(context.Users).AnyAsync(cancellationToken);

        if (isUserExists && request.Password == "password")
        {
            var claims = new List<Claim>
                {
                    new (ClaimTypes.NameIdentifier, request.Username),
                    new (ClaimTypes.Name, request.Username)
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authProperties);

            return Ok(new { message = "Logged in successfully" });
        }

        return Unauthorized(new { message = "Invalid username or password" });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new { message = "Logged out successfully" });
    }
}