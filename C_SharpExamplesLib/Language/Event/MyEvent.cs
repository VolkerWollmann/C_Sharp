namespace C_SharpExamplesLib.Language.Event
{
    public abstract class MyEvent
    {
        #region ActionDelegate
        // #action
        private class Alarm
        {
            // #delegate #event
            // Delegate for the alarm event
            // public Action OnAlarmRaised { get; set; } may work as well
            public event Action OnAlarmRaised = delegate { };
            public Action? OnAlarmRaised2 { get; set; } = delegate { };

            // Called to raise an alarm
            public void RaiseAlarm()
            {
	            // Only raise the alarm if someone has
                // subscribed. 
                OnAlarmRaised();
                OnAlarmRaised2?.Invoke();
            }
        }

        private abstract class AlarmUser
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

                // does not compile, can only participate,
                // but does not disturb others
                //alarm.OnAlarmRaised = AlarmListener1;

                alarm.OnAlarmRaised2 = AlarmListener1;
                alarm.OnAlarmRaised2 += AlarmListener2;

                alarm.RaiseAlarm();

                //remove listener 1
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
            AlarmUser.DoIt();
        }

        #endregion
    }
}
