using System;
using GalaSoft.MvvmLight;

namespace NativeAndForms.ViewModel
{   
    public class TabThreeViewModel : ViewModelBase
    {
        private string tabTitle;

        public TabThreeViewModel()
        {
            TabTitle = "Third tab";
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
