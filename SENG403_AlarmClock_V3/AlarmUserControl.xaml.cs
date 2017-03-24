using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SENG403_AlarmClock_V3
{
    public sealed partial class AlarmUserControl : UserControl
    {
        private StackPanel _parent;

        public AlarmUserControl(StackPanel parent)
        {
            this.InitializeComponent();
            _parent = parent;
        }

        private void EnableDisableAlarm_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditAlarm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteAlarm_Click(object sender, RoutedEventArgs e)
        {
            foreach(AlarmUserControl alarm in _parent.Children)
            {
                if(this.Equals(alarm))
                {
                    _parent.Children.Remove(this);
                    break;
                }
            }
        }
    }
}
