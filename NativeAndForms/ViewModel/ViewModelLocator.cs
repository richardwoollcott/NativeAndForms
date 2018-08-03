/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:BasicNavigation.iOS"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
//using Microsoft.Practices.ServiceLocation;

namespace NativeAndForms.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
		/// <summary>
		/// The key used by the NavigationService to go to the second page.
		/// </summary>
		public const string DashboardPageKey = "DashboardPage";

        public const string HomePageKey = "HomePage";

        public const string NativePageKey = "NativePage";

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            //ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<DashboardViewModel>();
            SimpleIoc.Default.Register<NativeViewModel>();
        }

        public HomeViewModel Home
        {
            get
            {
                return SimpleIoc.Default.GetInstance<HomeViewModel>();
            }
        }

        public DashboardViewModel Dashboard
        {
            get
            {
                return SimpleIoc.Default.GetInstance<DashboardViewModel>();
            }
        }

        public NativeViewModel Native
        {
            get
            {
                return SimpleIoc.Default.GetInstance<NativeViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}