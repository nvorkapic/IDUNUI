using IDUNUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
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
    public sealed partial class LoginPage : Page
    {
       

        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var _rootApp = Application.Current as App;
            _rootApp.vault.Add(new PasswordCredential("idun", UserName.Text, Password.Password));

            _rootApp.Navigation.Remove(_rootApp.Navigation.Single(x => x.Page == "Authorisation"));
            _rootApp.Navigation.Add(new NavigationModel { ImagePath = "Assets/tools.png", Page = "Initialization", PageType = typeof(InitializePage) });

            _rootApp.mainPage.Navigate(typeof(InitializePage));
  
        }
    }
}
