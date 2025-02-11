using CarparkInfoAssignmentDhia.CarparkInfo;
using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using CarparkInfoAssignmentDhia.Weather.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> AddToFavorite([FromBody] int carparkId)
    {
        var userId = 0; // TODO: get User Id from Claim NameIdentifier


        return Ok();
    }
}
