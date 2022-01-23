using Crystals.ViewModels.Templates.AddEditCrystal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crystals.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCrystalPage : ContentPage
    {
        public EditCrystalPage(int id)
        {
            InitializeComponent();
            EditCrystalViewModel vm = new EditCrystalViewModel(id); ;
            vm.SetId(id);
            BindingContext = vm;
        }
    }
}