using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Ioc;
using NativeAndForms.ViewModel;
using Xamarin.Forms;

namespace NativeAndForms.Views
{
    public partial class TabTwoPage : ContentPage
    {
        private TabTwoViewModel ViewModel;

        public TabTwoPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = SimpleIoc.Default.GetInstance<ViewModelLocator>().TabTwo;
        }
    }
}
