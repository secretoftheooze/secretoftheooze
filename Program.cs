﻿// COIS 3320 A1: Program
// Colin A. Marshall(0533528) and Brandon Root(0564499)
// The main program that performs the tests, using the algorithms defined in SchedulingAlgorithms

using System;
using System.Collections.Generic;
using System.Collections;


namespace Scheduler
{
    class Program
    {
        const int NUM_TESTS = 20;             // The number of tests being done
        const int NUM_TASKS = 10;            // The number of jobs being generated for processing 
        const int SYSTEM_STRIDE = 1000;      // System value used to calculate strides
        const int TS1 = 5;                   // Time slice 1 - used by Stride and Round Robin
        const int TS2 = 10;                  // Time slice 1 - used by Stride and Round Robin
        const int TS3 = 15;                  // Time slice 1 - used by Stride and Round Robin

        static void Main(string[] args)
        {
            // Initialize task queue and algorithms
            Queue<Task> taskQ = new Queue<Task>();
            FIFO fifo = new FIFO();
            SJF sjf = new SJF();
            PE_SJF pesjf = new PE_SJF();
            RR rr = new RR();
            Stride stride = new Stride();
            

            // Proceed to loop through all of the tests
            for (int i = 1; i <= NUM_TESTS; i++)
            {
                // Generate the tasks
                taskQ = generateTasks(10, i);
                //taskQ = testTasks();// TESTING

                // Perform the tests with each of the algorithms
                sjf.ProcessTasks(i, new Queue<Task>(taskQ));
                fifo.ProcessTasks(i, new Queue<Task>(taskQ));
                pesjf.ProcessTasks(i, new Queue<Task>(taskQ));
                stride.ProcessTasks(i, new Queue<Task>(taskQ), TS1);

                // Run the other algorithms for each of the time slices
                rr.ProcessTasks(i, new Queue<Task>(taskQ), TS1);
                rr.ProcessTasks(i, new Queue<Task>(taskQ), TS2);
                rr.ProcessTasks(i, new Queue<Task>(taskQ), TS3);

                
                
            }

            // Output results for each algorithm
            fifo.OutputResults();
            sjf.OutputResults();
            pesjf.OutputResults();
            stride.OutputResults();

            // Grab all the timeslices for RR
            rr.OutputResults(TS1);
            rr.OutputResults(TS2);
            rr.OutputResults(TS3);

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
                taskQ.Enqueue(new Task(rando.Next(2,61), arriveAcc, rando.Next(1,5) * 50, SYSTEM_STRIDE));           // Queue a new task with a random run time of 2-60 units and a arrival time every 5-8 seconds
                arriveAcc = arriveAcc + rando.Next(5, 9);                       // Tasks arrive every 5-8 units of time after previous task
            }

            return taskQ;
        }



        // TEST FUNCTIONS
        private static Queue<Task> testTasks()
        {
            Queue<Task> taskQ = new Queue<Task>();

            taskQ.Enqueue(new Task(2, 0,50,SYSTEM_STRIDE));
            taskQ.Enqueue(new Task(18, 6,100, SYSTEM_STRIDE));
            taskQ.Enqueue(new Task(2, 11,150, SYSTEM_STRIDE));
            taskQ.Enqueue(new Task(30, 18,100, SYSTEM_STRIDE));
            taskQ.Enqueue(new Task(18, 24,100, SYSTEM_STRIDE));
            taskQ.Enqueue(new Task(41, 30,200, SYSTEM_STRIDE));
            taskQ.Enqueue(new Task(36, 36,50, SYSTEM_STRIDE));
            taskQ.Enqueue(new Task(24, 42,100, SYSTEM_STRIDE));
            taskQ.Enqueue(new Task(43, 48,200, SYSTEM_STRIDE));
            taskQ.Enqueue(new Task(37, 54,150, SYSTEM_STRIDE));

            return taskQ;
        }
    }
}
