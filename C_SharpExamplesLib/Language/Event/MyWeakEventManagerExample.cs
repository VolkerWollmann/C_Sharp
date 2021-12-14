using System;
using System.Diagnostics.Tracing;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language.Event
{
    // #pattern #weak event pattern
    // #WeakEventManager
    // https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/weak-event-patterns?view=netframeworkdesktop-4.8
    public class MyWeakEventManagerExample
    {
        private class AlarmEventArgs : EventArgs
        {
            public readonly string Message;

            public AlarmEventArgs(string message)
            {
                Message = message;
            }
        }

        private class AlarmSource : EventSource
        {
            public EventHandler<AlarmEventArgs> OnAlarmRaised;

            public void RaiseAlarm()
            {
                if (OnAlarmRaised != null)
                {
                    OnAlarmRaised(this, new AlarmEventArgs( "Alarm was raised."));
                }
            }
        }

        private class AlarmConsumer
        {
            private readonly int _id;
            public void Listener(object sender, AlarmEventArgs alarmEventArgs)
            {
                Console.WriteLine($"Alarm listener {_id} received {alarmEventArgs.Message}");
            }

            public AlarmConsumer(int id)
            {
                _id = id;
            }
        }

        private class AlarmEventWeakEventManager : WeakEventManager
        {

            private AlarmEventWeakEventManager()
            {
            }

            /// <summary>
            /// Add a handler for the given source's event.
            /// </summary>
            public static void AddHandler(EventSource source,
                                          EventHandler<AlarmEventArgs> handler)
            {
                if (source == null)
                    throw new ArgumentNullException(nameof(source));
                if (handler == null)
                    throw new ArgumentNullException(nameof(handler));

                CurrentManager.ProtectedAddHandler(source, handler);
            }

            /// <summary>
            /// Remove a handler for the given source's event.
            /// </summary>
            public static void RemoveHandler(EventSource source,
                                             EventHandler<AlarmEventArgs> handler)
            {
                if (source == null)
                    throw new ArgumentNullException(nameof(source));
                if (handler == null)
                    throw new ArgumentNullException(nameof(handler));

                CurrentManager.ProtectedRemoveHandler(source, handler);
            }

            /// <summary>
            /// Get the event manager for the current thread.
            /// </summary>
            private static AlarmEventWeakEventManager CurrentManager
            {
                get
                {
                    Type managerType = typeof(AlarmEventWeakEventManager);
                    AlarmEventWeakEventManager manager =
                        (AlarmEventWeakEventManager)GetCurrentManager(managerType);

                    // at first use, create and register a new manager
                    if (manager == null)
                    {
                        manager = new AlarmEventWeakEventManager();
                        SetCurrentManager(managerType, manager);
                    }

                    return manager;
                }
            }

            /// <summary>
            /// Return a new list to hold listeners to the event.
            /// </summary>
            protected override ListenerList NewListenerList()
            {
                return new ListenerList<AlarmEventArgs>();
            }

            /// <summary>
            /// Listen to the given source for the event.
            /// </summary>
            protected override void StartListening(object source)
            {
                AlarmSource typedSource = (AlarmSource)source;
                typedSource.OnAlarmRaised += OnAlarmEvent;
            }

            /// <summary>
            /// Stop listening to the given source for the event.
            /// </summary>
            protected override void StopListening(object source)
            {
                AlarmSource typedSource = (AlarmSource)source;
                typedSource.OnAlarmRaised -= OnAlarmEvent;
            }

            /// <summary>
            /// Event handler for the SomeEvent event.
            /// </summary>
            void OnAlarmEvent(object sender, AlarmEventArgs e)
            {
                DeliverEvent(sender, e);
            }
        }

        public static void Test()
        {
            AlarmSource alarmSource = new AlarmSource();
            AlarmConsumer alarmConsumer1 = new AlarmConsumer(1);
            AlarmConsumer alarmConsumer2 = new AlarmConsumer(2);

            AlarmEventWeakEventManager.AddHandler(alarmSource, alarmConsumer1.Listener);
            AlarmEventWeakEventManager.AddHandler(alarmSource, alarmConsumer2.Listener);
            alarmSource.RaiseAlarm();

            // the weak connection: the alarm consumer can be deleted
            // and discarded by GC without removing the listener form the source.
            // Alarm source still stays alive.
            alarmConsumer1 = null;
            Assert.IsNull(alarmConsumer1);
            GC.Collect();

            alarmSource.RaiseAlarm();

            AlarmEventWeakEventManager.RemoveHandler(alarmSource, alarmConsumer2.Listener);

        }
    }
}
