
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Crystals.Helpers;
using Crystals.Helpers.Validations;
using Crystals.Helpers.Validations.Rules;
using Crystals.Services;
using Crystals.Models;
using Crystals.Views;

namespace Crystals.ViewModels.Templates.Auth
{
    public class LoginViewModel : BaseViewModel
    {


        #region Properties

        public ValidatableObject<string> Email { get; set; }
        public ValidatableObject<string> Password { get; set; }

        #endregion

        #region Commands

        public ICommand LoginCommand { get; set; }
        public ICommand ValidateCommand { get; set; }

        #endregion

        #region Constructors

        public LoginViewModel() : base()
        {

            LoginCommand = new Command(LoginCommandHandler);

            ValidateCommand = new Command<string>(ValidateCommandHandler);

            AddValidations();
        }

        #endregion

        #region Validation Handlers

        private void ValidateCommandHandler(string field)
        {
            switch (field)
            {
                case "email": Email.Validate(); break;
                case "password": Password.Validate(); break;
            }
        }

        #endregion

        #region Command Handlers

        private async void LoginCommandHandler()
        {
            try
            {
                MainState = LayoutState.Loading;
                if (ValidateLoginData())
                {
                    User user = await UserService.LoginValidate(Email.Value, Password.Value);
                    if(user != null)
                    {
                        App.CurrentUser = user;
                        Debug.WriteLine("User login");
                        await Application.Current.MainPage.Navigation.PushAsync(new CrystalsPage());
                        ClearAuthData();
                    }
                    else
                    {
                        Debug.WriteLine("User error");
                        await App.Current.MainPage.DisplayAlert("Warning", Constants.Errors.WrongUserOrPasswordError, "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", Constants.Errors.GeneralError, "Close");
                Debug.WriteLine(ex);
            }
            finally
            {
                MainState = LayoutState.None;
            }
        }

        #endregion

        #region Private Functionality

        private void AddValidations()
        {
            Email = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();

            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A email is required." });
            Email.Validations.Add(new IsEmailRule<string> { ValidationMessage = "Email format is not correct" });
            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
        }

        private bool ValidateLoginData()
        {
            if (Email.IsValid == false ||
                Password.IsValid == false)
                return false;
            return true;
        }

        private void ClearAuthData()
        {
            Email.Value = Password.Value = string.Empty;
        }

        #endregion
    }
}
