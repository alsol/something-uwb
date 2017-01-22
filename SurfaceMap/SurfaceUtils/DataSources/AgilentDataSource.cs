using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agilent.CommandExpert.ScpiNet.Ag86100_A_10_60;

namespace SurfaceMap.SurfaceUtils.DataSources
{
    // In order to use the following driver class, you need to reference this assembly : [C:\ProgramData\Keysight\Command Expert\ScpiNetDrivers\Ag86100_A_10_60.dll]
    class AgilentDataSource : IDataSource
    {
        private double xOrigin=0D;
        private double yOrigin=0D;
        private double yIncrement=0D;
        private double xIncrement=0D;
        private int numberOfPoints;
        private Ag86100 scope;

        public AgilentDataSource()
        {
            scope = null;
        }
        public AgilentDataSource(Ag86100 scope)
        {
            this.scope = scope;
        }
        public double getXOrigin()
        {
            scope.SCPI.WAVeform.XORigin.Query(out xOrigin);
            return xOrigin;
        }
        public double getYOrigin()
        {
            scope.SCPI.WAVeform.YORigin.Query(out yOrigin);
            return yOrigin;
        }
        public double getXIncrement()
        {
            scope.SCPI.WAVeform.XINCrement.Query(out xIncrement);
            return xIncrement;
        }
        public double getYIncrement()
        {
            scope.SCPI.WAVeform.YINCrement.Query(out yIncrement);
            return yIncrement;
        }
        public int getNumberOfPoints()
        {
            scope.SCPI.WAVeform.POINts.Query(out numberOfPoints);
            return numberOfPoints;
        }
        public double[] getWaveForm(byte type)
        {
            double[] output = null;
            scope.SCPI.WAVeform.BYTeorder.Command("LSBF");
            scope.SCPI.WAVeform.FORMat.Command("ASCii");
            switch (type)
            {
                case 1:
                    scope.SCPI.WAVeform.SOURce.Command("CHAN1");
                    break;
                case 2:
                    scope.SCPI.WAVeform.SOURce.Command("CHAN2");
                    break;
                default:
                    break;
            }
            scope.SCPI.WAVeform.DATA.QueryAscii(out output);
            return output;
        }
        public double getStep()
        {
            double value1 = 0D;
            scope.SCPI.WAVeform.XINCrement.Query(out value1);
            return value1;
        }

    }
}
