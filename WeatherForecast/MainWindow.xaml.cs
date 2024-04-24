using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using static System.Net.WebRequestMethods;
using Newtonsoft.Json.Linq;

namespace WeatherForecast
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string APIKey = "3a75bd2e9ee624791f19099a5687c9a3";
        private string city;
        private string Main;
        private string tempText;
        private int tempFull;
        private double RoundedTemp;
        public MainWindow()
        {
            InitializeComponent();

            
        }

        private async void ComfirmCityBtn_Click(object sender, RoutedEventArgs e)
        {
            SunImage.Visibility = Visibility.Collapsed;
            RainImage.Visibility = Visibility.Collapsed;
            SnowImage.Visibility = Visibility.Collapsed;
            CloudImage.Visibility = Visibility.Collapsed;
            city = CityTextBox.Text.Trim();
            HttpClient client = new HttpClient();
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={APIKey}&units=metric";
            string result = await client.GetStringAsync(url);


            var JsonResult = JObject.Parse(result);
            tempText = JsonResult["main"]["temp"].ToString();
            RoundedTemp = Double.Parse(tempText);
            int tempFull = (int)Math.Round(RoundedTemp);
            TempTextBlock.Text = "Температура: "+ tempFull + "С";
            Main = JsonResult["weather"][0]["main"].ToString();



            switch (Main)
            {
                case "Clear":
                    SunImage.Visibility = Visibility.Visible;
                    MainTextBlock.Text = "Погода: Солнечно";
                        break;

                case "Clouds":
                    CloudImage.Visibility = Visibility.Visible;
                    MainTextBlock.Text = "Погода: Облачно";
                    break;

                case "Rain":
                    RainImage.Visibility = Visibility.Visible;
                    MainTextBlock.Text = "Погода: Дождь";
                    break;

                case "Snow":
                    SnowImage.Visibility = Visibility.Visible;
                    MainTextBlock.Text = "Погода: Снег";
                    break;
            }
                

        }
    }
}
