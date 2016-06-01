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
    /// Логика взаимодействия для Message.xaml
    /// </summary>
    public partial class Message : UserControl
    {
        public Message(string User,string Message, ServerLib.ServerToUser.UserInfo u)
        {
            InitializeComponent();
            this.User.Content = "Отправитель: " + User;
            this.Text.Text = Message;
            if (u.NickName == User)
                ToolUser.Children.Add(new UserPage(u));
            else
                ToolSelfUser.Children.Add(new UserPage(u));

        }
    }
}
