
using Android.App;
using Android.OS;
using NativeAndForms.Views;

namespace NativeAndForms.Droid
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : XamarinFormsActivity<HomePage>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SupportActionBar.Title = "Home Page";
        }
    }
}