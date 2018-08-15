using Foundation;
using GalaSoft.MvvmLight.Helpers;
using System;
using System.Collections.Generic;
using UIKit;
using NativeAndForms.ViewModel;

namespace NativeAndForms.iOS
{
    public partial class MainPageViewController : UIViewController
    {
        // Keep track of bindings to avoid premature garbage collection
        private readonly List<Binding> bindings = new List<Binding>();

        private MainViewModel Vm
        {
            get
            {
                return Application.Locator.Main;
            }
        }

        public MainPageViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Binding and commanding

            // Binding between the first UILabel and the LoginTitle property on the VM.
            // Keep track of the binding to avoid premature garbage collection
            bindings.Add(
                this.SetBinding(
                    () => Vm.LoginTitle,
                    () => LoginTitle.Text));

            // Actuate the NavigateCommand on the VM.
            LoginButton.SetCommand(
                "TouchUpInside",
                Vm.LoginCommand);
        }
    }
}