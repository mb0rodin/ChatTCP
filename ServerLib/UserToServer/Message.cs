using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServerLib.Comands;

namespace ServerLib.UserToServer
{
    public class MessageUser : ServerLib.ServerToUser.IValue
    {
        public ServerLib.TypeMessage Type;
        public string From;
        public string Value;
        public string To;
        public IComand Comand;
    }
}
