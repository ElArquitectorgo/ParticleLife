using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleLife.Model
{
    public class Cluster
    {
        public Particle[] Particles { get; private set;}
        public Color Color { get; set; }
        public Rules Rules { get; set; }

        public Cluster(int numParticles, Color color, int width, int height)
        {
            this.Particles = new Particle[numParticles];
            this.Color = color;
            this.Rules = new Rules();

            AddParticles(width, height);
        }

        public void AddRule(Color color, double gravitationalConstant)
        {
            Rules.AddNewRule(this.Color, color, gravitationalConstant);
        }

        private void AddParticles(int width, int height)
        {
            Random random = new Random();
            for (int i = 0; i < Particles.Length; i++)
            {
                double x = random.NextDouble() * width;
                double y = random.NextDouble() * height;
                Particles[i] = new Particle(x, y, Color);
            }
        }
    }
}
