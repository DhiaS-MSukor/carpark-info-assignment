using CarparkInfoAssignmentDhia.CarparkInfo;
using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using CarparkInfoAssignmentDhia.CarparkInfo.Queries;
using CarparkInfoAssignmentDhia.Dtos;
using CarparkInfoAssignmentDhia.Exts;
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

    [HttpGet("GetCarpark")]
    public async Task<IEnumerable<CsvDto>> GetCarpark(
            [FromQuery] bool? freeParking,
            [FromQuery] bool? nightParking,
            [FromQuery] decimal? vehicleHeight,
            [FromQuery] int take = 20)
    {
        var cancellationToken = HttpContext.RequestAborted;
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        var result = await new CarparkGetWithFilters(freeParking, nightParking, vehicleHeight)
            .Query(context.Carparks)
            .Take(take)
            .ToListAsync(cancellationToken);

        return result.Select(x => x.ToCsvDto());
    }

    [Authorize]
    [HttpPost("AddToFavorite")]
    public async Task<IActionResult> AddToFavorite([FromBody] string carparkNo)
    {
        var cancellationToken = HttpContext.RequestAborted;

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized("User ID claim missing.");
        }

        if (!int.TryParse(userIdClaim.Value, out var userId))
        {
            return BadRequest("Invalid user ID.");
        }

        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        var user = await new UserGetById(userId).Query(context.Users).FirstAsync(cancellationToken);
        var carpark = await new CarparkGetByNo(carparkNo).Query(context.Carparks).FirstAsync(cancellationToken);

        var userFavouriteCarpark = new UserFavoriteCarpark
        {
            Carpark = carpark,
            User = user
        };

        var existing = await new UserFavoriteCarparkGetByUserIdCarparkId(user.Id, carpark.Id)
            .Query(context.UserFavoriteCarparks)
            .AnyAsync(cancellationToken);
        if (!existing)
        {
            context.UserFavoriteCarparks.Add(userFavouriteCarpark);
            await context.SaveChangesAsync(cancellationToken);
        }

        return Ok();
    }
}
