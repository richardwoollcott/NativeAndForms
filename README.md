NativeAndForms

This repo contains a spike into the use of Xamarin Native Forms. The motiviation being to investigate how Xamarin.Forms pages can be used and with what limitations in a traditional native xamarin application. So in the scenario that an app which has been implemented as Xamarin.Forms bumps against a limitation of Xamarin.Forms which requires switching to a xamarin native app what is involved.

Another consideration would be to start off with two Xamarin native apps using Xamarin.Forms pages in the first place to leverage Xamarin.Forms ease of view re-use where possible and support native views where required.

Things to note:-

The MVVMLight framework is used as it is supported across both Xamarin native and Xamarin.Forms platforms.

Comment out the Grid Margin in the DashboardPage.xaml and HomePage.xaml files when running the Xamarin.Forms iOS app (JustForms.iOS) as the behaviour is different between the native and forms platforms with respect to the navigation bar.

The NativeAndForms .Net standard library contains the two Xamarin.Forms pages (DashboardPage.xaml and HomePage.xaml) which are hosted in Native and Xamarin.Froms version of the app on both iOS and Android.

The ViewModels DashboardViewModel and HomeViewModel and the ViewModelLocator are used on all versions of the app without change.

The IViewNavigationService interface is used across all platforms with the following implementations:-

Xamarin.Forms - ViewNavigationService
Android - AndroidNavigationService - further work maybe required on this one
iOS - iOSNavigationService

Note the iOS and Android native applications cannot access the Application.Current.Resources

An important note is that the Xamarin forms pages when hosted in an iOS native app resolve as UIViewControllers and so are equivelent to native pages.

The Xamarin.Forms Android implementation uses a single Android Activity for all pages and as such each Xamarin.Forms ContentPage resolves to an Android Fragment when used in a native app. This presents an issue as native android apps will typically use an Activity for each page rather than a fragment. I've adapted a NavigationService to support this but further deveolpment and testing will be required. Alternatively you could implement native Android pages as Fragments but this will be limiting especially if integrating platfrom activities.

Summary:-

Xamarin.LiveReload works on the Xamarin.Forms apps only (JustForms.Droid & JustForms.iOS), not the native apps using Native Forms (NativeAndForms.Droid & NativeAndForms.iOS)

Also Xamarin.LiveReload works on the PC Visual Studio 2017 only and is not yet available for the mac.

You can use Merged Resource Dictionaries with Native forms but not the App.xaml so don't use this.

Custom Renderers work ok using Native Forms with the Renderers in a Shared Project (so they work in both types of app).

The Xamarin.Forms ContentPages and ViewModels can be used with Native forms without modification if a cross platfrom framework is used such as MVVMLight.

The native views (Activity/UIViewController) can also bind to the view models using MVVMLight.

A navigation service for iOS/Android/Xamarin.Forms is required using a common interface.






