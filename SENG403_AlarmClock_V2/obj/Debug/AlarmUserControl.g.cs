﻿#pragma checksum "..\..\AlarmUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2AD43B9406AA6D15C9F1EB06D2ED0AE6"
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


namespace SENG403_AlarmClock_V2 {
    
    
    /// <summary>
    /// AlarmUserControl
    /// </summary>
    public partial class AlarmUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 73 "..\..\AlarmUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label AlarmType_label;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\AlarmUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label AlarmTime_label;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\AlarmUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditAlarm_Button;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\AlarmUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteAlarm_Button;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\AlarmUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ReminderLabel;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\AlarmUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton EnableAlarmToggleButton;
        
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
            System.Uri resourceLocater = new System.Uri("/SENG403_AlarmClock_V2;component/alarmusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AlarmUserControl.xaml"
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
            this.AlarmType_label = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.AlarmTime_label = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.EditAlarm_Button = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\AlarmUserControl.xaml"
            this.EditAlarm_Button.Click += new System.Windows.RoutedEventHandler(this.EditAlarm_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.DeleteAlarm_Button = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\AlarmUserControl.xaml"
            this.DeleteAlarm_Button.Click += new System.Windows.RoutedEventHandler(this.DeleteAlarm_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ReminderLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.EnableAlarmToggleButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 81 "..\..\AlarmUserControl.xaml"
            this.EnableAlarmToggleButton.Checked += new System.Windows.RoutedEventHandler(this.EnableAlarmToggleButton_Checked);
            
            #line default
            #line hidden
            
            #line 81 "..\..\AlarmUserControl.xaml"
            this.EnableAlarmToggleButton.Unchecked += new System.Windows.RoutedEventHandler(this.EnableAlarmToggleButton_Unchecked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

