using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using NativeAndForms.Navigation;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace NativeAndForms.Droid.Navigation
{
    /// <summary>
    /// Xamarin Android implementation of <see cref="IViewNavigationService"/>.
    /// This implementation can be used in Xamarin Android applications (not Xamarin Forms).
    /// </summary>
    public class AndroidNavigationService : IViewNavigationService
    {
        public const string MainPageKey = "Main";

        public INavigationView CurrentView
        {
            get { return CrossCurrentActivity.Current.Activity as INavigationView; }
        }

        /// <summary>
        /// The key that is returned by the <see cref="CurrentPageKey"/> property
        /// when the current Activiy is the root activity.
        /// </summary>
        public const string RootPageKey = "-- ROOT --";

        private const string ParameterKeyName = "ParameterKey";

        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private readonly Dictionary<string, object> _parametersByKey = new Dictionary<string, object>();

        private readonly Stack<FragmentOrActivity> navigationStack = new Stack<FragmentOrActivity>();

        /// <summary>
        /// The key corresponding to the currently displayed page.
        /// </summary>
        public string CurrentPageKey
        {
            get
            {
                return CurrentView.Helper.ActivityKey ?? RootPageKey;
            }
        }

        /// <summary>
        /// Adds a key/page pair to the navigation service.
        /// </summary>
        /// <param name="key">The key that will be used later
        /// in the <see cref="NavigateTo(string)"/> or <see cref="NavigateTo(string, object)"/> methods.</param>
        /// <param name="activityType">The type of the activity (page) corresponding to the key.</param>
        public void Configure(string key, Type activityType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(key))
                {
                    _pagesByKey[key] = activityType;
                }
                else
                {
                    _pagesByKey.Add(key, activityType);
                }
            }
        }

        private Activity xamarinFormsHost;

        public void Initialize(Activity startActivity)
        {
            xamarinFormsHost = startActivity;

            navigationStack.Push(new FragmentOrActivity
            {
                IsActivity = true
            });
        }

        /// <summary>
        /// Allows a caller to get the navigation parameter corresponding 
        /// to the Intent parameter.
        /// </summary>
        /// <param name="intent">The <see cref="Android.App.Activity.Intent"/> 
        /// of the navigated page.</param>
        /// <returns>The navigation parameter. If no parameter is found,
        /// returns null.</returns>
        public object GetAndRemoveParameter(Intent intent)
        {
            if (intent == null)
            {
                throw new ArgumentNullException("intent", "This method must be called with a valid Activity intent");
            }

            var key = intent.GetStringExtra(ParameterKeyName);
            intent.RemoveExtra(ParameterKeyName);

            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            lock (_parametersByKey)
            {
                if (_parametersByKey.ContainsKey(key))
                {
                    var param = _parametersByKey[key];
                    _parametersByKey.Remove(key);
                    return param;
                }

                return null;
            }
        }

        /// <summary>
        /// Allows a caller to get the navigation parameter corresponding 
        /// to the Intent parameter.
        /// </summary>
        /// <typeparam name="T">The type of the retrieved parameter.</typeparam>
        /// <param name="intent">The <see cref="Android.App.Activity.Intent"/> 
        /// of the navigated page.</param>
        /// <returns>The navigation parameter casted to the proper type.
        /// If no parameter is found, returns default(T).</returns>
        public T GetAndRemoveParameter<T>(Intent intent)
        {
            return (T)GetAndRemoveParameter(intent);
        }

        /// <summary>
        /// If possible, discards the current page and displays the previous page
        /// on the navigation stack.
        /// </summary>
        public Task GoBackAsync()
        {
            GoBack();

            return Task.CompletedTask;
        }

        private void GoBack()
        {
            var popped = navigationStack.Pop();

            if (popped.IsActivity)
            {
                CurrentView.Helper.GoBack();    
            }
            else
            {
                xamarinFormsHost.FragmentManager.PopBackStack();    
            }
        }

        /// <summary>
        /// Displays a new page corresponding to the given key. 
        /// Make sure to call the <see cref="Configure"/>
        /// method first.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page
        /// that should be displayed.</param>
        /// <exception cref="ArgumentException">When this method is called for 
        /// a key that has not been configured earlier.</exception>
        public Task NavigateToAsync(string pageKey, bool animated = true)
        {
            NavigateTo(pageKey, null);

            return Task.CompletedTask;
        }

        private void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        /// <summary>
        /// Displays a new page corresponding to the given key,
        /// and passes a parameter to the new page.
        /// Make sure to call the <see cref="Configure"/>
        /// method first.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page
        /// that should be displayed.</param>
        /// <param name="parameter">The parameter that should be passed
        /// to the new page.</param>
        /// <exception cref="ArgumentException">When this method is called for 
        /// a key that has not been configured earlier.</exception>
        public Task NavigateToAsync(string pageKey, object parameter, bool animated = true)
        {
            NavigateTo(pageKey, parameter);

            return Task.CompletedTask;
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            lock (_pagesByKey)
            {
                if (!_pagesByKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException(
                        string.Format(
                            "No such page: {0}. Did you forget to call NavigationService.Configure?",
                            pageKey),
                        "pageKey");
                }

                var targetType = _pagesByKey[pageKey];

                if (targetType.IsSubclassOf(typeof(ContentPage)))
                {
                    NavigateToActivity(MainPageKey, null);

                    NavigateToFragment(pageKey, null);
                }
                else
                {
                    NavigateToActivity(pageKey, parameter);
                }
            }
        }

        /// <summary>
        /// Displays a new page corresponding to the given key,
        /// and passes a parameter to the new page.
        /// Make sure to call the <see cref="Configure"/>
        /// method first.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page
        /// that should be displayed.</param>
        /// <param name="parameter">The parameter that should be passed
        /// to the new page.</param>
        /// <exception cref="ArgumentException">When this method is called for 
        /// a key that has not been configured earlier.</exception>
        public void NavigateToActivity(string pageKey, object parameter)
        {
            if (CurrentView.Helper.ActivityKey == pageKey)
            {
                // nothing todo we are aleady on that page
                return;
            }

            if (CurrentView.Helper.CurrentActivity == null)
            {
                throw new InvalidOperationException("No CurrentActivity found");
            }

            lock (_pagesByKey)
            {
                var intent = new Intent(CurrentView.Helper.CurrentActivity, _pagesByKey[pageKey]);

                if (parameter != null)
                {
                    lock (_parametersByKey)
                    {
                        var guid = Guid.NewGuid().ToString();
                        _parametersByKey.Add(guid, parameter);
                        intent.PutExtra(ParameterKeyName, guid);
                    }
                }

                CurrentView.Helper.CurrentActivity.StartActivity(intent);

                navigationStack.Push(new FragmentOrActivity { IsActivity = true });

                CurrentView.Helper.NextPageKey = pageKey;
            }
        }

        public void NavigateToFragment(string pageKey, object parameter)
        {
            if (CurrentView.Helper.CurrentActivity == null)
            {
                throw new InvalidOperationException("No CurrentActivity found");
            }

            lock (_pagesByKey)
            {
                var page = (ContentPage)Activator.CreateInstance(_pagesByKey[pageKey]);
                var targetFragment = page.CreateFragment(xamarinFormsHost);

                xamarinFormsHost.FragmentManager
                           .BeginTransaction()
                           .AddToBackStack(null)
                           .Replace(Resource.Id.fragment_frame_layout, targetFragment)
                           .Commit();

                navigationStack.Push(new FragmentOrActivity { IsActivity = false });
                
                CurrentView.Helper.NextPageKey = pageKey; //TODO check this
            }
        }

        private class FragmentOrActivity
        {
            public bool IsActivity { get; set; }
        }

    }
}
