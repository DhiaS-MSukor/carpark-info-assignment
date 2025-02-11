using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace CarparkInfoAssignmentDhia.Handlers;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
         : base(options, logger, encoder, clock)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        await Task.CompletedTask; // HACK: to remove async method without await green line

        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Missing Authorization Header");

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            if (!"Basic".Equals(authHeader.Scheme, StringComparison.OrdinalIgnoreCase))
                return AuthenticateResult.Fail("Invalid Authorization Scheme");

            if (string.IsNullOrWhiteSpace(authHeader.Parameter))
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
            if (credentials.Length != 2)
                return AuthenticateResult.Fail("Invalid Authorization Header");

            var username = credentials[0];
            var password = credentials[1];

            if (password != "password") // HACK: not actual validation logic
                return AuthenticateResult.Fail("Invalid Username or Password");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, username)
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        catch (Exception)
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }
    }
}