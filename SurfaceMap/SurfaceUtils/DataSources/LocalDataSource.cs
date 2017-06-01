using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SurfaceMap.SurfaceUtils.DataSources
{
    class LocalDataSource : IDataSource
    {
        Boolean firstCall1 = true;
        Boolean firstCall2 = true;

        private String folder = "D:\\waveforms\\20_01_17\\additional_steel";

        private String firstCallPostfix = "_20-01-2017-12-20-01";

        private String secondCallPostfix = "_20-01-2017-12-23-05";

        private String[] avaliablePrefixes;

        private int currentIndex = 0;

        public LocalDataSource() {

            HashSet<String> existingFilesSet = new HashSet<string>();

            string[] existingFiles = Directory.GetFiles(@folder, "channel_1*");

            firstCallPostfix = "_" + Path.GetFileNameWithoutExtension(existingFiles[0]).ToString().Split('_')[2];

            foreach (string file in existingFiles)
            {
                string processedPostfix = Path.GetFileNameWithoutExtension(file).ToString().Split('_')[2];
                if("_"+processedPostfix != firstCallPostfix)
                existingFilesSet.Add("_"+processedPostfix);
            }

            avaliablePrefixes = existingFilesSet.ToArray();
        }
        public double getXOrigin()
        {
            return 4.2102E-08;
        }
        public double getYOrigin()
        {
            return 0.337128;
        }
        public int getNumberOfPoints()
        {
            return 1350;
        }
        public double getStep()
        {
            return 7.407407E-12;
        }
        public double[] getWaveForm(Byte type)
        {
            double[] output=null;
            if (firstCall1 || firstCall2)
            {
                switch (type)
                {
                    case 1:
                        double[] array = parseFile("channel_1", firstCallPostfix);
                        output = array;
                        firstCall1 = false;
                        break;
                    case 2:
                        double[] tmp = parseFile("channel_2", firstCallPostfix);
                        output = tmp;
                        firstCall2 = false;
                        break;
                    default:
                        return null;
                }
                return output;
            }

            if (++currentIndex >= avaliablePrefixes.Length)
            {
                currentIndex = 0;
            }

            switch (type)
            {
                case 1:
                    //double[] array = parseFile("channel_1", avaliablePrefixes[currentIndex]);
                    double[] array = parseFile("channel_1", secondCallPostfix);
                    output = array;
                    break;
                case 2:
                    //double[] tmp = parseFile("channel_2", avaliablePrefixes[currentIndex]);
                    double[] tmp = parseFile("channel_2", secondCallPostfix);
                    output = tmp;
                    break;
                default:
                    return null;
            }

            return output;
        }

        private Double[] parseFile(String prefix, String postfix)
        {
            return File.ReadAllLines(@folder + "\\" + prefix + postfix + ".txt").Select(d => Double.Parse(d)).ToArray();
        }
    }
}