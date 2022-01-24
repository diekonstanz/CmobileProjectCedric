using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Crystals.Models;
using Crystals.Services;
using Crystals.Views;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Crystals.ViewModels
{
    public class ProfilePageViewModel :
        BaseViewModel
    {


        #region Commands

        public ICommand BackCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand DarkModeToggleCommand { get; set; }

        #endregion

        #region Properties

        public string Username { get; set; }
        public string Email { get; set; }
        public int TotalCrystals { get; set; }
        public bool IsDarkMode { get; set; }
        public bool IsHideEnabled { get; set; }

        #endregion

        #region Constructors

        public ProfilePageViewModel() : base()
        {

            BackCommand = new Command(BackCommandHandler);
            LogOutCommand = new Command(LogOutCommandHandler);
            DarkModeToggleCommand = new Command(OnIsDarkModeChanged);
            MainState = LayoutState.Loading; 
            IsDarkMode = Application.Current.UserAppTheme.Equals(OSAppTheme.Dark);

            Username = App.CurrentUser.Name;
            Email = App.CurrentUser.Email;
            MainState = LayoutState.None;

            SetFieldsAsync();
        }

        #endregion

        #region Command Handlers

        private void LogOutCommandHandler()
        {

            App.CurrentUser = null;

            _ = Application.Current.MainPage.Navigation.PushAsync(new WelcomePage());
        }

        #endregion

        #region Private Methods

        private void OnIsDarkModeChanged()
        {
            if (IsDarkMode)
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;
                Preferences.Set("theme", "dark");
            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;
                Preferences.Set("theme", "light");
            }
        }

        private async void SetFieldsAsync()
        {
            TotalCrystals = await CrystalService.CountCrystalsForUserAsync(App.CurrentUser.Id);
        }
       

        #endregion
    }
}
