using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SquadAppAPI.Models;

namespace SquadAppAPI.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET api/values
        [ResponseCache(Duration = 60)]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, true /*RememberMe*/, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(""));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:57304",
                    audience: "http://localhost:57304",
                    claims: new List<Claim>(),
                    expires: /*DateTime.Now.AddMinutes(60)*/null,
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost, Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel userReg)
        {
            if (userReg == null)
            {
                return BadRequest("Invalid client request");
            }

            var user = new User
            {
                Email = userReg.Email,
                UserName = userReg.Username,
                FirstName = userReg.FirstName,
                LastName = userReg.LastName,
                BirthDate = userReg.BirthDate,
                PhoneNumber = userReg.PhoneNumber,
                DateCreated = DateTime.Now
            };
            try
            {
                var result = await _userManager.CreateAsync(user, userReg.Password);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest("Error creating user");
            }
            catch (Exception e) { }
            return BadRequest("Error creating user");

        }
    }
}