using Android.App;
using Android.OS;
using Android.Widget;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Helpers;

using System.Collections.Generic;

using NativeAndForms.Droid.Navigation;
using NativeAndForms.ViewModel;
using NativeAndForms.Navigation;

namespace NativeAndForms.Droid
{
	[Activity (Label = "Native Page")]			
    public class NativeActivity : Activity, INavigationView
	{
        /*
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }
        */

        private readonly NavigationHelper navigationHelper = new NavigationHelper();

        // Keep track of bindings to avoid premature garbage collection
        private readonly List<Binding> bindings = new List<Binding>();

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

            //SetResult(Result.Ok);

            // TODO temp
            var navigationService = SimpleIoc.Default.GetInstance<IViewNavigationService>();
            ((AndroidNavigationService)navigationService).AddToStack(this);

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

            // Binding and commanding

            // Binding between the first UILabel and the WelcomeTitle property on the VM.
            // Keep track of the binding to avoid premature garbage collection
            bindings.Add(
                this.SetBinding(
                    () => Vm.NativeTitle,
                    () => NativeTitle.Text));


			// Retrieve navigation service
            //var nav = (AndroidNavigationService)SimpleIoc.Default.GetInstance<INavigationService>();
			//BackButton.Click += (s, e) => nav.GoBack();

            // Actuate the NavigateCommand on the VM.
            BackButton.SetCommand(
                "Click",
                Vm.NavigateBackCommand);
		}
	}
}

