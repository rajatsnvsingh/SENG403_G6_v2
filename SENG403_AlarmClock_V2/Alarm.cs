

using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace SENG403_AlarmClock_V2
{
    public class Alarm
    {
        //instance variables
        private DateTime defaultAlarmTime; //default time (for repeated alarms)
        private DateTime notifyTime; //when the alarm should go off after being snoozed
        private double snoozeTime;
        MediaPlayer alarmSound = new MediaPlayer();
        Boolean enabled = false;
        private int repeatIntervalDays = -1; //how many days before alarm goes off
        private string label;

        public static Alarm createDailyAlarm(DateTime alarmTime, double snoozeTime)
        {
            TimeSpan ts = new TimeSpan(alarmTime.Hour, alarmTime.Minute, alarmTime.Second);
            DateTime dt = DateTime.Today.Add(ts);
            Console.WriteLine(dt);
            return new Alarm(dt, 1, snoozeTime);
        }

        public static Alarm createWeeklyAlarm(DayOfWeek day, DateTime alarmTime)
        {
            return null;
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
            alarmSound.Open(new Uri(alarmFile));
            this.snoozeTime = snoozeTime;
        }

        public void SetLabel(string label)
        {
            this.label = label;
        }

        /// <summary>
        /// Plays SoundPlayer file for alarm
        /// </summary>
        public void play()
        {
            if (enabled)
            {
                disable();
                Thread notifyThread = new Thread(alarmNotification);
                notifyThread.SetApartmentState(ApartmentState.STA);
                notifyThread.IsBackground = true;
                notifyThread.Start();
                alarmSound.Play();
            }
        }

        private void alarmNotification()
        {
            new NotificationWindow(this).ShowDialog();
            Dispatcher.Run();
        }

        /// <summary>
        /// Stops playing SoundPlayer file for alarm
        /// </summary>
        public void stop()
        {
            alarmSound.Stop();
        }

        public void snooze()
        {
            notifyTime.AddMinutes(snoozeTime);
            stop();
        }

        /// <summary>
        /// Copy Constructor for Alarm class
        /// </summary>
        /// <param name="newAlarm"></param>
        public Alarm(Alarm newAlarm)
        {
            snoozeTime = newAlarm.GetSnoozeTime();
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

            this.snoozeTime = snoozeMinutes;
        }

        /// <summary>
        /// Snooze an existing alarm by adding minutes until next alarm time
        /// </summary>
        /// <param name="currentTime"></param>
        public void Snooze(DateTime currentTime)
        {


            this.defaultAlarmTime = currentTime.AddMinutes(snoozeTime);

        }

        /// <summary>
        /// Gets the time for the alarm
        /// </summary>
        /// <returns></returns>
        public DateTime GetTime()
        {
            return defaultAlarmTime;
        }

        /// <summary>
        /// Sets the sound for the alarm
        /// </summary>
        /// <returns></returns>
        public void SetSound(String newSound)
        {
            alarmSound.Open(new Uri(newSound));
        }
        /// <summary>
        /// Gets the time for the alarm with added snooze time 
        /// </summary>
        /// <returns></returns>
        public double GetSnoozeTime()
        {
            return snoozeTime;
        }

        public override string ToString()
        {
            return "Default Alarm Time: " + defaultAlarmTime + " Snooze Time: " + snoozeTime +
                " Repeat Interval: " + repeatIntervalDays;
        }

        public void enable()
        {
            enabled = true;
        }

        public void disable()
        {
            enabled = false;
        }

        public void update()
        {
            if (repeatIntervalDays != -1)
            {
                defaultAlarmTime = defaultAlarmTime.AddDays(repeatIntervalDays);
                notifyTime = defaultAlarmTime;
                enable();
            }
            else
            {
                disable();
            }
        }
    }
}