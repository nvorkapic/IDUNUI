using IDUNUI.AppPages;
using IDUNUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }

    public class HubViewModel : BaseViewModel
    {
        

        private Page selectedPage2;
        public Page SelectedPage2
        {
            get { return selectedPage2; }
            set { selectedPage2 = value; Notify(); }
        }
    }

    public sealed partial class ConfigurePage : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        private HubViewModel ViewModel;

        public ConfigurePage()
        {
            this.InitializeComponent();

            ViewModel = new HubViewModel();
            this.DataContext = ViewModel;

        }

        private void onLoadList(object sender, RoutedEventArgs e)
        {
            var ViewList = ((ListView)sender as ListView);
            ViewList.ItemsSource = (Application.Current as App).Configuration;
            ViewList.SelectedIndex = 0;

            var SelectedItem = ViewList.SelectedItem as CModel;
            (Application.Current as App).ParameterItem = (Application.Current as App).CMConfigList.Where(x => x.Measurement == SelectedItem.Measurement).FirstOrDefault();
        }

        private void ListSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            CModel Config;

            Config = (sender as ListView).SelectedItem as CModel;

            if (Config == null) { return; }       

            (Application.Current as App).ParameterItem = (Application.Current as App).CMConfigList.Where(x => x.Measurement == Config.Measurement).FirstOrDefault();

            (Application.Current as App).mainPage.Navigate2(Config.PageType);
        }

        private async void FinalizeConfig_Click(object sender, RoutedEventArgs e)
        {
            
            var _rootApp = Application.Current as App;

            if (_rootApp.Configured == false)
            {
                string json = JsonConvert.SerializeObject((Application.Current as App).CMConfigList.ToArray(), Formatting.Indented);
                StorageFile ConfigFile = await localFolder.CreateFileAsync("Configuration.txt", CreationCollisionOption.ReplaceExisting);

                await FileIO.WriteTextAsync(ConfigFile, json);

                _rootApp.Configured = true;

                _rootApp.Navigation.Remove(_rootApp.Navigation.Single(x => x.Page == "Configuration"));
                _rootApp.Navigation.Add(new NavigationModel { ImagePath = "Assets/home.png", Page = "Home", PageType = typeof(HomePage) });
                _rootApp.Navigation.Add(new NavigationModel { ImagePath = "Assets/Finger.png", Page = "Usage", PageType = typeof(Usage) });
                _rootApp.Navigation.Add(new NavigationModel { ImagePath = "Assets/tools.png", Page = "Configuration", PageType = typeof(ConfigurePage) });
                _rootApp.mainPage.Navigate2(typeof(Section3));
            }
            else
            {
                string json = JsonConvert.SerializeObject((Application.Current as App).CMConfigList.ToArray(), Formatting.Indented);
                StorageFile ConfigFile = await localFolder.CreateFileAsync("Configuration.txt", CreationCollisionOption.ReplaceExisting);

                await FileIO.WriteTextAsync(ConfigFile, json);
            }

        }

        private void onLoadBtn(object sender, RoutedEventArgs e)
        {
            Button Button = (Button)sender as Button;
            if ((Application.Current as App).Configured == false)
            {
                
                Button.Content = "Finalize Startup Configuration";
            }
            else
            {
                Button.Content = "Save";
            }
        }
    }
}
