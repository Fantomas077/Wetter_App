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
using System.Net;
using Newtonsoft.Json.Linq;

namespace App_Meteo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string BaseApi = "http://goweather.herokuapp.com/weather/";
        public string JsonString;
        public string city = "Berlin";
        public string wetter;
        JObject o;
        
        public MainWindow()
        {
           
            InitializeComponent();
           
            Datum.Content = DateTime.Now.ToString("dddd,dd MMMM yyyy");
            forecast1.Content = DateTime.Now.AddDays(1).ToString("ddd");
            forecast2.Content= DateTime.Now.AddDays(2).ToString("ddd");
            forecast3.Content= DateTime.Now.AddDays(3).ToString("ddd");
            GetApiReponse();
            SetUiInfo();




        }

        public void GetApiReponse()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    JsonString = wc.DownloadString(BaseApi + city);

                    byte[] bytes = Encoding.Default.GetBytes(JsonString);
                    JsonString = Encoding.UTF8.GetString(bytes);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Not Found ","Nicht Gefunden",MessageBoxButton.OK,MessageBoxImage.Information);
            }
           
        }

       public void SetUiInfo()
        {
            o = JObject.Parse(JsonString);
            Description.Content = o["description"];
            temperatur.Content = o["temperature"].ToString().Replace(" ","");
            wind.Content = o["wind"];
            temp1.Content = o["forecast"] [0]["temperature"].ToString().Replace(" ","");
            temp2.Content = o["forecast"][1]["temperature"].ToString().Replace(" ", "");
            temp3.Content = o["forecast"][2]["temperature"].ToString().Replace(" ", "");


        }

        private void btnSuchen_Click(object sender, RoutedEventArgs e)
        {

            string capitalize;
            city = StadtTxt.Text;
            capitalize = char.ToUpper(city[0]) + city.Substring(1);
            Stadt.Content = capitalize;
            GetApiReponse();
            SetUiInfo();
        }

        private void btnInfos_Click(object sender, RoutedEventArgs e)
        {
            LblInfos.Visibility = Visibility.Visible;
        }

       

       
    }
}
