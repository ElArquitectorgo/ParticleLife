using ParticleLife.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ParticleLife.Program;

namespace ParticleLife
{
    public partial class Simulation : Form, IFormLoop
    {
        private Bitmap BackgroundImageBuffer;
        private long ElapsedTime;
        private int FrameCounter;
        private int FramesPerSecond;

        private Particle[] Particles = new Particle[1000];
        private Universe Universe;
        private Rules Rules = new Rules();
        public Simulation()
        {
            InitializeComponent();
            System.Reflection.PropertyInfo setDoubleBuffered = typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
            setDoubleBuffered.SetValue(this, true, null);
            BackgroundImageBuffer = new(ClientSize.Width, ClientSize.Height);
            Universe = new MultiThread(Rules, ClientSize.Width, ClientSize.Height);
            ClientSizeChanged += new((s, ev) =>
            {
                if (WindowState == FormWindowState.Minimized) return;
                BackgroundImageBuffer = new(BackgroundImageBuffer, ClientSize);
                Universe.Width = ClientSize.Width;
                Universe.Height = ClientSize.Height;
            });
        }

        public void UpdateEnvironment(long deltaTime)
        {
            ElapsedTime += deltaTime;
            if (ElapsedTime >= 1000)
            {
                FramesPerSecond = FrameCounter;
                ElapsedTime = 0;
                FrameCounter = 0;
            }
            FrameCounter++;
        }
        public void RenderEnvironment(long deltaTime)
        {
            using Graphics graphics = Graphics.FromImage(BackgroundImageBuffer);

            graphics.Clear(Color.Black);

            for (int i = 1; i <= 1; i++)
            {
                int alpha = i * 255 / 1;
                Universe.UpdateParticlesPosition(this.Particles);
                UniverseDraw(graphics, alpha);
            }

            BackgroundImage = BackgroundImageBuffer;
            Invalidate();

            fpsLabel.Text = FramesPerSecond.ToString();
        }

        public void UniverseDraw(Graphics graphics, int alpha)
        {
            foreach (Particle particle in Particles)
            {
                float x = (float)particle.X - (float)10;
                float y = (float)particle.Y - (float)10;


                ParticleDraw(graphics, alpha, particle.Color, x, y, 8, 8);
            }
        }

        public void ParticleDraw(Graphics graphics, int alpha, Color color, float x, float y, float width, float height)
        {
            Color fill = ControlPaint.Light(color);
            Color draw = ControlPaint.Dark(color);

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;

            graphics.FillEllipse(new SolidBrush(Color.FromArgb(alpha, fill)), x, y, width, height);
            graphics.DrawEllipse(new Pen(Color.FromArgb(alpha, draw), 1f), x, y, width, height);
        }

        private void Simulation_Load(object sender, EventArgs e)
        {
            Random random = new Random();
            for (int i = 0; i < Particles.Length; i++)
            {
                double x = random.NextDouble() * ClientSize.Width;
                double y = random.NextDouble() * ClientSize.Height;
                if (i < 300)
                {
                    Particles[i] = new Particle(x, y, Color.AliceBlue);
                }
                else if (i >= 300 && i < 700)
                {
                    Particles[i] = new Particle(x, y, Color.LightGreen);
                }
                else
                {
                    Particles[i] = new Particle(x, y, Color.Magenta);
                }
            }

            Rules.AddNewRule(Color.AliceBlue, Color.AliceBlue, 0.4);
            Rules.AddNewRule(Color.AliceBlue, Color.LightGreen, 0);
            Rules.AddNewRule(Color.AliceBlue, Color.Magenta, -1);

            Rules.AddNewRule(Color.LightGreen, Color.AliceBlue, -1);
            Rules.AddNewRule(Color.LightGreen, Color.LightGreen, 0.1);
            Rules.AddNewRule(Color.LightGreen, Color.Magenta, 0.7);

            Rules.AddNewRule(Color.Magenta, Color.AliceBlue, -1);
            Rules.AddNewRule(Color.Magenta, Color.LightGreen, -1);
            Rules.AddNewRule(Color.Magenta, Color.Magenta, 1);
        }

        private void setParallelOptions(Universe universe)
        {
            this.Universe = universe;
        }

        private void Auto_Click(object sender, EventArgs e)
        {
            setParallelOptions(new MultiThread(Rules, ClientSize.Width, ClientSize.Height));
        }

        private void Single_Click(object sender, EventArgs e)
        {
            setParallelOptions(new SingleThread(Rules, ClientSize.Width, ClientSize.Height));
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Universe.Beta = trackBar1.Value / 10d;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Universe.ForceFactor = trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Universe.Dt = trackBar3.Value / 10000d;
        }
    }
}
