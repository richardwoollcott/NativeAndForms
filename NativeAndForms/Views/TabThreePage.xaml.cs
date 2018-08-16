using GalaSoft.MvvmLight.Ioc;
using NativeAndForms.ViewModel;
using Xamarin.Forms;

namespace NativeAndForms.Views
{
    public partial class TabThreePage : ContentPage
    {
        private TabThreeViewModel ViewModel;

        public TabThreePage()
        {
            InitializeComponent();

            BindingContext = ViewModel = SimpleIoc.Default.GetInstance<ViewModelLocator>().TabThree;
        }
    }
}
