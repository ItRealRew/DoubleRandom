using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormDoubleRandom
{
    public partial class Form1 : Form
    {
        List<double> DoubleList = new List<double>();

        Pen pen = new Pen(Color.DarkRed);
        Pen pen0 = new Pen(Color.Black);
        Pen penExp = new Pen(Color.Yellow);

        private Thread _workerThread;
        private static volatile bool _stopRequest = false;

        const int WM_NCHITTEST = 0x84;
        const int HTCAPTION = 2;
        const int HTCLIENT = 1;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
                m.Result = (IntPtr)HTCAPTION;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _stopRequest = false;
           _workerThread = new Thread(GenerateGraphic);
           _workerThread.Start(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            _stopRequest = true;
            _workerThread.Join();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _stopRequest = true;
            _workerThread.Join();
        }

        private void AddList()
        {
            DoubleRandom.DoubleRandomClient client = new DoubleRandom.DoubleRandomClient("NetTcpBinding_IDoubleRandom");
            DoubleList.AddRange(client.GetDoubleList());
            client.Close();
        }

        private void Clear()
        {
            DoubleList.Clear();
        }

        private void GenerateGraphic()
        {
            Bitmap bmp = new Bitmap(picture.Width, picture.Height);
            Graphics graph = Graphics.FromImage(bmp);

            while (!_stopRequest)
            {
                AddList();

                graph.Clear(Color.White);

                int count = DoubleList.Count;
                int X = 640;
                int Z = 0;

                graph.DrawLine(pen0, 0, picture.Height / 2, picture.Width, picture.Height / 2);

                while (count > 1)
                {
                    var y1 = (picture.Height / 2) + (-1 * Convert.ToInt32(DoubleList[count - 2]));
                    var y = (picture.Height / 2) + (-1 * Convert.ToInt32(DoubleList[count - 1]));

                    graph.DrawLine(pen, X - 64, y1, X, y);
                    X = X - 64;
                    if (X > 0)
                    {
                        count--;
                    }
                    else
                    {
                        count = -1000;
                    }
                }

                picture.Image = bmp;

                Clear();
            }

        }
    }
}
