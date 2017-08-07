// WMI == Windows Management Instrumentation
// WQL == Windows Management Instrumentation Query Language

using System;
using System.Management;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace Console_Process_Watcher
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("Console process watcher started.");
            
            // Create event query to be notified within 1 second of a change in a service
            WqlEventQuery query = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");
           
            Console.WriteLine(query.QueryString);

            // Initialize an event watcher and subscribe to temporary event notifications based on a this event query. 
            ManagementEventWatcher watcher = new ManagementEventWatcher();
            watcher.Query = query;

            // times out watcher.WaitForNextEvent in 5 seconds
            //watcher.Options.Timeout = new TimeSpan(0, 0, 50);

            // Block until the next event occurs 
            // Note: this can be done in a loop if waiting for more than one occurrence
            ManagementBaseObject mbo;
            while (true)
            {
                mbo = watcher.WaitForNextEvent();

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
            
            //Cancel the subscription
            watcher.Stop();

            Console.ReadLine();
            return 0;
        }
    }
}
