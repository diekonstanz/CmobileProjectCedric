using Crystals.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Crystals.ViewModels
{
    public class LoginPageViewModel :
        BaseViewModel
    {


        #region Commands

        public ICommand BackCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand SwitchToSignUpCommand { get; set; }

        #endregion

        #region Constructors

        public LoginPageViewModel() : base()
        {
            BackCommand = new Command(BackCommandHandler);
            SwitchToSignUpCommand = new Command(SwitchToSignUpCommandHandler);
        }

        public void Initialize()
        {
            Title = "Login";
        }

        #endregion

        #region Command Handlers


        private void SwitchToSignUpCommandHandler()
        {
            _ = Application.Current.MainPage.Navigation.PushAsync(new SignUpPage());
        }

        #endregion
    }
}
