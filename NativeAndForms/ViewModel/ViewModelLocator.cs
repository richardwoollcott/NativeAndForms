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

using GalaSoft.MvvmLight.Ioc;

namespace NativeAndForms.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public const string MainPageKey = "MainPage";

        public const string TabPageKey = "TabPage";

        public const string DashboardPageKey = "DashboardPage";

        public const string HomePageKey = "HomePage";

        public const string NativePageKey = "NativePage";

        public const string TabOnePageKey = "TabOnePage";

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<DashboardViewModel>();
            SimpleIoc.Default.Register<NativeViewModel>();
            SimpleIoc.Default.Register<TabPageViewModel>();
            SimpleIoc.Default.Register<TabOneViewModel>();
            SimpleIoc.Default.Register<TabTwoViewModel>();
            SimpleIoc.Default.Register<TabThreeViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MainViewModel>();
            }
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

        public TabPageViewModel TabPage
        {
            get
            {
                return SimpleIoc.Default.GetInstance<TabPageViewModel>();
            }
        }

        public TabOneViewModel TabOne
        {
            get
            {
                return SimpleIoc.Default.GetInstance<TabOneViewModel>();
            }
        }

        public TabTwoViewModel TabTwo
        {
            get
            {
                return SimpleIoc.Default.GetInstance<TabTwoViewModel>();
            }
        }

        public TabThreeViewModel TabThree
        {
            get
            {
                return SimpleIoc.Default.GetInstance<TabThreeViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}