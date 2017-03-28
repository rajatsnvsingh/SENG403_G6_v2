using SENG403_AlarmClock_V3;
using System;
using System.Runtime.Serialization;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace SENG403_AlarmClock_V2
{
    [DataContract]
    public class Alarm
    {
        private const string DEFAULT_ALARM_SOUND = @"C:\Users\tcai\Documents\Visual Studio 2015\Projects\SENG403_G6_v2\SENG403_AlarmClock_V2\Sounds\missileAlert.wav";

        private MediaElement alarmSound;
        //instance variables
        [DataMember]
        public DateTime defaultAlarmTime { get; set; } //default time (for repeated alarms)
        [DataMember]
        public DateTime notifyTime { get; set; } //when the alarm should go off after being snoozed
        [DataMember]
        public double snoozeTime { get; set; }
        [DataMember]
        public bool enabled { get; set; }
        [DataMember]
        public int repeatIntervalDays { get; set; } //how many days before alarm goes off
        [DataMember]
        public string label { get; set; }

        internal void setWeeklyAlarm(DayOfWeek day, TimeSpan ts)
        {
            enabled = true;
            repeatIntervalDays = 7;
            defaultAlarmTime = DateTime.Today.AddDays(day - DateTime.Now.DayOfWeek).Add(ts);
            if (defaultAlarmTime.CompareTo(DateTime.Now) <= 0)
                defaultAlarmTime.AddDays(repeatIntervalDays);
            notifyTime = defaultAlarmTime;
        }

        internal void setDailyAlarm(TimeSpan ts)
        {
            enabled = true;
            repeatIntervalDays = 1;
            defaultAlarmTime = DateTime.Today.Add(ts);
            if (defaultAlarmTime.CompareTo(DateTime.Now) <= 0)
                defaultAlarmTime.AddDays(1);
            notifyTime = defaultAlarmTime;
        }

        internal void setOneTimeAlarm(DateTime dateTime)
        {
            enabled = true;
            repeatIntervalDays = -1;
            notifyTime = defaultAlarmTime = dateTime;
        }

        public Alarm(DateTime alarmTime, int repeatInterval, double snoozeTime)
        {
            defaultAlarmTime = notifyTime = alarmTime;
            this.repeatIntervalDays = repeatInterval;
            this.snoozeTime = snoozeTime;
        }

        /// <summary>
        /// Alarm class constructor, takes in path filename for sound file
        /// </summary>
        /// <param name="alarmFile"></param>
        public Alarm(string alarmFile, double snoozeTime)
        {
            enabled = false;
            this.snoozeTime = snoozeTime;
            setUpSound();
        }

        public async void setUpSound()
        {
            alarmSound = new MediaElement();
            StorageFolder folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            folder = await folder.GetFolderAsync("Assets");
            StorageFile sf = await folder.GetFileAsync("missileAlert.wav");
            //alarmSound.Source = new Uri("///Assets/missileAlert.wav");
        }

        /// <summary>
        /// Copy Constructor for Alarm class
        /// </summary>
        /// <param name="newAlarm"></param>
        public Alarm(Alarm newAlarm)
        {
            snoozeTime = newAlarm.snoozeTime;
        }

        public void snooze()
        {
            notifyTime = MainPage.currentTime.AddMinutes(snoozeTime);
            enabled = true;
        }

        internal void play()
        {
            alarmSound.Volume = 10;
        }

        public void update()
        {
            alarmSound.Volume = 0;
            if (repeatIntervalDays != -1)
            {
                enabled = true;
                defaultAlarmTime = defaultAlarmTime.AddDays(repeatIntervalDays);
                notifyTime = defaultAlarmTime;
            }
            else
            {
                enabled = false;
            }
        }
    }
}