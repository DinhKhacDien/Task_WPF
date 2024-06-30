using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;
using TASK1_WPF.BaseConfig;
using TASK1_WPF.Models;
using TASK1_WPF.Views;

namespace TASK1_WPF.ViewModel
{
    public class GroupUsersViewModelData
    {
        public byte GroupUserID { get; set; }
        public string Name { get; set; }
        public DateTime NgayTao { get; set; }
        public int TotalUsers { get; set; }
    }
    public class GroupUsersViewModel : ViewModelBase
    {
        private readonly DBContext _context;
        private ObservableCollection<GroupUsersViewModelData> _groupUsersList;

        public ICommand AddGroupUserCommand { get; set; }
        public ICommand EditGroupUserCommand { get; set; }
        public ICommand DeleteGroupUserCommand { get; set; }
        private GroupUsersViewModelData _selectedGroupUserItem;

        public GroupUsersViewModelData selectedGroupUserItem
        {
            get { return _selectedGroupUserItem; }
            set { _selectedGroupUserItem = value; OnPropertyChanged(); }
        }

        public ObservableCollection<GroupUsersViewModelData> groupUsersList
        {
            get { return _groupUsersList; }
            set { _groupUsersList = value; OnPropertyChanged(); }
        }
        public GroupUsersViewModel()
        {
            _context = new DBContext();
            TASK1_WPF.Views.GroupUsers gpu = new TASK1_WPF.Views.GroupUsers();
            gpu.DataContext = this;
            AddGroupUserCommand = new ReplayCommands(showUserControlAddGroup, canAddGroupUser);
            EditGroupUserCommand = new ReplayCommands(showUserControlEditGroup, canEditGroupUser);
            DeleteGroupUserCommand = new ReplayCommands(deleteUserControlAddGroup, canDeleteGroupUser);
            LoadGroupUsers();
        }

        public void LoadGroupUsers()
        {
            var data = from gru in _context.GroupUserses
                       join user in _context.Users
                       on gru.GroupUserID equals user.GroupUserID into userGroup
                       from user in userGroup.DefaultIfEmpty()
                       group user by new { gru.GroupUserID, gru.Name, gru.NgayTao } into g
                       select new GroupUsersViewModelData
                       {
                           GroupUserID = g.Key.GroupUserID,
                           Name = g.Key.Name,
                           NgayTao = g.Key.NgayTao,
                           TotalUsers = g.Count(x => x != null),
                       };

            groupUsersList = new ObservableCollection<GroupUsersViewModelData>(data.ToList());
        }
        private bool canAddGroupUser(object obj)
        {
            return true;
        }
        private void showUserControlAddGroup(object obj)
        {
            AddGroupUserViewModel showAddGroupUserWindow = new AddGroupUserViewModel(this);
        }
        private bool canDeleteGroupUser(object obj)
        {
            if (selectedGroupUserItem == null || selectedGroupUserItem.TotalUsers > 0) return false;
            return true;

        }
        private void deleteUserControlAddGroup(object obj)
        {
            var check = MessageBox.Show($"Are you sure delete {selectedGroupUserItem.Name} ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                try
                {
                    var currentGroupUser = new TASK1_WPF.Models.GroupUsers();
                    currentGroupUser.GroupUserID = selectedGroupUserItem.GroupUserID;
                    _context.GroupUserses.Remove(currentGroupUser);
                    _context.SaveChanges();

                    LoadGroupUsers();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Error when remove user {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                selectedGroupUserItem = null;
            }
        }

        private void showUserControlEditGroup(object obj)
        {
            if (obj is GroupUsersViewModelData selectedUser)
            {
                UpdateGroupUserViewModel upuw = new UpdateGroupUserViewModel(this);
            }
        }


        private bool canEditGroupUser(object obj)
        {
            return selectedGroupUserItem == null ? false : true;
        }

    }
}
