using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace RenderExperiment3
{
    public partial class Form1 : Form
    {
        private Bitmap buffer;
        private Graphics bufferGraphics;
        private Thread renderThread;
        private bool running = true;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Initialize buffer
            buffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            bufferGraphics = Graphics.FromImage(buffer);

            // Start rendering thread
            renderThread = new Thread(RenderLoop);
            renderThread.IsBackground = true;
            renderThread.Start();
        }

        private void RenderLoop()
        {
            while (running)
            {
                DrawFrame();
                this.Invoke((MethodInvoker)Invalidate);
                Thread.Sleep(16); // ~60 FPS
            }
        }

        private void DrawFrame() //here we cook later
        {
            bufferGraphics.Clear(Color.Black);

            // Example: Random Pixels
            Random rnd = new Random();
            for (int i = 0; i < 500; i++)
            {
                int x = rnd.Next(buffer.Width);
                int y = rnd.Next(buffer.Height);
                buffer.SetPixel(x, y, Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)));
                
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(buffer, 0, 0);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            running = false;
            renderThread.Join();
            bufferGraphics.Dispose();
            buffer.Dispose();
            base.OnFormClosing(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

