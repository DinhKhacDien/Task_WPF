using System.Windows.Controls;
using TASK1_WPF.ViewModel;

namespace TASK1_WPF.UserControlManager
{
    /// <summary>
    /// Interaction logic for UserControlBar.xaml
    /// </summary>
    public partial class UserControlBar : UserControl
    {
        public UserControlBarViewModel UserControlBarVM { get; set; }
        public UserControlBar()
        {
            InitializeComponent();
            this.DataContext = UserControlBarVM = new UserControlBarViewModel();
        }
    }
}
