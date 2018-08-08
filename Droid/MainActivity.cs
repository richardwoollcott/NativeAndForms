using Android.App;
using Android.OS;
using NativeAndForms.ViewModel;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Helpers;
using NativeAndForms.Droid.Navigation;
using GalaSoft.MvvmLight.Ioc;
using NativeAndForms.Navigation;
using FFImageLoading.Forms.Platform;

namespace NativeAndForms.Droid
{
    [Activity(Label = "NativeAndForms", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity, INavigationView
    {
        private IViewNavigationService navigationService;

        bool initialised;

        // Keep track of bindings to avoid premature garbage collection
        private readonly List<Binding> bindings = new List<Binding>();

        private readonly NavigationHelper navigationHelper = new NavigationHelper();

        public NavigationHelper Helper
        {
            get
            {
                return navigationHelper;
            }
        }

        protected override void OnResume()
        {
            Helper.OnResume(this);
            Helper.ActivityKey = "Main";

            if (!initialised)
            {
                initialised = true;  
                navigationService.NavigateToAsync(ViewModelLocator.HomePageKey);
            }

            base.OnResume();
        }

        /// <summary>
        /// Gets a reference to the MainViewModel from the ViewModelLocator.
        /// </summary>
        private HomeViewModel Vm
        {
            get
            {
                return App.Locator.Home;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            CachedImageRenderer.Init(true);

            //ensure the navigation service is configured
            // by accessing the Vm property which calls the App.Locator
            // TODO refator this initialisation
            var startLocator = Vm;

            navigationService = SimpleIoc.Default.GetInstance<IViewNavigationService>();
            ((AndroidNavigationService)navigationService).Initialize(this);
        }
    }
}

