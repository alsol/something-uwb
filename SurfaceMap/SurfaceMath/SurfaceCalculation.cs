using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace SurfaceMap.SurfaceMath
{
    class SurfaceCalculation
    {
        private Signal signal1;
        private Signal signal2;
        private SurfaceUtils.DataSource dataSource;

        private int height;
        private int length;

        private Double[,] output;
        private ConcurrentDictionary<String, Double>  parallelMap = new ConcurrentDictionary<string, double>();

        public SurfaceCalculation(Signal signal1, Signal signal2)
        {
            this.signal1 = signal1;
            this.signal2 = signal2;
            output = new Double[signal1.getLength(),signal1.getHeight()];
            height = signal1.getHeight();
            length = signal2.getLength();
        }

        public SurfaceCalculation(SurfaceUtils.DataSource dataSource, PixelMap map)
        {
            this.dataSource = dataSource;
            signal1 = new Signal(map.convertDistanceMapToTick(1), map.getHeight(), map.getLength());
            signal2 = new Signal(map.convertDistanceMapToTick(2), map.getHeight(), map.getLength());
            output = new Double[signal1.getLength(), signal1.getHeight()];
            height = signal1.getHeight();
            length = signal2.getLength();
        }

        public Double[,] getOutput()
        {
            signal1.updateSignal(dataSource.getWaveForm(1));
            signal2.updateSignal(dataSource.getWaveForm(2));
            this.updateOutput();
            return output;
        }

        private void updateOutput()
        {
            parallelMap.Clear();
            Parallel.For(0, length, i =>
            {
                Parallel.For(0, height, j =>
                {
                    Tuple<int, int> key = new Tuple<int, int>(i, j);

                    List<double>.Enumerator ifSignal = signal1.getSignalByTuple(key).GetEnumerator();
                    List<double>.Enumerator isSignal = signal2.getSignalByTuple(key).GetEnumerator();

                    int pointCount = 0;
                    double sum = 0;
                    double ifSignalFirst = ifSignal.Current;
                    double isSignalFirst = isSignal.Current;

                    while (ifSignal.MoveNext() && isSignal.MoveNext() && (pointCount++ < 10))
                    {
                        sum += ifSignal.Current * isSignal.Current;
                    }
                    Double value = sum / pointCount;
                    Double threshold = Threshold(i, j);
                    //Double threshold = 0;
                    if (value > threshold)
                    {
                        parallelMap.TryAdd(i.ToString() + " " + j.ToString(), value);
                    }
                    output[i, j] = value > threshold ? value : 0;
                });
            });
            if (false)
            {
                Thread.Sleep(2);
                String currentTime = DateTime.Now.Millisecond.ToString().Replace(" ", "-").Replace(".", "-").Replace(":", "-");
                StreamWriter file2 = new StreamWriter(@"D:\waveforms\maximuses_all\output" + currentTime + ".txt", true);
                foreach (String str in parallelMap.Keys)
                {
                    double current = parallelMap[str];
                    file2.WriteLine(str + " " + current.ToString().Replace(',', '.'));
                }
                file2.Close();
            }
            if (false)
            {
                Thread.Sleep(10);
                String maxKey = null;
                double max = 0;
                foreach (String str in parallelMap.Keys)
                {
                    double current = parallelMap[str];
                    if (current > max)
                    {
                        max = current;
                        maxKey = str;
                    }
                }
                Debug.Print(maxKey + " " + max.ToString());
                if (max != 0)
                {
                    StreamWriter file2 = new StreamWriter(@"D:\waveforms\maximuses_all\output.txt", true);
                    file2.WriteLine(maxKey + " " + max.ToString().Replace(',', '.'));
                    file2.Close();
                }
            }
        }

        private double Threshold(int i, int j)
        {
            double a = 0.000623;
            double b = -1.239e-06;
            double c = -4.863e-06;
            
            return a + b * i + c * j;
        }
    
    }
}
