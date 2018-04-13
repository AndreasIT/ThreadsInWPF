using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ThreadsInWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool SensorsRunning = false;
        public delegate void CalTemp(object o);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void B_Start_Click(object sender, RoutedEventArgs e)
        {
            if (SensorsRunning)
            {
                SensorsRunning = false;
                B_Start.Content = "Start";
            }
            else
            {
                SensorsRunning = true;
                B_Start.Content = "Stop";
                Label[] labels = { L_01, L_02, L_03, L_04, L_05, L_06, L_07, L_08, L_09, L_10 };
                Thread[] threads = new Thread[10];
                
                
                for (int i = 0; i < threads.Count(); i++)
                {
                    threads[i] = new Thread(Sensor);
                    threads[i].Start(labels[i]);
                }
            }
        }
        private void Sensor(Object o)
        {
            Label l = (Label)o;
            Random r = new Random();
            while (SensorsRunning)
            {
                Thread.Sleep(1000);
                double temp = 10 + r.NextDouble() * 15;

                l.Dispatcher.Invoke(new Action(() => l.Content = temp.ToString("F2")));
                //tilføj kode her som overfører temp til vinduets label
            }
        }

    }
}
