﻿using Messages.EventArgs.Network;
using Network.Thread;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamScreenClientPortable
{
    public partial class MainForm : Form
    {

        public ClientThread clientThread { get { return Network.Instance.Client.Instance.Thread; } }

        public delegate void ShowFormCallback(String systemId);

        public MainForm()
        {
            InitializeComponent();

            connectionStatus = new System.Timers.Timer(1000);

            connectionStatus.Elapsed += Connection_Elapsed;

            ConfigManager = new Utils.Config.Manager();

            clientThread.Events.OnHostInitalizeConnected += Events_OnHostInitalizeConnected;
            clientThread.Events.OnClientConnected += ClientListener_OnClientConnected;
            clientThread.Events.onNetworkError += ClientListener_onNetworkError;

            this.txtServer.Text = ConfigManager.ClientConfig.ServerName;
            this.txtServerPort.Text = ConfigManager.ClientConfig.ServerPort.ToString();

            clientThread.Start();
        }

        private void Events_OnHostInitalizeConnected(object sender, Messages.EventArgs.Network.Client.HostInitalizeConnectedEventArgs e)
        {
            Messages.Connection.Request.HostConnectionMessage ms = new Messages.Connection.Request.HostConnectionMessage();
            ms.HostSystemId = e.HostSystemId;
            ms.ClientSystemId = e.ClientSystemId;
            ms.Password = clientThread.Manager.Encode(e.HostSystemId, this.txtPassword.Text);
            ms.SymmetricKey = clientThread.Manager.Encode(e.HostSystemId, clientThread.Manager.getSymmetricKeyForRemoteId(e.HostSystemId));

            clientThread.Manager.sendMessage(ms);
        }

        private void ClientListener_onPeerDisconnected(object sender, EventArgs e)

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigManager.ClientConfig.ServerName = this.txtServer.Text;
            ConfigManager.ClientConfig.ServerPort = Convert.ToInt32(this.txtServerPort.Text);
            ConfigManager.saveClientConfig();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            var pair = clientThread.Manager.CreateNewKeyPairKey(this.txtSystemId.Text);
            clientThread.Manager.sendMessage(
                new Messages.Connection.Request.InitalizeHostConnectionMessage
                {
                    ClientSystemId = clientThread.Manager.SystemId,
                    HostSystemId = this.txtSystemId.Text,
                    ClientPublicKey = pair.PublicKey
                }
            );
        }

        private void openForm(String systemId)

        private void Connection_Elapsed(object sender, System.Timers.ElapsedEventArgs e)

        private void ClientListener_OnClientConnected(object sender, ClientConnectedEventArgs e)

                if (this.InvokeRequired)

                this.lblStatus.Text = "Passwort Ok Verbunden mit: " + e.SystemId;

            }
    }
}