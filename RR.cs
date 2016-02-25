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

        public void RRInit(int testNum)
        {
            Init(testNum, algName);
            Console.WriteLine(testPath);
        }


        public void ProcessTasks(int testNum, Queue<Task> taskQ, int timeSlice)
        {
            RRInit(testNum);

            // Count the number of tasks that need to be completed
            int taskCount = taskQ.Count;
            int currentRT;  // Keeps track of current tasks runtime
            int count = 0; // counter for timeslice loop

            while (completedTasks.Count < taskCount)
            {

                if (taskQ.Count > 0 && taskQ.Peek().arriveTime == clock)
                {
                    waitingQ.Enqueue(taskQ.Dequeue()); // Adds the top task to the waitingQ
                }

                if (currentProcess.isEmpty() && waitingQ.Count > 0)
                {
                    currentProcess = waitingQ.Dequeue();
                    currentProcess.startTime = clock;
                    currentRT = currentProcess.runTime;
                }

                if(count < timeSlice)
                { 
                    // Check if task is done and add it to completedTasks list
                    if (currentProcess.timeLeft == 0 && !currentProcess.isEmpty())
                    {
                        currentProcess.endTime = clock;         // Set end time for the job
                        completedTasks.Add(currentProcess);     // Add it to the completed task list
                        currentProcess = new Task();            // Resets the current process
                        count = 0;               

                    }
                    
                    else
                    {
                        // Process the current task
                        currentProcess.timeLeft--;

                    }
                    // Increment clock and reset the loop
                    clock++;
                    count++;
                }
                if (count == timeSlice)
                {
                    waitingQ.Enqueue(currentProcess);
                    count = 0;
                }
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
