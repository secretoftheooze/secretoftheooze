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
        protected const string TESTDIR = "Tests";                // Test directory

        // Algorithm Identifiers
        protected string algName;                                 // Name of the algorithm, set in each derived algorithm class
        protected string testPath;                                // The file path for the current test. Set in Init

        // Algorithm Values
        protected int clock;                                      // The global clock, initialized in ProcessTasks
        protected Queue<Task> waitingQ = new Queue<Task>();       // Queue of tasks waiting to be processed
        protected Task currentProcess;                            // The task being processed
        protected List<Task> completedTasks = new List<Task>();   // List of completed tasks

        // Standard constructor
        // public SchedulingAlgorithm(int testNum) { }

        // Initialize the test
        // Creates the file name for the test based on the number and algorithm name provided
        protected void Init(int testNum, string algName)
        {
            // Construct the filepath for the output file of the test (AlgorithmName_Test_TestNum.csv)
            testPath = String.Format("{0}/{1}_Test_{2}.csv", TESTDIR, algName, testNum);

            // Clear queues/lists
            clock = 0;
            waitingQ.Clear();
            currentProcess = new Task();
            completedTasks.Clear();
        }

        // The main algorithm - just a skeleton for the base class
        public void ProcessTasks(int testNum, Queue<Task> taskQ)
        {
            // Initialize the test settings and then run the test
            Init(testNum, algName);
        }
        /* ProcessTasks - General Mockup
            while (completedTasks < numTasks)
            - Check for arrival and set current task OR process current task
                - This may also be where a context switch is performed
            - Check if current task is done

            Output(completedTasks)

        */



        // TEST ME PLEASE - Colin
        // Output
        // Processes the output of the ProcessTasks algorithm and outputs it into a CSV file
        public void OutputTest(List<Task> taskList)
        {
            try
            {
                // Create the directory if it does not already exist
                if (!Directory.Exists(TESTDIR))
                {
                    Directory.CreateDirectory(TESTDIR);
                }

                using (StreamWriter sw = new StreamWriter(testPath))
                {
                    // Start with the headers
                    // Note: Stride algorithm has its own output that adds the stride specific values
                    sw.WriteLine("Job#,Arrival Time, Run Time, Start Time, Time Left, End Time,  Waiting Time, Turnaround Time,");

                    // Initialize counters/accumulators
                    int taskCount = 0;                      // Counts the number of tasks
                    int turnaroundTime = 0;                 // Stores current turnaround time 
                    int waitTime = 0;                       // Stores current wait time
                    decimal averageWait = 0;                // Average wait time for the test
                    decimal averageTurnaround = 0;          // Average turnaround time for the test

                    // Then write each job's values
                    foreach (Task task in taskList)
                    {
                        // Processes the waiting times (EndTime - StartTime - RunTime) and turnaround times (EndTime - RunTime) for each task
                        waitTime = task.EndTime - task.ArriveTime - task.RunTime;
                        turnaroundTime = task.EndTime - task.ArriveTime;

                        // Record all of the values the task has
                        sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},", taskCount, task.ArriveTime, task.RunTime, task.StartTime, task.TimeLeft, task.EndTime, waitTime, turnaroundTime);

                        // Increment counters/accumulators
                        taskCount++;
                        averageWait = averageWait + waitTime;                           // AverageWait + wait time for current task
                        averageTurnaround = averageTurnaround + turnaroundTime;         // AverageTurnaround + turnaround time for current task
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

            // For testing purposes
            Console.WriteLine("Test file successfully generated for {0}", testPath);
        }

        // Output Final Results
        // Cleans up all of the test files and puts them all together in one CSV file
        public void OutputResults()
        {
            // Search for all of the CSV files
        }
    }
}
