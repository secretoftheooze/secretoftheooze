using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    class RR : SchedulingAlgorithm
    {
        new string algName = "RR";

        public void RRInit(int testNum, int timeSlice)
        {
            Init(testNum, String.Format("{0}_{1}", algName, timeSlice));         // Set the testpath to be the algName + the time slice
        }


        public void ProcessTasks(int testNum, Queue<Task> taskQ, int timeSlice)
        {
            RRInit(testNum, timeSlice);

            // Count the number of tasks that need to be completed
            int taskCount = taskQ.Count;
            int count = 0; // counter for timeslice loop

            while (completedTasks.Count < taskCount)
            {
                // Check for arrivals
                if (taskQ.Count > 0 && taskQ.Peek().arriveTime == clock)
                {
                    waitingQ.Enqueue(taskQ.Dequeue()); // Adds the top task to the waitingQ
                }


                // Process or set new task

                if (currentProcess.isEmpty() && waitingQ.Count > 0)
                {
                    currentProcess = waitingQ.Dequeue();


                    // Set start time if it has not been set before
                    if (!currentProcess.hasStarted())
                    {
                        currentProcess.startTime = clock;
                    }

                    count = 0;
                }

                // Processing time is up for the current process
                else if(count == timeSlice && waitingQ.Count > 0)
                {
                    // Check if task is done and add it to completedTasks list
                    waitingQ.Enqueue(currentProcess);
                    currentProcess = waitingQ.Dequeue();

                    // Set start time if it has not been set before
                    if (!currentProcess.hasStarted())
                    {
                        currentProcess.startTime = clock;
                    }

                    count = 0;
                }

                // Process the current task
                else if (currentProcess.startTime != clock)
                {
                    currentProcess.timeLeft--;
                    count++;                                // Increment counter
                    
                }


                // Complete Process
                if (currentProcess.isDone())
                {
                    currentProcess.endTime = clock;         // Set end time for the job
                    completedTasks.Add(currentProcess);     // Add it to the completed task list
                    currentProcess = new Task();            // Resets the current process
                    count = 0;

                }

                // Increment clock and reset the loop
                clock++;

            }
            
        
            // After processing, output the results
            OutputTest(completedTasks);
        }

        // Output the results of the tests with the set time slice
        public void OutputResults(int timeSlice)
        {
            OutputResults(String.Format("{0}_{1}",algName,timeSlice));
        }

    }
}
