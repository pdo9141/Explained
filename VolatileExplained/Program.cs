using System;
using System.Threading;

namespace VolatileExplained
{
    class Program
    {
        //private bool _loop = true;
        private volatile bool _loop = true;

        static void Main(string[] args)
        {
            // Building in Release and Debug mode makes a difference for this example, run in Release mode to show issue without volatile
            // There's main memory that holds value for _loop as true
            // There's also local virtual memory for each thread with value for _loop
            // Changing test1._loop to false updates local virtual thread and main memory only even though you're changing a reference type value
            // How do can you ensure Step 1-3 prints out properly and not just Step 1-2? You use the volatile keyword
            // Using volatile will sync main and all local virtual memory
            // Use volatile when doing multi-threaded applications and intend on updating data concurrently on multiple threads

            Program test1 = new Program();
            Thread obj = new Thread(SomeThread);
            obj.Start(test1);

            Thread.Sleep(20);

            test1._loop = false;
            Console.WriteLine("Step 2 :- The value is set to false");
            Console.ReadLine();
        }

        private static void SomeThread(object o1)
        {
            Program o = (Program)o1;
            Console.WriteLine("Step 1 :- Entered the loop");

            while (o._loop)
            {
            }

            Console.WriteLine("Step 3 :- Exited the loop");
        }
    }
}
