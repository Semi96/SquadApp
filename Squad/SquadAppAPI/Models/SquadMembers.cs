using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SquadAppAPI.Models
{
    public class SquadMembers
    {
        [Key]
        public int SquadMembersId { get; set; }
        [ForeignKey("Squad")]
        public int SquadId { get; set; }
        public User User { get; set; }
        public string UserRole { get; set; }
        public DateTime UserAdded { get; set; }
    }
}
