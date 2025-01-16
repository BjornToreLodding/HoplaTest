using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersApi.Data;
using UsersApi.Models;

namespace UsersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] int? id)
    {
        if (id.HasValue)
        {
            var user = await _context.Users
                .Include(u => u.Horses) // Inkluder hester
                .FirstOrDefaultAsync(u => u.Id == id.Value);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }

        var users = await _context.Users
            .Include(u => u.Horses) // Inkluder hester
            .ToListAsync();

        return Ok(users);
    }
}
