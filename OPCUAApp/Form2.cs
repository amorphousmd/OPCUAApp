using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opc.UaFx.Client; // Read

namespace OPCUAApp
{
    public partial class Form2 : Form
    {
        public OpcClient client = new OpcClient("opc.tcp://localhost:62640/");

        public Form2()
        {
            InitializeComponent();
        }

        void OpcRead()
        {
            string tagName = "ns=2;s=Tag7";

            Opc.UaFx.OpcValue opcData = client.ReadNode(tagName);

            double temperature = (double)opcData.Value;

            txtOpcValue.Text = temperature.ToString("#.##");
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            client.Connect();
            timer1.Start();
            lblStatusMessage.Text = "Connected to OPC Server";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (client != null)
                client.Disconnect();
            lblStatusMessage.Text = "Disconnected from OPC Server";
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            if (client != null)
                client.Disconnect();
            lblStatusMessage.Text = "Disconnected from OPC Server";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            OpcRead();
        }
    }
}
