using System;
using GalaSoft.MvvmLight;

namespace NativeAndForms.ViewModel
{
    public class TabOneViewModel : ViewModelBase
    {
        private string tabTitle;

        public TabOneViewModel()
        {
            TabTitle = "First tab";
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
