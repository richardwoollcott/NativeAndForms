using Android.App;
using Android.OS;

using Android.Support.Design.Widget;
using Android.Support.V7.App;
using NativeAndForms.Views;
using Xamarin.Forms.Platform.Android;

namespace NativeAndForms.Droid
{
    [Activity(Label = "TabPageActivity")]
    public class TabPageActivity : AppCompatActivity
    {

        BottomNavigationView bottomNavigation;
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.TabPage);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.tabpagetoolbar);
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled(false);
                SupportActionBar.SetHomeButtonEnabled(false);

            }

            bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);


            bottomNavigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;

            LoadFragment(Resource.Id.menu_home);
        }

        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }

        void LoadFragment(int id)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.menu_home:
                    fragment = new TabOnePage().CreateSupportFragment(this);
                    break;
                case Resource.Id.menu_audio:
                    fragment = new TabTwoPage().CreateSupportFragment(this);
                    break;
                case Resource.Id.menu_video:
                    fragment = new TabThreePage().CreateSupportFragment(this);
                    break;
            }
            if (fragment == null)
                return;

            SupportFragmentManager.BeginTransaction()
               .Replace(Resource.Id.tab_content_frame, fragment)
               .Commit();
        }
    }
}
