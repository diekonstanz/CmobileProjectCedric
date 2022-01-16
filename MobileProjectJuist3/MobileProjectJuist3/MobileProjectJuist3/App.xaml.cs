using MobileProjectJuist3.Services;
using MobileProjectJuist3.Views;
using System;
using System.IO;
using MobileProjectJuist3.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Montserrat-Bold.ttf",Alias="Montserrat-Bold")]
     [assembly: ExportFont("Montserrat-Medium.ttf", Alias = "Montserrat-Medium")]
     [assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat-Regular")]
     [assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "Montserrat-SemiBold")]
     [assembly: ExportFont("UIFontIcons.ttf", Alias = "FontIcons")]
namespace MobileProjectJuist3
{
    public partial class App : Application
    {
        public static string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Crystal.db");


        private static DatabaseService databaseService;

        public static DatabaseService DatabaseService
        {
            get
            {
                if (databaseService == null)
                {
                    databaseService = new DatabaseService(DatabasePath);
                }
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
                if (user == null)
                {
                    user = value;
                }
            }
        }



        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
