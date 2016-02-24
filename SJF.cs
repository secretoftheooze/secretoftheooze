// Shortest Job First Algorithm
// Will also host Pre-Emptive SJF in the future
// SJF - Prioritizes jobs with the shortest run time, and processes them until completion

using System;
using System.Collections.Generic;


namespace Scheduler
{
    class SJF : SchedulingAlgorithm
    {
        new string algName = "SJF";                           // Set the algorithm name
        protected List<Task> waitingList = new List<Task>();  // This algorithm uses a waiting list as opposed to a waiting Queue

        // Initialize the test
        // Altered initializer to factor in waitingList
        private void SJFInit(int testNum)
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
            

            // It is considered done once the completed tasks equals the size of the original arrival queue
            while (completedTasks.Count < taskCount)
            {
                // Check for new arrivals, and if the arrival queue is empty
                if (taskQ.Count > 0 && taskQ.Peek().arriveTime == clock)
                {
                    waitingList.Add(taskQ.Dequeue());   // Adds the top task to the waitingList
                }

                // Check if there is a current process and that there is a process to queue up
                if (currentProcess.isEmpty() && waitingList.Count > 0)
                {
                    // Figure out which task has the shortest run time in the waiting list

                    sjIndex = shortestJob(waitingList);

                    // Set current process to shortest job, and initialize the task by setting its start time
                    currentProcess = waitingList[sjIndex];
                    currentProcess.startTime = clock;

                    // Remove the current process from the waiting list
                    waitingList.Remove(waitingList[sjIndex]);
                }

                // Process the current task
                else
                {
                    currentProcess.timeLeft--;
                }
                

                // Check if task is done and add it to completedTasks list
                if (currentProcess.timeLeft == 0 && !currentProcess.isEmpty())
                {
                    currentProcess.endTime = clock;         // Set end time for the job
                    completedTasks.Add(currentProcess);     // Add it to the completed task list
                    currentProcess = new Task();            // Resets the current process
                }

                // Increment clock
                clock++;
            }

            // After processing, output the results
            OutputTest(completedTasks);
        }

        // Determines the shortest job in the waiting list
        protected int shortestJob(List<Task> jobList)
        {
            int shortestRT;                     // Keeps track of the shortest run time in the waiting list 
            int index;                          // Keeps track of the index of the shortest job in the waiting list

            // Initialize to the first task's run time 
            shortestRT = jobList[0].timeLeft;
            index = 0;

            // Check for the shortest job in the list
            for (int i = 1; i < jobList.Count; i++)
            {
                if (jobList[i].timeLeft < shortestRT)
                {
                    shortestRT = jobList[i].timeLeft;
                    index = i;
                }
            }

            return index;
        }

        // Clean up all of the files and output the results
        public new void OutputResults()
        {
            OutputResults(algName);
        }

    }

    class PE_SJF : SJF
    {
        new string algName = "PE-SJF";                      // Pre-Emptive SJF

        // Initialize the test
        // Altered initializer to factor in waitingList
        private void PESJFInit(int testNum)
        {
            // Call original Init
            Init(testNum, algName);

            // Clear waitingList
            waitingList.Clear();
        }

        // Hide original method, and proceed to process the tasks as per the algorithm
        public new void ProcessTasks(int testNum, Queue<Task> taskQ)
        {
            PESJFInit(testNum);

            // Count the number of tasks that need to be completed
            int taskCount = taskQ.Count;
            int sjIndex;                        // Keeps track of the index of the shortest job in the waiting list 


            // It is considered done once the completed tasks equals the size of the original arrival queue
            while (completedTasks.Count < taskCount)
            {
                // Check for new arrivals, and if the arrival queue is empty
                if (taskQ.Count > 0 && taskQ.Peek().arriveTime == clock)
                {
                    //waitingList.Add(taskQ.Dequeue());   // Adds the top task to the waitingList

                    // Context Switch
                    // Determine if the new arrival has a short enough run time that it justifies switching jobs
                    if (currentProcess.timeLeft <= taskQ.Peek().timeLeft)
                    {
                        waitingList.Add(taskQ.Dequeue());   // Adds the top task to the waitingList
                    }

                    else
                    {
                        waitingList.Add(currentProcess);   // Adds current process to waiting list
                        currentProcess = taskQ.Dequeue();  // Sets current process to new arrival
                        currentProcess.startTime = clock;  // Set the start time for the new current process
                    }
                }

                // Check if there is a current process and that there is a process to queue up
                if (currentProcess.isEmpty() && waitingList.Count > 0)
                {
                    // Figure out which task has the shortest run time in the waiting list
                    sjIndex = shortestJob(waitingList);

                    // Set current process to shortest job, and initialize the task by setting its start time
                    currentProcess = waitingList[sjIndex];

                    // Set start time if it has not been set before
                    if (currentProcess.startTime == -1)
                    {
                        currentProcess.startTime = clock;
                    }
                   
                    // Remove the current process from the waiting list
                    waitingList.Remove(waitingList[sjIndex]);
                }

                // Process the current task, unless there was a recent context switch on this tick
                else if(currentProcess.startTime != clock)
                {
                    currentProcess.timeLeft--;
                }


                // Check if task is done and add it to completedTasks list
                if (currentProcess.timeLeft == 0 && !currentProcess.isEmpty())
                {
                    currentProcess.endTime = clock;         // Set end time for the job
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
}
