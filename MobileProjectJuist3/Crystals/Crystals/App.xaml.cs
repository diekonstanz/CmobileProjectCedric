using Crystals.Models;
using Crystals.Services;
using Crystals.Views;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("FontAwesome-Regular.ttf", Alias = "FontAwesome_Regular")]
[assembly: ExportFont("FontAwesome-Solid.ttf", Alias = "FontAwesome_Solid")]

[assembly: ExportFont("Mulish-Black.ttf", Alias = "Mulish_Black")]
[assembly: ExportFont("Mulish-Bold.ttf", Alias = "Mulish_Bold")]
[assembly: ExportFont("Mulish-ExtraBold.ttf", Alias = "Mulish_ExtraBold")]
[assembly: ExportFont("Mulish-ExtraLight.ttf", Alias = "Mulish_ExtraLight")]
[assembly: ExportFont("Mulish-Light.ttf", Alias = "Mulish_Light")]
[assembly: ExportFont("Mulish-Medium.ttf", Alias = "Mulish_Medium")]
[assembly: ExportFont("Mulish-Regular.ttf", Alias = "Mulish_Regular")]
[assembly: ExportFont("Mulish-SemiBold.ttf", Alias = "Mulish_SemiBold")]

namespace Crystals
{
    public partial class App : Application
    {
        public static string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Crystal.db");
        private static DatabaseService databaseService;

        public static DatabaseService DatabaseService
        {
            get
            {
                return databaseService;
            }
        }

        private static User user;

        public static User CurrentUser
        {
            get
            {
                return user;
            }
            set
            {
                user = value;

            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        protected override void OnStart()
        {
            if (databaseService == null)
            {
                databaseService = new DatabaseService(DatabasePath);
            }

            SetAppTheme();

            MainPage = new NavigationPage(new WelcomePage());
            
        }

        private void SetAppTheme()
        {
            var theme = Preferences.Get("theme", string.Empty);
            if (string.IsNullOrEmpty(theme) || theme == "light")
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;
            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;
            }
        }

     

    }
}
