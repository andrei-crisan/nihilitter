using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NihilitterApi.Dto;
using NihilitterApi.Models;

namespace nihilitterApi.Controllers;

[Route("friendship/")]
[ApiController]
public class FriendshipController : ControllerBase
{
    private readonly NihilContext _context;

    public FriendshipController(NihilContext context)
    {
        _context = context;
    }

    // GET: api/NihilItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Friendship>>> GetFriendShips()
    {
        return await _context.Friends.ToListAsync();
    }

    // GET: api/friends
    [HttpGet("friends")]
    public async Task<ActionResult<IEnumerable<FriendDto>>> GetAllFriendsByUser()
    {
        {
            //Todo: checkings for NULLS
            long userIdClaim = Convert.ToInt64(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

            var friendShipListOfUser = await _context.Friends.Where(friend => friend.UserId == userIdClaim).ToListAsync();
            var friendsOfUser = await _context.ApplicationUsers
     .Where(user => friendShipListOfUser.Select(friend => friend.FriendId).Contains(user.Id))
     .Select(user => new FriendDto
     {
         Id = user.Id,
         FirstName = user.FirstName,
         LastName = user.LastName,
         Country = user.Country,
         Email = user.Email
     })
     .ToListAsync();

            foreach (var friend in friendsOfUser)
            {
                var friendShip = friendShipListOfUser.FirstOrDefault(f => f.FriendId == friend.Id);
                friend.IsConfirmed = friendShip != null && friendShip.isConfirmed;
            }
            return friendsOfUser;
        }
    }

    // POST: api/NihilItem
    [HttpPost]
    public async Task<ActionResult<Nihil>> SaveFriendship([FromBody] FriendshipDto friendDto)
    {
        string userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

        if (userIdClaim == null)
        {
            return NotFound();
        }

        Friendship myFriendship = new Friendship
        {
            UserId = Convert.ToInt64(userIdClaim),
            FriendId = friendDto.FriendId,
            isConfirmed = false
        };

        Friendship hisFriendship = new Friendship
        {
            UserId = friendDto.FriendId,
            FriendId = Convert.ToInt64(userIdClaim),
            isConfirmed = true
        };

        _context.Friends.Add(myFriendship);
        _context.Friends.Add(hisFriendship);
        await _context.SaveChangesAsync();

        //Todo: To return here and impl GetNihilItem Route
        return CreatedAtAction("GetFriends", new { id = myFriendship.Id }, myFriendship);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Nihil>> GetFriendShipById(long id)
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
    public async Task<IActionResult> DeleteFriendship(long id)
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
