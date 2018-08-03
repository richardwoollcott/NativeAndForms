using GalaSoft.MvvmLight.Ioc;
using NativeAndForms.ViewModel;
using Xamarin.Forms;

namespace NativeAndForms.Views
{
    public partial class DashboardPage : ContentPage
    {
        private DashboardViewModel ViewModel;

        public DashboardPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = SimpleIoc.Default.GetInstance<ViewModelLocator>().Dashboard;
        }
    }
}
