using GalaSoft.MvvmLight.Ioc;
using NativeAndForms.ViewModel;
using Xamarin.Forms;

namespace NativeAndForms.Views
{
    public partial class HomePage : ContentPage
    {
        private HomeViewModel ViewModel;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = ViewModel = SimpleIoc.Default.GetInstance<ViewModelLocator>().Home;
        }
    }
}
