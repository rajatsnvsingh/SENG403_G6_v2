using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SENG403_AlarmClock_V2
{
    /// <summary>
    /// Interaction logic for NewAlarmWindow.xaml
    /// </summary>
    public partial class NewAlarmWindow : Window
    {
        private List<String> alarmSounds = new List<string>();
        AlarmUserControl alarmControl;

        private Stream fileStream;
        private BinaryFormatter formatter;
        

        public NewAlarmWindow(AlarmUserControl alarmControl)
        {
            InitializeComponent();
            RadioGrid.Visibility = Visibility.Collapsed;
            OtherProps.Margin = new Thickness(26, 149, 21, 34);
            this.alarmControl = alarmControl;
            alarmSounds.Add(@"..\..\Sounds\missileAlert.wav");
            alarmSounds.Add(@"..\..\Sounds\fogHorn.wav");
            AlarmTone_comboBox.ItemsSource = alarmSounds;
            AlarmTone_comboBox.SelectedIndex = 0;
            Alarm_TimePicker.Value = alarmControl.alarm.notifyTime;
            AlarmMessage.Text = alarmControl.alarm.GetLabel();

            formatter = new BinaryFormatter();
            

        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            DateTime alarmTime;
            DateTime.TryParse(Alarm_TimePicker.Text, out alarmTime);
            if ((bool)radioButton_Daily.IsChecked)
                alarmControl.alarm = Alarm.createDailyAlarm(alarmTime, MainWindow.snoozeTime);
            else if ((bool)radioButton_Sun.IsChecked)
            {
                alarmControl.alarm.setWeeklyAlarm(DayOfWeek.Sunday, alarmTime);
                alarmControl.AlarmType_label.Content = "Sunday";
            }

            else if ((bool)radioButton_Sat.IsChecked)
            {
                alarmControl.alarm.setWeeklyAlarm(DayOfWeek.Saturday, alarmTime);
                alarmControl.AlarmType_label.Content = "Saturday";
            }

            else if ((bool)radioButton_Mon.IsChecked)
            {
                alarmControl.alarm.setWeeklyAlarm(DayOfWeek.Monday, alarmTime);
                alarmControl.AlarmType_label.Content = "Monday";
            }

            else if ((bool)radioButton_Tue.IsChecked)
            {
                alarmControl.alarm.setWeeklyAlarm(DayOfWeek.Tuesday, alarmTime);
                alarmControl.AlarmType_label.Content = "Tuesday";
            }

            else if ((bool)radioButton_Wed.IsChecked)
            {
                alarmControl.alarm.setWeeklyAlarm(DayOfWeek.Wednesday, alarmTime);
                alarmControl.AlarmType_label.Content = "Wednesday";
            }

            else if ((bool)radioButton_Thu.IsChecked)
            {
                alarmControl.alarm.setWeeklyAlarm(DayOfWeek.Thursday, alarmTime);
                alarmControl.AlarmType_label.Content = "Thursday";
            }

            else if ((bool)radioButton_Fri.IsChecked)
            {
                alarmControl.alarm.setWeeklyAlarm(DayOfWeek.Friday, alarmTime);
                alarmControl.AlarmType_label.Content = "Friday";
            }
            else
            {
                alarmControl.alarm.notifyTime = alarmTime;
                alarmControl.AlarmType_label.Content = "No Repeat";
            }
            alarmControl.alarm.setSnooze(MainWindow.snoozeTime);


            alarmControl.setTimeLabel(alarmTime);
            alarmControl.alarm.SetSound((String)AlarmTone_comboBox.SelectedValue);
            if (!AlarmMessage.Text.Equals("Set Alarm Label"))
                alarmControl.alarm.SetLabel(AlarmMessage.Text);


            

            //writes new alarm to object file
            if(File.Exists("alarmFile.bin"))
            {
                
                fileStream = new FileStream("alarmFile.bin", FileMode.Append, FileAccess.Write, FileShare.None);
                formatter.Serialize(fileStream, alarmControl.alarm);
                fileStream.Close();

            }
            else
            {
                //fileStream = new FileStream("alarmFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            }





            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void radioButton_Thu_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void repeat_checkBox_Checked(object sender, RoutedEventArgs e)
        {
            RadioGrid.Visibility = Visibility.Visible;
            OtherProps.Margin = new Thickness(26, 230, 21, 34);
        }

        private void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {

            RadioGrid.Visibility = Visibility.Collapsed;
            OtherProps.Margin = new Thickness(26, 149, 21, 34);
        }
        private void Global_Mice(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

    }
}
