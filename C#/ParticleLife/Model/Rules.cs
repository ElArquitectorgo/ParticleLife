using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleLife.Model
{
    public class Rules
    {
        public Dictionary<(Color, Color), double> rules;
        public Rules() 
        {
            rules = new Dictionary<(Color, Color), double>();
        }

        public void AddNewRule(Color colorA, Color colorB, double gravitationalConstant)
        {
            rules.Add((colorA, colorB), gravitationalConstant);
        }
    }
}
