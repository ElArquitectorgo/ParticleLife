using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleLife.Model
{
    public abstract class Universe
    {
        public Rules Rules { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double Dt { get; set; }
        public double Friction { get; set; }
        public double ForceFactor { get; set; }
        public double Beta { get; set; }
        public int AttractionRange { get; set; }
        public abstract void UpdateParticlesPosition(Particle[] Particles);

    }
}
