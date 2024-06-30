using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;
using TASK1_WPF.BaseConfig;
using TASK1_WPF.Models;
using TASK1_WPF.Views;

namespace TASK1_WPF.ViewModel
{
    public class ListUserInGroupUserViewModel : ViewModelBase
     {
        private readonly GroupUsersViewModel _guvmd;
        private readonly ListUserInGroupUser _luigu;

        //public void GridUserLoaded()
        //{
        //    _guvmd.LoadGroupUsers();
        //}
        public byte currentGroupUserId { get; set; }
        private readonly DBContext _context;
        private User _selectedItem;

        public User selectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        public ICommand AddUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; } // last fix
        public ICommand EditUserCommand { get; set; } // last fix
        public ObservableCollection<User> _userList;
        public ObservableCollection<User> userList
        {
            get { return _userList; }
            set { _userList = value; OnPropertyChanged(); }
        }
        public ListUserInGroupUserViewModel(GroupUsersViewModel guvmd)
        {
            _context = new DBContext();
            _luigu = new ListUserInGroupUser();
            _luigu.DataContext = this;
            AddUserCommand = new ReplayCommands(AddShowWindow, canAddShowWindow);
            DeleteUserCommand = new ReplayCommands(DeleteUser, canDeleteUser);
            EditUserCommand = new ReplayCommands(EditShowWindow, canDeleteUser);
            _guvmd = guvmd;
            currentGroupUserId = guvmd.selectedGroupUserItem.GroupUserID;
            LoadUsers();

            _luigu.Show();
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
            try
            {
                var data = _context.Users.Where(x => x.GroupUserID == _guvmd.selectedGroupUserItem.GroupUserID).ToList();
                userList = new ObservableCollection<User>(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool canDeleteUser(object obj)
        {
            return selectedItem != null;
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
    }
}
