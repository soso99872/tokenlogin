using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tokenlogin.Models
{
    public class Token
    {
        public string Access_token { get; set; }
        public string Refresh_token { get; set; }

        public DateTime expires_in { get; set; }
    }
}