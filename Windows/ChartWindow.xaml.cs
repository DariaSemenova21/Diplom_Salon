using LiveCharts;
using LiveCharts.Wpf;
using Salon.Models.ModelViews;
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
using System.Windows.Shapes;

namespace Salon.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window
    {
        public ChartWindow(Int32 totalRevenue, ServiceView[] serviceViews)
        {
            InitializeComponent();
            SeriesCollection collection = new SeriesCollection();
            serviceViews.Where(sv => sv.TotalPrice > 0).Select(sv =>
            {
                PieSeries data = new PieSeries();
                data.Values = new ChartValues<Int32>(new Int32[] { totalRevenue / sv.TotalPrice });
                data.Title = sv.ServiceName;
                collection.Add(data);

                return data;
            }).ToArray();

            PieChart.Series = collection;
        }
    }
}
