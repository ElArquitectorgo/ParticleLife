using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleLife.Model
{
    internal interface Universe
    {
        public void UpdateParticlesPosition(Particle[] Particles);
        public void setWidth(int width);
        public void setHeight(int height);
    }
}
