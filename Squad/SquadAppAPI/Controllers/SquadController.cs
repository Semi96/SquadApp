using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SquadAppAPI.Models;


namespace SquadAppAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Squad")]
    public class SquadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SquadController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET api/user
        [HttpGet]
        [Route("what")]
        public IEnumerable<string> Get()
        {
            return new string[] { "what ?" };
        }
        [HttpGet]
        [Route("hello")]
        public IEnumerable<string> Hello()
        {
            return new string[] { "hello ?" };
        }

        // GET api/values/5
        [HttpGet("{SquadName}")]
        public async Task<IActionResult> Get(string SquadName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var squad = await _context.Squad.SingleOrDefaultAsync(m => m.SquadName == SquadName);

            if (squad == null)
            {
                return NotFound();
            }

            return Ok(squad);
        }

        [HttpPost]
        public async Task<IActionResult> UserToSquad(string squadName, string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _context.User.SingleOrDefaultAsync((m => m.UserName == userName));
            var squad = await _context.Squad.SingleOrDefaultAsync((m => m.SquadName == squadName));

            var newMember = new SquadMembers
            {
                SquadId = squad.SquadId,
                User = user,
                UserAdded = DateTime.Now,
                UserRole = "participant"

            };
            _context.SquadMembers.Add(newMember);
            await _context.SaveChangesAsync();

            return Ok(newMember);
        }
        [HttpPost]
        [Route("NewSquad")]
        public async Task<IActionResult> NewSquad(string squadName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var newSquad = new Squad
            {
               SquadName = squadName

            };
            _context.Squad.Add(newSquad);
            await _context.SaveChangesAsync();

            return Ok(newSquad);
        }

        //// PUT: api/PutUser/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != user.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
                
                
        //            throw;
                
        //    }

        //    return NoContent();
        //}

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
