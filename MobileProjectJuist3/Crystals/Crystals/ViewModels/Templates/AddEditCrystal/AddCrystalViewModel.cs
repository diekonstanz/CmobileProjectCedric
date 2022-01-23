
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Input;
using Crystals.Helpers;
using Crystals.Helpers.Validations;
using Crystals.Helpers.Validations.Rules;
using Crystals.Models;
using Crystals.Services;
using Xamarin.Forms;

namespace Crystals.ViewModels.Templates.AddEditCrystal
{
    public class AddCrystalViewModel : BaseViewModel
    {
        #region Properties

        public ValidatableObject<string> Name { get; set; }
        public ValidatableObject<DateTime> RegisteredDate { get; set; }

        #endregion

        #region Commands

        public ICommand CreateCommand { get; set; }

        public ICommand ValidateCommand { get; set; }

        #endregion

        #region Constructors

        public AddCrystalViewModel() : base()
        {
            CreateCommand = new Command(CreateCommandHandler);
            ValidateCommand = new Command<string>(ValidateCommandHandler);

            AddValidations();

            RegisteredDate.Value = DateTime.Now;
        }

        #endregion

        #region Validation Handlers

        private void ValidateCommandHandler(string field)
        {
            switch (field)
            {
                case "name": Name.Validate(); break;
                case "date": RegisteredDate.Validate(); break;
            }
        }

        #endregion

        #region Command Handlers

        private async void CreateCommandHandler()
        {
            ValidateForm();
            if (!IsFormValid())
            {
                Debug.WriteLine("Invalid");
                return;
            }
            try
            {
                Crystal crystal = new Crystal();
                crystal.UserId = App.CurrentUser.Id;
                crystal.Name = Name.Value;
                crystal.RegisteredDate = RegisteredDate.Value;
                int id = await CrystalService.SaveCrystalAsync(crystal);

                Debug.WriteLine("Created crystal" + id);
                MessagingCenter.Send<string, bool>("SetCrystals", "SetCrystals", true);
                await Application.Current.MainPage.Navigation.PopAsync(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Close");
            }
        }

        #endregion

        #region Private Methods


        private bool IsFormValid()
        {
            return Name.Validate() && RegisteredDate.Validate();
        }

        private void ValidateForm()
        {
           Debug.WriteLine( Name.Validate());
            Debug.WriteLine(RegisteredDate.Validate());
            //RegisteredDate.Validate();
        }

        private void AddValidations()
        {
            Name = new ValidatableObject<string>();
            RegisteredDate = new ValidatableObject<DateTime>();

            Name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A name is required." });
            RegisteredDate.Validations.Add(new IsNotNullOrEmptyRule<DateTime> { ValidationMessage = "A date is required." });
        }

        #endregion
    }
}
