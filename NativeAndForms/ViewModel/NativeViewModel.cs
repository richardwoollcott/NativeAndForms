using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using NativeAndForms.Navigation;

namespace NativeAndForms.ViewModel
{
    public class NativeViewModel : ViewModelBase
    {
        private readonly IViewNavigationService navigationService;

        private string nativeTitle;


        private RelayCommand navigateBackCommand;

        public NativeViewModel()
        {
            navigationService = SimpleIoc.Default.GetInstance<IViewNavigationService>();

            NativeTitle = "A Native Page";
        }

        public string NativeTitle
        {
            get
            {
                return nativeTitle;
            }
            set
            {
                Set(ref nativeTitle, value);
            }
        }

        public RelayCommand NavigateBackCommand
        {
            get
            {
                return navigateBackCommand
                    ?? (navigateBackCommand = new RelayCommand(() => navigationService.GoBackAsync()));
            }
        }
    }
}
