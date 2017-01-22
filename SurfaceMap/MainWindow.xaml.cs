using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Agilent.CommandExpert.ScpiNet.Ag86100_A_10_60;
using SurfaceMap.SurfaceMath;
using System.Threading;

namespace SurfaceMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Ag86100 v86100;
        private Boolean isStoped = false;
        private SurfaceUtils.DataSource dataSource;

        public MainWindow()
        {
            InitializeComponent();
            this.Model = CreateNormalDistributionModel();
            this.DataContext = this;
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public PlotModel Model { get; set; }

        /// <summary>
        /// Creates a model showing normal distributions.
        /// </summary>
        /// <returns>A PlotModel.</returns>
        private static PlotModel CreateNormalDistributionModel()
        {
            var plot = new PlotModel();

            plot.Axes.Add(new OxyPlot.Axes.LinearColorAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Right,
                Palette = OxyPalettes.Gray(1000),
                HighColor = OxyColors.White,
                LowColor = OxyColors.Black
            });

            PixelMap pixelMap = new PixelMap(400, 400, 5, 10);
            pixelMap.setDt(7.407407E-12);

            Double[,] data = pixelMap.getD1();

            var heatMapSeries1 = new OxyPlot.Series.HeatMapSeries
            {
                X0 = -1.5,
                X1 = 1.5,
                Y0 = 4.0,
                Y1 = 0.0,
                Data = data

            };

            plot.Series.Add(heatMapSeries1);

            return plot;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (false)
                {
                    v86100 = new Ag86100("TCPIP0::AGILENT-13547EA::inst0::INSTR");
                    dataSource = new SurfaceUtils.DataSource(v86100);
                }
                if (true)
                {
                    v86100 = new Ag86100("C:\\Users\\Александр\\Desktop\\IOMonitorLog_426.xml");
                    dataSource = new SurfaceUtils.DataSource();
                }
            }
            catch (Exception error)
            {
                Status.Text = error.Message;
                return;
            }
           
            Status.Text = "Connected";
            ConnectButton.IsEnabled = false;
            PlayButton.IsEnabled = true;
            ReadyMarker.Text = "Ready";
            ReadyMarker.Foreground = new SolidColorBrush(Colors.Green);

            string idn = null;
            v86100.SCPI.IDN.Query(out idn);
            Idn.Text = idn+':';

        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            PlayButton.IsEnabled = false;
            isStoped = false;
            StopButton.IsEnabled = true;
            SaveButton.IsEnabled = false;
            Status.Text = "Running";
            Thread workThread = new Thread(this.doWork);
            workThread.Start();
        }

        private DateTime last;

        public void doWork()
        {
            PixelMap pixelMap = new PixelMap(dataSource.getNumberOfPoints(), dataSource.getNumberOfPoints(), 30, 100);
            pixelMap.setDt(dataSource.getStep());
            pixelMap.setXOrigin(dataSource.getXOrigin());
            pixelMap.getD1();

            SurfaceCalculation surfaceCalculation = new SurfaceCalculation(dataSource, pixelMap);
            Double[,] data = surfaceCalculation.getOutput();

            Model.Series.Clear();

            HeatMapSeries heatMapSeries1 = new OxyPlot.Series.HeatMapSeries()
            {
                X0 = -dataSource.getNumberOfPoints() * dataSource.getStep() / 2.0 *(3 * 10 * 10 * 10 * 10 * 10 * 10 * 10 * 10),
                X1 = dataSource.getNumberOfPoints() * dataSource.getStep() / 2.0*(3*10*10*10*10*10*10*10*10),
                Y0 = dataSource.getNumberOfPoints()*dataSource.getStep() * (3*10*10*10*10*10*10*10*10),
                Y1 = 0.0,
                Data = data
            };
            Model.Series.Add(heatMapSeries1);
            Model.InvalidatePlot(true);

            last = DateTime.Now;
            DateTime current;
            while (!isStoped)
            {
                if (false)
                {
                    current = DateTime.Now;
                    if ((current - last).Seconds > 3)
                    {
                        this.Save();
                        last = DateTime.Now;
                    }
                }

                heatMapSeries1.Data = surfaceCalculation.getOutput();

                Model.InvalidatePlot(true);
            }

        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            isStoped = true;
            StopButton.IsEnabled = false;
            PlayButton.IsEnabled = true;
            SaveButton.IsEnabled = true;
            Status.Text = "Stopped";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Save();
        }

        private void Save()
        {
            String folderToSave = "D:\\waveforms\\additional\\";
            String currentTime = DateTime.Now.ToString().Replace(" ", "-").Replace(".", "-").Replace(":", "-");
            Double[] channel1 = dataSource.getWaveForm(1);
            Double[] channel2 = dataSource.getWaveForm(2);
            try
            {
                File.WriteAllLines(@folderToSave + "channel_1_" + currentTime + ".txt", channel1.Select(d => d.ToString()).ToArray());
                File.WriteAllLines(@folderToSave + "channel_2_" + currentTime + ".txt", channel2.Select(d => d.ToString()).ToArray());
            }
            catch (Exception error)
            {
                return;
            }
        }
    }
}
