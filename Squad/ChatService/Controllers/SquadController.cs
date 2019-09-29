using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace easychat
{
    [Produces("application/json")]
    [Route("api/Squad")]
    public class ChatController : Controller
    {
      

        public ChatController()
        {
         
        }
        // GET api/user
        [HttpGet]
        [Route("what")]
        public IEnumerable<string> Get()
        {
            return new string[] { "what ?" };
        }
       
    }
}
