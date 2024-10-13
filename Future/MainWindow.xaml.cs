using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Newtonsoft.Json;

namespace Future
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        int maxRandomValue = 50;
        int frags = 0;
        private readonly string PREDICTIONS_CONFIG_PATH = $"{Environment.CurrentDirectory}\\PredictionsConfig.json";
        public DispatcherTimer timer = new DispatcherTimer();
        private string[] predictions;
        private Random rand = new Random();
        public MainWindow()
        {
            InitializeComponent();
            bPredict.Click += BPredict_Click;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += new EventHandler(Timer_tick);
        }

        private void Timer_tick(object sender, EventArgs e)
        {
            progressPredict.Value += 1;
            this.Title = progressPredict.Value.ToString() + "%";
            if (progressPredict.Value == progressPredict.Maximum)
            {
                timer.Stop();
                MessageBox.Show(predictions[rand.Next(0,predictions.Length)]);
                progressPredict.Value = 0;
                this.Title = "Predictor";
            }

        }
        private void BPredict_Click(object sender, RoutedEventArgs e)
        {
            /*progressPredict.Value += rand.Next(0,maxRandomValue);
            if(progressPredict.Value == progressPredict.Maximum)
            {
                frags++;
                maxRandomValue -= 5;
                if(maxRandomValue <= 0)
                {
                    MessageBox.Show("ты победил всех врагов!");
                    this.Close();
                    return;
                }
                MessageBox.Show($"Congrutilations! ты победил{frags} врагов");
                progressPredict.Value = 0;
                
                
            }*/
            
            try
            {
                var data = File.ReadAllText(PREDICTIONS_CONFIG_PATH);
                predictions = JsonConvert.DeserializeObject<string[]>(data);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
            finally
            {
                if (predictions == null)
                {
                    this.Close();
                }
                    if (predictions.Length == 0)
                {
                    MessageBox.Show("Стоп, а куда делись все предсказания?");
                    this.Close();
                    
                }
            }
            timer.Start();
        }
    }
}
