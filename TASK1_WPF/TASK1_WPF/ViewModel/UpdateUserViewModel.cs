using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;
using TASK1_WPF.BaseConfig;
using TASK1_WPF.Models;
using TASK1_WPF.Views;

namespace TASK1_WPF.ViewModel
{
    public class UpdateUserViewModel : ViewModelBase
    {
        private readonly DBContext _context;
        public readonly UsersViewModel _uvm;
        private UpdateUserWindow upuw;
        private Guid userId;

        public User selectedUser { get; set; }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged(); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged(); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(); }
        }

        public ICommand updateUserToDatabaseCommand { get; set; }

        public User currentUser { get; set; }
        public UpdateUserViewModel(UsersViewModel uvm)
        {
            currentUser = uvm.selectedItem;
            _context = new DBContext();
            _uvm = uvm;
            upuw = new UpdateUserWindow();
            if (currentUser != null)
            {
                userId = currentUser.UserID;
                UserName = currentUser.UserName ?? "";
                Password = currentUser.Password ?? "";
                FirstName = currentUser.FirstName ?? "";
                LastName = currentUser.LastName ?? "";
                Address = currentUser.Address ?? "";
            }
            updateUserToDatabaseCommand = new ReplayCommands(updateUser, canUpdateUser);
            upuw.DataContext = this;
            upuw.Show();
        }
        private bool canUpdateUser(object obj)
        {
            if (string.IsNullOrWhiteSpace(UserName) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(Address))
            {
                return false;
            }

            return true;
        }

        private void updateUser(object obj)
        {
            try
            {
                var userCurrent = _context.Users.Find(userId);
                if (userCurrent != null)
                {
                    userCurrent.Password = Password;
                    userCurrent.UserName = UserName;
                    userCurrent.Address = Address;
                    userCurrent.LastName = LastName;
                    userCurrent.FirstName = FirstName;

                    _context.SaveChanges();
                    MessageBox.Show($"Update user is successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    upuw.Close();
                    //_uvm.LoadUsers();
                    _uvm.userList = new ObservableCollection<User>(_context.Users.ToList());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error when update user {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

