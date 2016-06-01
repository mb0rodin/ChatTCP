using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для UserSettingsPage.xaml
    /// </summary>
    public partial class UserSettingsPage : UserControl
    {    
        private ServerLib.ServerToUser.UserInfo UI;
        
        public UserSettingsPage(ServerLib.ServerToUser.UserInfo ui)
        {
            UI = ui;
            InitializeComponent();
            SetControls();
        }
        private void SetControls()
        {
            if(UI.Image.Count() != 1)
            Image.Source = StaticUtilites.byteArrayToImage(UI.Image);
            UserAge.Text = UI.Age.ToString();
            NickName.Text =UI.NickName;
            UserName.Text =UI.FullName;
            UserInfo.Text =UI.Info;
            switch (UI.Type)
            {
                case ServerLib.TypeUser.Admin:
                UserType.Text = "Администратор";
                break;
                case ServerLib.TypeUser.Moderator:
                UserType.Text = "Модератор";
                break;
                case ServerLib.TypeUser.User:
                UserType.Text = "Пользователь";
                break;
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            //MessageBox.Show(op.ShowDialog().ToString());
            if (op.ShowDialog().ToString().Contains("True"))
            {
                byte[] b = File.ReadAllBytes(op.FileName);

                Image.Source = StaticUtilites.ResizeImage(StaticUtilites.byteArrayToBitmap(b), new System.Drawing.Size(200, 200)).ToWpfBitmap();
                UI.Image = b;
                //cl c = new cl
                //{
                //    b = b,
                //    bt = byteArrayToImage(b)
                //};
                //string s = Newtonsoft.Json.JsonConvert.SerializeObject(c);
                //File.AppendAllText("1.txt", s);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UI.Info = UserInfo.Text;
            UI.FullName = UserName.Text;
            UI.NickName = NickName.Text;
            UI.Age = Convert.ToInt32(UserAge.Text);
            MainWindow.pClass.PConnecting.SetUserInfo(UI);
        }
    }
}
