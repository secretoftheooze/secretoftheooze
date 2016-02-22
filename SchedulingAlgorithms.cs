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
                    averageWait = averageWait / taskCount;                  // taskCount is used since it equals NUM_TASKS as opposed to the largest index of completedTasks
                    averageTurnaround = averageTurnaround / taskCount;

                    sw.WriteLine("Average wait time: {0}", averageWait); // need to format for 3 decimal places. I forget how and I'm too tired now. - Colin
                    sw.WriteLine("Average turnaround time: {0}", averageTurnaround); // need to format for 3 decimal places. I forget how and I'm too tired now. - Colin

                    // For testing purposes
                    Console.WriteLine("Test file successfully generated for {0}", testPath);
                }
            }
           catch (Exception e)
            {
                Console.WriteLine("Error recording results for {0}", testPath);
                Console.WriteLine("Error Message: {0}", e.Message);
            }
        }

        // Output Final Results
        public void OutputResults()
        {
            OutputResults(algName);
        }

        // Creates the final CSV final containing all tests for a single algorithm
        protected void OutputResults(string algName)
        {
            // Generate a single file that will hold all of the tests

            // See if test directory exists
            if (Directory.Exists(TESTDIR))
            {
                string outputPath = String.Format("{0}/{1}_Output.csv", TESTDIR, algName);  // The filepath for the output file
                string search = String.Format("*{0}_Test_*.csv", algName);                  // Creates a search string that specifies to the function that it should only collect files of a certain algorithm
                string[] testFiles = Directory.GetFiles(TESTDIR, search);                   // Gets all of the file paths for the specified algorithm's tests
                string currentTest;                                                         // The contents of the current test


                // Construct the new file
                try
                {
                    using (StreamWriter sw = new StreamWriter(outputPath))
                    {
                        // Header
                        sw.WriteLine("{0} Test Results", algName);
                        sw.WriteLine();
                        
                        // Results for each test
                        for (int i = 0; i < testFiles.Length; i++)
                        {
                            // Get the text of the current test
                            currentTest = File.ReadAllText(testFiles[i]);

                            // Create a header for the current test (index + 1)
                            sw.WriteLine("Test {0}", i+1);

                            // Paste the contents of the test
                            sw.Write(currentTest);
                            
                            // Space out each test
                            sw.WriteLine();

                            // Delete collected file
                            File.Delete(testFiles[i]);
                        }
                    }

                    // For testing
                    Console.WriteLine("Results collected for {0}", outputPath);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error collecting results for {0}", outputPath);
                    Console.WriteLine("Error Message: {0}", e.Message);
                }                
            }
        }
    }
}
