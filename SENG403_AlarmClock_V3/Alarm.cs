using SENG403_AlarmClock_V3;
using System;
using System.Runtime.Serialization;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace SENG403_AlarmClock_V3
{
    public enum AlarmState { NONE, FIRST_TO_GO_OFF, SIDE_NOTIFICATION }

    [DataContract]
    public class Alarm
    {
        private const string DEFAULT_ALARM_SOUND = @"C:\Users\tcai\Documents\Visual Studio 2015\Projects\SENG403_G6_v2\SENG403_AlarmClock_V2\Sounds\missileAlert.wav";
        [DataMember]
        public MediaPlayer mediaPlayer { get; set; }
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
        [DataMember]
        public bool initialized { get; set; }
        [DataMember]
        public AlarmState currentState { get; set;} //whether this is the first alarm that went off (needed to handle multiple alarm gracefully)

        internal void setWeeklyAlarm(DayOfWeek day, TimeSpan ts)
        {
            initialized = true;
            repeatIntervalDays = 7;
            defaultAlarmTime = DateTime.Today.AddDays(day - DateTime.Now.DayOfWeek).Add(ts);
            if (defaultAlarmTime.CompareTo(DateTime.Now) <= 0)
                defaultAlarmTime.AddDays(repeatIntervalDays);
            notifyTime = defaultAlarmTime;
        }

        internal void setDailyAlarm(TimeSpan ts)
        {
            initialized = true;
            repeatIntervalDays = 1;
            defaultAlarmTime = DateTime.Today.Add(ts);
            if (defaultAlarmTime.CompareTo(DateTime.Now) <= 0)
                defaultAlarmTime.AddDays(1);
            notifyTime = defaultAlarmTime;
        }

        internal void setOneTimeAlarm(DateTime dateTime)
        {
            initialized = true;
            repeatIntervalDays = -1;
            notifyTime = defaultAlarmTime = dateTime;
        }

        /// <summary>
        /// Alarm class constructor, takes in path filename for sound file
        /// </summary>
        /// <param name="alarmFile"></param>
        public Alarm(string alarmFile, double snoozeTime)
        {
            currentState = AlarmState.NONE;
            enabled = initialized = false;
            this.snoozeTime = snoozeTime;
            mediaPlayer = new MediaPlayer();
            Uri pathUri = new Uri("ms-appx:///Assets/missileAlert.wav");
            mediaPlayer.Source = MediaSource.CreateFromUri(pathUri);
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
            mediaPlayer.Pause();
            currentState = AlarmState.NONE;
            notifyTime = MainPage.currentTime.AddMinutes(snoozeTime);
        }

        internal void updateAlarmTime()
        {
            mediaPlayer.Pause();
            currentState = AlarmState.NONE;
            if (repeatIntervalDays != -1)
            {
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