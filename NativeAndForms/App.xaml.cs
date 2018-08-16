using GalaSoft.MvvmLight.Ioc;
using NativeAndForms.Navigation;
using NativeAndForms.ViewModel;
using NativeAndForms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NativeAndForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Configure and register the MVVM Light NavigationService
            var nav = new ViewNavigationService();
            SimpleIoc.Default.Register<IViewNavigationService>(() => nav);

            nav.Configure(ViewModelLocator.HomePageKey, typeof(HomePage));

            nav.Configure(ViewModelLocator.DashboardPageKey, typeof(DashboardPage));

            //nav.Configure(ViewModelLocator.TabOnePageKey, typeof(TabOnePage));

            var locator = new ViewModelLocator();

            SimpleIoc.Default.Register<ViewModelLocator>(() => locator);

            var mainPage = nav.SetRootPage(ViewModelLocator.HomePageKey);

            //var mainPage = nav.SetRootPage(ViewModelLocator.TabOnePageKey);

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
