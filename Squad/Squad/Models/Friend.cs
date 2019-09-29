using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Squad
{
    public class Friend
    {
        [PrimaryKey]
        public string friendName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
