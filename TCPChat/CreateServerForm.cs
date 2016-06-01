using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TCPChat
{
    public partial class CreateServerForm : Form
    {
        public CreateServerForm()
        {
            InitializeComponent();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label4.Enabled = true;
                textBox4.Enabled = true;
            }
            else
            {
                label4.Enabled = false;
                textBox4.Enabled = false;
            }
        }
          //192.168.0.103
        private void button1_Click_1(object sender, EventArgs e)
        {
            string host = System.Net.Dns.GetHostName();
            string ip = "192.168.0.103";
            ServerLib.Server se = new ServerLib.Server(new ServerLib.Settings.SettingsServerClass
            {
                IP = ip,
                MaximumUser = 20,
            });
            se.StartListening();
            //Server
        }

        private void CreateServerForm_Load(object sender, EventArgs e)
        {
            string host = System.Net.Dns.GetHostName();
            System.Net.IPAddress ip = System.Net.Dns.GetHostByName(host).AddressList[0];
            textBox1.Text = ip.ToString();
        }
       
    }
}
