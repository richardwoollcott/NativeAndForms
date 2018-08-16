using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Ioc;
using NativeAndForms.ViewModel;
using Xamarin.Forms;

namespace NativeAndForms.Views
{
    public partial class TabOnePage : ContentPage
    {
        private TabOneViewModel ViewModel;

        public TabOnePage()
        {
            InitializeComponent();

            BindingContext = ViewModel = SimpleIoc.Default.GetInstance<ViewModelLocator>().TabOne;
        }
    }
}
