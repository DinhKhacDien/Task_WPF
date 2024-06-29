using System.Windows;
using System.Windows.Controls;

namespace TASK1_WPF.BaseConfig
{
    public class Btn:RadioButton
    {
         static Btn()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Btn), new FrameworkPropertyMetadata(typeof(Btn)));
        }
    }
}
