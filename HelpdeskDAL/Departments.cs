﻿using System;
using System.Collections.Generic;

namespace HelpdeskDAL
{
    public partial class Departments : Entity
    {
        public Departments()
        {
            Employees = new HashSet<Employees>();
        }

        public string DepartmentName { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
