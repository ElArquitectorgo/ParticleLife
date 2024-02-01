using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleLife.Model
{
    public class TupleComparer : IComparer<(int, uint)>
    {
        public int Compare((int, uint) x, (int, uint) y)
        {
            return x.Item2.CompareTo(y.Item2);
        }
    }
}
