using Agilent.CommandExpert.ScpiNet.Ag86100_A_10_60;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceMap.SurfaceMath
{
    class PixelMap
    {
        private double length;
        private double height;
        private double step;
        private double distance;
        private int lMax;
        private int hMax;
        private double dt;
        private double xOrigin;

        private Double[,] d0;
        private Double[,] d1;
        private Double[,] d2;

        private Int32[,] d1Int;
        private Int32[,] d2Int;

        public PixelMap(double length, double height, double step, double distance)
        {
            this.length = length;
            this.height = height;
            this.step = step;
            this.distance = distance;
        }

        public int getLength()
        {
            return lMax;
        }

        public int getHeight()
        {
            return hMax;
        }

        public void setXOrigin(double origin)
        {
            this.xOrigin = origin;
        }

        private Double[,] calculateMap(int type)
        {
            double addition = distance * type;

            int lMax = Convert.ToInt32(length / step);
            int hMax = Convert.ToInt32(height / step);

            lMax += lMax % 2 == 0 ? 1 : 0;

            this.hMax = hMax;
            this.lMax = lMax;

            Double[,] data = new Double[lMax,hMax];

            for (int i = 0; i < hMax; i++)
            {
                double cath = i * step;
                for (int j = -(lMax / 2), l=0; j < lMax / 2+1; j++, l++)
                {
                    double xDistance = j * step + addition;
                    data[l,i] = Math.Sqrt((cath * cath + xDistance * xDistance));
                }
            }

            return data;
        }

        public Double[,] getD1()
        {
            if (d1 == null)
            {
                d1 = calculateMap(1);

                if (d0 == null)
                {
                    d0 = calculateMap(0);
                }

                /*for (int i = 0; i < hMax; i++)
                {
                    for (int j = 0; j < lMax; j++)
                    {
                        d1[j, i] += d0[j, i];
                    }
                }*/
            }
            return d1;
        }
        public Double[,] getD2()
        {
            if (d2 == null)
            {            
    
                d2 = calculateMap(-1);

                if (d0 == null)
                {
                    d0 = calculateMap(0);
                }

                Parallel.For(0, lMax, i => 
                {
                    Parallel.For (0, hMax, j =>
                    {
                        if (i == 40)
                        {
                            d2[i, j] = Math.Abs(d0[i, j] - d2[i, j]);
                        }
                        d2[i, j] += d0[i, j]-d2[i,j];
                    });
                });
            }
            return d2;
        }
        public void setDt(double dt)
        {
            this.dt = dt;
        }

        public Int32[,] convertDistanceMapToTick(int type)
        {
            Int32[,] output;
            Double[,] victimArray;
            switch (type)
            {
                case 1:
                    victimArray = getD1();
                    if (d1Int == null)
                    {
                        d1Int = new Int32[lMax, hMax];
                        output = d1Int;
                    }
                    else
                    {
                        return d1Int;
                    }
                    break;
                case 2:
                    victimArray = getD2();
                    if (d2Int == null)
                    {
                        d2Int = new Int32[lMax, hMax];
                        output = d2Int;
                    }
                    else
                    {
                        return d2Int;
                    }
                    break;
                default:
                    return null;
            }

            for (int i = 0; i < hMax; i++)
            {
                for (int j = 0; j < lMax; j++)
                {
                    if (victimArray[j, i] < dt)
                    {
                        output[j, i] = 0;
                        continue;
                    }
                    output[j, i] = Convert.ToInt32((victimArray[j, i]));
                }
            }

            return output;

        }

    }
}