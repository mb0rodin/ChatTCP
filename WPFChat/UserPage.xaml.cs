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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : UserControl
    {   
        private ServerLib.ServerToUser.UserInfo UI;
        public UserPage(ServerLib.ServerToUser.UserInfo ui)
        {
            UI = ui;
            InitializeComponent();
            SetControls();
        }
     
        private void SetControls()
        {
            if (UI.Image.Count() != 1)
            Image.Source = StaticUtilites.byteArrayToImage( UI.Image);
            UserAge.Text ="Возраст: "+ UI.Age;
            NickName.Text = "Ник: " + UI.NickName;
            UserName.Text = "Имя: " + UI.FullName;
            UserInfo.Text = "Инфо: " + UI.Info;
            switch (UI.Type)
            {
                case ServerLib.TypeUser.Admin:
                UserType.Text = "Статус: Администратор";
                break;
                case ServerLib.TypeUser.Moderator:
                UserType.Text = "Статус: Модератор";                                                         
                break;
                case ServerLib.TypeUser.User:
                UserType.Text = "Статус: Пользователь";
                break;
            }

        }

    }
}
