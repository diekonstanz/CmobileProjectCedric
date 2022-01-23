
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

using System.Threading.Tasks;
using System.Windows.Input;
using Crystals.Helpers;
using Crystals.Models;
using Crystals.Services;
using Crystals.Views;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Crystals.ViewModels
{
    public class CrystalsPageViewModel :
        BaseViewModel
    {

        #region Properties

        public ObservableCollection<Crystal> CrystalList { get; set; }
        public LayoutState CrystalListState { get; set; }
        public string Name { get; set; }
        public string Filter { get; set; }

        #endregion

        #region Commands

        public ICommand AddCommand { get; set; }
        public ICommand ProfileCommand { get; set; }
        public ICommand MoreCommand { get; set; }
        public ICommand DeleteCrystalCommand { get; set; }
        public ICommand EditCrystalCommand { get; set; }



        #endregion

        #region Constructors

        public CrystalsPageViewModel() : base()
        {
            MessagingCenter.Subscribe<string, bool>("SetCrystals", "SetCrystals", (sender, arg) => { if (arg) { SetCrystals(); } });

            AddCommand = new Command(AddCommandHandler);
            DeleteCrystalCommand = new Command<Crystal>(DeleteCrystalCommandHandler);
            EditCrystalCommand = new Command<Crystal>(EditCrystalCommandHandler);
            ProfileCommand = new Command(ProfileCommandHandler);
            MoreCommand = new Command(MoreCommandHandler);

            Initialize();

        }

        public void Initialize()
        {
            CrystalListState = LayoutState.Loading;
            CrystalList = new ObservableCollection<Crystal>();
            SetUserName();
            SetCrystals();
        }

        #endregion

        #region Command Handlers

        private void AddCommandHandler()
        {
            //_navigationService.NavigateAsync(nameof(AddEditPage));

            _ = Application.Current.MainPage.Navigation.PushAsync(new AddCrystalPage());
        }

        private void MoreCommandHandler()
        {
            //_navigationService.NavigateAsync(nameof(MorePage));
        }

        private async void DeleteCrystalCommandHandler(Crystal crystal)
        {
            await CrystalService.DeleteCrystalAsync(crystal);
            SetCrystals();
        }

        private async void EditCrystalCommandHandler(Crystal crystal)
        {
           
            _ = Application.Current.MainPage.Navigation.PushAsync(new EditCrystalPage(crystal.Id));
        }

        private void ProfileCommandHandler()
        {
            //_navigationService.NavigateAsync(nameof(ProfilePage));
        }



        #endregion

        #region Private Methods

        private async void SetCrystals()
        {
            CrystalListState = LayoutState.Loading;
            CrystalList.Clear();
            try
            {
                List<Crystal> crystals = await CrystalService.GetCrystalsForUserAsync(App.CurrentUser.Id);
                if (crystals.Count > 0)
                {
                    foreach(Crystal c in crystals)
                    {
                        CrystalList.Add(c);
                    }
                    CrystalListState = LayoutState.None;
                }
                else
                {
                    CrystalListState = LayoutState.Empty;
                }
                Debug.Write(CrystalListState);
                Debug.Write(crystals.Count);
            } catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", Constants.Errors.GeneralError, "Close");
                Debug.WriteLine(ex);
            }
            
        }

        private void SetUserName()
        {

            Name = App.CurrentUser.Name;
        }
        #endregion

        #region Override

        public override void Destroy()
        {
            base.Destroy();
        }

        #endregion
    }
}
