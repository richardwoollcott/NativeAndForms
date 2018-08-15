using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using GalaSoft.MvvmLight.Ioc;
using NativeAndForms.Droid.Navigation;
using NativeAndForms.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace NativeAndForms.Droid
{
    /*
    [Activity(Label = "XamarinFormsActivity")]
    public class XamarinFormsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
    }
    */


    /// <summary>
    /// Base xamarin forms activity.
    /// This activity contains a single fragment in the layout and renders the fragment pulled from the Xamarin.Forms page
    /// </summary>
    public abstract class XamarinFormsActivity<TPage> : AppCompatActivity, INavigationView
        where TPage : ContentPage, new()
    {
        private IViewNavigationService navigationService;

        private readonly NavigationHelper navigationHelper = new NavigationHelper();

        public NavigationHelper Helper
        {
            get
            {
                return navigationHelper;
            }
        }

        protected readonly TPage _page;
        protected int _containerLayoutId = Resource.Id.activity_container_fragment;
        public Android.Support.V7.Widget.Toolbar Toolbar { get; set; }
        public AppBarLayout AppBar { get; set; }

        public XamarinFormsActivity()
        {
            _page = new TPage();
        }

        /// <summary>
        /// Creates the activity and maps the Xamarin.Forms page to the fragment
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.xamarin_forms_activity);

            Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (Toolbar?.Parent != null)
            {
                AppBar = Toolbar?.Parent as AppBarLayout;
                SetSupportActionBar(Toolbar);
            }

            // register the fragment
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Add(Resource.Id.activity_container_fragment, _page.CreateSupportFragment(this));
            transaction.Commit();
            SupportActionBar?.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar?.SetDisplayShowHomeEnabled(true);
            Toolbar?.SetBackgroundColor(Android.Graphics.Color.White);
            // everything else from this point should be managed by the Xamarin.Forms page behind the fragment

            navigationService = SimpleIoc.Default.GetInstance<IViewNavigationService>();
        }

        protected override void OnResume()
        {
            Helper.OnResume(this);
            //TODO review if we need this
            //Helper.ActivityKey = _page.MainPageKey;

            base.OnResume();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            navigationService.GoBackAsync();

            return base.OnOptionsItemSelected(item);
        }
    }
  
    
}