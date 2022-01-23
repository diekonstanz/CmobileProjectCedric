
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

        bool isRefreshing;

        public bool IsRefreshing
        {
            get
            {
                return isRefreshing;
            }
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand AddCommand { get; set; }
        public ICommand ProfileCommand { get; set; }
        public ICommand MoreCommand { get; set; }
        public ICommand DeleteCrystalCommand { get; set; }
        public ICommand EditCrystalCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand FilterCommand { get; set; }


        #endregion

        #region Constructors

        public CrystalsPageViewModel() : base()
        {

            AddCommand = new Command(AddCommandHandler);
            DeleteCrystalCommand = new Command<Crystal>(DeleteCrystalCommandHandler);
            EditCrystalCommand = new Command<Crystal>(EditCrystalCommandHandler);
            ProfileCommand = new Command(ProfileCommandHandler);
            MoreCommand = new Command(MoreCommandHandler);
            FilterCommand = new Command(FilterCommandHandlerAsync);
            RefreshCommand = new Command(() =>
            {
                Debug.Write("Refresh " + IsRefreshing);
                IsRefreshing = true;
                SetCrystals();
                IsRefreshing = false;
                isRefreshing = false;
                OnPropertyChanged("IsRefreshing");
                OnPropertyChanged();
                Debug.Write("Refresh " + IsRefreshing);
            });

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
        private async void FilterCommandHandlerAsync()
        {
            Debug.WriteLine("Filter");
            Debug.WriteLine(IsRefreshing);
            if (IsRefreshing)
            {
                return;
            }
            IsRefreshing = true;
            CrystalList.Clear();
            try
            {
                List<Crystal> crystals = await CrystalService.GetCrystalsForUserByNameAsync(App.CurrentUser.Id, Filter);
                if (crystals.Count > 0)
                {
                    foreach (Crystal c in crystals)
                    {
                        CrystalList.Add(c);
                    }
                }
                Debug.Write(CrystalListState);
                Debug.Write(crystals.Count);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            IsRefreshing = false;
            isRefreshing = false;

        }

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
           
            await Application.Current.MainPage.Navigation.PushAsync(new EditCrystalPage(crystal.Id));
        }

        private void ProfileCommandHandler()
        {
            //_navigationService.NavigateAsync(nameof(ProfilePage));
            _ = Application.Current.MainPage.Navigation.PushAsync(new ProfilePage());
        }



        #endregion

        #region Private Methods

        private async void SetCrystals(bool silentFail = false)
        {
            Debug.WriteLine("Setting crystals");
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
                }
                Debug.Write(CrystalListState);
                Debug.Write(crystals.Count);
            } catch(Exception ex)
            {
                if (!silentFail)
                {
                    await App.Current.MainPage.DisplayAlert("Error", Constants.Errors.GeneralError, "Close");
                }
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

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
