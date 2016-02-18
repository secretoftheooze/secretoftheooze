// COIS 3320 A1: SchedulingAlgorithms
// Colin A. Marshall(0533528) and Brandon Root(SID)
// Contains the scheduling algorithms used for testing

using System;
using System.Collections.Generic;
using System.IO;


namespace Scheduler
{
    class SchedulingAlgorithm
    {
        // Constants
        const string TESTDIR = "Tests/";                // Test directory

        // Algorithm Identifiers
        string algName;                                 // Name of the algorithm, set in each derived algorithm class
        string testPath;                                // The file path for the current test. Set in constructor

        // Algorithm Values
        int clock;                                      // The global clock, initialized in ProcessTasks
        Queue<Task> waitingQ = new Queue<Task>();       // Queue of tasks waiting to be processed
        Task currentProcess;                            // The task being 
        List<Task> completedTasks = new List<Task>();   // List of completed tasks

        // Initialize the test
        public SchedulingAlgorithm(int testNum)
        {
            testPath = String.Format("{0}/{1}_Test_{2}.csv", TESTDIR, algName, testNum);    // Constructs the filepath for the output file of the test (AlgorithmName_Test_TestNum.csv)
        }

        // The main algorithm - just a skeleton for the base class
        public void ProcessTasks(Queue<Task> taskQ) { }
        /* ProcessTasks - General Mockup
        
            // Set clock to 0
            clock = 0;

            Set up a loop based on incrementing clock. May actually be able to set clock as part of a for loop.
            With each "tick", we need to
                - check if it's time to pop job from queue, by matching arrival time with clock time, then putting it in waitingQ
                - Assess if a context switch is needed (switching processes by adding currentProcess to waitingQ, then popping the waitingQ and making it the currentProcess)
                - Other shit 
        */



        // TEST ME PLEASE - Colin
        // Output
        // Processes the output of the ProcessTasks algorithm and outputs it into a CSV file
        public void OutputTest(List<Task> taskList)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(testPath))
                {
                    // Start with the headers
                    // Note: Stride algorithm has its own output that adds the stride specific values
                    sw.WriteLine("Job#,Arrival Time, Run Time, Start Time, Time Left, End Time,  Waiting Time, Turnaround Time,");

                    // Initialize counters/accumulators
                    int taskCount = 0;                  // Counts the number of tasks
                    decimal averageWait = 0;                // Average wait time for the test
                    decimal averageTurnaround = 0;          // Average turnaround time for the test

                    // Then write each job's values
                    foreach (Task task in taskList)
                    {
                        // Record all of the values the task has
                        // Also processes the waiting times (EndTime - StartTime - RunTime) and turnaround times (EndTime - RunTime) for each task
                        sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},", taskCount, task.ArriveTime, task.RunTime, task.StartTime, task.TimeLeft, task.EndTime, task.EndTime - task.StartTime - task.RunTime, task.EndTime - task.RunTime);

                        // Increment counters/accumulators
                        taskCount++;
                        averageWait = averageWait + task.EndTime - task.StartTime - task.RunTime;   // AverageWait + wait time for current task
                        averageTurnaround = averageTurnaround + task.EndTime - task.RunTime;        // AverageTurnaround + turnaround time for current task
                    }

                    // Process and output average wait and turnaround times
                    averageWait = averageWait / taskCount;                  // As far as I know, taskCount = NUM_TESTS as opposed to equaling taskList.Count. This is because the counter increments one more time in the loop than needed.
                    averageTurnaround = averageTurnaround / taskCount;

                    sw.WriteLine("Average wait time: {0}", averageWait); // need to format for 3 decimal places. I forget how and I'm too tired now. - Colin
                    sw.WriteLine("Average turnaround time: {0}", averageTurnaround); // need to format for 3 decimal places. I forget how and I'm too tired now. - Colin
                }
            }
           catch (Exception e)
            {
                Console.WriteLine("Error recording results for {0}", testPath);
                Console.WriteLine("Error Message: {0}", e.Message);
            }
        }

        // Output Final Results
        // Cleans up all of the test files and puts them all together in one CSV file
        public void OutputResults()
        {
            // Search for all of the CSV files
        }
    }
}
