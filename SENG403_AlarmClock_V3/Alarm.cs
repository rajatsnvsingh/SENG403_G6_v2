using SENG403_AlarmClock_V3;
using System;
using System.Runtime.Serialization;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace SENG403_AlarmClock_V3
{
    /// <summary>
    /// State of the alarm
    /// IDLE: Alarm has not rung (could be either enabled or disabled
    /// FIRST_TO_GO_OFF: Alarm is the first alarm that rang (out of all the alarms which are currently ringing). 
    /// If multiple alarms go off, the first alarm that rang will be the only alarm that displays a notification window.
    /// SIDE_NOTIFICATION: Alarm rang after some other alarm, which has not been dismissed yet.
    /// </summary>
    public enum AlarmState { IDLE, FIRST_TO_GO_OFF, SIDE_NOTIFICATION }

    /// <summary>
    /// This class encapsulates the logic and data associated with an alarm. 
    /// </summary>
    [DataContract]
    public class Alarm
    {
        /// <summary>
        /// defaultNotificationTime is the default time for which the alarm goes off. This is only used for repeating alarms.
        /// This variable keeps track of when an alarm should go off again in the next cycle (which doesn't depend on
        /// when the alarm is dismissed).
        /// </summary>
        [DataMember]
        public DateTime defaultNotificationTime { get; set; }

        /// <summary>
        /// currentNotificationTime keeps track of the alarm should ring again. This is usually the same as defaultAlarmTime, except
        /// when the user snooze the alarm, in which defaultAlarmTime doesn't change, but notifyAlarmTime := notifyAlarmTime + snoozeTime.
        /// </summary>
        [DataMember]
        public DateTime currentNotificationTime { get; set; } //when the alarm should go off after being snoozed

        [DataMember]
        public double snoozeTime { get; set; }

        [DataMember]
        public bool enabled { get; set; }

        [DataMember]
        public string label { get; set; }

        [DataMember]
        public bool initialized { get; set; }

        [DataMember]
        public AlarmState currentState { get; set;}

        [DataMember]
        public int alarmNotificationDaysMask { get; set; }

        [DataMember]
        public bool oneTimeAlarm { get; set; }

        [DataMember]
        public int alarmToneIndex { get; set; }

        private const string DEFAULT_ALARM_SOUND = "missileAlert.wav";
        
        public MediaPlayer mediaPlayer { get; set; }

        public Alarm(double snoozeTime)
        {
            alarmToneIndex = 0;
            label = "An alarm";
            currentState = AlarmState.IDLE;
            enabled = initialized = false;
            this.snoozeTime = snoozeTime;
        }

        private static string[] alarmSoundList = new string[] { "troll.wav", "missileAlert.wav", "fogHorn.wav" };

        public void playAlarmSound()
        {
            mediaPlayer = new MediaPlayer();
            Uri pathUri = new Uri("ms-appx:///Assets/" + alarmSoundList[alarmToneIndex]);
            mediaPlayer.Source = MediaSource.CreateFromUri(pathUri);
            mediaPlayer.Play();
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
            currentState = AlarmState.IDLE;
            currentNotificationTime = MainPage.currentTime.AddMinutes(AlarmsManager.SNOOZE_TIME);
        }

        /// <summary>
        /// Updates the defaultNotificationTime and currentNotificationTime when an alarm is dismissed.
        /// </summary>
        internal void updateAlarmTime()
        {
            currentState = AlarmState.IDLE;
            if (oneTimeAlarm)
            {
                enabled = false;
            }
            else
            {
                int cur = ((int)MainPage.currentTime.DayOfWeek + 1) % 7;
                while (((1 << cur) & alarmNotificationDaysMask) == 0) cur = (cur + 1) % 7;
                defaultNotificationTime = defaultNotificationTime.AddDays((cur + 7 - (int)MainPage.currentTime.DayOfWeek) % 7);
                currentNotificationTime = defaultNotificationTime;
            }
        }

        internal void setRepeatingNotificationTime(int mask, DateTime alarmTime)
        {
            initialized = true;
            alarmNotificationDaysMask = mask;
            defaultNotificationTime = alarmTime;
            int cur = (int)alarmTime.Date.DayOfWeek;
            while (((1<<cur) & alarmNotificationDaysMask) == 0)
            {
                cur = (cur + 1) % 7;
                defaultNotificationTime = defaultNotificationTime.AddDays(1);
            }
            currentNotificationTime = defaultNotificationTime;
        }

        internal void setOnetimeAlarm(DateTime alarmTime)
        {
            oneTimeAlarm = true;
            initialized = true;
            alarmNotificationDaysMask = 0;
            defaultNotificationTime = currentNotificationTime = alarmTime;
        }
    }
}