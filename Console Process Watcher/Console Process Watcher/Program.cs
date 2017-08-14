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
            uint processId = (uint)e.NewEvent.Properties["ProcessID"].Value;
            string processName = (string)e.NewEvent.Properties["ProcessName"].Value;

            ObjectQuery querryTheProcess = new ObjectQuery("Select * From Win32_Process Where ProcessId = " + processId);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(querryTheProcess);
            ManagementObjectCollection allVolumes = searcher.Get();
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Created: ");
            Console.ResetColor();
            if (allVolumes.Count == 0)
            {
                Console.WriteLine("[CREATION] Approximate creation time: {0}, PID: {1}\n[CREATION] {2}, this quick process terminated immediately.", DateTime.Now, processId, processName);
            }
            else
            {
                foreach (ManagementObject oneVolume in allVolumes)
                {
                    DateTime processCreationTimeDateTime = ManagementDateTimeConverter.ToDateTime((string)oneVolume["CreationDate"]);
                    Console.WriteLine("{0}, PID: {1}, threads: {2}\n{3}, path: {4}", processCreationTimeDateTime, processId, oneVolume["ThreadCount"],processName, oneVolume["ExecutablePath"]);
                }
            }

            /*
            ManagementClass c = new ManagementClass("Win32_Process");
            foreach (ManagementObject o in c.GetInstances())
                Console.WriteLine("Next instance of Win32_Process : {0}", o["Name"]);
            */

            /*  ManagementBaseObject mbo = (ManagementBaseObject)e.NewEvent; // Gets the WMI event that was delivered. 

              //Display information from the event
              Console.ForegroundColor = ConsoleColor.Red;
              Console.Write("Created: ");
              Console.ResetColor();
              string processCreationTimeString = (string)((ManagementBaseObject)mbo["TargetInstance"])["CreationDate"];
              DateTime processCreationTimeDateTime = ManagementDateTimeConverter.ToDateTime(processCreationTimeString);
              Console.Write("{0},", processCreationTimeDateTime);
              Console.WriteLine(" PID: {0}", ((ManagementBaseObject)mbo["TargetInstance"])["ProcessId"]);
              Console.WriteLine(
                  "{0}, path: {1}",
                  ((ManagementBaseObject)mbo["TargetInstance"])["Name"],
                  ((ManagementBaseObject)mbo["TargetInstance"])["ExecutablePath"]
                  );*/
        }

        static void HandleEventOnDeletion(object sender, EventArrivedEventArgs e)
        {
            uint processId = (uint)e.NewEvent.Properties["ProcessID"].Value;
            string processName = (string)e.NewEvent.Properties["ProcessName"].Value;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Ended: ");
            Console.ResetColor();

            // Note about Win32_Process termination date: Process was stopped or terminated. To get the termination time, a handle to the process must be held open. Otherwise, this property returns NULL.
            Console.WriteLine("Approximate termination time: {0}, PID: {1}\n{2}", DateTime.Now, processId, processName);

            /*ManagementBaseObject mbo = (ManagementBaseObject)e.NewEvent; // Gets the WMI event that was delivered. 
            
            //Display information from the event
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Ended: ");
            Console.ResetColor();
            // note about Win32_Process termination date: Process was stopped or terminated. To get the termination time, a handle to the process must be held open. Otherwise, this property returns NULL.
            DateTime processTerminationTimeDateTime = DateTime.Now;
            Console.Write("approximate termination date: {0},", processTerminationTimeDateTime);
            Console.WriteLine(" PID: {0}", (uint)e.NewEvent.Properties["ProcessID"].Value);
            Console.WriteLine(
                "{0}, path: {1}",
                ((ManagementBaseObject)mbo["TargetInstance"])["Name"],
                ((ManagementBaseObject)mbo["TargetInstance"])["ExecutablePath"]
                );*/
        }
            
            
        static int Main(string[] args)
        {
            Console.WriteLine("Console process watcher started, refresh rate is within 2 seconds.\n");
            
            // Create event query to be notified within 1 second of a change in a service.
            // WqlEventQuery queryOnCreationEvent = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 2), "TargetInstance isa \"Win32_Process\"");
            // WqlEventQuery queryOnDeletionEvent = new WqlEventQuery("__InstanceDeletionEvent", new TimeSpan(0, 0, 2), "TargetInstance isa \"Win32_Process\"");
            /*
            Console.WriteLine(queryOnCreationEvent.QueryString);
            Console.WriteLine(queryOnDeletionEvent.QueryString);
            */
            // Initialize an event watcher and subscribe to temporary event notifications based on a this event query. 
            ManagementEventWatcher watcher_queryOnCreationEvent = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
            ManagementEventWatcher watcher_queryOnDeletionEvent = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace"));

            // Non blocking way
            watcher_queryOnCreationEvent.EventArrived += new EventArrivedEventHandler(HandleEventOnCreation); // Listener for events, by delegate.
            watcher_queryOnDeletionEvent.EventArrived += new EventArrivedEventHandler(HandleEventOnDeletion);
            watcher_queryOnCreationEvent.Start(); // Start listening.
            watcher_queryOnDeletionEvent.Start();
            Thread.Sleep(Timeout.Infinite); // Do something in the meanwhile.
            watcher_queryOnCreationEvent.Stop(); // Stop listening.
            watcher_queryOnDeletionEvent.Stop();

            Console.WriteLine("\nOut of thread.");

            return 0;
        }
    }
}
