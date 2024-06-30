using System.Windows.Forms;
using System.Windows.Input;
using TASK1_WPF.BaseConfig;
using TASK1_WPF.Models;
using TASK1_WPF.Views;

namespace TASK1_WPF.ViewModel
{
    public class AddGroupUserViewModel : ViewModelBase
    {
        private readonly DBContext _context;
        private readonly AddGroupUserWindow adru;
        private readonly GroupUsersViewModel guvmd;

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }
        private string groupUserID;
        public string GroupUserID
        {
            get { return groupUserID; }
            set { groupUserID = value; OnPropertyChanged(); }
        }
        public ICommand addUserToDatabaseCommand { get; set; }
        public AddGroupUserViewModel(GroupUsersViewModel guvmd)
        {
            addUserToDatabaseCommand = new ReplayCommands(addUser, canAddUser);
            _context = new DBContext();
            adru = new AddGroupUserWindow();
            adru.DataContext = this;
            adru.Show();
            this.guvmd = guvmd;
        }
        private bool canAddUser(object obj)
        {
            if (string.IsNullOrEmpty(GroupUserID))
            {
                return false;
            }
            try
            {
                var isNameExist = _context.GroupUserses.Find(byte.Parse(GroupUserID));


                if (string.IsNullOrEmpty(UserName))
                {
                    return false;
                }
                return isNameExist == null ? true : false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void addUser(object obj)
        {
            try
            {
                var newGroupUser = new TASK1_WPF.Models.GroupUsers();

                newGroupUser.GroupUserID = byte.Parse(GroupUserID);
                newGroupUser.Name = UserName;

                _context.GroupUserses.Add(newGroupUser);
                _context.SaveChanges();
                MessageBox.Show($"Add group user is successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                adru.Close();

                guvmd.LoadGroupUsers();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error when add group user {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
