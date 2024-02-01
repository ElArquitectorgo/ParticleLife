using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleLife.Model
{
    public class MultiThread : Universe
    {
        public MultiThread(Rules Rules, int Width, int Height, int AttractionRange = 150, double Dt = 0.001, double Friction = 0.02, double ForceFactor = 1, double Beta = 0.5) {
            this.Rules = Rules;
            this.Width = Width;
            this.Height = Height;
            this.AttractionRange = AttractionRange;
            this.Dt = Dt;
            this.Friction = Friction;
            this.ForceFactor = ForceFactor;
            this.Beta = Beta;

        }

        private double Force(double r, double a)
        {
            if (r < Beta)
            {
                return r / Beta - 1;
            }
            else if (Beta < r && r < 1)
            {
                return a * (1 - Math.Abs(2 * r - 1 - Beta) / (1 - Beta));
            }
            else
            {
                return 0;
            }
        }

        public override void UpdateParticlesPosition(Particle[] Particles)
        {
            Parallel.ForEach(Particles, (Action<Particle>)(particleA =>
            {
                double fx = 0;
                double fy = 0;

                foreach (Particle particleB in Particles)
                {
                    if (particleA == particleB) continue;

                    double dx = particleB.X - particleA.X;
                    double dy = particleB.Y - particleA.Y;

                    if (dx < -Width / 2) dx += Width;
                    else if (dx >= Width / 2) dx -= Width;
                    if (dy < -Height / 2) dy += Height;
                    else if (dy >= Height / 2) dy -= Height;

                    // Euclidean distance
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance > 0 && distance < this.AttractionRange)
                    {
                        double gravitationalConstant = Rules.rules[(particleA.Color, particleB.Color)];
                        double f = this.Force(distance / this.AttractionRange, gravitationalConstant);

                        fx += dx / distance * f;
                        fy += dy / distance * f;

                    }
                }
                fx *= this.AttractionRange * ForceFactor;
                fy *= this.AttractionRange * ForceFactor;

                particleA.VelX *= Friction;
                particleA.VelY *= Friction;

                particleA.X += fx * Dt;
                particleA.Y += fy * Dt;

            }));

            Parallel.ForEach(Particles, particle =>
            {
                particle.X += particle.VelX * Dt;
                particle.Y += particle.VelY * Dt;

                // wrap(x) = x - floor((x-a)/(b-a))*(b-a)
                particle.X =particle.X - Math.Floor(particle.X / Width) * Width;
                particle.Y = particle.Y - Math.Floor(particle.Y / Height) * Height;
            });
        }
    }
}
