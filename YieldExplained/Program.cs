using System;
using System.Collections.Generic;

namespace YieldExplained
{
    class Program
    {
        private static List<int> _myList = new List<int>();

        private static void FillValues()
        {
            _myList.Add(1);
            _myList.Add(2);
            _myList.Add(3);
            _myList.Add(4);
            _myList.Add(5);
        }

        static void Main(string[] args)
        {
            // Yield keyword helps you do custom stateful iteration over a collection
            FillValues();
            //ExecuteCustomIteration();
            //ExecuteCustomIterationWithFilter();
            ExecuteCustomIterationWithYield();
            Console.WriteLine("");
            ExecuteStatefulIteration();

            Console.ReadLine();
        }

        private static void ExecuteCustomIteration()
        {            
            foreach (int i in _myList)
                Console.WriteLine(i);
        }

        private static void ExecuteCustomIterationWithFilter()
        {
            // Now you only want to print values greater than 3
            // Using Filter() forces you to use a new temporary list
            foreach (int i in Filter())
                Console.WriteLine(i);
        }

        private static void ExecuteCustomIterationWithYield()
        {
            // If you leverage Yield, you remove the need for a temporary list
            // You basically yield control back to the caller then back into the filter with state of iteration persisted 
            // Performance boost!
            foreach (int i in FilterYield())
                Console.WriteLine(i);
        }

        private static IEnumerable<int> Filter()
        {
            List<int> temp = new List<int>();
            foreach (int i in _myList)
                if (i > 3)
                    temp.Add(i);
        
            return temp;
        }

        private static IEnumerable<int> FilterYield()
        {
            foreach (int i in _myList)
                if (i > 3)
                    yield return i;
        }

        private static void ExecuteStatefulIteration()
        {
            // If you leverage Yield you will perserve the value of runningTotal in RunningTotal()!
            // Performance boost!
            foreach (int i in RunningTotal())
                Console.WriteLine(i);
        }

        private static IEnumerable<int> RunningTotal()
        {
            int runningTotal = 0;
            foreach (int i in _myList)
            {
                runningTotal += i;
                yield return (runningTotal);
            }
        }
    }
}
