using MobileProjectJuist3.Models;
using MobileProjectJuist3.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MobileProjectJuist3.Views
{
    /// <summary>
    /// Page to sign in with user details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpPage" /> class.
        /// </summary>
        public SignUpPage()
        {
            this.InitializeComponent();
        }
        private async void SfButton_OnClicked(object sender, System.EventArgs e)
        {

            await Navigation.PushAsync(new LoginPage());

        }

    }
}