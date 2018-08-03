using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NativeAndForms.Navigation;

namespace NativeAndForms.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class HomeViewModel : ViewModelBase
    {
        private string welcomeTitle;

        private RelayCommand navigateCommand;

        private readonly IViewNavigationService navigationService;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public HomeViewModel(IViewNavigationService navigationService)
        {
            this.navigationService = navigationService;

            WelcomeTitle = "Home Page";
        }

        public string WelcomeTitle
        {
            get
            {
                return welcomeTitle;
            }
            set
            {
                Set(ref welcomeTitle, value);
            }
        }

        /// <summary>
        /// Gets the NavigateCommand.
        /// Goes to the second page, using the navigation service.
        /// Use the "mvvmr*" snippet group to create more such commands.
        /// </summary>
        ///

		public RelayCommand NavigateCommand
		{
			get
			{
				return navigateCommand
					?? (navigateCommand = new RelayCommand(() => navigationService.NavigateToAsync(
                        ViewModelLocator.DashboardPageKey)));
			}
		}

    }
}