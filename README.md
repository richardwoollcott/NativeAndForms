NativeAndForms

This repo contains a spike into the use of Xamarin Native Forms. The motiviation being to investigate how Xamarin.Forms pages can be used and with what limitations in a traditional native Xamarin application.

So in the scenario that an app which has been implemented as Xamarin.Forms encounters a limitation(s) of Xamarin.Forms which means switching to a traditional Xamarin native app would make more sense what is invloved.

Another option to consider would be to start off with two Xamarin native apps using Xamarin.Forms pages via the Native.Forms feature in the first place to leverage Xamarin.Forms ease of view re-use where possible and support native views where required.

Things to note:-

The MVVMLight framework is used as it is supported across both Xamarin native and Xamarin.Forms platforms.


The NativeAndForms .Net standard library contains the two Xamarin.Forms pages (DashboardPage.xaml and HomePage.xaml) which are hosted in Native and Xamarin.Forms version of the app on both iOS and Android.

The ViewModels DashboardViewModel and HomeViewModel and the ViewModelLocator are used on all versions of the app without change.

The IViewNavigationService interface is used across all platforms with the following implementations:-

Xamarin.Forms - ViewNavigationService - based on https://mallibone.com/post/a-simple-navigation-service-for-xamarinforms?mode=edit
Android - AndroidNavigationService - based on MVVMLight and modified to handle Activity and Fragments. Further testing required on this
iOS - iOSNavigationService - stock MVVMLight implementation

An important note is that the Xamarin forms pages when hosted in an iOS native app resolve as UIViewControllers and so are equivalent to native pages.

The Xamarin.Forms Android implementation uses a single Android Activity for all pages and as such each Xamarin.Forms ContentPage resolves to an Android Fragment when used in a native app. This presents an issue as native android apps will typically use an Activity for each page rather than a fragment. I've adapted a NavigationService to support this but further deveolpment and testing will be required. Alternatively you could implement native Android pages as Fragments but this will be limiting especially if integrating platfrom activities.

A second approach of wrapping the Xamarin.Forms pages with a UIViewController on iOS and an Activity on Android is presented in these articles by Craig Dunn:- https://alexdunn.org/2018/08/08/xamarin-tip-embed-your-xamarin-forms-pages-in-your-ios-viewcontrollers/ & https://alexdunn.org/2018/07/19/xamarin-tip-embed-your-xamarin-forms-pages-in-your-android-activities/

I've implemented this approach as outlined in the blog posts in a new brnach 'PageWrappers'. This has the advantage that the wrapper provides a place to set properties such as EdgeInsets. This is not much of an advantage on iOS since the construction methods on the AppDelegate could do the same thing in the other approach. On Android it is more fo an advantage since the navigation is simplified in that a page is always an Activity and the previous approach didn't provide an easy place to set properties on the Activity. The original approach is more in keeping with the Xamarin.Forms implementation of a single activity, not sure that this is any particular advantage.

This second approach also uses a native page as the initial page which allows the Xamarin.Forms.Init() to be called after the first page has been displayed to improve startup performance. I've not yet validated how much impact this has.

Using Native Forms does potentially aloow for some more flexibility around navigation transitions, improved Toolbar visuals and more flexibility and potentially better perfromance in some situations.

Summary:-

Xamarin.LiveReload works on the Xamarin.Forms apps only (JustForms.Droid & JustForms.iOS), not the native apps using Native Forms (NativeAndForms.Droid & NativeAndForms.iOS)

Also Xamarin.LiveReload works on the PC Visual Studio 2017 only and is not yet available for the mac.

You can use Merged Resource Dictionaries with Native forms so shared styles can be used.

Placing of styles etc in App.xaml is unsupported on the Xamarin native platfroms so don't use this.

The iOS and Android native applications cannot access the Application.Current.Resources of Xamarin.Forms. This caused a problem when initially trying to use the FFImageLoading library, it could also affect other third part libraries so is something to be aware of as a limitation of this approach.

Custom Renderers work ok using Native Forms with the Renderers in a Shared Project (so they work in both types of app).

The Xamarin.Forms ContentPages and ViewModels can be used with Native forms without modification if a cross platfrom framework is used such as MVVMLight.

The native views (Activity/UIViewController) can also bind to the view models using MVVMLight.

I've implemented the second navigation approach of Page Wrappers with Android AppCompat support, refer to the instructions here https://docs.microsoft.com/en-us/xamarin/xamarin-forms/platform/android/appcompat

A navigation service for iOS/Android/Xamarin.Forms is required using a common interface.

The Fast and Furious Image Loading library was used to load images held in the .Net Standard library containing the Xamarin.Forms Views. The idea being that in order to use the Live Reload feature ony available in a Xamarin.Forms app to keep both Native Forms and Xamarin.Forms versions of the app running in parallel. By default images are kept in the actual app so we investigated avoiding duplication of the images in both versions of the app. Also the use of SVG images on both platforms was investigate using this library, the downside of this is that performance appeared to be a little slower than the use of images. The library provides many features such as placeholder and error images and caching of images to use the same bitmap and other image download features. The library SVG support worked out of the box in the Xamarin.Forms apps but not in the Native Forms apps. This looks to be beacuse of the use of the Xamarin.Forms Application resources which isn't supported in Native Forms. I've implemented a workaround, a proper resolution replacing the use of the Appplication resources could be worked out ok I think if required.

Notes
Comment out the Grid Margin in the DashboardPage.xaml and HomePage.xaml files when running the Xamarin.Forms iOS app (JustForms.iOS) as the behaviour is different between the native and forms platforms with respect to the navigation bar.

The Xamarin.Help blog has an example behaviour which will handle the iPhoneX notch also so we can look at using this as a start point.

Useful links:-

https://blog.xamarin.com/unleashed-embedding-xamarin-forms-xamarin-native/

https://docs.microsoft.com/en-us/xamarin/xamarin-forms/platform/native-forms

https://montemagno.com/embedding-xamarinforms-into-a-xamarin-native-app/







