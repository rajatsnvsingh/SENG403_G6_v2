using SENG403_AlarmClock_V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENG403_AlarmClock_V3
{
    class AlarmsManager
    {
        private List<Alarm> alarms;
        private static AlarmsManager alarmsManager = null;

        private AlarmsManager()
        {
            alarms = new List<Alarm>();
        }

        public static AlarmsManager getAlarmsManager()
        {
            if (alarmsManager == null)
                alarmsManager = new AlarmsManager();
            return alarmsManager;
        }

        public void addAlarm(Alarm alarm)
        {

        }

        public void removeAlarm(Alarm alarm)
        {
            foreach (Alarm a in alarms)
                if (a.Equals(alarm))
                {
                    alarms.Remove(a);
                    return;
                }
            throw new ArgumentException();
        }
    }
}
