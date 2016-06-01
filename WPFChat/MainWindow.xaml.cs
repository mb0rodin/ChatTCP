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
using FirstFloor.ModernUI.Windows.Controls;

namespace WPFChat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Client pClass;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonChat(object sender, RoutedEventArgs e)
        {
            Grid.Children.Clear();
            Grid.Children.Add(pClass.PChatControl);
        }
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pClass = new Client(this);
            Grid.Children.Add(pClass.PConnectPage);
        }

        private void HomePage_OnClick(object sender, RoutedEventArgs e)
        {
            Grid.Children.Clear();
            Grid.Children.Add(pClass.PConnectPage);
        }

        private void ChatPage_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CleanWindow sl = new CleanWindow();
            sl.Window.Children.Add(pClass.PConnectPage);
            sl.Show();
        }

        private void HomePage_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        //private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        //{
            
        //Button button = sender as Button;
        //ContextMenu contextMenu = button.ContextMenu;
        //contextMenu.PlacementTarget = button;
        //contextMenu.IsOpen = true;   
            
        //}
    }
}
