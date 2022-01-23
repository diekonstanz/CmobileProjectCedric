
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
    public class EditCrystalViewModel : BaseViewModel
    {
        #region Private & Protected
        private int _id;

        #endregion

        #region Properties
        public ValidatableObject<string> Name { get; set; }
        public ValidatableObject<DateTime> RegisteredDate { get; set; }

        #endregion

        #region Commands

        public ICommand CreateCommand { get; set; }
        public ICommand ValidateCommand { get; set; }

        #endregion

        #region Constructors

        public EditCrystalViewModel() : base()
        {

            CreateCommand = new Command(CreateCommandHandler);
            ValidateCommand = new Command<string>(ValidateCommandHandler);
            AddValidations();

        }

        public EditCrystalViewModel(int id) : base()
        {
            this._id = id;
            CreateCommand = new Command(CreateCommandHandler);
            ValidateCommand = new Command<string>(ValidateCommandHandler);
            AddValidations();

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
                return;
            }
            try
            {
                Crystal crystal = new Crystal();
                crystal.Id = this._id;
                crystal.Name = Name.Value;
                crystal.RegisteredDate = RegisteredDate.Value;
                crystal.UserId = App.CurrentUser.Id;

                int id = await CrystalService.SaveCrystalAsync(crystal);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Close");
            }
        }

        public void SetId(int id)
        {
            this._id = id;
            LoadCrystal();
        }

        #endregion

        #region Private Methods

        private async void LoadCrystal()
        {
            Crystal crystal = await CrystalService.GetCrystalAsync(this._id);
            Name.Value = crystal.Name;
            RegisteredDate.Value = crystal.RegisteredDate;
        }
        private bool IsFormValid()
        {
            return Name.Validate() && RegisteredDate.Validate(); ;
        }

        private void ValidateForm()
        {
            Name.Validate();
            RegisteredDate.Validate();
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
