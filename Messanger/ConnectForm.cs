using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Messanger
{
    public partial class ConnectForm : Form
    {


        string ServerIpAdress;
        int ServerPort;
        public string IP
        {
            get { return ServerIpAdress; }
        }
        public int Port
        {
            get { return ServerPort; }
        }
        public ConnectForm()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            ServerPort = Convert.ToInt32(PortTextBox.Text);
            ServerIpAdress = IpTextBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
