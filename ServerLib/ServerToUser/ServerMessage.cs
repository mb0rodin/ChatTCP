
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerLib.ServerToUser
{
    public class ServerMessage : IServerMessage
    {
        public string Result;
        public string Message;
        public IValue Value;
    }
}
