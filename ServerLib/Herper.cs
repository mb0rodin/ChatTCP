using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerLib
{
    public class Herper
    {
        static Herper _Helper;
        static Herper()
        {
            _Helper = new Herper();
        }
        private Herper()
        {

        }
        public static string GetNewErrorMessage(string s)
        {
           return Newtonsoft.Json.JsonConvert.SerializeObject(new ServerToUser.ServerMessage
            {
                Result = "error",
                Message = s
            });
        }
        public static string GetNewOkMessage(string s)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new ServerToUser.ServerMessage
            {
                Result = "ok",
                Message = s
            });
        }
        public static string GetNew(string s)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new ServerToUser.ServerMessage
            {
                Result = "ok",
                Message = s
            });
        }
    }
}
