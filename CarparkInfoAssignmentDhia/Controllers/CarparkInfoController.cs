using CarparkInfoAssignmentDhia.CarparkInfo;
using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using CarparkInfoAssignmentDhia.CarparkInfo.Queries;
using CarparkInfoAssignmentDhia.Weather.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarparkInfoAssignmentDhia.Controllers;
[ApiController]
[Route("[controller]")]
public class CarparkInfoController : Controller
{
    private readonly IDbContextFactory<CarparkContext> dbContextFactory;

    public CarparkInfoController(IDbContextFactory<CarparkContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    [HttpGet(Name = "GetCarpark")]
    public async Task<IEnumerable<Carpark>> GetCarpark(
            [FromQuery] bool? freeParking,
            [FromQuery] bool? nightParking,
            [FromQuery] decimal? vehicleHeight)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();

        return await context.Carparks.ToListAsync();
    }

    [Authorize]
    [HttpPost(Name = "AddToFavorite")]
    public async Task<IActionResult> AddToFavorite([FromBody] string carparkNo)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized("User ID claim missing.");
        }

        if (!int.TryParse(userIdClaim.Value, out var userId))
        {
            return BadRequest("Invalid user ID.");
        }

        await using var context = await dbContextFactory.CreateDbContextAsync();
        var user = await new UserGetById(userId).Query(context.Users).FirstAsync();
        var carpark = await new CarParkGetByNo(carparkNo).Query(context.Carparks).FirstAsync();

        var userFavouriteCarpark = new UserFavoriteCarpark
        {
            Carpark = carpark,
            User = user
        };

        var existing = await new UserFavoriteCarparkGetByUserIdCarparkId(user.Id, carpark.Id)
            .Query(context.UserFavoriteCarparks)
            .AnyAsync();
        if (!existing)
        {
            context.UserFavoriteCarparks.Add(userFavouriteCarpark);
            await context.SaveChangesAsync();
        }

        return Ok();
    }
}
