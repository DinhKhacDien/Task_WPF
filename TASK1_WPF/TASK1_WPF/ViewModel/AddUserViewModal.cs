using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;
using TASK1_WPF.BaseConfig;
using TASK1_WPF.Models;
using TASK1_WPF.Views;

namespace TASK1_WPF.ViewModel
{
    public class AddUserViewModal : ViewModelBase
    {
        private readonly DBContext _context;
        private readonly AddUserWindow wd;
        public readonly UsersViewModel _uvm;
        public ICommand addUserToDatabaseCommand { get; set; }
        public string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }
        public string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged(); }
        }
        public string lastName;
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
        public AddUserViewModal(UsersViewModel uvm)
        {
            addUserToDatabaseCommand = new ReplayCommands(addUser, canAddUser);
            _context = new DBContext();
            wd = new AddUserWindow();
            wd.DataContext = this;
            _uvm = uvm;
            wd.Show();
        }

        private bool canAddUser(object obj)
        {
            var isNameExist = _context.Users.FirstOrDefault(x => x.UserName.Equals(UserName));
            if (string.IsNullOrEmpty(lastName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(FirstName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {

            }
            if (string.IsNullOrEmpty(UserName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(Address))
            {
                return false;
            }
            return isNameExist == null ? true : false;
        }

        private void addUser(object obj)
        {
            try
            {
                var newUer = new User();
                newUer.UserID = new Guid();
                newUer.Password = Password;
                newUer.UserName = UserName;
                newUer.Address = Address;
                newUer.LastName = LastName;
                newUer.FirstName = FirstName;
                newUer.GroupUserID = 0;

                _context.Add(newUer);
                _context.SaveChanges();
                MessageBox.Show($"Add user is successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                wd.Close();

                _uvm.LoadUsers();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error when add user {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
