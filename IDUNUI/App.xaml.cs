using IDUNUI.InitializationConfiguration;
using IDUNUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace IDUNUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public bool Initialized;
        public bool Configured;
        public bool Credentialized;

        public PasswordVault vault = new PasswordVault();

        public ObservableCollection<NavigationModel> Navigation = new ObservableCollection<NavigationModel>();

        public NavigationModel SelectedListItem { get; set; }

        public MainPage mainPage;
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            CredentialsCheck();
            InitializeCheck();
            ConfigurationCheck();

            if (!Credentialized) { Navigation.Add(new NavigationModel { ImagePath="Assets/Users.png", Page="Authorisation", PageType=typeof(LoginPage) }); }
            else
            {
                if (!Initialized) { Navigation.Add(new NavigationModel { ImagePath = "Assets/tools.png", Page = "Initialization", PageType = typeof(InitializePage) }); }
                else
                {
                    if (!Configured) { Navigation.Add(new NavigationModel { ImagePath = "Assets/tools.png", Page = "Configuration", PageType = typeof(ConfigurePage) }); }
                }
            }
        }

        public void CredentialsCheck()
        {
            try
            {
                //if it passes it will use saved username and password
                var credList = vault.FindAllByResource("idun");
                Credentialized = true;
            }
            catch (Exception)
            {
                Credentialized = false; //if here show login page
                //vault.Add(new PasswordCredential("idun", "alain", "alain")); <on the login button>
            }
        }


        public async void InitializeCheck()
        {
            try
            {
                StorageFile InitializationFile = await localFolder.GetFileAsync("Initialization.txt");
                Stream InitializationFileStream = await InitializationFile.OpenStreamForReadAsync();
                InitializationFileStream.Dispose();
                Initialized = true;
            }
            catch
            {
                Initialized = false;
            }
        }

        public async void ConfigurationCheck()
        {
            try
            {
                StorageFile ConfigurationFile = await localFolder.GetFileAsync("Configuration.txt");
                Stream ConfigurationFileStream = await ConfigurationFile.OpenStreamForReadAsync();
                ConfigurationFileStream.Dispose();
                Configured = true;
            }
            catch
            {
                Configured = false;
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
