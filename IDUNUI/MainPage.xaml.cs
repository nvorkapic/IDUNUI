using IDUNUI.AppPages;
using IDUNUI.Models;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IDUNUI
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

        private Page selectedPage;
        public Page SelectedPage
        {
            get { return selectedPage; }
            set { selectedPage = value; Notify(); }
        }

        private Page selectedPage2;
        public Page SelectedPage2
        {
            get { return selectedPage2; }
            set { selectedPage2 = value; Notify(); }
        }
    }



    public sealed partial class MainPage : Page
    {

        private HubViewModel ViewModel;


        public MainPage()
        {
            this.InitializeComponent();

            ViewModel = new HubViewModel();
            this.DataContext = ViewModel;

            (Application.Current as App).mainPage = this;

        }



        private void ListViewSectionOne_onLoad(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            listView.ItemsSource = (Application.Current as App).Navigation;
            listView.SelectedIndex = 0;
        }

        public void Navigate(Type T)
        {
            ViewModel.SelectedPage = Activator.CreateInstance(T) as Page;
            
        }

        public void Navigate2 (Type T)
        {
            ViewModel.SelectedPage2 = Activator.CreateInstance(T) as Page;
        }
        
        private void ListViewSelectionOne_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationModel Config;
          
            Config = (sender as ListView).SelectedItem as NavigationModel;
             
            if (Config == null) { return; }

            HeaderImage.Source = new BitmapImage(new Uri("ms-appx://IDUNUI/" + Config.ImagePath));
            HeaderText.Text = Config.Page;

            Navigate(Config.PageType);
            Navigate2(typeof(Section3));
        }

        private void ContainerChange(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            var listView = (ListView)sender;
            listView.SelectedIndex = 0;
        }

    }
}
