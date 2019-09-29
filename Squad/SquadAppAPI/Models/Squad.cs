using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SquadAppAPI.Models
{
    public class Squad
    {
        [Key]
        public int SquadId { get; set; }
        public string SquadName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
