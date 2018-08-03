using Foundation;
using UIKit;

using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;

using NativeAndForms.ViewModel;
using NativeAndForms.iOS.Navigation;
using Xamarin.Forms;
using NativeAndForms.Views;
using NativeAndForms.Navigation;

namespace NativeAndForms.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        public static AppDelegate Instance;

        //UIWindow _window;
        UINavigationController _navigation;


        public override UIWindow Window
        {
            get;
            set;
        }

        UIViewController CreateHome(object parameter)
        {
            var homePage = new HomePage().CreateViewController();
            homePage.Title = "Home";

            return homePage;
        }

        UIViewController CreateDashboard(object parameter)
        {
            var dashboardPage = new DashboardPage().CreateViewController();
            dashboardPage.Title = "Dashboard";

            return dashboardPage;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method

            Forms.Init();
             
            /*
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
            {
                TextColor = UIColor.Black
            });
            */

            SimpleIoc.Default.Register<ViewModelLocator>(() => Application.Locator);

            // Configure and register the MVVM Light NavigationService
            var nav = new iOSNavigationService();
            SimpleIoc.Default.Register<IViewNavigationService>(() => nav);

            var mainPage = CreateHome(null);
            mainPage.Title = "Native Forms";

            _navigation = Window.RootViewController as UINavigationController;


            //_navigation = new UINavigationController(mainPage);
            //UIStoryboard Storyboard = UIStoryboard.FromName("Main", null);

            //_navigation.Storyboard = Storyboard;
            Window.RootViewController = _navigation;
            Window.MakeKeyAndVisible();

            // MVVM Light's DispatcherHelper for cross-thread handling.
            DispatcherHelper.Initialize(application);

            nav.Initialize(_navigation);
            //nav.Initialize((UINavigationController)Window.RootViewController);
            nav.Configure(ViewModelLocator.NativePageKey, "NativePage");
            //nav.Configure(ViewModelLocator.SecondPageKey, typeof(SecondViewController));
                          
            nav.Configure(ViewModelLocator.HomePageKey, CreateHome);
            nav.Configure(ViewModelLocator.DashboardPageKey, CreateDashboard);

            nav.NavigateToAsync(ViewModelLocator.HomePageKey);
                             
            return true;
        }
       
        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}

