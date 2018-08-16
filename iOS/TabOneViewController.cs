using NativeAndForms.Views;
using System;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace NativeAndForms.iOS
{
    public partial class TabOneViewController : XamarinFormsViewController<TabOnePage> //UIKit.UIViewController
    {
        public TabOneViewController()
        {            
        }

        public TabOneViewController (IntPtr handle) : base (handle)
        {
        }

        /*
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIKit.UIColor.Green;

            var page = new TabOnePage().CreateViewController();
            page.View.BackgroundColor = UIKit.UIColor.Red;

            foreach (var subView in page.View.Subviews)
            {
                var subViewcolor = subView.BackgroundColor;
                subView.BackgroundColor = UIColor.Clear;
            }

            AddChildViewController(page);
            View.AddSubview(page.View);

            //page.DidMoveToParentViewController(this);
        }
        */
    }
}