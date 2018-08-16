using System;
using GalaSoft.MvvmLight;

namespace NativeAndForms.ViewModel
{
    public class TabTwoViewModel : ViewModelBase
    {
        private string tabTitle;

        public TabTwoViewModel()
        {
            TabTitle = "Second tab";
        }

        public string TabTitle
        {
            get
            {
                return tabTitle;
            }
            set
            {
                Set(ref tabTitle, value);
            }
        }
    }
}
