using Opc.UaFx.Client; // Write

namespace OPCUAApp
{
    public partial class Form1 : Form
    {
        public OpcClient client = new OpcClient("opc.tcp://localhost:62640/");
        public Form1()
        {
            InitializeComponent();
            // Co the initialize timer vao day

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        


        private void timer1_Tick(object sender, EventArgs e)
        {
            double sensorValue;

            sensorValue = ReadSensorData();
            OpcWrite(sensorValue);
        }

        void OpcWrite(double sensorValue)
        {
            string tagName = "ns=2;s=Tag7";
            client.WriteNode(tagName, sensorValue);
        }

        double ReadSensorData()
        {
            var rand = new Random();
            int minValue = 20, maxValue = 30;
            double sensorValue;

            sensorValue = rand.NextDouble() * (maxValue - minValue) + minValue;
            txtSensorValue.Text = sensorValue.ToString("#.##");
            DateTime sensorDateTime = DateTime.Now;
            txtTimeStamp.Text = sensorDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            
            return sensorValue;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
                client.Disconnect();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            client.Connect();
            timer1.Start();
            lblStatusMessage.Text = "Logging Started and Connected to OPC Server";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if(client!=null)
                client.Disconnect();
            lblStatusMessage.Text = "Logging Stopped and Disconnected from OPC Server";
        }
    }
}