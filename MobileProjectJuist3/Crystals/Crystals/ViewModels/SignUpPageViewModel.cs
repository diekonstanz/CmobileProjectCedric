using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Crystals.ViewModels
{
    public class SignUpPageViewModel : 
        BaseViewModel
    {

        #region Commands

        public ICommand BackCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand SwitchToLoginCommand { get; set; }

        #endregion

        #region Constructors

        public SignUpPageViewModel() : base()
        {
            BackCommand = new Command(BackCommandHandler);
            SwitchToLoginCommand = new Command(SwitchToLoginCommandHandler);
        }

        public void Initialize()
        {
            Title = "Sign Up";
        }

        #endregion

        #region Command Handlers

        private void SwitchToLoginCommandHandler()
        {
            _ = Application.Current.MainPage.Navigation.PopAsync();
        }


        #endregion
    }
}
