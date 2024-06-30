using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;
using TASK1_WPF.BaseConfig;
using TASK1_WPF.Models;
using TASK1_WPF.Views;
using GroupUsers = TASK1_WPF.Models.GroupUsers;

namespace TASK1_WPF.ViewModel
{
    public class AddUserViewModal : ViewModelBase
    {
        private readonly DBContext _context;
        private readonly AddUserWindow wd;
        public readonly UsersViewModel _uvm;
        public readonly ListUserInGroupUserViewModel _luigvm;
        public ICommand addUserToDatabaseCommand { get; set; }
        public string userName;

        private ObservableCollection<GroupUsers> groupUserItems;
        public ObservableCollection<GroupUsers> GroupUserItems
        {
            get { return groupUserItems; }
            set { groupUserItems = value; OnPropertyChanged(); }
        }

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
        private GroupUsers _selectedGroupUser;

        public GroupUsers selectedGroupUser
        {
            get { return _selectedGroupUser; }
            set { _selectedGroupUser = value; OnPropertyChanged(); }
        }
        public AddUserViewModal(object vm)
        {
            try
            {
                addUserToDatabaseCommand = new ReplayCommands(addUser, canAddUser);
                _context = new DBContext();
                wd = new AddUserWindow();
                wd.DataContext = this;
                _uvm = (UsersViewModel)vm;
                LoadGroupUser();
                wd.Show();
            }
            catch
            {
                _context = new DBContext();
                _luigvm = (ListUserInGroupUserViewModel)vm;
                wd = new AddUserWindow();
                wd.DataContext = this;
                LoadGroupUser();
                wd.Show();
            }
        }

        public void LoadGroupUser()
        {
            GroupUserItems = new ObservableCollection<GroupUsers>(_context.GroupUserses);
            if (_uvm != null)
            {
                if (GroupUserItems.Count() > 0)
                {
                    selectedGroupUser = GroupUserItems.First();
                }
            }
            else
            {
                GroupUserItems = new ObservableCollection<GroupUsers>(_context.GroupUserses.Where(x =>x.GroupUserID ==_luigvm.currentGroupUserId));
                selectedGroupUser = GroupUserItems.First();
            }

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
                if (_uvm != null)
                {
                    var newUer = new User();
                    newUer.UserID = new Guid();
                    newUer.Password = Password;
                    newUer.UserName = UserName;
                    newUer.Address = Address;
                    newUer.LastName = LastName;
                    newUer.FirstName = FirstName;
                    newUer.GroupUserID = selectedGroupUser.GroupUserID;

                    _context.Users.Add(newUer);

                }
                else
                {
                    var newUer = new User();
                    newUer.UserID = new Guid();
                    newUer.Password = Password;
                    newUer.UserName = UserName;
                    newUer.Address = Address;
                    newUer.LastName = LastName;
                    newUer.FirstName = FirstName;
                    newUer.GroupUserID = _luigvm.currentGroupUserId;

                    _context.Users.Add(newUer);
                }
                _context.SaveChanges();
                MessageBox.Show($"Add user is successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                wd.Close();
                if (_uvm != null)
                    _uvm.LoadUsers();
                else
                {
                    _luigvm.LoadUsers();
                    //_luigvm.GridUserLoaded(); // fix to update total user in list user in group user
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error when add user {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
