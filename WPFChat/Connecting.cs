using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WPFChat
{
    public class Connecting
    {
        #region Constructor
        public Connecting(Client cl)
        {
            pClient = cl;
        }
        #endregion
        #region  Vars
        private StreamWriter swSender;
        private StreamReader srReceiver;
        private TcpClient tcpServer;
        private Thread thrMessaging;
        private IPAddress ipAddr;
        private Client pClient;
        private Dictionary<string, PrivateChat> PrivateMessages = new Dictionary<string, PrivateChat>();
        private bool Connected = true;
        private ServerLib.ServerToUser.UserInfo _UI;
        private ServerLib.ServerToUser.UserInfo UI
        {
            get
            {
                if (_UI != null)
                    return _UI;
                return new ServerLib.ServerToUser.UserInfo();
            }
            set
            {
                _UI = value;
            }
        }
        public Dictionary<string, ServerLib.ServerToUser.UserInfo> Users = new Dictionary<string,ServerLib.ServerToUser.UserInfo>();
        public Messages Ex;
        public Messages MessChat;

        public delegate void Messages(object message);
        #endregion
        #region User
        public PrivateChat GetUserPrivateChat(string user)
        {
            PrivateChat p;
            if (PrivateMessages.TryGetValue(user, out p))
                return p;
            PrivateMessages.Add(user, new PrivateChat(pClient, user));
            PrivateMessages.TryGetValue(user, out p);
            return p;
        }
        public void SetUserInfo(ServerLib.ServerToUser.UserInfo i) 
        {
            UI = i;
        }
        public ServerLib.ServerToUser.UserInfo GetUserInfo()
        {
            return UI;
        }

        public ServerLib.ServerToUser.UserInfo GetUserInfo(string s)
        {
            if (GetUserInfo().NickName == s)
                return GetUserInfo();
            ServerLib.ServerToUser.UserInfo user = null;
            if (Users.TryGetValue(s, out user))
            {
                return user;
            }
            else
            {
                string d = "";
                string json = JsonConvert.SerializeObject
                    (
                    new ServerLib.UserToServer.MessageUser
                    {
                        Comand = new ServerLib.Comands.GetUserInfo { UserName = s },
                        From = UI.NickName,
                        Type = ServerLib.TypeMessage.Comand
                    }
                    );
                Send(d);
                while (true)
                    if (Users.TryGetValue(s, out user))
                    {
                        return user;
                    }
            }

        }
        #endregion
        #region Server
        public bool Connect(string IP)
        {
            try
            {
                ipAddr = IPAddress.Parse(IP);
                tcpServer = new TcpClient();
                tcpServer.Connect(ipAddr, 2016);
                Start();
                Connected = true;
                return true;
            }
            catch (Exception e)
            {
                Ex(e.Message);
                return false;
            }
        }
        public void Send(string Data)
        {
            swSender.WriteLine(Data);
            swSender.Flush();
        }
        public void AuthOrRegister(string Data)
        {
            swSender.WriteLine(Data);
            swSender.Flush();
            //Start();
        }
        private void Start()
        {
            swSender = new StreamWriter(tcpServer.GetStream());
            thrMessaging = new Thread(new ThreadStart(ReceiveMessages));
            thrMessaging.IsBackground = true;
            thrMessaging.Start();
        }
        private void Disponse()
        {
            swSender.Close();
            srReceiver.Close();
            tcpServer.Close();
        }
        public void Disconect()
        {
            Connected = false;
            Disponse();
        }
        private void ReceiveMessages()
        {
            srReceiver = new StreamReader(tcpServer.GetStream());
            string ConResponse = srReceiver.ReadLine();
            if (ConResponse.Contains("ok"))
            {
                pClient.PWindow.ChatPage.Visibility = System.Windows.Visibility.Visible;
                MessChat(ConResponse);
            }
            else
            {
                MessChat(srReceiver.ReadLine());
                return;
            }
            while (Connected)
            {
                MessChat(srReceiver.ReadLine());
                //this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { srReceiver.ReadLine() });
            }
        }
        #endregion
    }
}
