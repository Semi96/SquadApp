using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SquadAppAPI.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       
        public DbSet<SquadAppAPI.Models.Todo> Todo { get; set; }
        public DbSet<SquadAppAPI.Models.User> User { get; set; }
        public DbSet<SquadAppAPI.Models.Squad> Squad { get; set; }
        public DbSet<SquadAppAPI.Models.Friendship> Friendship { get; set; }
        public DbSet<SquadAppAPI.Models.SquadMembers> SquadMembers { get; set; }
    }
}
