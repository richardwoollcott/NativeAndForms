using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using NativeAndForms.Views;

namespace NativeAndForms.iOS
{
    public class DashboardViewController : XamarinFormsViewController<DashboardPage>
    {
        public DashboardViewController()
        {
        }

        public DashboardViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.Title = "Dashboard page";
        }
    }
}