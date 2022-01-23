using System.ComponentModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Crystals.ViewModels
{
    public class BaseViewModel
    {

        #region Properties
        public string Title { get; set; }
        public LayoutState MainState { get; set; }
        #endregion

        #region Constructor

        public BaseViewModel()
        {

        }

        #endregion

        #region

        protected async void BackCommandHandler()
        {
            if (Device.RuntimePlatform == Device.UWP && Application.Current.MainPage.Navigation.NavigationStack.Count > 1)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else if (Device.RuntimePlatform != Device.UWP && Application.Current.MainPage.Navigation.NavigationStack.Count > 0)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        #endregion


        #region IPageLifecycleAware

        public virtual void OnAppearing() { }

        public virtual void OnDisappearing() { }

        #endregion

        #region IDestructible

        public virtual void Destroy() { }

        #endregion
    }
}
