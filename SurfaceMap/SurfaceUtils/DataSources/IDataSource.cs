using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceMap.SurfaceUtils
{
    interface IDataSource
    {
        double getXOrigin();
        double getYOrigin();
        int getNumberOfPoints();
        double getStep();
        double[] getWaveForm(byte channel);
    }
}
