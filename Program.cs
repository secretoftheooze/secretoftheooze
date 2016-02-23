// COIS 3320 A1: Program
// Colin A. Marshall(0533528) and Brandon Root(0564499)
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
            // Initialize task queue and algorithms
            Queue<Task> taskQ = new Queue<Task>();
            //SJF sjf = new SJF();
            FIFO fifo = new FIFO();

            // Proceed to loop through all of the tests
            for (int i = 1; i <= NUM_TESTS; i++)
            {
                // Generate the tasks
                taskQ = generateTasks(10, i);
                //taskQ = testTasks();// TESTING
                
                /*
                // Testing the queue generator (No issues, being used to compare to algorithm output now)
                Task[] tasks = taskQ.ToArray();

                for (int n = 0; n < taskQ.Count; n++)
                {
                    Console.WriteLine("Task Number: {0}, Run Time: {1}, Arrival Time: {2}", n, tasks[n].RunTime, tasks[n].ArriveTime);
                }
                */

                // Perform the tests with each of the algorithms
               // sjf.ProcessTasks(i, taskQ);
                fifo.ProcessTasks(i, taskQ);
                // Other algorithms
            }

            // Output results for each algorithm
            //sjf.OutputResults();
            fifo.OutputResults();

            Console.ReadLine();

        }

        // Generate Tasks
        // Generates a queue of tasks of length numTasks, with run times and arrival times randomly generated
        private static Queue<Task> generateTasks(int numTasks, int rSeed)
        {
            Queue<Task> taskQ = new Queue<Task>();
            Random rando = new Random((int) DateTime.Now.Ticks * rSeed); // Initializes random number generator
            int arriveAcc = 0; // Initialize arrive time accumulator

            // Generate each task and add it to the queue
            // The first task arrives at 0, and then every task after that arrives 5-8 units after the previous one
            for(int i = 0; i < numTasks; i++)
            {
                taskQ.Enqueue(new Task(rando.Next(2,60), arriveAcc));           // Queue a new task with a random run time of 2-60 units and a arrival time every 5-8 seconds
                arriveAcc = arriveAcc + rando.Next(5, 8);                       // Tasks arrive every 5-8 units of time after previous task

                // This will be where the Stride-specific values are set

            }

            return taskQ;
        }



        // TEST FUNCTIONS
        private static Queue<Task> testTasks()
        {
            Queue<Task> taskQ = new Queue<Task>();

            taskQ.Enqueue(new Task(2, 0));
            taskQ.Enqueue(new Task(18, 6));
            taskQ.Enqueue(new Task(28, 11));
            taskQ.Enqueue(new Task(30, 18));
            taskQ.Enqueue(new Task(18, 24));
            taskQ.Enqueue(new Task(41, 30));
            taskQ.Enqueue(new Task(36, 36));
            taskQ.Enqueue(new Task(24, 42));
            taskQ.Enqueue(new Task(43, 48));
            taskQ.Enqueue(new Task(37, 54));

            return taskQ;
        }
    }
}
