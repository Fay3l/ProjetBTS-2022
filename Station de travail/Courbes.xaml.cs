using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Station_de_travail
{
    /// <summary>
    /// Logique d'interaction pour Courbes.xaml
    /// </summary>
    public partial class Window_Courbes : Window
    {
        Window_Historique histo = new Window_Historique();
        List<Data_Class> Datamodels;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
        
        public Window_Courbes()
        {
            InitializeComponent();
            Datamodels = histo.DateCourbes().Result;
            ModelCO.Model = Courbe((Capteur)0, OxyColor.FromRgb(249, 255, 43),"Courbe CO");
            ModelCO2.Model = Courbe((Capteur)1,OxyColor.FromRgb(67,234,78),"Courbe CO2");
            ModelLux.Model = Courbe((Capteur)2,OxyColor.FromRgb(255, 234, 78), "Courbe Luminosité");
            ModelTEM.Model = Courbe((Capteur)3, OxyColor.FromRgb(255, 0, 0), "Courbe Température");
            ModelLP.Model = Courbe((Capteur)5, OxyColor.FromRgb(0, 255, 0), "Courbe Humidité au sol");
            ModelPRES.Model= Courbe((Capteur)4, OxyColor.FromRgb(0, 0, 255), "Courbe Pression ambiante");
            ModelHUM.Model= Courbe((Capteur)6, OxyColor.FromRgb(67, 234, 255), "Courbe Humidité relative");
        }

        public enum Capteur
        {
            CO, CO2,LUX, TC, PRES,LP,HUM,
        }

        public PlotModel Courbe(Capteur capteur,OxyColor color, string title )
        {
            double sensor_value = 0;
            var plot = new PlotModel();
            try
            {
                plot = new PlotModel
                {
                    Title = title
                };
                var lineSerie = new LineSeries
                {
                    Color = color
                };
                plot.Annotations.Add(new LineAnnotation
                {
                    TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Center
                });
                var startDate = Datamodels.Where(x => x.sensor == capteur.ToString()).Select(x => x.timestamp).First();
                var endDate = Datamodels.Where(x => x.sensor == capteur.ToString()).Select(x => x.timestamp).Last();
                var minValue = DateTimeAxis.ToDouble(endDate);
                var maxValue = DateTimeAxis.ToDouble(startDate);
                plot.Axes.Add(new DateTimeAxis
                {
                    Position = AxisPosition.Bottom,
                    Minimum = minValue,
                    Maximum = maxValue,
                    StringFormat = "d/M/yy ",
                });

                List<DataPoint> dataPoints = new List<DataPoint>();

                foreach (var sensor in Datamodels)
                {
                    if (sensor.sensor == capteur.ToString())
                    {
                        var capteur_value = sensor.value;
                        var capteur_time = sensor.timestamp;
                        sensor_value = double.Parse(capteur_value, System.Globalization.CultureInfo.InvariantCulture);
                        dataPoints.Add(new DataPoint(DateTimeAxis.ToDouble(capteur_time), sensor_value));
                    }

                }
                lineSerie.ItemsSource = dataPoints;
                plot.Series.Add(lineSerie);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PlotModel Error");
            }
            return plot;

        }
        private void Button_Click_Imprimer(object sender, RoutedEventArgs e)
        {
            try
            {
                var exporter = new XpsExporter { Width = 800, Height = 600 };
                var select = (TAB.SelectedItem as TabItem).Header.ToString();
                switch (select)
                {
                    case "CO":
                        PlotModel model = Courbe((Capteur)0, OxyColor.FromRgb(249, 255, 43), "Courbe CO");
                        exporter.Print(model);
                        break;
                    case "CO2":
                        PlotModel model1 = Courbe((Capteur)1, OxyColor.FromRgb(249, 255, 43), "Courbe CO2");
                        exporter.Print(model1);
                        break;
                    case "Luminosité":
                        PlotModel model2 = Courbe((Capteur)2, OxyColor.FromRgb(249, 255, 43), "Courbe Luminosité");
                        exporter.Print(model2);
                        break;
                    case "Température":
                        PlotModel model3 = Courbe((Capteur)3, OxyColor.FromRgb(249, 255, 43), "Courbe Température");
                        exporter.Print(model3);
                        break;
                    case "Humidité au sol":
                        IPlotModel model4 = Courbe((Capteur)5, OxyColor.FromRgb(249, 255, 43), "Courbe Humidité au sol");
                        exporter.Print(model4);
                        break;
                    case "Pression ambiante":
                        IPlotModel model5 = Courbe((Capteur)4, OxyColor.FromRgb(249, 255, 43), "Courbe Pression ambiante");
                        exporter.Print(model5);
                        break;
                    case "Humidité relative":
                        IPlotModel model6 = Courbe((Capteur)6, OxyColor.FromRgb(249, 255, 43), "Courbe Humidité relative");
                        exporter.Print(model6);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
