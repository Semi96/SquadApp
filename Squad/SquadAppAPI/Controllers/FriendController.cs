using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SquadAppAPI.Models;

namespace SquadAppAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Friend")]
    public class FriendController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public FriendController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET api/user
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "what ?" };
        }

        [Authorize]
        [HttpGet, Route("request")]

        public async Task<IActionResult> FriendRequest(string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var friend = await _userManager.FindByNameAsync(username);

       
            var newFriendship = new Friendship
            {
                UserA = user,
                UserB = friend,
                DateCreated = DateTime.Now,
                IsAccepted = false
            };

            var friends = _context.Friendship.Add(newFriendship);

            return Ok();
        }
        // GET api/values/5
        [HttpGet, Route("search")]
        
        public async Task<IActionResult> Get(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
    
             var friends =  _context.User.Where(x => x.UserName.Contains(name)).Select(y => new { FriendName = y.UserName, y.FirstName, y.LastName }).ToList();
           

            var jsonUsers = JsonConvert.SerializeObject(friends);

            if (jsonUsers == null)
            {
                return NotFound();
            }

            return Ok(friends);
        }

        [HttpPost]
        public async Task<IActionResult> UserTodo(string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newUser = new User
            {
                FirstName = username,
                UserName = username,
                DateCreated = DateTime.Now,


            };

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/PutUser/5
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
