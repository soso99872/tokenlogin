using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tokenlogin.Models
{
    public class Payload
    {
        public User Info{get;set;}

        public int exp { get; set; }
    }
}