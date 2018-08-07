using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NativeAndForms.Navigation;

namespace NativeAndForms.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        private string dashboardTitle;

        private RelayCommand navigateCommand;

        private readonly IViewNavigationService navigationService;

        public DashboardViewModel(IViewNavigationService navigationService)
        {
            this.navigationService = navigationService;

            DashboardTitle = "Dashboard";
        }

        public string DashboardTitle
        {
            get
            {
                return dashboardTitle;
            }
            set
            {
                Set(ref dashboardTitle, value);
            }
        }

        /// <summary>
        /// Gets the NavigateCommand.
        /// Goes to the native page, using the navigation service.
        /// Use the "mvvmr*" snippet group to create more such commands.
        /// </summary>
        ///
        public RelayCommand NavigateCommand
        {
            get
            {
                return navigateCommand
                    ?? (navigateCommand = new RelayCommand(() => navigationService.NavigateToAsync(
                        ViewModelLocator.NativePageKey)));
            }
        }

    }
}
