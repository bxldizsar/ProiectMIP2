using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectMIP2
{
    public partial class Form1 : Form
    {
        private MyThread myThread;
        private int x = 0;
        private Font textFont = new Font("Arial", 16);
        private volatile Brush textBrush = Brushes.Black;
        private static BooleanSwitch logSwitch = new BooleanSwitch("LogSwitch", "Logarea in fisier");

        public Form1()
        {
            InitializeComponent();
            Trace.Listeners.Add(new TextWriterTraceListener("log.txt"));
            Trace.AutoFlush = true;

            myThread = new MyThread(this);                           // 1. változat: referencia
            // myThread = new MyThread(new DrawDelegate(DrawText));  // 2. változat: delegate
        }

        // A rajzolás: ezt hívja meg a szál 100 ms-onként
        public void DrawText()
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.Clear(this.BackColor);
                g.DrawString("Szevasz!", textFont, textBrush, x, 50);
            }

            x += 5;
            if (x > this.ClientSize.Width)
                x = 0;

            Trace.WriteLineIf(logSwitch.Enabled, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") + " - text mutat la x=" + x);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myThread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myThread.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    this.BackColor = dlg.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    textBrush = new SolidBrush(dlg.Color);
            }
        }
    }
}