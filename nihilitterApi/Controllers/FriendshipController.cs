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
    public async Task<ActionResult<IEnumerable<FriendDto>>> GetAllFriendShipsOfUser()
    {
        {
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
                                   Email = user.Email,

                               }).ToListAsync();

            foreach (var friend in friendsOfUser)
            {
                var friendShip = friendShipListOfUser.FirstOrDefault(f => f.FriendId == friend.Id);
                friend.IsConfirmed = friendShip != null && friendShip.isConfirmed;
                friend.FriendshipId = friendShip?.Id;

            }
            return friendsOfUser;
        }
    }
    [HttpGet("followers")]
    public async Task<ActionResult<IEnumerable<FriendDto>>> GetFollowers()
    {
        {
            long userIdClaim = Convert.ToInt64(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            var friendShipListOfUser = await _context.Friends.Where(friend => friend.FriendId == userIdClaim).ToListAsync();
            var friendsOfUser = await _context.ApplicationUsers
                               .Where(user => friendShipListOfUser.Select(friend => friend.UserId).Contains(user.Id))
                               .Select(user => new FriendDto
                               {
                                   Id = user.Id,
                                   FirstName = user.FirstName,
                                   LastName = user.LastName,
                                   Country = user.Country,
                                   Email = user.Email,

                               }).ToListAsync();

            foreach (var friend in friendsOfUser)
            {
                var friendShip = friendShipListOfUser.FirstOrDefault(f => f.FriendId == friend.Id);
                friend.IsConfirmed = friendShip != null && friendShip.isConfirmed;
                friend.FriendshipId = friendShip?.Id;

            }
            return friendsOfUser;
        }
    }
    // POST: api/NihilItem
    [HttpPost]
    public async Task<ActionResult<Friendship>> SaveFriendship([FromBody] long newFriendId)
    {
        string userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

        if (userIdClaim == null)
        {
            return NotFound();
        }

        Friendship myFriendship = new Friendship
        {
            UserId = Convert.ToInt64(userIdClaim),
            FriendId = newFriendId,
            isConfirmed = false
        };

        _context.Friends.Add(myFriendship);
        await _context.SaveChangesAsync();

        //Todo: To return here and impl Friendship Route
        return CreatedAtAction("SaveFriendship", new { id = myFriendship.Id }, myFriendship);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Friendship>> GetFriendShipById(long id)
    {
        var friendship = await _context.Friends.FindAsync(id);

        if (friendship == null)
        {
            return NotFound();
        }

        return friendship;
    }

    //DELETE: /api/friendships/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFriendship(long id)
    {
        var friendshipItem = await _context.Friends.FindAsync(id);

        if (friendshipItem == null)
        {
            return NotFound();
        }

        _context.Friends.Remove(friendshipItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ConfirmFriendship(long id, FriendRequest friendRequest)
    {
        long userIdClaim = Convert.ToInt64(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

        var friendItem = await _context.Friends.FindAsync(id);
        var userToAdd = await _context.Friends.FirstOrDefaultAsync(f => f.UserId == friendRequest.UserId && f.FriendId == userIdClaim);


        if (userToAdd == null)
        {
            return NotFound();
        }

        userToAdd.isConfirmed = true;

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
