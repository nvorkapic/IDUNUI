using IDUNUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IDUNUI.InitializationConfiguration
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MeasurementConfig : Page
    {
        public CModel Config { get; set; }

        

        string value;
        double number;

        public MeasurementConfig()
        {
            this.InitializeComponent();

            EnableConfig.Checked += EnableConfig_Change;
            EnableConfig.Unchecked += EnableConfig_Change;

            EnableUsageThreshold.Checked += EnableUsageThreshold_Change;
            EnableUsageThreshold.Unchecked += EnableUsageThreshold_Change;
        }

        private void EnableConfig_Change(object sender, RoutedEventArgs e)
        {
            DefBtn.IsEnabled = !DefBtn.IsEnabled;

            if (ConfigurationPanel.Visibility == Visibility.Collapsed)
            {
                ConfigurationPanel.Visibility = Visibility.Visible;
            }
            else
            {
                ConfigurationPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void EnableUsageThreshold_Change(object sender, RoutedEventArgs e)
        {

            ThresholdBottom.IsEnabled = !ThresholdBottom.IsEnabled;
            ThresholdCeiling.IsEnabled = !ThresholdCeiling.IsEnabled;
        }


        private void LoadDefaults(object sender, RoutedEventArgs e)
        {
            Config.Report = 1;
            Config.Interval = 1000;
            Config.Threshold = false;
            Config.ThresholdBottomValue = null;
            Config.ThresholdCeilingValue = null;

            IntervalSetting.Text = "1000";
            EnableUsageThreshold.IsChecked = Config.Threshold;
            ThresholdBottom.Text = "";
            ThresholdBottom.IsEnabled = false;
            ThresholdCeiling.Text = "";
            ThresholdCeiling.IsEnabled = false;
            FirstOptionReporting.IsChecked = true;

        }

        private void SaveDataBoxBot(object sender, RoutedEventArgs e)
        {


            value = ThresholdBottom.Text;

            if (double.TryParse(value, out number))
            {
                Config.ThresholdBottomValue = number;
            }
            else
            {
                ThresholdBottom.Text = "0";
            }

        }

        private void SaveDataBoxCei(object sender, RoutedEventArgs e)
        {

            value = ThresholdCeiling.Text;

            if (double.TryParse(value, out number))
            {
                Config.ThresholdCeilingValue = number;
            }
            else
            {
                ThresholdCeiling.Text = "0";
            }

        }

        //private void PanelOnLoad(object sender, RoutedEventArgs e)
        //{
        //    if (Config.Enabled == true)
        //    {
        //        ConfigurationPanel.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        ConfigurationPanel.Visibility = Visibility.Collapsed;
        //    }
        //}


        private void PageOnLoad(object sender, RoutedEventArgs e)
        {

            if ((Application.Current as App).ParameterItem == null)
            {
                (Application.Current as App).ParameterItem = (Application.Current as App).Configuration.FirstOrDefault();
            }
            Config = (Application.Current as App).ParameterItem;

            this.DataContext = Config;

            if (Config.Enabled == true)
            {
                ConfigurationPanel.Visibility = Visibility.Visible;
            }
            else
            {
                ConfigurationPanel.Visibility = Visibility.Collapsed;
            }

            if (Config.Measurement == "Usage")
            {
                Image img = new Image();
                BitmapImage bitmap = new BitmapImage(new Uri("ms-appx://MainApp/Assets/Finger.png"));


                ImageTitle.Source = bitmap;
            }
            if (Config.Measurement == "Temperature")
            {
                Image img = new Image();
                BitmapImage bitmap = new BitmapImage(new Uri("ms-appx://MainApp/Assets/thermo.png"));


                ImageTitle.Source = bitmap;

            }
            if (Config.Measurement == "Pressure")
            {
                Image img = new Image();
                BitmapImage bitmap = new BitmapImage(new Uri("ms-appx://MainApp/Assets/pressurex.png"));


                ImageTitle.Source = bitmap;

            }
            if (Config.Measurement == "Humidity")
            {
                Image img = new Image();
                BitmapImage bitmap = new BitmapImage(new Uri("ms-appx://MainApp/Assets/humidity.png"));


                ImageTitle.Source = bitmap;

            }
            if (Config.Measurement == "Acceleration")
            {
                Image img = new Image();
                BitmapImage bitmap = new BitmapImage(new Uri("ms-appx://MainApp/Assets/Accelerometer.png"));

                ImageTitle.Source = bitmap;

            }
            if (Config.Measurement == "Magnetic Field")
            {
                Image img = new Image();
                BitmapImage bitmap = new BitmapImage(new Uri("ms-appx://MainApp/Assets/magnet.png"));

                ImageTitle.Source = bitmap;

            }
            if (Config.Measurement == "Gyroscope")
            {
                Image img = new Image();
                BitmapImage bitmap = new BitmapImage(new Uri("ms-appx://MainApp/Assets/gyrow.png"));

                ImageTitle.Source = bitmap;

            }
        }
    }
}
