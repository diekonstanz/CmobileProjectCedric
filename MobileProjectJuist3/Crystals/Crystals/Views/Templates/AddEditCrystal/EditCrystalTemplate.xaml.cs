using Crystals.ViewModels.Templates.AddEditCrystal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crystals.Views.Templates.AddEditCrystal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCrystalTemplate : ContentView
    {
        public EditCrystalTemplate()
        {
            InitializeComponent();

            EditCrystalViewModel vm = BindingContext as EditCrystalViewModel;
            vm.SetId(0);
        }

        public EditCrystalTemplate(int id)
        {
            InitializeComponent();

            EditCrystalViewModel vm = BindingContext as EditCrystalViewModel;
            vm.SetId(id);
        }
    }
}