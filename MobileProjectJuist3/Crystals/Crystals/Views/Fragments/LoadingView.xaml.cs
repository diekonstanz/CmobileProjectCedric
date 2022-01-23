using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crystals.Views.Fragments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingView : StackLayout
    {
        public LoadingView()
        {
            InitializeComponent();
        }
    }
}