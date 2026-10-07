using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectMIP2
{
    public partial class Form1 : Form
    {
        private Thread thread;
        private volatile bool running = false;
        private int x = 0;
        private Font textFont = new Font("Arial", 16);
        public Form1()
        {
            InitializeComponent();
        }
        private void Run()
        {
            using (Graphics g = this.CreateGraphics())
            {
                while (running)
                {
                    g.Clear(this.BackColor);
                    g.DrawString("Szevasz!", textFont, Brushes.Black, x, 50);

                    x += 5;
                    if (x > this.ClientSize.Width)
                        x = 0;
                    Thread.Sleep(100);
                }    
            }
        }
        private void StopThread()
        {
            running = false;
            if (thread != null && thread.IsAlive)
                thread.Join();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (thread != null && thread.IsAlive)
                return;

            running = true;
            thread = new Thread(Run);
            thread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StopThread();
        }
    }
}
