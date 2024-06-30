using System.Windows.Input;
using TASK1_WPF.BaseConfig;

namespace TASK1_WPF.ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }


        public ICommand HomeCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand GroupUserCommand { get; set; }


        public void Home(object obj)
        {
            CurrentView = new HomeViewModel();
        }
        private void User(object obj) /*=> CurrentView = new UsersViewModel();*/
        {
            CurrentView = new UsersViewModel();
        }
        public void GroupUsers(object obj)
        {
            CurrentView = new GroupUsersViewModel();
        }
        public bool canExcute(object obj)
        {
            return true;
        }
        public NavigationViewModel()
        {
            HomeCommand = new ReplayCommands(Home, canExcute);
            UserCommand = new ReplayCommands(User, canExcute);
            GroupUserCommand = new ReplayCommands(GroupUsers, canExcute);

            CurrentView = new HomeViewModel();
        }
    }

}
