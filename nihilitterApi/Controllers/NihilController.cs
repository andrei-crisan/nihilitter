using System.IdentityModel.Tokens.Jwt;
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
    public async Task<ActionResult<IEnumerable<NihilDto>>> GetNihilItems()
    {
        long userIdClaim = Convert.ToInt64(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

        var friendUserIds = await _context.Friends
            .Where(f => f.UserId == userIdClaim)
            .Select(f => f.FriendId)
            .ToListAsync();

        var users = await _context.ApplicationUsers.ToListAsync();

        var nihilItems = await _context.NihilItems
            .Where(n => friendUserIds.Contains(n.UserId))
            .ToListAsync();

        var nihilWithUserDtos = nihilItems.Select(nihil => new NihilDto
        {
            Id = nihil.Id,
            FirstName = users.FirstOrDefault(u => u.Id == nihil.UserId).FirstName,
            LastName = users.FirstOrDefault(u => u.Id == nihil.UserId).LastName,
            Post = nihil.Post,
            PostDate = nihil.PostDate,
            UserId = nihil.UserId
        }).ToList();

        return nihilWithUserDtos;
    }

    //GET OWN
    [HttpGet("/allNihil")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Nihil>>> GetOwnNihilItems()
    {
        long userIdFromHeaders = headerTokenReturner();
        long userIdClaim = Convert.ToInt64(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

        if (userIdFromHeaders != userIdClaim)
        {
            return Unauthorized();
        }
        return await _context.NihilItems.Where(f => f.UserId == userIdFromHeaders).ToListAsync();

    }

    // POST: api/NihilItem
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Nihil>> PostNihilItem([FromBody] Nihil nihilDto)
    {
        long userIdFromHeaders = headerTokenReturner();
        long userIdClaim = Convert.ToInt64(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

        if (userIdFromHeaders != userIdClaim)
        {
            return Unauthorized();
        }

        if (userIdClaim == 0)
        {
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

        return CreatedAtAction("GetNihilItems", new { id = nihilItem.Id }, nihilItem);
    }

    [HttpGet("{id}")]
    [Authorize]
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
    [Authorize]
    public async Task<IActionResult> DeleteNihilItem(long id)
    {
        long userIdFromHeaders = headerTokenReturner();
        long userIdClaim = Convert.ToInt64(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

        if (userIdFromHeaders != userIdClaim)
        {
            return Unauthorized();
        }

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
    [Authorize]
    public async Task<IActionResult> PutNihilItem(long id, NihilDto nihilItemDto)
    {
        long userIdFromHeaders = headerTokenReturner();
        long userIdClaim = Convert.ToInt64(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

        if (userIdFromHeaders != userIdClaim)
        {
            return Unauthorized();
        }

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

    private long headerTokenReturner()
    {
        string authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        string sentToken = authorizationHeader.Replace("Bearer ", "");
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadToken(sentToken) as JwtSecurityToken;
        string userIdStringFromHeaders = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

        return Convert.ToInt64(userIdStringFromHeaders);

    }
}
