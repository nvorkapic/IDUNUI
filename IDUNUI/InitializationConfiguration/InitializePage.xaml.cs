using IDUNUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IDUNUI.InitializationConfiguration
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InitializePage : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public List<InitializationModel> InitList = new List<InitializationModel>();

        public InitializePage()
        {
            this.InitializeComponent();

        }

        private async void Initialize_Click(object sender, RoutedEventArgs e)
        {
            var _rootApp = Application.Current as App;

            var UserCred = _rootApp.vault.FindAllByResource("idun");
            var User = UserCred.SingleOrDefault().UserName;

            var Device = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily.ToString();

            InitList.Add(new InitializationModel { UserName = User, DeviceType = Device, Date = DateTime.Now.ToLocalTime() });

            InitListView.ItemsSource = InitList;

            ProceedButton.IsEnabled = true;

            string json = JsonConvert.SerializeObject(InitList.ToArray(), Formatting.Indented);
            StorageFile ConfigFile = await localFolder.CreateFileAsync("Initialization.txt", CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(ConfigFile, json);

            _rootApp.Initialized = true;
            

        }

        private void ProceedButton_Click(object sender, RoutedEventArgs e)
        {
            var _rootApp = Application.Current as App;

            _rootApp.Navigation.Remove(_rootApp.Navigation.Single(x => x.Page == "Initialization"));
            _rootApp.Navigation.Add(new NavigationModel { ImagePath = "Assets/tools.png", Page = "Configuration", PageType = typeof(ConfigurePage) });

            _rootApp.mainPage.Navigate(typeof(ConfigurePage));

            _rootApp.Configuration.Add(new CModel { Measurement = "Usage", Enabled = false, ImagePath = "/Assets/Finger.png", PageType = typeof(MeasurementConfig) });
            _rootApp.Configuration.Add(new CModel { Measurement = "Temperature", Enabled = false, ImagePath = "/Assets/thermo.png", PageType = typeof(MeasurementConfig) });
            _rootApp.Configuration.Add(new CModel { Measurement = "Pressure", Enabled = false, ImagePath = "/Assets/pressurex.png", PageType = typeof(MeasurementConfig) });
            _rootApp.Configuration.Add(new CModel { Measurement = "Humidity", Enabled = false, ImagePath = "/Assets/humidity.png", PageType = typeof(MeasurementConfig) });
            _rootApp.Configuration.Add(new CModel { Measurement = "Acceleration", Enabled = false, ImagePath = "/Assets/Accelerometer.png", PageType = typeof(MeasurementConfig) });
            _rootApp.Configuration.Add(new CModel { Measurement = "Magnetic Field", Enabled = false, ImagePath = "/Assets/magnet.png", PageType = typeof(MeasurementConfig) });
            _rootApp.Configuration.Add(new CModel { Measurement = "Gyroscope", Enabled = false, ImagePath = "/Assets/gyrow.png", PageType = typeof(MeasurementConfig) });


            _rootApp.CMConfigList.Add(new CModel { Measurement = "Usage", Enabled = false, ImagePath = "/Assets/Finger.png", Report = 1, Interval = 1000 });
            _rootApp.CMConfigList.Add(new CModel { Measurement = "Temperature", Enabled = false, ImagePath = "/Assets/thermo.png", Report = 1, Interval = 1000 });
            _rootApp.CMConfigList.Add(new CModel { Measurement = "Pressure", Enabled = false, ImagePath = "/Assets/pressurex.png", Report = 1, Interval = 1000 });
            _rootApp.CMConfigList.Add(new CModel { Measurement = "Humidity", Enabled = false, ImagePath = "/Assets/humidity.png", Report = 1, Interval = 1000 });
            _rootApp.CMConfigList.Add(new CModel { Measurement = "Acceleration", Enabled = false, ImagePath = "/Assets/Accelerometer.png", Report = 1, Interval = 1000 });
            _rootApp.CMConfigList.Add(new CModel { Measurement = "Magnetic Field", Enabled = false, ImagePath = "/Assets/magnet.png", Report = 1, Interval = 1000 });
            _rootApp.CMConfigList.Add(new CModel { Measurement = "Gyroscope", Enabled = false, ImagePath = "/Assets/gyrow.png", Report = 1, Interval = 1000 });
        }
    }
}
