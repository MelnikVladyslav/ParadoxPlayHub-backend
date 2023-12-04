﻿using BussnesLogic.DTO_s;
using BussnesLogic.Entity;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ParadoxPlayHub_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly PPHDbContext _context;

        public GameController(PPHDbContext context)
        {
            _context = context;
        }

        [HttpPost("add-game")]
        public async Task<ActionResult> AddGame(GameDTO gameDTO)
        {
            Game game = new Game()
            {
                Name = gameDTO.Name,
                Description = gameDTO.Description,
                DeveloperId = gameDTO.DeveloperId,
                GanrId = gameDTO.GanrId,
                Price = gameDTO.Price,
                ImagePath = gameDTO.ImagePath
            };

            _context.Games.Add(game);
            _context.SaveChanges();

            return Ok(game);
        }

        [HttpGet("get-games")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            var gamers = await _context.Games.ToListAsync();
            return Ok(gamers);
        }

        [HttpGet("get-my-games")]
        public async Task<ActionResult<IEnumerable<Game>>> GetMyGame(LibraryDTO libUser)
        {
            var myGames = libUser.Games.ToList();
            return Ok(myGames);
        }

        [HttpGet("get-game")]
        public async Task<ActionResult> GetGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }
    }
}
