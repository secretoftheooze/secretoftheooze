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
            Console.WriteLine(testPath);
        }


        public new void ProcessTasks(int testNum, Queue<Task> taskQ)
        {
            FIFOInit(testNum);

            // Count the number of tasks that need to be completed
            int taskCount = taskQ.Count;
            int jIndex;      // Keeps track of the index of the current job in the waiting list 
            int currentRT;  // Keeps track of current tasks runtime
            
            foreach (int x in taskQ)
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
