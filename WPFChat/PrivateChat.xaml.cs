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
    /// Логика взаимодействия для PrivateChat.xaml
    /// </summary>
    public partial class PrivateChat : UserControl
    {
        private Client pClient;
        public string User
        {
            get;
            private set;
        }
        string To;
        public PrivateChat(Client cl,string user)
        {
            pClient = cl;
            To = user;
            User = user;
            InitializeComponent();
            
        }
        private bool b = true;
        private void SendMessage_Click(object sender, RoutedEventArgs e)
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
           

            Items.Children.Add(ms);
            Message.Text = "";
        }

        private void Message_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Message_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
