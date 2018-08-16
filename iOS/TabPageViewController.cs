using Foundation;
using NativeAndForms.ViewModel;
using System;
using UIKit;

namespace NativeAndForms.iOS
{
    public partial class TabPageViewController : UITabBarController
    {
        private TabPageViewModel Vm
        {
            get
            {
                return Application.Locator.TabPage;
            }
        }

        public TabPageViewController (IntPtr handle) : base (handle)
        {
            var vm = Vm;
        }

        public override void ViewDidLoad()
        {
            var image = UIImage.FromBundle("ic_dashboard_background");

            View.BackgroundColor = UIColor.FromPatternImage(image);

            base.ViewDidLoad();
        }
    }
}