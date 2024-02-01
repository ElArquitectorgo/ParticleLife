using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleLife.Model
{
    public class SpacePartition : Universe
    {
        private List<(int, int)> cellOffsets = new List<(int, int)>
        {
            (-1, 1),
            (0, 1),
            (1, 1),
            (-1, 0),
            (0, 0),
            (1, 0),
            (-1, -1),
            (0, -1),
            (1, -1),
        };
        private (int, uint)[] SpatialLookup;
        private int[] StartIndices;
        public SpacePartition(int n, Rules Rules, int Width, int Height, int AttractionRange = 150, double Dt = 0.001, double Friction = 0.02, double ForceFactor = 1, double Beta = 0.5)
        {
            this.Rules = Rules;
            this.Width = Width;
            this.Height = Height;
            this.AttractionRange = AttractionRange;
            this.Dt = Dt;
            this.Friction = Friction;
            this.ForceFactor = ForceFactor;
            this.Beta = Beta;
            this.SpatialLookup = new (int, uint)[n];
            this.StartIndices = new int[n];

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

        public override void UpdateParticlesPosition(Particle[] particles)
        {
            UpdateSpatialLookup(particles);

            for (int i = 0; i < particles.Length; i++)
            {
                (double fx, double fy) = ForeachPointWithinRadius(particles, particles[i]);
                
                fx *= this.AttractionRange * ForceFactor;
                fy *= this.AttractionRange * ForceFactor;

                particles[i].VelX *= Friction;
                particles[i].VelY *= Friction;

                particles[i].X += fx * Dt;
                particles[i].Y += fy * Dt;
            }

            Parallel.ForEach(particles, particle =>
            {
                particle.X += particle.VelX * Dt;
                particle.Y += particle.VelY * Dt;

                particle.X = particle.X - Math.Floor(particle.X / Width) * Width;
                particle.Y = particle.Y - Math.Floor(particle.Y / Height) * Height;
            });
        }

        public void UpdateSpatialLookup(Particle[] particles)
        {
            Parallel.For(0, particles.Length, i =>
            {
                (int cellX, int cellY) = PositionToCellCoord(particles[i]);
                uint cellKey = GetKeyFromHash(HashCell((uint)cellX, (uint)cellY));
                SpatialLookup[i] = new(i, cellKey);
                StartIndices[i] = int.MaxValue;
            });

            Array.Sort(SpatialLookup, new TupleComparer());

            Parallel.For(0, particles.Length, i =>
            {
                uint key = SpatialLookup[i].Item2;
                uint keyPrev = i == 0 ? int.MaxValue : SpatialLookup[i - 1].Item2;
                if (key != keyPrev)
                {
                    StartIndices[key] = i;
                }
            });
        }

        public (int, int) PositionToCellCoord(Particle particle)
        {
            int cellX = (int) particle.X / this.AttractionRange;
            int cellY = (int) particle.Y / this.AttractionRange;
            return (cellX, cellY);
        }

        public uint HashCell(uint cellX, uint cellY)
        {
            uint a = cellX * 15823;
            uint b = cellY * 9737333;
            return a + b;
        }

        public uint GetKeyFromHash(uint Hash)
        {
            return Hash % (uint)SpatialLookup.Length;
        }

        public (double, double) ForeachPointWithinRadius(Particle[] particles, Particle particle)
        {
            double fx = 0;
            double fy = 0;

            (int centreX, int centreY) = PositionToCellCoord(particle);

            foreach ((int offsetX, int offsetY) in cellOffsets)
            {
                uint key = GetKeyFromHash(HashCell((uint)(centreX + offsetX), (uint)(centreY + offsetY)));
                int cellStartIndex = StartIndices[key];

                for (int i = cellStartIndex; i < SpatialLookup.Length; i++)
                {
                    if (SpatialLookup[i].Item2 != key) break;

                    int particleIndex = SpatialLookup[i].Item1;
                    Particle particleB = particles[particleIndex];

                    double dx = particleB.X - particle.X;
                    double dy = particleB.Y - particle.Y;

                    if (dx < -Width / 2) dx += Width;
                    else if (dx >= Width / 2) dx -= Width;
                    if (dy < -Height / 2) dy += Height;
                    else if (dy >= Height / 2) dy -= Height;

                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance > 0 && distance < this.AttractionRange)
                    {
                        double gravitationalConstant = Rules.rules[(particle.Color, particleB.Color)];
                        double f = this.Force(distance / this.AttractionRange, gravitationalConstant);

                        fx += dx / distance * f;
                        fy += dy / distance * f;
                    }
                }
            }
            return (fx, fy);
        }
    }
}
