using System.Windows.Controls;
using TASK1_WPF.ViewModel;
namespace TASK1_WPF.Views
{
    public partial class GroupUsers : UserControl
    {
        public GroupUsers()
        {
            InitializeComponent();
        }

        private void txtFilterGroupUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            listViewGroupUser.Items.Filter = FilterUserMethod;
        }
        public bool FilterUserMethod(object obj)
        {
            var user = obj as GroupUsersViewModelData;
            if (user == null) return false;
            return user.Name.Contains(txtFilterGroupUser.Text, StringComparison.OrdinalIgnoreCase);
        }
    }
}
