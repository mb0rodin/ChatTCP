using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerLib
{
    class User
    {
        TcpClient tcpClient;
        private Thread thrSender;
        private StreamReader srReceiver;
        private StreamWriter swSender;
        private Server _Server; 
        private string currUser;
        private ServerToUser.UserInfo UI;
        private string strResponse;
        public User(TcpClient tcpCon, Server S)
        {
            tcpClient = tcpCon;
            thrSender = new Thread(AcceptClient);
            thrSender.Start();
        }

        private void CloseConnection()
        {   
            srReceiver.Close();
            swSender.Close();
            tcpClient.Close();
            
        }

        private void RegistrationUser(Comands.Register d)
        {
            string user = d.Username;
            string pass = d.Password;
            string passServ = d.PasServer;
            if (passServ == _Server.settingsServer.Pass)
            {
                if (!Directory.Exists(user))
                {
                    Directory.CreateDirectory(user);
                    string t = Newtonsoft.Json.JsonConvert.SerializeObject(new ServerToUser.UserInfo
                    {
                        NickName = user,
                        Password = pass,
                        Type = TypeUser.User
                    });
                    File.AppendAllText(user + "\\UserInfo.txt", t);
                    swSender.WriteLine(Herper.GetNewOkMessage("Вы успешно зарегистрировались!"));
                    swSender.Flush();
                }
                else
                {

                    swSender.WriteLine(Herper.GetNewErrorMessage("Пользователь с таким именем уже зарегистрирован!"));
                    swSender.Flush();
                    CloseConnection();

                }
            }
            else
            {
                swSender.WriteLine(Herper.GetNewErrorMessage("Неверный пароль регистрации!"));
                swSender.Flush();    
                CloseConnection();
            }
        }
        private void AcceptClient()
        {
            srReceiver = new System.IO.StreamReader(tcpClient.GetStream());
            swSender = new System.IO.StreamWriter(tcpClient.GetStream());

            string rl = srReceiver.ReadLine();
            UserToServer.MessageUser mu = Newtonsoft.Json.JsonConvert.DeserializeObject<UserToServer.MessageUser>(rl);
            
            if (mu.Comand is Comands.Register)
            {
                RegistrationUser(mu.Comand as Comands.Register);
                return;
            }
            else
                if (mu.Comand is Comands.Auth)
                {

                    Comands.Auth au = mu.Comand as Comands.Auth;
                    string pass = au.Password;
                   
                    if (Server.htUsers.Keys.Contains(au.Username))
                    {
                        swSender.WriteLine(Herper.GetNewErrorMessage("Пользователь с таким именем уже в чате!"));
                        swSender.Flush();
                        CloseConnection();
                        return;
                    }
                    else
                    {
                        if (Directory.Exists(au.Username))
                        {
                            ServerToUser.UserInfo s = Newtonsoft.Json.JsonConvert.DeserializeObject<ServerToUser.UserInfo>(File.ReadAllText(au.Username + "\\UserInfo.txt"));
                            if (s.Password == au.Password)
                            {
                                swSender.WriteLine(Herper.GetNewOkMessage("Успешно авторизированы!"));
                                swSender.Flush();
                                Server.AddUser(tcpClient, au.Username);
                                UI = s;
                            }
                            else
                            {
                                swSender.WriteLine(Herper.GetNewErrorMessage("Неверный пароль!"));
                                swSender.Flush();
                                CloseConnection();
                                return;
                            }
                        }
                        else
                        {
                            swSender.WriteLine(Herper.GetNewErrorMessage("Пользователь не найден!"));
                            swSender.Flush();
                            CloseConnection();
                            return;
                        }
                    }
                }
                else
                {
                    CloseConnection();
                    return;
                }

            try
            {
                while ((strResponse = srReceiver.ReadLine()) != "")
                {
                    if (strResponse == null)
                    {
                        Server.RemoveUser(tcpClient);
                    }
                    else
                    {
                        Server.SendMessage((mu.Comand as Comands.Auth).Username, strResponse);
                    }
                }
            }
            catch
            {
                Server.RemoveUser(tcpClient);
            }
        }
    }
}
