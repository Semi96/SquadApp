﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Squad
{
    public class UserModel
    {
        [PrimaryKey]
        public string UserName { get; set; }
    }
}
