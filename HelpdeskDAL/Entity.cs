//Author Minh Nguyen
//description: use to reduce cost while each table alway have ID and timer
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace HelpdeskDAL
{
    public class Entity
    {
        public int Id { get; set; }
        [Timestamp]
        public byte[] Timer { get; set; }
    }
}
