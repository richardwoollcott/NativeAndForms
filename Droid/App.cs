using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using NativeAndForms.Droid.Navigation;
using NativeAndForms.Navigation;
using NativeAndForms.ViewModel;
using NativeAndForms.Views;
using Xamarin.Forms;

namespace NativeAndForms.Droid
{
	public static class App
	{
		private static ViewModelLocator locator;

		public static ViewModelLocator Locator
		{
			get
			{
				if (locator == null)
				{
					// Initialize the MVVM Light DispatcherHelper.
					// This needs to be called on the UI thread.
					DispatcherHelper.Initialize();

					// Configure and register the MVVM Light NavigationService
                    var nav = new AndroidNavigationService();
                    SimpleIoc.Default.Register<IViewNavigationService>(() => nav);

                    nav.Configure("Main", typeof(MainActivity));

                    nav.Configure(ViewModelLocator.NativePageKey, typeof(NativeActivity));

                    nav.Configure(ViewModelLocator.HomePageKey, typeof(HomePage));

                    nav.Configure(ViewModelLocator.DashboardPageKey, typeof(DashboardPage));

					locator = new ViewModelLocator();

                    SimpleIoc.Default.Register<ViewModelLocator>(() => locator);
				}

				return locator;
			}
		}
	}
}

