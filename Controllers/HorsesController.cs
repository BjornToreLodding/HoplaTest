using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersApi.Data;
using UsersApi.Models;

namespace UsersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HorsesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public HorsesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetHorses([FromQuery] int? id)
    {
        if (id.HasValue)
        {
            var horse = await _context.Horses
                .Include(h => h.User) // Inkluder relasjonen til User
                .FirstOrDefaultAsync(h => h.Id == id.Value);

            if (horse == null)
            {
                return NotFound($"Horse with ID {id} not found.");
            }

            return Ok(horse);
        }

        var horses = await _context.Horses
            .Include(h => h.User) // Inkluder relasjonen til User
            .ToListAsync();

        return Ok(horses);
    }

    [HttpPost]
    public async Task<IActionResult> AddHorse([FromBody] Horse horse)
    {
        _context.Horses.Add(horse);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetHorses), new { id = horse.Id }, horse);
    }
}
