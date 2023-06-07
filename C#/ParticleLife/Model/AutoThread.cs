using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleLife.Model
{
    public class AutoThread : Universe
    {
        public int Width;
        public int Height;
        public int AttractionRange { get; set; }
        public AutoThread(int Width, int Height, int AttractionRange = 150) {
            this.Width = Width;
            this.Height = Height;
            this.AttractionRange = AttractionRange;
        }

        public void UpdateParticlesPosition(Particle[] Particles)
        {
            Parallel.ForEach(Particles, particleA =>
            {
                double fx = 0;
                double fy = 0;

                // Hay una pérdida de rendimiento si este bucle se paraleliza
                foreach (Particle particleB in Particles)
                {
                    double dx = particleB.X - particleA.X;
                    double dy = particleB.Y - particleA.Y;

                    if (dx > Width - AttractionRange) dx -= Width;
                    else if (dx < -Width + AttractionRange) dx += Width;
                    if (dy > Height - AttractionRange) dy -= Height;
                    else if (dy < -Height + AttractionRange) dy += Height;

                    // Manhattan distance
                    double distance = Math.Abs(dx) + Math.Abs(dy);

                    if (distance > 0 && distance < AttractionRange)
                    {
                        double gravitationalConstant = particleA.Rules.rules[(particleA.Color, particleB.Color)];
                        double f = (distance < 30) ? distance / (-5) : gravitationalConstant * (1 - Math.Abs(15 - 2 * distance) / 15);
                        double Force = f / Math.Pow(distance, 2);

                        fx += Force * dx;
                        fy += Force * dy;

                    }
                }
                particleA.VelX = (particleA.VelX + fx) * 0.5;
                particleA.VelY = (particleA.VelY + fy) * 0.5;

                particleA.X += particleA.VelX;
                particleA.Y += particleA.VelY;

                if (particleA.X < 0) { particleA.X = Width; }
                if (particleA.X > Width) { particleA.X = 0; }
                if (particleA.Y < 0) { particleA.Y = Height; }
                if (particleA.Y > Height) { particleA.Y = 0; }
            });
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
