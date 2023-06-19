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
}
