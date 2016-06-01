using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;

namespace TCPChat
{
    public partial class LoadForm : Form
    {
        public LoadForm()
        {
            InitializeComponent(); 
            string host = System.Net.Dns.GetHostName();
            // Получение ip-адреса.
            StringBuilder sb = new StringBuilder();
            foreach (var VARIABLE in System.Net.Dns.GetHostByName(host).AddressList)
            {
                sb.AppendLine(VARIABLE.ToString());
            }
            //string g = string.Join("\r\n", (object[])System.Net.Dns.GetHostByName(host).AddressList);
            //System.Net.IPAddress ip = System.Net.Dns.GetHostByName(host).AddressList[0];
            // Показ адреса в label'е.
            MessageBox.Show(sb.ToString(), System.Net.Dns.GetHostByName(host).AddressList.Length.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
          // ((UserControl2)elementHost1.Child).AddItem();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
