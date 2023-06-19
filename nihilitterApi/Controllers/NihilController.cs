using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<ActionResult<IEnumerable<Nihil>>> GetNihilItems()
    {
        return await _context.NihilItems.ToListAsync();
    }

    // POST: api/NihilItem
    [HttpPost]
    public async Task<ActionResult<Nihil>> PostNihilItem([FromBody] Nihil nihilDto)
    {
        Nihil nihilItem = new Nihil
        {
            Id = nihilDto.Id,
            Post = nihilDto.Post,
            PostDate = nihilDto.PostDate
        };

        _context.NihilItems.Add(nihilItem);
        await _context.SaveChangesAsync();

        //Todo: To return here and impl GetNihilItem Route
        return CreatedAtAction("GetNihilItems", new { id = nihilItem.Id }, nihilItem);
    }
}
