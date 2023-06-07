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

        private Particle[] Particles = new Particle[750];
        private Universe Universe;
        public Simulation()
        {
            InitializeComponent();
            System.Reflection.PropertyInfo setDoubleBuffered = typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
            setDoubleBuffered.SetValue(this, true, null);
            BackgroundImageBuffer = new(ClientSize.Width, ClientSize.Height);
            Universe = new SingleThread(ClientSize.Width, ClientSize.Height);
            ClientSizeChanged += new((s, ev) =>
            {
                if (WindowState == FormWindowState.Minimized) return;
                BackgroundImageBuffer = new(BackgroundImageBuffer, ClientSize);
                Universe.setWidth(ClientSize.Width);
                Universe.setHeight(ClientSize.Height);
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


                ParticleDraw(graphics, alpha, particle.Color, x, y, 10, 10);
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
            // Negativo se atraen
            Random random = new Random();
            for (int i = 0; i < Particles.Length; i++)
            {
                double x = random.NextDouble() * ClientSize.Width;
                double y = random.NextDouble() * ClientSize.Height;
                if (i < 250)
                {
                    Particles[i] = new Particle(x, y, Color.AliceBlue);
                    Particles[i].AddRule(Color.AliceBlue, -5);
                    Particles[i].AddRule(Color.LightGreen, 5);
                    Particles[i].AddRule(Color.Magenta, -3);
                }
                else if (i >= 250 && i < 500)
                {
                    Particles[i] = new Particle(x, y, Color.LightGreen);
                    Particles[i].AddRule(Color.AliceBlue, -1);
                    Particles[i].AddRule(Color.LightGreen, 0);
                    Particles[i].AddRule(Color.Magenta, -3);
                }
                else
                {
                    Particles[i] = new Particle(x, y, Color.Magenta);
                    Particles[i].AddRule(Color.AliceBlue, 3);
                    Particles[i].AddRule(Color.LightGreen, 3);
                    Particles[i].AddRule(Color.Magenta, 3);
                }
            }
        }

        private void setParallelOptions(Universe universe)
        {
            this.Universe = universe;
        }

        private void Auto_Click(object sender, EventArgs e)
        {
            setParallelOptions(new AutoThread(ClientSize.Width, ClientSize.Height));
        }

        private void Single_Click(object sender, EventArgs e)
        {
            setParallelOptions(new SingleThread(ClientSize.Width, ClientSize.Height));
        }

        private void Multi_Click(object sender, EventArgs e)
        {
            setParallelOptions(new MultiThread(ClientSize.Width, ClientSize.Height));
        }

        private void SIMD_Click(object sender, EventArgs e)
        {
            setParallelOptions(new SIMDVersion(ClientSize.Width, ClientSize.Height));
        }
    }
}
