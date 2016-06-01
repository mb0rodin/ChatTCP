using ServerLib.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;


namespace ServerLib
{
    public class Server
    {
        public static Dictionary<string,TcpClient> htUsers = new Dictionary<string,TcpClient>();
        public static Dictionary<TcpClient, string> htConnections = new Dictionary<TcpClient, string>();
       // private IPAddress ipAddress;
        private TcpClient tcpClient;
        public SettingsServerClass settingsServer;
        public Server(SettingsServerClass set)
        {
            settingsServer = set;
        }
        private Thread thrListener;
        private TcpListener tlsClient;
        bool ServRunning = false;
        public static void AddUser(TcpClient tcpUser, string strUsername)
        {
            Server.htUsers.Add(strUsername, tcpUser);
            Server.htConnections.Add(tcpUser, strUsername);

            SendAdminMessage(htConnections[tcpUser] + " Подключился");
        }
        public static void RemoveUser(TcpClient tcpUser)
        {
            if (htConnections[tcpUser] != null)
            {
                SendAdminMessage(htConnections[tcpUser] + " Покинул чат");
                Server.htUsers.Remove(Server.htConnections[tcpUser]);
                Server.htConnections.Remove(tcpUser);
            }
        }
        public static void SendAdminMessage(string Message)
        {
            StreamWriter swSenderSender;
            string[] StringMessage = new string[Server.htUsers.Count];
            Server.htConnections.Values.CopyTo(StringMessage, 0);

            TcpClient[] tcpClients = new TcpClient[Server.htUsers.Count];
            Server.htUsers.Values.CopyTo(tcpClients, 0);
            for (int i = 0; i < tcpClients.Length; i++)
            {
                try
                {
                    if (Message.Trim() == "" || tcpClients[i] == null)
                    {
                        continue;
                    }
                    {
                        swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                        swSenderSender.WriteLine(GetTime() + "|" + "Server: " + Message);
                        swSenderSender.WriteLine("Users|" + String.Join("|", StringMessage));
                        swSenderSender.Flush();
                    }
                }
                catch
                {
                    RemoveUser(tcpClients[i]);
                }
            }
        }

        public static string GetTime()
        {
            return DateTime.Now.ToLongTimeString();
        }
        private static void CheckComand(UserToServer.MessageUser mu)
        {
            try
            {
                if (mu.Comand is Comands.GetUserInfo)
                {
                    Comands.GetUserInfo gui = mu.Comand as Comands.GetUserInfo;
                    string s = File.ReadAllText(gui.UserName + "\\UserInfo");
                    ServerLib.ServerToUser.UserInfo ui = JsonConvert.DeserializeObject<ServerLib.ServerToUser.UserInfo>(s);
                    if (gui.UserName != mu.From)
                        ui.Password = "";
                    ServerToUser.ServerMessage se = new ServerToUser.ServerMessage
                    {
                        Value = ui,
                        Result = "ok"
                    };
                    TcpClient tc;
                    StreamWriter swSenderSender;
                    Server.htUsers.TryGetValue(mu.From, out tc);
                    swSenderSender = new StreamWriter(tc.GetStream());
                    swSenderSender.WriteLine(se);
                    swSenderSender.Flush();
                }
            }
            catch
            {

            }
        }

        public static void SendMessage(string From, string Message)
        {
            StreamWriter swSenderSender;
            UserToServer.MessageUser mu = Newtonsoft.Json.JsonConvert.DeserializeObject<UserToServer.MessageUser>(Message);
            if (mu.Type == TypeMessage.Comand)
                CheckComand(mu);          
            ///////////////////////////.
            TcpClient[] tcpClients = new TcpClient[Server.htUsers.Count];
            string[] StringMessage = new string[Server.htUsers.Count];
            Server.htConnections.Values.CopyTo(StringMessage, 0);
            if (mu.Type == TypeMessage.Message)
            {
                for (int i = 0; i < tcpClients.Length; i++)
                {
                    try
                    {

                            if (mu.To == "All" || mu.To == "" || mu.To.ToLower().Trim() == StringMessage[i].ToLower().Trim() || From.ToLower().Trim() == StringMessage[i].ToLower().Trim())
                            {
                                ServerToUser.ServerMessage se = new ServerToUser.ServerMessage
                                {
                                    Result = "ok",
                                    Value = mu ,
                                    Message =""
                                };
                                swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                                swSenderSender.WriteLine(se);
                                swSenderSender.Flush();
                            }
                    }
                    catch
                    {
                        RemoveUser(tcpClients[i]);
                    }
                }
            }
            //string[] StringMessage = new string[Server.htUsers.Count];
            //Server.htConnections.Values.CopyTo(StringMessage, 0);

            //TcpClient[] tcpClients = new TcpClient[Server.htUsers.Count];
            //Server.htUsers.Values.CopyTo(tcpClients, 0);
            //for (int i = 0; i < tcpClients.Length; i++)
            //{
            //    try
            //    {
            //        if (Message.Trim() == "" || tcpClients[i] == null)
            //        {
            //            continue;
            //        }
            //        //if (Message.Contains("GetUsers"))
            //        //{
            //        //    swSenderSender = new StreamWriter(tcpClients[i].GetStream());
            //        //    swSenderSender.WriteLine("Users|" + String.Join("|", StringMessage));
            //        //    swSenderSender.Flush();
            //        //}
            //        else
            //            if (To[0] == "All" || To[0].ToLower().Trim() == StringMessage[i].ToLower().Trim() || From.ToLower().Trim() == StringMessage[i].ToLower().Trim())
            //            {
            //                swSenderSender = new StreamWriter(tcpClients[i].GetStream());
            //                swSenderSender.WriteLine(GetTime() + "|" + From + ": " + To[1]);
            //                swSenderSender.Flush();
            //            }
            //    }
            //    catch
            //    {
            //        RemoveUser(tcpClients[i]);
            //    }
            //}
        }

        public void StartListening()
        {

            IPAddress ipaLocal = IPAddress.Parse(settingsServer.IP);

            tlsClient = new TcpListener(ipaLocal,2016);

            tlsClient.Start();

            ServRunning = true;

            thrListener = new Thread(KeepListening);
            thrListener.Start();
        }

        private void KeepListening()
        {
            while (ServRunning)
            {
                tcpClient = tlsClient.AcceptTcpClient();
                User newUser = new User(tcpClient,this);
            }
        }
    }
}
