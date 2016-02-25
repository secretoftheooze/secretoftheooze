using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    class FIFO : SchedulingAlgorithm
    {
        new string algName = "FIFO";

        public void FIFOInit(int testNum)
        {
            Init(testNum, algName);
        }


        public new void ProcessTasks(int testNum, Queue<Task> taskQ)
        {
            FIFOInit(testNum);

            // Count the number of tasks that need to be completed
            int taskCount = taskQ.Count;


            while (completedTasks.Count < taskCount)
            {
                if (taskQ.Count > 0 && taskQ.Peek().arriveTime == clock)
                {
                    waitingQ.Enqueue(taskQ.Dequeue()); // Adds the top task to the waitingQ
                }

                // Check if there is another process to queue if there is no current process
                if (currentProcess.isEmpty() && waitingQ.Count > 0)
                {
                    currentProcess = waitingQ.Dequeue();
                    currentProcess.startTime = clock;

                }
                
                // Check if task is done and add it to completedTasks list
                if (currentProcess.timeLeft == 0 && !currentProcess.isEmpty())
                {
                    currentProcess.endTime = clock;         // Set end time for the job
                    completedTasks.Add(currentProcess);     // Add it to the completed task list
                    currentProcess = new Task();            // Resets the current process
                }
                else
                {
                    // Process the current task
                    currentProcess.timeLeft--;
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
