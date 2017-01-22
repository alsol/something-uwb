using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agilent.CommandExpert.ScpiNet.Ag86100_A_10_60;

namespace SurfaceMap.SurfaceUtils
{
    class DataSource : IDataSource
    {
        private IDataSource dataSource;

        public DataSource()
        {
            dataSource = new DataSources.LocalDataSource();
        }

        public DataSource(Ag86100 scope)
        {
            dataSource = new DataSources.AgilentDataSource(scope);
        }

        public double getXOrigin()
        {
            return dataSource.getXOrigin();
        }
        public double getYOrigin()
        {
            return dataSource.getYOrigin();
        }
        public int getNumberOfPoints()
        {
            return dataSource.getNumberOfPoints();
        }
        public double getStep()
        {
            return dataSource.getStep();
        }
        public double[] getWaveForm(Byte type)
        {
            return dataSource.getWaveForm(type);
        }
    }
}
