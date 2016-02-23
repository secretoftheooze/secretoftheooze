// Shortest Job First Algorithm
// Will also host Pre-Emptive SJF in the future
// SJF - Prioritizes jobs with the shortest run time, and processes them until completion

using System;
using System.Collections.Generic;


namespace Scheduler
{
    class SJF : SchedulingAlgorithm
    {
        new string algName = "SJF";                 // Set the algorithm name
        List<Task> waitingList = new List<Task>();  // This algorithm uses a waiting list as opposed to a waiting Queue

        // Initialize the test
        // Altered initializer to factor in waitingList
        protected void SJFInit(int testNum)
        {
            // Call original Init
            Init(testNum, algName);

            // Clear waitingList
            waitingList.Clear();
        }

        // Hide original method, and proceed to process the tasks as per the algorithm
        public new void ProcessTasks(int testNum, Queue<Task> taskQ)
        {
            SJFInit(testNum);

            // Count the number of tasks that need to be completed
            int taskCount = taskQ.Count;
            int sjIndex;                        // Keeps track of the index of the shortest job in the waiting list 
            int shortestRT;                     // Keeps track of the shortest run time in the waiting list 

            // It is considered done once the completed tasks equals the size of the original arrival queue
            while (completedTasks.Count < taskCount)
            {
                // Check for new arrivals, and if the arrival queue is empty
                if (taskQ.Count > 0 && taskQ.Peek().ArriveTime == clock)
                {
                    waitingList.Add(taskQ.Dequeue());   // Adds the top task to the waitingList
                }

                // Check if there is a current process and that there is a process to queue up
                if (currentProcess.StartTime == -1 && waitingList.Count > 0)
                {
                    // Figure out which task has the shortest run time in the waiting list

                    // Initialize to the first task's run time 
                    shortestRT = waitingList[0].RunTime;
                    sjIndex = 0;

                    // Check for the shortest job in the list
                    for (int i = 1; i < waitingList.Count - 1; i++)
                    {
                        if (waitingList[i].RunTime < shortestRT)
                        {
                            shortestRT = waitingList[i].RunTime;
                            sjIndex = i;
                        }
                    }

                    // Set current process to shortest job, and initialize the task by setting its start time
                    currentProcess = waitingList[sjIndex];
                    currentProcess.StartTime = clock;

                    // Remove the current process from the waiting list
                    waitingList.Remove(waitingList[sjIndex]);
                }

                // Process the current task
                else
                {
                    currentProcess.TimeLeft--;
                }
                

                // Check if task is done and add it to completedTasks list
                if (currentProcess.TimeLeft == 0 && currentProcess.StartTime > -1)
                {
                    currentProcess.EndTime = clock;         // Set end time for the job
                    completedTasks.Add(currentProcess);     // Add it to the completed task list
                    currentProcess = new Task();            // Resets the current process
                }

                // Increment clock
                clock++;
            }

            // After processing, output the results
            OutputTest(completedTasks);
        }

        // Clean up all of the files and output the results
        public new void OutputResults()
        {
            OutputResults(algName);
        }

    }

    //class PE_SJF : SJF
    //{
    //    new string algName = "PE-SJF";                      // Pre-Emptive SJF
    //}

class FIFO : SchedulingAlgorithm
    {
        new string algName = "FIFO";
        
    public void FIFOInit(int testNum)
    { 
            Init(testNum, algName);
            Console.WriteLine(testPath);
        }

       
        public new void ProcessTasks(int testNum, Queue<Task> taskQ)
        {
            FIFOInit(testNum);

            // Count the number of tasks that need to be completed
            int taskCount = taskQ.Count;
            int jIndex;      // Keeps track of the index of the current job in the waiting list 
            int currentRT;  // Keeps track of current tasks runtime

            foreach(int x in taskQ)
            {
                
                // Check if there is a current process
                if (currentProcess.StartTime == -1 && taskQ.Count > 0)
                {
                   
                        // Initialize to the first task's run time 
                        currentRT = taskQ[0].RunTime;
                        jIndex = 0;

                        
                        for (int i = 1; i < taskQ.Count - 1; i++)
                        {
                           
                                currentRT = taskQ[i].RunTime;
                                jIndex = i;
                            
                        

                        // Set current process to current job, and initialize the task by setting its start time
                        currentProcess = taskQ[jIndex];
                        currentProcess.StartTime = clock;

                        // Remove the current process from the waiting list
                        waitingList.Remove(waitingList[jIndex]);

                        }
                    }
                

                else
                {
                    // Process the current task
                    currentProcess.TimeLeft--;
                }
                

                // Check if task is done and add it to completedTasks list
                if (currentProcess.TimeLeft == 0)
                {
                    currentProcess.EndTime = clock;         // Set end time for the job
                    completedTasks.Add(currentProcess);     // Add it to the completed task list
                    currentProcess = new Task();            // Resets the current process
                }

                // Increment clock and reset the loop
                clock++;
            }

            // After processing, output the results
            OutputTest(completedTasks);
        }
     public new void OutputResults()
        {
            OutputResults(algName);
        }

    }
}
