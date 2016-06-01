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
using System.Windows.Shapes;

namespace WPFChat
{
    /// <summary>
    /// Логика взаимодействия для CleanWindow.xaml
    /// </summary>
    public partial class CleanWindow : Window
    {
        public CleanWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Title = (Window.Children[0] as PrivateChat).User;
            }
            catch { }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Children.Clear();
        }
    }
}
                              