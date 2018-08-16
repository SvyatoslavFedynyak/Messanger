﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace Messanger
{
    public partial class MainForm : Form
    {
        ConnectForm MainConnectForm;
        LogInForm MainLogInForm;
        Socket ClientSocket;
        string ServerIpAdress;
        int ServerPort;
        string UserName;
        public MainForm()
        {
            InitializeComponent();

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainLogInForm = new LogInForm();
            if (MainLogInForm.ShowDialog() == DialogResult.OK)
            {
                UserName = MainLogInForm.GetData;
            }

        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainConnectForm = new ConnectForm();
            if (MainConnectForm.ShowDialog() == DialogResult.OK)
            {
                ServerIpAdress = MainConnectForm.IP;
                ServerPort = MainConnectForm.Port;
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ClientSocket.Connect(ServerIpAdress, ServerPort);
                Listen();
            }
        }
        private async void Listen()
        {
            await Task.Run(() =>
            {
                do
                {
                    byte[] buffer = new byte[1024];
                    ClientSocket.Receive(buffer);
                    ChatTextBox.AppendText($"{UserName}: {Encoding.ASCII.GetString(buffer)}\n"); 
                } while (true);
            });
        }
        private async void SendButton_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                byte[] buffer = Encoding.ASCII.GetBytes(MessageTextBox.Text);
                ClientSocket.Send(buffer);
                ChatTextBox.AppendText($"{UserName}: {MessageTextBox.Text}\n");
                MessageTextBox.Clear();
            });
        }
    }
}