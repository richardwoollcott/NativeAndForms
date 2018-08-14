
using Android.App;
using Android.OS;
using NativeAndForms.Views;

namespace NativeAndForms.Droid
{
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : XamarinFormsActivity<DashboardPage>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SupportActionBar.Title = "Dashboard Page";
        }
    }
}