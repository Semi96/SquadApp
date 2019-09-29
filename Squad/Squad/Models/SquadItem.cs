using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Squad
{
    public class SquadItem
    {
        [PrimaryKey, AutoIncrement]
        public int SquadId { get; set; }
        public string SquadName { get; set; }
        public string SquadMembersId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
