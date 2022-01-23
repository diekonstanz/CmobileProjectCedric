
using System;
using System.Diagnostics;
using System.Windows.Input;
using Crystals.Helpers;
using Crystals.Helpers.Validations;
using Crystals.Helpers.Validations.Rules;
using Crystals.Models;
using Crystals.Services;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Crystals.ViewModels.Templates.Auth
{
    public class SignUpViewModel : BaseViewModel
    {

        #region Properties
        public ValidatableObject<string> Username { get; set; }
        public ValidatableObject<string> Email { get; set; }
        public ValidatableObject<string> Password { get; set; }
        public ValidatableObject<string> ConfirmPassword { get; set; }

        #endregion

        #region Commands

        public ICommand SignUpCommand { get; set; }
        public ICommand ValidateCommand { get; set; }

        #endregion

        #region Constructors

        public SignUpViewModel() : base()
        {
            SignUpCommand = new Command(SignUpCommandHandler);

            ValidateCommand = new Command<string>(ValidateCommandHandler);

            AddValidations();
        }

        #endregion

        #region Validation Handlers

        private void ValidateCommandHandler(string field)
        {
            switch (field)
            {
                case "username": Username.Validate(); break;
                case "email": Email.Validate(); break;
                case "password": Password.Validate(); break;
                case "confirmPassword": ConfirmPassword.Validate(); break;
            }
        }

        #endregion

        #region Command Handlers

        private async void SignUpCommandHandler()
        {
            try
            {
                MainState = LayoutState.Loading;
                if (ValidateSignUpData())
                {
                    User user = new User();
                    user.Name = Username.Value;
                    user.Email = Email.Value;
                    user.Password = Password.Value;
                    int id = await UserService.SaveUserAsync(user);

                    if(id >= 0)
                    {
                        ClearAuthData();
                        Debug.WriteLine("User Created");
                        // TODO GOTO LOGIN
                    } else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", Constants.Errors.GeneralError, "Close");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Close");
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
            Username = new ValidatableObject<string>();
            Email = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
            ConfirmPassword = new ValidatableObject<string>();

            Username.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A email is required." });
            Email.Validations.Add(new IsEmailRule<string> { ValidationMessage = "Email format is not correct." });
            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
            ConfirmPassword.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A confirm password is required." });
        }

        private bool ValidateSignUpData()
        {
            if (Username.IsValid == false ||
                Email.IsValid == false ||
                Password.IsValid == false ||
                ConfirmPassword.IsValid == false ||
                !Password.Value.Equals(ConfirmPassword.Value))
                return false;
            return true;
        }

        private void ClearAuthData()
        {
            Username.Value = Email.Value = Password.Value = ConfirmPassword.Value = string.Empty;
        }

        #endregion
    }
}
