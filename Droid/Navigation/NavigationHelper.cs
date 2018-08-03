using Android.App;

namespace NativeAndForms.Droid.Navigation
{
    public class NavigationHelper
    {
        private Activity currentActivity;

        public Activity CurrentActivity
        {
            get { return currentActivity; }
            set { currentActivity = value; }
        }

        //public Activity CurrentActivity { get; set; }

        public string ActivityKey { get; set; }

        public string NextPageKey { get; set; }

        public void GoBack()
        {
            if (CurrentActivity != null)
            {
                CurrentActivity.Finish();

                //CurrentActivity.OnBackPressed();
            }
        }

        public void OnResume(Activity view)
        {
            CurrentActivity = view;

            if (string.IsNullOrEmpty(ActivityKey))
            {
                ActivityKey = NextPageKey;
                NextPageKey = null;
            }
        }
    }
}
