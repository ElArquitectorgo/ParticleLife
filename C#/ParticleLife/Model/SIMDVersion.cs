using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ParticleLife.Model
{
    public class SIMDVersion : Universe
    {
        public int Width;
        public int Height;
        public int AttractionRange { get; set; }
        public SIMDVersion(int Width, int Height, int AttractionRange = 150)
        {
            this.Width = Width;
            this.Height = Height;
            this.AttractionRange = AttractionRange;
        }
        public void UpdateParticlesPosition(Particle[] Particles)
        {
            foreach (Particle particleA in Particles)
            {
                Vector2 f = new Vector2(0, 0);

                foreach (Particle particleB in Particles)
                {
                    Vector2 d = Vector2.Subtract(particleB.Pos, particleA.Pos);

                    if (d.X > Width - AttractionRange) d.X -= Width;
                    else if (d.X < -Width + AttractionRange) d.X += Width;
                    if (d.Y > Height - AttractionRange) d.Y -= Height;
                    else if (d.Y < -Height + AttractionRange) d.Y += Height;

                    // Manhattan distance
                    float distance = Math.Abs(d.X) + Math.Abs(d.Y);

                    if (distance > 0 && distance < AttractionRange)
                    {
                        double gravitationalConstant = particleA.Rules.rules[(particleA.Color, particleB.Color)];
                        float fg = (distance < 30) ? distance / (-5) : (float)gravitationalConstant * (1 - Math.Abs(15 - 2 * distance) / 15);
                        float Force = (float)(fg / Math.Pow(distance, 2));
                        f += Vector2.Multiply(Force, d);
                    }
                }

                particleA.Vel = Vector2.Multiply(0.5f, particleA.Vel + f);
                particleA.Pos += particleA.Vel;

                particleA.X = particleA.Pos.X;
                particleA.Y = particleA.Pos.Y;

                if (particleA.X < 0) { particleA.X = Width; }
                if (particleA.X > Width) { particleA.X = 0; }
                if (particleA.Y < 0) { particleA.Y = Height; }
                if (particleA.Y > Height) { particleA.Y = 0; }

                particleA.Pos = new Vector2((float) particleA.X, (float) particleA.Y);
            }
        }
        public void setHeight(int height)
        {
            this.Height = height;
        }

        public void setWidth(int width)
        {
            this.Width = width;
        }
    }
}
