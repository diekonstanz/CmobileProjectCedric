using System.Windows.Input;
using Crystals.Views;
using Xamarin.Forms;

namespace Crystals.ViewModels
{
    public class WelcomePageViewModel: BaseViewModel
    {
        #region Commands

        public ICommand GetStartedCommand { get; set; }

        #endregion

        #region Constructors

        public WelcomePageViewModel() : base()
        {
            GetStartedCommand = new Command(GetStartedCommandHandler);
        }

        #endregion

        #region Command Handlers

        private async void GetStartedCommandHandler()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
        #endregion
    }
}
