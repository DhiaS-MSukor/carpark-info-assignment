using CarparkInfoAssignmentDhia.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarparkInfoAssignmentDhia.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (request.Password == "password")// HACK: not actual validation logic
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