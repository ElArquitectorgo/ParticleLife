using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ParticleLife.Model
{
    public class Particle
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double VelX { get; set; }
        public double VelY { get; set; }

        public Vector2 Pos { get; set; }
        public Vector2 Vel { get; set; }

        public Color Color { get; set; }
        public Rules Rules { get; set; }

        public Particle(double x, double y, Color color, double velX = 0, double velY = 0)
        {
            X = x;
            Y = y;
            VelX = velX;
            VelY = velY;
            this.Color = color;
            this.Rules = new Rules();

            Pos = new Vector2((float) x, (float) y);
            Vel = new Vector2((float) velX, (float) velY);
        }

        public void AddRule(Color color, double gravitationalConstant)
        {
            Rules.AddNewRule(this.Color, color, gravitationalConstant);
        }
    }
}
