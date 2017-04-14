using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENG403_AlarmClock_V3
{
    class AlarmsManager
    {
        /// <summary>
        /// Global snooze time for all alarms
        /// </summary>
        public static double SNOOZE_TIME = 1.0;

        /// <summary>
        /// Whether the alarm notification is opened or not (required for handling multiple alarms).
        /// </summary>
        public static bool IS_ALARM_NOTIFICATION_OPEN = false;
    }
}
