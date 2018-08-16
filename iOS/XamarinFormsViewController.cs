using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Xamarin.Forms;

namespace NativeAndForms.iOS
{
    /*
    [Register("UniversalView")]
    public class UniversalView : UIView
    {
        public UniversalView()
        {
            Initialize();
        }

        public UniversalView(RectangleF bounds) : base(bounds)
        {
            Initialize();
        }

        void Initialize()
        {
            BackgroundColor = UIColor.Red;
        }
    }

    [Register("XamarinFormsViewController")]
    public class XamarinFormsViewController : UIViewController
    {
        public XamarinFormsViewController()
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            View = new UniversalView();

            base.ViewDidLoad();

            // Perform any additional setup after loading the view
        }
    }
    */

    /// <summary>
    /// Base xamarin forms view controller. Used for embedding a Xamarin.Forms page within a native view controller.
    /// When inheriting from this, be sure to create a ViewController within the storyboard as well so that navigation
    /// can properly work.
    /// </summary>
    public abstract class XamarinFormsViewController<TPage> : UIViewController
        where TPage : ContentPage, new()
    {
        protected TPage _page;

        public XamarinFormsViewController()
        {
        }

        public XamarinFormsViewController(IntPtr handle) : base(handle)
        {
        }

        /// <summary>
        /// Load the Xamarin.Forms Page's ViewController into the parent
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _page = new TPage();

            var xamarinFormsController = _page.CreateViewController();

            AddChildViewController(xamarinFormsController);

            View.AddSubview(xamarinFormsController.View);
                       
            xamarinFormsController.DidMoveToParentViewController(this);

            // add whatever other settings you want - ex:
            EdgesForExtendedLayout = UIKit.UIRectEdge.None;
            ExtendedLayoutIncludesOpaqueBars = false;
            AutomaticallyAdjustsScrollViewInsets = false;

        }
    }
}