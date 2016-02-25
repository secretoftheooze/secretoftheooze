using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    class Stride : SchedulingAlgorithm
    {
        new string algName = "STRIDE";                      // Stride
        List<Task> waitingList = new List<Task>();

        // Hide original method, and proceed to process the tasks as per the algorithm
        public void ProcessTasks(int testNum, Queue<Task> taskQ, int timeSlice)
        {
            Init(testNum, algName);

            // Count the number of tasks that need to be completed
            int taskCount = taskQ.Count;
            int passIndex;                       // Keeps track of the index of the job with the lowest Pass Count
            int tickCount = 0;                   // Counts the clock ticks afforded to the process

            // It is considered done once the completed tasks equals the size of the original arrival queue
            while (completedTasks.Count < taskCount)
            {
                // Manage new arrivals/Context Switch
                // Check for new arrivals, and if the arrival queue is empty
                if (taskQ.Count > 0 && taskQ.Peek().arriveTime == clock)
                {
                    //waitingList.Add(taskQ.Dequeue());   // Adds the top task to the waitingList

                    // Context Switch
                    // Determine if the new arrival has a smaller PASS count and if it is time for a new context switch
                    if (tickCount < timeSlice && currentProcess.passCount <= taskQ.Peek().passCount)
                    {
                        waitingList.Add(taskQ.Dequeue());   // Adds the new arrival to the waitingList
                    }

                    else
                    {
                        waitingList.Add(currentProcess);   // Adds current process to waiting list
                        currentProcess = taskQ.Dequeue();  // Sets current process to new arrival
                        currentProcess.startTime = clock;  // Set the start time for the new current process
                        tickCount = 0;                     // Reset the counter for alloted ticks
                    }
                }


                // Process or set new task
                // Check if there is a current process and that there is a process to queue up
                if (currentProcess.isEmpty() && waitingList.Count > 0)
                {
                    // Figure out which task has the shortest run time in the waiting list
                    passIndex = shortestPASS(waitingList);

                    // Set current process to shortest job, and initialize the task by setting its start time
                    currentProcess = waitingList[passIndex];

                    // Set start time if it has not been set before
                    if (!currentProcess.hasStarted())
                    {
                        currentProcess.startTime = clock;
                    }

                    // Remove the current process from the waiting list
                    waitingList.Remove(waitingList[passIndex]);

                    // Reset tickCount
                    tickCount = 0;
                }

                // Switch processes if it its time is up
                else if (tickCount == timeSlice && waitingList.Count > 0)
                {
                    // Increment the pass count
                    currentProcess.passCount = currentProcess.passCount + currentProcess.stride;    

                    // Re-Add the CurrentProcess to the waitingList
                    waitingList.Add(currentProcess);

                    // Figure out which task has the shortest run time in the waiting list
                    passIndex = shortestPASS(waitingList);

                    // Set current process to shortest job, and initialize the task by setting its start time
                    currentProcess = waitingList[passIndex];
                   
                    // Remove the current process from the waiting list
                    waitingList.Remove(waitingList[passIndex]);

                    // Reset tickCount
                    tickCount = 0;
                }

                // Process the current task, unless there was a recent context switch on this tick
                else if (currentProcess.startTime != clock)
                {
                    currentProcess.timeLeft--;                                                      // Decrement the time left
                    currentProcess.passCount = currentProcess.passCount + currentProcess.stride;    // Increment the pass count
                    tickCount++;                                                                    // Increment process counter
                }


                // Complete Task
                // Check if task is done and add it to completedTasks list
                if (currentProcess.isDone())
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

        // Determine the lowest PASS COUNT in the list
        protected int shortestPASS(List<Task> jobList)
        {
            int shortestPass;                     // Keeps track of the lowest pass count in the job list 
            int index;                            // Keeps track of the index of the lowest pass count in the job list

            // Initialize to the first task's pass count 
            shortestPass = jobList[0].passCount;
            index = 0;

            // Check for the shortest pass count in the list
            for (int i = 1; i < jobList.Count; i++)
            {
                if (jobList[i].passCount < shortestPass)
                {
                    shortestPass = jobList[i].passCount;
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
}
