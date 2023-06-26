using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NihilitterApi.Dto;
using NihilitterApi.Models;

namespace nihilitterApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FriendshipController : ControllerBase
{
    private readonly NihilContext _context;

    public FriendshipController(NihilContext context)
    {
        _context = context;
    }

    // GET: api/NihilItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Friendship>>> GetFriends()
    {
        return await _context.Friends.ToListAsync();
    }

    [HttpGet("friends")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllFriends()
    {
        {
            //Todo: hardcoded Id to be removed with TokenClaimId
            var friendShipListOfUser = await _context.Friends.Where(friend => friend.UserId == 1).ToListAsync();
            var friendsOfUser = await _context.ApplicationUsers
                .Where(user => friendShipListOfUser.Select(friend => friend.FriendId).Contains(user.Id)).ToListAsync();

            var friendsOfUser2 = await _context.ApplicationUsers
                     .Where(user => friendShipListOfUser.Select(friend => friend.FriendId).Contains(user.Id))
                     .Select(user => new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Country = user.Country,
                        Email = user.Email
                    }).ToListAsync();
            return friendsOfUser2;
        }
    }

    // POST: api/NihilItem
    [HttpPost]
    public async Task<ActionResult<Nihil>> SaveFriend([FromBody] FriendDto friendDto)
    {
        string userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

        if (userIdClaim == null)
        {
            return NotFound();
        }

        Friendship friendItem = new Friendship
        {
            UserId = Convert.ToInt64(userIdClaim),
            FriendId = friendDto.FriendId,
            isConfirmed = false
        };

        _context.Friends.Add(friendItem);
        await _context.SaveChangesAsync();

        //Todo: To return here and impl GetNihilItem Route
        return CreatedAtAction("GetFriends", new { id = friendItem.Id }, friendItem);
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
    public async Task<IActionResult> ConfirmFriendship(long id)
    {
        var friendItem = await _context.Friends.FindAsync(id);

        if (friendItem == null)
        {
            return NotFound();
        }

        friendItem.isConfirmed = true;

        //TODO: To add in the list of the one who accepts the new confirmed friend also!!!!!
        //Todo: See my own nihilieets on my PROFILE page endpoint pentru

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FriendItem(id))
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

    private bool FriendItem(long id)
    {
        return _context.Friends.Any(e => e.Id == id);
    }
}
