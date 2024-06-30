using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TASK1_WPF.BaseConfig;

namespace TASK1_WPF.ViewModel
{
    public class UserControlBarViewModel : ViewModelBase
    {
        public ICommand closeWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public UserControlBarViewModel()
        {
            closeWindowCommand = new ReplayCommands(closeWindow, canExcuteWindow);
            MinimizeWindowCommand = new ReplayCommands(MinimizeWindow, canExcuteWindow);
            MaximizeWindowCommand = new ReplayCommands(MaximizeWindow, canExcuteWindow);
        }

        private void MinimizeWindow(object obj)
        {
            FrameworkElement lastElement = getCurrentElement(obj as UserControl);
            var lastWindow = lastElement as Window;
            if (lastWindow != null)
            {
                if (lastWindow.WindowState != WindowState.Minimized)
                {
                    lastWindow.WindowState = WindowState.Minimized;
                }
                else
                {
                    lastWindow.WindowState = WindowState.Normal;
                }
            }
        }

        private void MaximizeWindow(object obj)
        {
            FrameworkElement lastElement = getCurrentElement(obj as UserControl);
            var lastWindow = lastElement as Window;
            if (lastWindow != null)
            {
                if(lastWindow.WindowState != WindowState.Maximized)
                {
                    lastWindow.WindowState = WindowState.Maximized;
                }
                else
                {
                    lastWindow.WindowState = WindowState.Normal;
                }
            }
        }

        private bool canExcuteWindow(object obj)
        {
            return obj == null ? false : true ;
        }

        private void closeWindow(object obj)
        {
            FrameworkElement lastElement = getCurrentElement(obj as UserControl);
            var lastWindow = lastElement as Window;
            if( lastWindow != null)
            {
                lastWindow.Close();
            }
        }
        public FrameworkElement getCurrentElement(UserControl controlbar)
        {
            FrameworkElement p = controlbar;
            while(p.Parent != null)
            {
                p = p.Parent as FrameworkElement;
            }
            return p;
        }
    }
}
