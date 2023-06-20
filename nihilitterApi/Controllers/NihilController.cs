using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NihilitterApi.Dto;
using NihilitterApi.Models;

namespace nihilitterApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NihilController : ControllerBase
{
    private readonly NihilContext _context;

    public NihilController(NihilContext context)
    {
        _context = context;
    }

    // GET: api/NihilItems
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Nihil>>> GetNihilItems()
    {
        return await _context.NihilItems.ToListAsync();
    }

    // POST: api/NihilItem
    [HttpPost]
    public async Task<ActionResult<Nihil>> PostNihilItem([FromBody] Nihil nihilDto)
    {
        //Todo: ValidareNotToCrash
        string userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

        if(userIdClaim==null){
            return NotFound();
        }

        Nihil nihilItem = new Nihil
        {
            Id = nihilDto.Id,
            Post = nihilDto.Post,
            PostDate = nihilDto.PostDate,
            UserId = Convert.ToInt64(userIdClaim)
        };

        _context.NihilItems.Add(nihilItem);
        await _context.SaveChangesAsync();

        //Todo: To return here and impl GetNihilItem Route
        return CreatedAtAction("GetNihilItems", new { id = nihilItem.Id }, nihilItem);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Nihil>> GetNihilItem(long id)
    {
        var nihilItem = await _context.NihilItems.FindAsync(id);

        if (nihilItem == null)
        {
            return NotFound();
        }

        return nihilItem;
    }

    //DELETE: /api/NihilItems/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNihilItem(long id)
    {
        var nihilItem = await _context.NihilItems.FindAsync(id);

        if (nihilItem == null)
        {
            return NotFound();
        }

        _context.NihilItems.Remove(nihilItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutNihilItem(long id, NihilDto nihilItemDto)
    {
        if (id != nihilItemDto.Id)
        {
            return BadRequest();
        }

        var nihilItem = await _context.NihilItems.FindAsync(id);

        if (nihilItem == null)
        {
            return NotFound();
        }

        nihilItem.Post = nihilItemDto.Post;
        nihilItem.PostDate = nihilItem.PostDate;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NihilItemExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    private bool NihilItemExists(long id)
    {
        return _context.NihilItems.Any(e => e.Id == id);
    }
}
