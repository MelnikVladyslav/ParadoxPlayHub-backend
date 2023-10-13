using BussnesLogic.Entity;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ParadoxPlayHub_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly PPHDbContext _context;

        public NewsController(PPHDbContext context)
        {
            _context = context;
        }

        [HttpPost("add-news")]
        public async Task<IActionResult> AddNews(News news)
        {
            _context.News.Add(news);
            _context.SaveChanges();

            return Ok(news);
        }

        [HttpGet("get-news")]
        public async Task<ActionResult<IEnumerable<News>>> GetNews()
        {
            var news = await _context.News.ToListAsync();
            return Ok(news);
        }
    }
}
