using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using TASK1_WPF.Models;

namespace TASK1_WPF.Views
{

    public partial class Users : UserControl
    {
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value;}
        }
        public Users()
        {
            InitializeComponent();
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
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
