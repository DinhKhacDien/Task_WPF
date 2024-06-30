using System.Windows;
using System.Windows.Controls;
using TASK1_WPF.Models;

namespace TASK1_WPF.Views
{

    public partial class ListUserInGroupUser : Window
    {
        public ListUserInGroupUser()
        {
            InitializeComponent();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            listView.Items.Filter = FilterUserMethod;
        }
        public bool FilterUserMethod(object obj)
        {
            var user = obj as User;
            return user.UserName.Contains(txtFilter.Text, StringComparison.OrdinalIgnoreCase);
        }
    }
}
