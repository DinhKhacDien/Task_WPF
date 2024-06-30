using System.Windows.Forms;
using System.Windows.Input;
using TASK1_WPF.BaseConfig;
using TASK1_WPF.Models;
using TASK1_WPF.Views;

namespace TASK1_WPF.ViewModel
{
    public class UpdateGroupUserViewModel:ViewModelBase
    {
        private readonly GroupUsersViewModel _gruvmd;
        private readonly UpdateGroupUserWindow _udguw;
        private readonly DBContext _context;
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }
        private byte groupUserID;
        public byte GroupUserID
        {
            get { return groupUserID; }
            set { groupUserID = value; OnPropertyChanged(); }
        }
        public ICommand updateUserToDatabaseCommand { get; set; }
        public UpdateGroupUserViewModel(GroupUsersViewModel gruvmd)
        {
            _gruvmd = gruvmd;
            _udguw = new UpdateGroupUserWindow();
            _context = new DBContext();
            _udguw.DataContext = this;
            GroupUserID = gruvmd.selectedGroupUserItem.GroupUserID;
            UserName = gruvmd.selectedGroupUserItem.Name;
            updateUserToDatabaseCommand = new ReplayCommands(updateGroupUser, canUpdateGroupUser);
            _udguw.Show();
        }

        private bool canUpdateGroupUser(object obj)
        {
            if (string.IsNullOrEmpty(UserName)) return false;
            return true;
        }

        private void updateGroupUser(object obj)
        {
            try
            {
                var currentGroupUserUpdate = _context.GroupUserses.Find(groupUserID);
                if(currentGroupUserUpdate != null)
                {
                    currentGroupUserUpdate.Name = UserName;
                    _context.SaveChanges();
                    MessageBox.Show($"Update group user is successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _udguw.Close();
                    _gruvmd.LoadGroupUsers();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error when update group user {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
