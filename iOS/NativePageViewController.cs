using Foundation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Helpers;

using NativeAndForms.ViewModel;

using System;
using UIKit;

namespace NativeAndForms.iOS
{
    public partial class NativePageViewController : UIViewController
    {
        // Keep track of bindings to avoid premature garbage collection
        private readonly List<Binding> bindings = new List<Binding>();

        private NativeViewModel Vm
        {
            get
            {
                return Application.Locator.Native;
            }
        }

        public NativePageViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            /*
            BackButton.TouchUpInside += (s, e) =>
            {
                var nav = SimpleIoc.Default.GetInstance<IViewNavigationService>();
                nav.GoBack();
            };
            */

            // Binding and commanding

            // Binding between the first UILabel and the WelcomeTitle property on the VM.
            // Keep track of the binding to avoid premature garbage collection
            bindings.Add(
                this.SetBinding(
                    () => Vm.NativeTitle,
                    () => NativeTitle.Text));

            // Actuate the NavigateCommand on the VM.
            BackButton.SetCommand(
                "TouchUpInside",
                Vm.NavigateBackCommand);
        }
    }
}