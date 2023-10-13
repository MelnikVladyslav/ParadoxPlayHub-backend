using BussnesLogic.Entity;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace ParadoxPlayHub_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GanresController : ControllerBase
    {
        private readonly PPHDbContext _context;

        public GanresController(PPHDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-ganres")]
        public async Task<ActionResult<IEnumerable<Ganr>>> GetGanres()
        {
            var ganres = await _context.Ganrs.ToListAsync();
            return Ok(ganres);
        }
    }
}
