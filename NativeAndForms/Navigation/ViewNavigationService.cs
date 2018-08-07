using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NativeAndForms.Navigation
{
    // Based on https://mallibone.com/post/a-simple-navigation-service-for-xamarinforms?mode=edit

    public class ViewNavigationService : IViewNavigationService
    {
        private readonly object sync = new object();
        private readonly Dictionary<string, Type> pagesByKey = new Dictionary<string, Type>();
        private readonly Stack<NavigationPage> navigationPageStack = new Stack<NavigationPage>();
        private NavigationPage CurrentNavigationPage => navigationPageStack.Peek();

        public void Configure(string pageKey, Type pageType)
        {
            lock (sync)
            {
                if (pagesByKey.ContainsKey(pageKey))
                {
                    pagesByKey[pageKey] = pageType;
                }
                else
                {
                    pagesByKey.Add(pageKey, pageType);
                }
            }
        }

        public Page SetRootPage(string rootPageKey)
        {
            var rootPage = GetPage(rootPageKey);
            navigationPageStack.Clear();
            var mainPage = new NavigationPage(rootPage);
            navigationPageStack.Push(mainPage);
            return mainPage;
        }

        public string CurrentPageKey
        {
            get
            {
                lock (sync)
                {
                    if (CurrentNavigationPage?.CurrentPage == null)
                    {
                        return null;
                    }

                    var pageType = CurrentNavigationPage.CurrentPage.GetType();

                    return pagesByKey.ContainsValue(pageType)
                        ? pagesByKey.First(p => p.Value == pageType).Key
                        : null;
                }
            }
        }

        public async Task GoBackAsync()
        {
            var navigationStack = CurrentNavigationPage.Navigation;
            if (navigationStack.NavigationStack.Count > 1)
            {
                await CurrentNavigationPage.PopAsync();
                return;
            }

            if (navigationPageStack.Count > 1)
            {
                navigationPageStack.Pop();
                await CurrentNavigationPage.Navigation.PopModalAsync();
                return;
            }

            await CurrentNavigationPage.PopAsync();
        }

        public async Task NavigateModalAsync(string pageKey, bool animated = true)
        {
            await NavigateModalAsync(pageKey, null, animated);
        }

        public async Task NavigateModalAsync(string pageKey, object parameter, bool animated = true)
        {
            var page = GetPage(pageKey, parameter);
            NavigationPage.SetHasNavigationBar(page, false);
            var modalNavigationPage = new NavigationPage(page);
            await CurrentNavigationPage.Navigation.PushModalAsync(modalNavigationPage, animated);
            navigationPageStack.Push(modalNavigationPage);
        }

        public async Task NavigateToAsync(string pageKey, bool animated = true)
        {
            await NavigateToAsync(pageKey, null, animated);
        }

        public async Task NavigateToAsync(string pageKey, object parameter, bool animated = true)
        {
            var page = GetPage(pageKey, parameter);
            await CurrentNavigationPage.Navigation.PushAsync(page, animated);
        }

        private Page GetPage(string pageKey, object parameter = null)
        {

            lock (sync)
            {
                if (!pagesByKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException(
                        $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?");
                }

                var type = pagesByKey[pageKey];
                ConstructorInfo constructor;
                object[] parameters;

                if (parameter == null)
                {
                    constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault(c => !c.GetParameters().Any());

                    parameters = new object[]
                    {
                    };
                }
                else
                {
                    constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault(
                            c =>
                            {
                                var p = c.GetParameters();
                                return p.Length == 1
                                       && p[0].ParameterType == parameter.GetType();
                            });

                    parameters = new[]
                    {
                    parameter
                };
                }

                if (constructor == null)
                {
                    throw new InvalidOperationException(
                        "No suitable constructor found for page " + pageKey);
                }

                var page = constructor.Invoke(parameters) as Page;
                return page;
            }
        }
    }
}
