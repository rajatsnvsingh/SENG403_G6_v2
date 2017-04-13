

using System;
using System.Media;
using System.Threading;
using System.Windows.Media;
using System.Windows.Threading;

namespace SENG403_AlarmClock_V2
{
    [Serializable]
    public class Alarm
    {
        private const string defaultSoundFile = @"C:\Users\tcai\Documents\Visual Studio 2015\Projects\SENG403_G6_v2\SENG403_AlarmClock_V2\Sounds\missileAlert.wav";

        //instance variables
        public DateTime defaultAlarmTime { get; set; } //default time (for repeated alarms)

        public DateTime notifyTime { get; set; } //when the alarm should go off after being snoozed
        public double snoozeTime { get; set; } //time for snooze period
        public SoundPlayer alarmSound = new SoundPlayer(defaultSoundFile); //sound for alarm notification
        public bool enabled { get; set; } //enables and disables alarm
        public bool oneTimeAlarm { get; set; }
        public bool firstcreation = false;
        public string label { get; set; }

        public int alarmNotificationDaysMask { get; set; }

        /// <summary>
        /// Alarm class constructor, takes in time for alarm to go off, repeat interval and time for snooze period
        /// </summary>
        /// <param name="alarmTime"></param>
        /// <param name="repeatInterval"></param>
        /// <param name="snoozeTime"></param>
        public Alarm(DateTime alarmTime, double snoozeTime)
        {
            defaultAlarmTime = notifyTime = alarmTime;
            this.snoozeTime = snoozeTime;
            alarmNotificationDaysMask = 0;
            oneTimeAlarm = false;
            
        }

        /// <summary>
        /// Alarm class constructor, takes in path filename for sound file and time for snooze period
        /// </summary>
        /// <param name="alarmFile"></param>
        /// <param name="snoozeTime"></param>
        public Alarm(string alarmFile, double snoozeTime)
        {
            alarmSound.SoundLocation = alarmFile;
            this.snoozeTime = snoozeTime;
            alarmNotificationDaysMask = 0;
            oneTimeAlarm = false;
        }

        /// <summary>
        /// Set custome message for this alarm
        /// </summary>
        /// <param name="label"></param>
        public void SetLabel(string label)
        {
            this.label = label;
        }

        /// <summary>
        /// Gets the custom message for this alarm
        /// </summary>
        public string GetLabel()
        {
            return label;
        }
        public Boolean getStatus()
        {
            return enabled;
        }

        /// <summary>
        /// Stops playing SoundPlayer file for alarm
        /// </summary>
        public void stop()
        {
            alarmSound.Stop();
        }

        internal void setNotificationTime(int mask, DateTime alarmTime)
        {
            if (mask == 0)
                oneTimeAlarm = true;
            else
                alarmNotificationDaysMask = mask;

            defaultAlarmTime = alarmTime;
        }

        /// <summary>
        /// Adds the snooze time to the alarms time to go off
        /// </summary>
        public void snooze()
        {
            notifyTime = MainWindow.currentTime.AddMinutes(snoozeTime);
            stop();
        }

        /// <summary>
        /// Set time for alarm instance
        /// </summary>
        /// <param name="newTime"></param>
        public void SetTime(DateTime newTime)
        {
            defaultAlarmTime = newTime;
        }

        /// <summary>
        /// Set time interval for alarm snooze in minutes
        /// </summary>
        /// <param name="snoozeMinutes"></param>
        public void setSnooze(double snoozeMinutes)
        {
            snoozeTime = snoozeMinutes;
        }

        /// <summary>
        /// Snooze an existing alarm by adding minutes until next alarm time
        /// </summary>
        /// <param name="currentTime"></param>
        public void Snooze(DateTime currentTime)
        {
            defaultAlarmTime = currentTime.AddMinutes(snoozeTime);
        }

        /// <summary>
        /// Sets the sound for the alarm
        /// </summary>
        public void SetSound(String newSound)
        {
            Console.WriteLine(newSound);
            alarmSound.SoundLocation = newSound;
        }

        /// <summary>
        /// Gets a string representation of this alarm
        /// </summary>
        public override string ToString()
        {
            return "Default Alarm Time: " + defaultAlarmTime + " Snooze Time: " + snoozeTime;
        }

        /// <summary>
        /// Resets a daily repeating alarm to go off again after its time has come
        /// </summary>
        public void update()
        {
            
            alarmSound.Stop();
            if (oneTimeAlarm)
            {
                enabled = false;
            }
            else
            {
                int cur = ((int)MainWindow.currentTime.DayOfWeek + 1) % 7;
                while (((1 << cur) & alarmNotificationDaysMask) == 0) cur = (cur + 1) % 7;
                defaultAlarmTime = defaultAlarmTime.AddDays((cur + 7 - (int)MainWindow.currentTime.DayOfWeek) % 7);
                notifyTime = defaultAlarmTime;
            }
        }
    }
}