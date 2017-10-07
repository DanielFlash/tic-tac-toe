using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroLibrary
{
    internal class Neuron : INeuron
    {
        public int IdX { get; }
        public int IdY { get; }
        public float W { get; }

        internal Neuron(int idx, int idy)
        {
            IdX = idx;
            IdY = idy;
            if (IdX == 1 && IdY == 1)
                W = 1;
            else if ((IdX == 0 && IdY == 0) || (IdX == 0 && IdY == 2) || (IdX == 2 && IdY == 0) || (IdX == 2 && IdY == 2))
                W = 0.95f;
            else
                W = 0.9f;
        }

        public float FActivate(int[,] field)
        {
            if (field[IdX, IdY] != 0)
                return 0 * W;
            else
            {
                int x1 = IdX, x2 = IdX, y1 = IdY, y2 = IdY;
                while (x1 - 1 >= 0)
                    x1--;
                while (x2 + 1 <= 2)
                    x2++;
                while (y1 - 1 >= 0)
                    y1--;
                while (y2 + 1 <= 2)
                    y2++;

                int p = 0, k = 0, l1 = 0, m = 0;
                if (IdX == IdY)
                    for(int i = y1; i <= y2; i++)
                    {
                        if (i != IdY && field[i, i] == 0)
                            m++;
                        if (i != IdY && field[i, i] == 2)
                            k++;
                        if (i != IdY && field[i, i] == 1)
                            l1++;
                    }
                if (k >= 2)
                    return 1 * W;
                if (k >= 1 && m >= 1)
                    p++;

                int l2 = 0;
                k = 0;
                m = 0;
                if (IdX == IdY || IdX == IdY - 2 || IdY == IdX - 2)
                    for (int i = y1; i <= y2; i++)
                    {
                        if (i != IdY && field[IdX, i] == 0 && (i == IdX || i == IdX - 2 || IdX == i - 2))
                            m++;
                        if (i != IdY && field[IdX, i] == 2 && (i == IdX || i == IdX - 2 || IdX == i - 2))
                            k++;
                        if (i != IdY && field[IdX, i] == 1 && (i == IdX || i == IdX - 2 || IdX == i - 2))
                            l2++;
                    }
                if (k >= 2)
                    return 1 * W;
                if (k >= 1 && m >= 1)
                    p++;

                int l3 = 0;
                k = 0;
                m = 0;
                for (int i = y1; i <= y2; i++)
                {
                    if (i != IdY && field[IdX, i] == 0)
                        m++;
                    if (i != IdY && field[IdX, i] == 2)
                        k++;
                    if (i != IdY && field[IdX, i] == 1)
                        l3++;
                }
                if (k >= 2)
                    return 1 * W;
                if (k >= 1 && m >= 1)
                    p++;

                int l4 = 0;
                k = 0;
                m = 0;
                for (int i = x1; i <= x2; i++)
                {
                    if (i != IdX && field[i, IdY] == 0)
                        m++;
                    if (i != IdX && field[i, IdY] == 2)
                        k++;
                    if (i != IdX && field[i, IdY] == 1)
                        l4++;
                }
                if (k >= 2)
                    return 1 * W;
                if (k >= 1 && m >= 1)
                    p++;

                if (l1 >= 2 || l2 >= 2 || l3 >= 2 || l4 >= 2)
                    return 0.8f * W;

                if (p >= 2)
                    return 0.6f * W;
                else
                    return 0.4f * W;
            }
        }
    }
}
