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
        List<Task> waitingList = new List<Task>();  // This algorithm uses a waiting List as opposed to a waiting Queue

        // Initialize the test
        public SJF(int testNum)
        {
            Init(testNum, algName);
            Console.WriteLine(testPath);
        }

        // Hide original method, and proceed to process the tasks as per the algorithm
        public new void ProcessTasks(Queue<Task> taskQ)
        {
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

                // Check if there is a current process
                if (currentProcess.StartTime == -1)
                {
                    // Figure out which task has the shortest run time in the waiting list, if the waiting list isn't empty
                    if (waitingList.Count != 0)
                    {
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

    }
}
