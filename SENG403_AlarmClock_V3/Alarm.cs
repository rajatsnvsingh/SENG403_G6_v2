using System;
using System.Runtime.Serialization;

namespace SENG403_AlarmClock_V2
{
    [DataContract]
    public class Alarm
    {
        private const string defaultSoundFile = @"C:\Users\tcai\Documents\Visual Studio 2015\Projects\SENG403_G6_v2\SENG403_AlarmClock_V2\Sounds\missileAlert.wav";

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
            this.snoozeTime = snoozeTime;
        }

        /// <summary>
        /// Copy Constructor for Alarm class
        /// </summary>
        /// <param name="newAlarm"></param>
        public Alarm(Alarm newAlarm)
        {
            snoozeTime = newAlarm.snoozeTime;
        }

        /// <summary>
        /// Snooze an existing alarm by adding minutes until next alarm time
        /// </summary>
        /// <param name="currentTime"></param>
        public void Snooze(DateTime currentTime)
        {
            defaultAlarmTime = currentTime.AddMinutes(snoozeTime);
        }
    }
}