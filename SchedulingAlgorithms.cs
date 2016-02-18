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
            testPath = String.Format("{0}/{1}_Test_{2}.csv", TESTDIR, algName, testNum);    // Constructs the filepath for the output file of the test
        }

        // The main algorithm - just a skeleton for the base class
        public void ProcessTasks(Queue<Task> taskQ) { }

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

                    int taskCount = 0;                   // Counts the number of tasks

                    // Then write each job's values
                    foreach(Task task in taskList)
                    {
                        // Record all of the values the task has
                        // Also processes the waiting times (EndTime - StartTime - RunTime) and turnaround times (EndTime - RunTime) for each task
                        sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},", taskCount, task.ArriveTime, task.RunTime, task.StartTime, task.TimeLeft, task.EndTime, task.EndTime - task.StartTime - task.RunTime, task.EndTime - task.RunTime);
                        taskCount++;
                    }

                    // Process the average waiting time and average turnaround time
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
