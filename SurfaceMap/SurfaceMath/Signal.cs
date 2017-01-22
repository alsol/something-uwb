
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceMap.SurfaceMath
{
    class Signal
    {
        private ConcurrentDictionary<Tuple<int, int>, List<double>> surfaceArray;
        private Double[] signal;
        private Int32[,] distanceArray;
        private Double[] delay = null;
        private int height;
        private int length;

        public Signal(Int32[,] distanceArray, int height, int length)
        {
            this.distanceArray = distanceArray;
            this.surfaceArray = new ConcurrentDictionary<Tuple<int, int>, List<double>>();
            this.height = height;
            this.length = length;
            //delayList = new LinkedList<double[]>();
        }

        public void updateSignal(Double[] signal)
        {
            //delayList.AddLast(signal);
            if (delay == null)
            {
                delay = new Double[signal.Length];
                this.signal = new Double[signal.Length];
                signal.CopyTo(delay, 0);
            }

            for (int i=0; i < signal.Length;i++ )
            {
                this.signal[i] = Math.Abs(delay[i] - signal[i]);
            };
                /* if (delayList.Count > 10)
                 {
                     Double[] first = delayList.First.Value;
                     Parallel.For(0, signal.Length, i =>
                     {
                         this.signal[i] = Math.Abs(first[i] - signal[i]);
                     });
                     delayList.RemoveFirst();
                 }*/
                this.updateDistanceArray();
        }

        public int getHeight()
        {
            return height;
        }

        public int getLength()
        {
            return length;
        }

        public List<double> getSignalByTuple(Tuple<int, int> key) 
        {
                return surfaceArray[key];
        }

        private void updateDistanceArray()
        {
            for(int i=0; i< length; i++)
            {
                Parallel.For (0, height, j =>
                {
                    List<double> list;
                    Tuple<int, int> key = new Tuple<int, int>(i, j);
                    if (surfaceArray.ContainsKey(key))
                    {
                        list = surfaceArray[key];
                        list.Clear();
                    }
                    else
                    {
                        list = new List<double>();
                        surfaceArray.TryAdd(key, list);
                    }
                    for (int k = distanceArray[i, j]; k < signal.Length; k++)
                    {
                        list.Add(signal[k]);
                    }
                });
            }
        }
    }
}
