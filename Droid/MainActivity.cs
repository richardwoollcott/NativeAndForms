using Android.App;
using Android.OS;
using NativeAndForms.ViewModel;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Helpers;
using NativeAndForms.Droid.Navigation;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;
using Android.Support.V7.App;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using System.Threading.Tasks;

namespace NativeAndForms.Droid
{
    [Activity(Label = "NativeAndForms", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : AppCompatActivity, INavigationView
    {
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

        private MainViewModel Vm
        {
            get
            {
                return App.Locator.Main;
            }
        }

        private Bundle savedInstanceState;

        protected override void OnResume()
        {
            Helper.OnResume(this);
            Helper.ActivityKey = ViewModelLocator.MainPageKey;

            base.OnResume();           
        }

        protected override void OnPostResume()
        {
            base.OnPostResume();

            //InitialiseForms();
        }

        private TextView loginTitle;

        public TextView LoginTitle
        {
            get
            {
                return loginTitle
                    ?? (loginTitle = FindViewById<TextView>(Resource.Id.loginTitle));
            }
        }

        private Button loginButton;

        public Button LoginButton
        {
            get
            {
                return loginButton
                    ?? (loginButton = FindViewById<Button>(Resource.Id.loginButton));
            }
        }

        private void InitialiseForms()
        {
            if (!Xamarin.Forms.Forms.IsInitialized)
            {
                Task.Run(() =>
                {
                    Xamarin.Forms.Forms.Init(this, savedInstanceState);

                    CachedImageRenderer.Init(true);
                    var ignore = typeof(SvgCachedImage);

                    /*
                    RunOnUiThread(() =>
                    {
                        Xamarin.Forms.Forms.Init(this, savedInstanceState);

                        CachedImageRenderer.Init(true);
                        var ignore = typeof(SvgCachedImage);
                    });
                    */
                });
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            this.savedInstanceState = savedInstanceState;

            // set the layout resources first
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.Toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.Tabbar;

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            InitialiseForms();

            // Binding and commanding

            // Binding between the first UILabel and the LoginTitle property on the VM.
            // Keep track of the binding to avoid premature garbage collection
            bindings.Add(
                this.SetBinding(
                    () => Vm.LoginTitle,
                    () => LoginTitle.Text));

            // Actuate the Command on the VM.
            LoginButton.SetCommand(
                "Click",
                Vm.LoginCommand);
            
            //ensure the navigation service is configured
            // by accessing the Vm property which calls the App.Locator
            // TODO refator this initialisation
            var startLocator = Vm;
        }
    }
}

