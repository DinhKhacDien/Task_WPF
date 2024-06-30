using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using TASK1_WPF.BaseConfig;
using TASK1_WPF.Models;
using TASK1_WPF.ViewModel;

namespace TASK1_WPF.Views
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
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
            //nv =
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
