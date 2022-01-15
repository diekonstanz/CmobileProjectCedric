using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MobileProjectJuist3.Views
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        public LoginPage()
        {
            this.InitializeComponent();
        }
        

        private async void SfButton_OnClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }



        private async void ForgotPasswordLabel_OnClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPasswordPage());
        }
    }
}