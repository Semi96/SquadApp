using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SquadAppAPI.Models
{
    public class Friendship
    {
        [Key]
        public int FriendshipId { get; set; }
        public User UserA { get; set; }
        public User UserB { get; set; }
        public DateTime DateCreated { get; set; }
        public Boolean IsAccepted { get; set; }
    }
}
