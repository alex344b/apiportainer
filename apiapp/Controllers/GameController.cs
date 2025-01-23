using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using apiapp.Models;
using apiapp.Data;

namespace apiapp.Controllers;

[Route("api/[controller]")]
[ApiController]

public class GameController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;

    public GameController(DatabaseContext dbcontext)
    {
        _databaseContext = dbcontext;
    }

    [HttpGet("GetAllGames")]

    public async Task<IActionResult> GetAllGames()
    {
        List<Game> Games = await _databaseContext.Games.ToListAsync();
        return Ok(Games);
    }
    
    [HttpGet("GetSpecificGames")]
    public async Task<IActionResult> GetSpecificGame([FromQuery] int id)
    {
        Game game = await _databaseContext.Games.FindAsync(id);
        return Ok(game);
    }
    
    
    [HttpPost("CreateGame")]
    public async Task<IActionResult> AddGame([FromQuery] Game game)
    {
        if (game == null)
        {
            return BadRequest("Game already exists");
        }

        game.created_at = DateTime.UtcNow.AddHours(+1);
        game.updated_at = DateTime.UtcNow.AddHours(+1);
        
        _databaseContext.Games.Add(game);
        await _databaseContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAllGames), new { id = game.id }, game);
    }

    [HttpPut("Update{id}")]
    public async Task<IActionResult> UpdateGame([FromBody] Game game)
    {
        if (game == null || game.id != game.id)
        {
            return BadRequest("Game already exists");
        }
        var existingGame = await _databaseContext.Games.FindAsync(game.id);

        if (existingGame == null)
        {
            return NotFound("game not found");
        }
        existingGame.gameName = game.gameName;
        existingGame.gameDuration = game.gameDuration;
        existingGame.updated_at =DateTime.UtcNow.AddHours(+1);
        await _databaseContext.SaveChangesAsync();
        return Ok(existingGame);
    }


    [HttpDelete("Delete{id}")]
    public async Task<IActionResult> DeleteGame([FromRoute] int id)
    {
        Game game = await _databaseContext.Games.FindAsync(id);

        if (game == null)
        {
            return NotFound("game not found");
        }
        
        _databaseContext.Games.Remove(game);
        
        await _databaseContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("PatchSpecificGame")]
        public async Task<IActionResult> PatchGameName( string gameName, [FromBody] Game game)
        {
            if (game == null)
            {
                return BadRequest("Game already exists");
            }
            game.gameName = gameName;
            game.updated_at = DateTime.UtcNow.AddHours(+1);
            game.created_at = game.created_at;
            _databaseContext.Games.Update(game);
            _databaseContext.SaveChanges();
            return NoContent();
        }
}