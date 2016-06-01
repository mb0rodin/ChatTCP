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
    /// Логика взаимодействия для ChatControl.xaml
    /// </summary>
    #region HZ
    public class StringUpperConverter : IValueConverter
    {
        string text;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            text = (string)value;
            return text.ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter,  System.Globalization.CultureInfo culture)
        {
            return text;
        }
    }
    #endregion
    public partial class ChatControl : UserControl
    {
        private Client pClient;
        public ChatControl(Client cl)
        {
            pClient = cl;
            InitializeComponent();
        }
        private bool b = true;
        private void SendMessage_OnClickendMessage_Click(object sender, RoutedEventArgs e)
        {
            WPFChat.Message ms = new Message(pClient.PConnecting.GetUserInfo().NickName, Message.Text, pClient.PConnecting.GetUserInfo());
            //ms.Picture.Source = new BitmapImage(new Uri(@"F:\ProgramFiles\zzz new\НОВОЕ\НОВОЕ\2RjCbHaxLe0.jpg"));
            //ms.Tool.Source = new BitmapImage(new Uri(@"F:\ProgramFiles\zzz new\НОВОЕ\НОВОЕ\2RjCbHaxLe0.jpg"));
            if (b)
            {
                if (pClient.PConnecting.GetUserInfo().Image.Count() == 1)
                    ms.Image.Source = new BitmapImage(new Uri("images/avatar.png", UriKind.Relative));
                else
                    ms.Image.Source = StaticUtilites.byteArrayToBitmap(pClient.PConnecting.GetUserInfo().Image).ToWpfBitmap();

          

                ms.HorizontalAlignment = HorizontalAlignment.Left;
                b = false;
            }
            else
            {
                if (pClient.PConnecting.GetUserInfo().Image.Count() == 1)
                    ms.Image1.Source = new BitmapImage(new Uri("images/avatar.png", UriKind.Relative));
                else
                    ms.Image1.Source = StaticUtilites.byteArrayToBitmap(pClient.PConnecting.GetUserInfo().Image).ToWpfBitmap();

                ms.HorizontalAlignment = HorizontalAlignment.Right; b = true;
            }
            UserList.Items.Add(2533452345234);

            Items.Children.Add(ms);
            Message.Text = "";
            //throw new NotImplementedException();
        }

        public void UpdateMessages(object o)
        {
           
            ServerLib.ServerToUser.ServerMessage se = JsonConvert.DeserializeObject<ServerLib.ServerToUser.ServerMessage>(o.ToString());
            if (se.Message==null|| se.Message =="")
            {
                if (se.Value is ServerLib.UserToServer.MessageUser)
                {

                    ServerLib.UserToServer.MessageUser mu = se.Value as ServerLib.UserToServer.MessageUser;
                    WPFChat.Message ms = new Message(mu.From, mu.Value, pClient.PConnecting.GetUserInfo(mu.From));
                    //ms.Picture.Source = new BitmapImage(new Uri(@"F:\ProgramFiles\zzz new\НОВОЕ\НОВОЕ\2RjCbHaxLe0.jpg"));
                    //ms.Tool.Source = new BitmapImage(new Uri(@"F:\ProgramFiles\zzz new\НОВОЕ\НОВОЕ\2RjCbHaxLe0.jpg"));

                    if (pClient.PConnecting.GetUserInfo().NickName != mu.From)
                    {
                        if (pClient.PConnecting.GetUserInfo(mu.From).Image.Count() == 1)
                            ms.Image.Source = new BitmapImage(new Uri("images/avatar.jpg", UriKind.Relative));
                        else
                            ms.Image.Source = StaticUtilites.byteArrayToBitmap(pClient.PConnecting.GetUserInfo(mu.From).Image).ToWpfBitmap();
                        ms.HorizontalAlignment = HorizontalAlignment.Left;
                        //b = false;
                    }
                    else
                    {
                        if (pClient.PConnecting.GetUserInfo(mu.From).Image.Count() == 1)
                        ms.Image1.Source = new BitmapImage(new Uri("images/avatar.jpg", UriKind.Relative));
                        else
                            ms.Image1.Source = StaticUtilites.byteArrayToBitmap(pClient.PConnecting.GetUserInfo().Image).ToWpfBitmap();
                        ms.HorizontalAlignment = HorizontalAlignment.Right;// b = true;
                    }
                }
                if (se.Value is ServerLib.ServerToUser.UserInfo)
                {
                    ServerLib.ServerToUser.UserInfo user = se.Value as  ServerLib.ServerToUser.UserInfo;
                    pClient.PConnecting.Users.Add(user.NickName,user);
                }
                if (se.Value is ServerLib.ServerToUser.ServerUsers)
                {
                    ServerLib.ServerToUser.ServerUsers user = se.Value as ServerLib.ServerToUser.ServerUsers;
                    foreach(var v in user.Users)
                        UserList.Items.Add(v);
                }
                //UserList.Items.Add(2533452345234);

                //Items.Children.Add(ms);
                Message.Text = "";
            }
            else
            {
                pClient.PConnecting.Ex(se.Message);
            }
        }
        private void MenuItem_OnClickItem_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            pClient.PWindow.Grid.Children.Clear();
           
            pClient.PWindow.Grid.Children.Add(new PrivateChat(pClient,"13"));
        }

        private void UserList_OnMouseDoubleClickDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string s = UserList.SelectedItem.ToString();
               

                if (s != "")
                {
                    CleanWindow cl = new CleanWindow();
    
                    cl.Window.Children.Add(pClient.PConnecting.GetUserPrivateChat(s));
                    cl.Show();
                }
            }
            catch (Exception ex)
            {
                pClient.PConnecting.Ex(ex.Message);
            }
            //throw new NotImplementedException();
        }

        private void Message_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            int num = this.Message.Text.Length;
            int num2 = 300;
            if (num > 300)
            {
                this.Message.Text = this.Message.Text.Substring(0, 300);
                num = 300;
            }
            this.MaxLength.Text = (num2 - num).ToString();

            //MaxLength.Text = (300 - Message.Text.Length).ToString();
        }

        private void Message_OnKeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}
