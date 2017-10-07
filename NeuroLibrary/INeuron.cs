using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroLibrary
{
    interface INeuron
    {
        int IdX { get; }
        int IdY { get; }
        float W { get; }
        float FActivate(int[,] field);
    }
}
