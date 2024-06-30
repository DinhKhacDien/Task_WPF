using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;
using TASK1_WPF.BaseConfig;
using TASK1_WPF.Models;
using TASK1_WPF.Views;

namespace TASK1_WPF.ViewModel
{
    public class UsersViewModel : ViewModelBase
    {
        private readonly DBContext _context;
        private User _selectedItem;

        public User selectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        public ICommand AddUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }
        public ICommand EditUserCommand { get; set; }
        public ICommand SortUserByCreatedDate { get; set; }
        public ICommand SortUserByName { get; set; }
        public ObservableCollection<User> _userList;
        public ObservableCollection<User> userList
        {
            get { return _userList; }
            set { _userList = value; OnPropertyChanged(); }
        }
        public UsersViewModel()
        {
            _context = new DBContext();
            AddUserCommand = new ReplayCommands(AddShowWindow, canAddShowWindow);
            DeleteUserCommand = new ReplayCommands(DeleteUser, canDeleteUser);
            EditUserCommand = new ReplayCommands(EditShowWindow, canDeleteUser);
            SortUserByCreatedDate = new ReplayCommands(SortUser, canSortUser);
            SortUserByName = new ReplayCommands(SorByNametUser, canSortUser);
            LoadUsers();
        }
        
        private void EditShowWindow(object obj)
        {
            if (obj is User selectedUser)
            {
                UpdateUserViewModel upuw = new UpdateUserViewModel(this);
            }
        }

        public void LoadUsers()
        {
            userList = new ObservableCollection<User>(_context.Users.ToList());
        }
        private bool canDeleteUser(object obj)
        {
            return selectedItem != null ? true : false;
        }

        private void DeleteUser(object obj)
        {
            var check = MessageBox.Show($"Are you sure delete {selectedItem.UserName} ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                try
                {
                    _context.Remove(selectedItem);
                    _context.SaveChanges();

                    LoadUsers();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Error when remove user {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                selectedItem = null;
            }
        }

        private bool canAddShowWindow(object obj)
        {
            return true;
        }

        private void AddShowWindow(object obj)
        {
            AddUserViewModal auvm = new AddUserViewModal(this);
        }
        private bool canSortUser(object obj)
        {
            return _context.Users.Count() > 0 ? true : false;
        }

        private void SortUser(object obj)
        {
            userList = new ObservableCollection<User>(_context.Users.OrderByDescending(x => x.NgayTao).ToList());
        }
        private void SorByNametUser(object obj)
        {
            userList = new ObservableCollection<User>(_context.Users.OrderByDescending(x => x.UserName).ToList());
        }

    }

}
