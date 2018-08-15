using Android.App;
using Android.OS;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;

using System.Collections.Generic;

using NativeAndForms.Droid.Navigation;
using NativeAndForms.ViewModel;
using Android.Support.V7.App;
using Android.Views;
using NativeAndForms.Navigation;
using GalaSoft.MvvmLight.Ioc;
using Android.Support.Design.Widget;

namespace NativeAndForms.Droid
{
	[Activity (Label = "Native Page")]			
    public class NativeActivity : AppCompatActivity, INavigationView
	{
        private IViewNavigationService navigationService;

        private readonly NavigationHelper navigationHelper = new NavigationHelper();

        // Keep track of bindings to avoid premature garbage collection
        private readonly List<Binding> bindings = new List<Binding>();

        public Android.Support.V7.Widget.Toolbar Toolbar { get; set; }
        public AppBarLayout AppBar { get; set; }

        public NavigationHelper Helper
        {
            get
            {
                return navigationHelper;
            }
        }

        private NativeViewModel Vm
        {
            get
            {
                return App.Locator.Native;
            }
        }

        protected override void OnResume()
        {
            Helper.OnResume(this);

            base.OnResume();
        }

        private TextView nativeTitle;

        public TextView NativeTitle
        {
            get
            {
                return nativeTitle
                    ?? (nativeTitle = FindViewById<TextView>(Resource.Id.nativeTitle));
            }
        }

		private Button backButton;

		public Button BackButton
		{
			get
			{
				return backButton
					?? (backButton = FindViewById<Button>(Resource.Id.backButton));
			}
		}
			
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "Native" layout resource
			SetContentView(Resource.Layout.NativePage);

            Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.nativepagetoolbar);
            if (Toolbar?.Parent != null)
            {
                AppBar = Toolbar?.Parent as AppBarLayout;
                SetSupportActionBar(Toolbar);
            }

            SupportActionBar?.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar?.SetDisplayShowHomeEnabled(true);
            Toolbar?.SetBackgroundColor(Android.Graphics.Color.White);

            navigationService = SimpleIoc.Default.GetInstance<IViewNavigationService>();

            // Binding and commanding

            // Binding between the first UILabel and the NativeTitle property on the VM.
            // Keep track of the binding to avoid premature garbage collection
            bindings.Add(
                this.SetBinding(
                    () => Vm.NativeTitle,
                    () => NativeTitle.Text));


            // Retrieve navigation service
            //var nav = (AndroidNavigationService)SimpleIoc.Default.GetInstance<INavigationService>();
            //BackButton.Click += (s, e) => nav.GoBack();

            // Actuate the Command on the VM.
            BackButton.SetCommand(
                "Click",
                Vm.NavigateBackCommand);
		}
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            navigationService.GoBackAsync();

            return base.OnOptionsItemSelected(item);
        }
    }
}

