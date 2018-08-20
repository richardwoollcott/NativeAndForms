using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using NativeAndForms.Views;

namespace NativeAndForms.iOS
{
    public class HomeViewController : XamarinFormsViewController<HomePage>
    {
        public HomeViewController()
        {
        }

        public HomeViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //NavigationItem.Title = "Home page";
        }
    }
}