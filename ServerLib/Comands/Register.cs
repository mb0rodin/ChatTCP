using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerLib.Comands
{
    public class Register : IComand
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasServer { get; set; }
    }
}
