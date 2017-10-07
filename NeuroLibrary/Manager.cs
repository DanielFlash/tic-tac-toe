using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroLibrary
{
    public class Manager
    {
        internal Neuron[,] NeuronWeb = new Neuron[3,3];

        public Manager()
        {
            for (int i = 0; i < 3; i++)
                for(int j = 0; j < 3; j++)
                    NeuronWeb[i,j] = new Neuron(i,j);
        }

        public int Step(int[,] field)
        {
            float[] I = new float[9];
            float max = 0;
            int step = -1;
            for (int i = 0, k = 0; i < 3; i++)
                for (int j = 0; j < 3; j++, k++)
                {
                    I[k] = NeuronWeb[i, j].FActivate(field);
                    if (I[k] > max)
                    {
                        max = I[k];
                        step = k;
                    }
                }
            return step;
        }
    }
}
