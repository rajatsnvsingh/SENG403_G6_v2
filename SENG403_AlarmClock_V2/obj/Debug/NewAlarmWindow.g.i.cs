﻿#pragma checksum "..\..\NewAlarmWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F39ABC808F7271878F5D5D629188041D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SENG403_AlarmClock_V2;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace SENG403_AlarmClock_V2 {
    
    
    /// <summary>
    /// NewAlarmWindow
    /// </summary>
    public partial class NewAlarmWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Title_Label;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid RadioGrid;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckBox_Sun;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckBox_Mon;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckBox_Tue;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckBox_Wed;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckBox_Thu;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckBox_Fri;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckBox_Sat;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckBox_Daily;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox repeat_checkBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid OtherProps;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Done;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Cancel;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox AlarmTone_comboBox;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AlarmMessage;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Title_Label_Copy;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Title_Label_Copy1;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.TimePicker Alarm_TimePicker;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\NewAlarmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker Alarm_DatePicker;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SENG403_AlarmClock_V2;component/newalarmwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NewAlarmWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 12 "..\..\NewAlarmWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Global_Mice);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Title_Label = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.RadioGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.CheckBox_Sun = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 5:
            this.CheckBox_Mon = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.CheckBox_Tue = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 7:
            this.CheckBox_Wed = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 8:
            this.CheckBox_Thu = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 9:
            this.CheckBox_Fri = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 10:
            this.CheckBox_Sat = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 11:
            this.CheckBox_Daily = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 12:
            this.repeat_checkBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 27 "..\..\NewAlarmWindow.xaml"
            this.repeat_checkBox.Checked += new System.Windows.RoutedEventHandler(this.repeat_checkBox_Checked);
            
            #line default
            #line hidden
            
            #line 27 "..\..\NewAlarmWindow.xaml"
            this.repeat_checkBox.Unchecked += new System.Windows.RoutedEventHandler(this.Checkbox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 13:
            this.OtherProps = ((System.Windows.Controls.Grid)(target));
            return;
            case 14:
            this.Done = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\NewAlarmWindow.xaml"
            this.Done.Click += new System.Windows.RoutedEventHandler(this.Done_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.Cancel = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\NewAlarmWindow.xaml"
            this.Cancel.Click += new System.Windows.RoutedEventHandler(this.Cancel_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.AlarmTone_comboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 17:
            this.AlarmMessage = ((System.Windows.Controls.TextBox)(target));
            return;
            case 18:
            this.Title_Label_Copy = ((System.Windows.Controls.Label)(target));
            return;
            case 19:
            this.Title_Label_Copy1 = ((System.Windows.Controls.Label)(target));
            return;
            case 20:
            this.Alarm_TimePicker = ((Xceed.Wpf.Toolkit.TimePicker)(target));
            return;
            case 21:
            this.Alarm_DatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

