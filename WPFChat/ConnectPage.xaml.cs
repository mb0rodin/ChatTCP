using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFChat
{
    /// <summary>
    /// Логика взаимодействия для ConnectPage.xaml
    /// </summary>
    public partial class ConnectPage : UserControl
    {
        private Client pClient;
        
        public ConnectPage(Client cl)
        {
            pClient = cl;
            pClient.PConnecting.Ex += MesEx;
            InitializeComponent(); 
        }
        private void MesEx(object o)
        {
            MessageBox.Show(o.ToString()+"\r\n"+pClient.ExInfo(o));
        }
        private void RegisterButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                grid.Children.Clear();
                grid.Children.Add(new UserSettingsPage(pClient.PConnecting.GetUserInfo()));
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            //string username = NameUserReg.Text;
            //string pass1 = PassUserReg.Password;
            //string pass2 = PassUser2Reg.Password;
            //string passServer = PassServerReg.Password;
            //string ipAddress = ServerIpReg.Text;
            //if (pass1 == pass2)
            //{
            //    ServerLib.Comands.Register reg = new ServerLib.Comands.Register
            //    {
            //        PasServer = passServer,
            //        Password = pass1,
            //        Username = username
            //    };
            //    ServerLib.UserToServer.MessageUser mu = new ServerLib.UserToServer.MessageUser
            //    {
            //        To = "",
            //        Type = ServerLib.TypeMessage.Comand,
            //        Comand = reg,
            //        Value = ""
            //    };

            //    string json = JsonConvert.SerializeObject(mu);
            //    if (pClient.PConnecting.Connect(ipAddress))
            //    {
            //        pClient.PConnecting.MessChat = pClient.PChatControl.UpdateMessages;
            //        pClient.PConnecting.AuthOrRegister(json);

            //        MessageBox.Show(json);
            //    }
            //}
        }
        
        private void ButtonAuth_OnClick(object sender, RoutedEventArgs e)
        {
            string username = NameUserAuth.Text;
            string password = PassUserAuth.Text;
            string ipAddress = ServerIpAuth.Text;
            ServerLib.Comands.Auth reg = new ServerLib.Comands.Auth
            {
                Password = password,
                Username = username
            };
            ServerLib.UserToServer.MessageUser mu = new ServerLib.UserToServer.MessageUser
            {
                To = "",
                Type = ServerLib.TypeMessage.Comand,
                Comand = reg,
                Value = ""
            };
         
            string json = JsonConvert.SerializeObject(mu);
            if (pClient.PConnecting.Connect(ipAddress))
            {
                pClient.PConnecting.MessChat = pClient.PChatControl.UpdateMessages;
                pClient.PConnecting.AuthOrRegister(json);
                //MessageBox.Show(json);
            }
        }
    }

}
