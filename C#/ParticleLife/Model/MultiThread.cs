using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleLife.Model
{
    public class MultiThread : Universe
    {
        public int Width;
        public int Height;
        public int AttractionRange { get; set; }
        private readonly int NumLogicalProcs;
        private Thread[] Threads;
        public MultiThread(int Width, int Height, int AttractionRange = 150)
        {
            this.Width = Width;
            this.Height = Height;
            this.AttractionRange = AttractionRange;
            this.NumLogicalProcs = Environment.ProcessorCount;
            Threads = new Thread[NumLogicalProcs];
        }

        public void UpdateParticlesPosition(Particle[] Particles)
        {
            int partition = Particles.Length / NumLogicalProcs;
            int ini = 0;

            for (int i = 0; i < NumLogicalProcs; i++)
            {
                Threads[i] = new Thread(new ThreadStart(() => { 
                    UpdateParticles(Particles, ini, Math.Min(Particles.Length, ini + partition)); 
                }));
                Threads[i].Start();
                ini += partition;
            }
            for (int i = 0; i < NumLogicalProcs; ++i)
            {
                Threads[i].Join();
            }
        }

        private void UpdateParticles(Particle[] Particles, int ini, int fin)
        {
            for (int i = ini; i < fin; i++)
            {
                double fx = 0;
                double fy = 0;

                foreach (Particle particleB in Particles)
                {
                    double dx = particleB.X - Particles[i].X;
                    double dy = particleB.Y - Particles[i].Y;

                    if (dx > Width - AttractionRange) dx -= Width;
                    else if (dx < -Width + AttractionRange) dx += Width;
                    if (dy > Height - AttractionRange) dy -= Height;
                    else if (dy < -Height + AttractionRange) dy += Height;

                    // Manhattan distance
                    double distance = Math.Abs(dx) + Math.Abs(dy);

                    if (distance > 0 && distance < AttractionRange)
                    {
                        double gravitationalConstant = Particles[i].Rules.rules[(Particles[i].Color, particleB.Color)];
                        double f = (distance < 30) ? distance / (-5) : gravitationalConstant * (1 - Math.Abs(15 - 2 * distance) / 15);
                        double Force = f / Math.Pow(distance, 2);

                        fx += Force * dx;
                        fy += Force * dy;

                    }
                }
                Particles[i].VelX = (Particles[i].VelX + fx) * 0.5;
                Particles[i].VelY = (Particles[i].VelY + fy) * 0.5;

                Particles[i].X += Particles[i].VelX;
                Particles[i].Y += Particles[i].VelY;

                if (Particles[i].X < 0) { Particles[i].X = Width; }
                if (Particles[i].X > Width) { Particles[i].X = 0; }
                if (Particles[i].Y < 0) { Particles[i].Y = Height; }
                if (Particles[i].Y > Height) { Particles[i].Y = 0; }
            }
        }
        public void setWidth(int width)
        {
            this.Width = width;
        }

        public void setHeight(int height)
        {
            this.Height = height;
        }
    }
}
