// COIS 3320 A1: Program
// Colin A. Marshall(0533528) and Brandon Root(SID)
// The main program that performs the tests, using the algorithms defined in SchedulingAlgorithms

using System;
using System.Collections.Generic;


namespace Scheduler
{
    class Program
    {
        const int NUM_TESTS = 20;   // The number of tests being done
        const int NUM_TASKS = 10;   // The number of jobs being generated for processing 

        static void Main(string[] args)
        {

            // Testing the queue generator, single test
            Queue<Task> taskQ = generateTasks(NUM_TASKS);
            Task[] tasks = taskQ.ToArray();

            for (int i = 0; i < taskQ.Count; i++)
            {
                Console.WriteLine("Task Number: {0}, Run Time: {1}, Arrival Time: {2}", i, tasks[i].RunTime, tasks[i].ArriveTime);
            }

            Console.ReadLine();

        }

        // Generate Tasks
        // Generates a queue of tasks of length numTasks, with run times and arrival times randomly generated
        private static Queue<Task> generateTasks(int numTasks)
        {
            Queue<Task> taskQ = new Queue<Task>();
            Random rando = new Random(); // Initializes random number generator
            int arriveAcc = 0; // Initialize arrive time accumulator

            // Generate each task and add it to the queue
            // The first task arrives at 0, and then every task after that arrives 5-8 units after the previous one
            for(int i = 0; i < numTasks; i++)
            {
                taskQ.Enqueue(new Task(rando.Next(2,60), arriveAcc));           // Queue a new task with a random run time of 2-60 units and a arrival time every 5-8 seconds
                arriveAcc = arriveAcc + rando.Next(5, 8);                       // Tasks arrive every 5-8 units of time after previous task
            }

            return taskQ;

        }
    }
}
