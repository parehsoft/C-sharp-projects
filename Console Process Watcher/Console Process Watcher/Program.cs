// WMI == Windows Management Instrumentation
// WQL == Windows Management Instrumentation Query Language
// CIM == Common Information Model - A model used to describe how to represent real-world managed objects.

using System;
using System.Management;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace Console_Process_Watcher
{
    class Program
    {
        static void HandleEventOnCreation(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject mbo = (ManagementBaseObject)e.NewEvent; // Gets the WMI event that was delivered. 

            //Display information from the event
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Created: ");
            Console.ResetColor();
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(
                "{0}, path: {1}",
                ((ManagementBaseObject)mbo["TargetInstance"])["Name"],
                ((ManagementBaseObject)mbo["TargetInstance"])["ExecutablePath"]
                );
        }

        static void HandleEventOnDeletion(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject mbo = (ManagementBaseObject)e.NewEvent; // Gets the WMI event that was delivered. 

            //Display information from the event
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Ended: ");
            Console.ResetColor();
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(
                "{0}, path: {1}",
                ((ManagementBaseObject)mbo["TargetInstance"])["Name"],
                ((ManagementBaseObject)mbo["TargetInstance"])["ExecutablePath"]
                );
        }

        static int Main(string[] args)
        {
            Console.WriteLine("Console process watcher started.");
            
            // Create event query to be notified within 1 second of a change in a service.
            WqlEventQuery queryOnCreationEvent = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");
            WqlEventQuery queryOnDeletionEvent = new WqlEventQuery("__InstanceDeletionEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");

            Console.WriteLine(queryOnCreationEvent.QueryString);
            Console.WriteLine(queryOnDeletionEvent.QueryString);

            // Initialize an event watcher and subscribe to temporary event notifications based on a this event query. 
            ManagementEventWatcher watcher_queryOnCreationEvent = new ManagementEventWatcher(queryOnCreationEvent);
            ManagementEventWatcher watcher_queryOnDeletionEvent = new ManagementEventWatcher(queryOnDeletionEvent);

            // Non blocking way
            watcher_queryOnCreationEvent.EventArrived += new EventArrivedEventHandler(HandleEventOnCreation); // Listener for events, by delegate.
            watcher_queryOnDeletionEvent.EventArrived += new EventArrivedEventHandler(HandleEventOnDeletion);
            watcher_queryOnCreationEvent.Start(); // Start listening.
            watcher_queryOnDeletionEvent.Start();
            Thread.Sleep(99999); // Do something in the meanwhile.
            watcher_queryOnCreationEvent.Stop(); // Stop listening.
            watcher_queryOnDeletionEvent.Stop();
            

            Console.ReadLine();
            for (int i = 0; i < 100000000; i++)
                for (int j = 0; j < 100000000; i++)
                    Console.WriteLine("Exitting!!");

            return 0;
        }
    }
}
