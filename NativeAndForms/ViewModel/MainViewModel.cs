using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NativeAndForms.Navigation;

namespace NativeAndForms.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string loginTitle;

        private RelayCommand loginCommand;

        private readonly IViewNavigationService navigationService;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IViewNavigationService navigationService)
        {
            this.navigationService = navigationService;

            LoginTitle = "Please enter your login details.";
        }

        public string LoginTitle
        {
            get
            {
                return loginTitle;
            }
            set
            {
                Set(ref loginTitle, value);
            }
        }
       
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand
                    ?? (loginCommand = new RelayCommand(() => navigationService.NavigateToAsync(
                        ViewModelLocator.HomePageKey)));
            }
        }
    }
}
