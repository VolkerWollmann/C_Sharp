using System;

namespace C_Sharp
{
    public class MyEvent
    {
        #region ActionDelegate
        // #action
        protected class Alarm
        {
            // #delegate
            // Delegate for the alarm event
            // public Action OnAlarmRaised { get; set; } may work as well
            public event Action OnAlarmRaised = delegate { };
            public Action OnAlarmRaised2 { get; set; }

            // Called to raise an alarm
            public void RaiseAlarm()
            {
                // Only raise the alarm if someone has
                // subscribed. 
                if (OnAlarmRaised != null)
                {
                    OnAlarmRaised();
                    OnAlarmRaised2();
                }
            }
        }

        protected class AlaramUser
        {
            //#delegate
            // Method that must run when the alarm is raised
            static void AlarmListener1()
            {
                Console.WriteLine("Alarm listener 1 ");
            }

            //#delegate
            // Method that must run when the alarm is raised
            static void AlarmListener2()
            {
                Console.WriteLine("Alarm listener 2 ");
            }

            internal static void DoIt()
            {
                // Create a new alarm
                Alarm alarm = new Alarm();

                // Connect the two listener methods
                Console.WriteLine("Add two listeners");
                alarm.OnAlarmRaised += AlarmListener1;
                alarm.OnAlarmRaised += AlarmListener2;

                alarm.OnAlarmRaised2 += AlarmListener1;
                alarm.OnAlarmRaised2 += AlarmListener2;

                alarm.RaiseAlarm();

                //reomove listener 1
                Console.WriteLine("Remove one listener");
                alarm.OnAlarmRaised -= AlarmListener1;
                alarm.OnAlarmRaised2 -= AlarmListener1;

                alarm.RaiseAlarm();
                
                Console.WriteLine("Alarm raised");
            }
        }
       
        // #action #delegate
        public static void ActionDelegate()
        {
            AlaramUser.DoIt();
        }

        #endregion
    }
}
