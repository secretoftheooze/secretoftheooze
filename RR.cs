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


        public new void ProcessTasks(int testNum, Queue<Task> taskQ, int timeSlice)
        {
            RRInit(testNum);

            // Count the number of tasks that need to be completed
            int taskCount = taskQ.Count;
            int currentRT;  // Keeps track of current tasks runtime
            int i; // counter for timeslece loop

            while (completedTasks.Count < taskCount)
            {
                
                if (taskQ.Count > 0 && taskQ.Peek().ArriveTime == clock)
                {
                    waitingQ.Enqueue(taskQ.Dequeue()); // Adds the top task to the waitingQ
                }
                for (i = 0; i < timeSlice; i++)
                {
                    if (currentProcess.StartTime == -1 && waitingQ.Count > 0)
                    {


                        currentProcess = waitingQ.Dequeue();
                        currentProcess.StartTime = clock;
                        currentRT = currentProcess.RunTime;

                    }
                    // Check if task is done and add it to completedTasks list
                    if (currentProcess.TimeLeft == 0)
                    {
                        currentProcess.EndTime = clock;         // Set end time for the job
                        completedTasks.Add(currentProcess);     // Add it to the completed task list
                        currentProcess = new Task();            // Resets the current process
                    }
                    else
                    {
                        // Process the current task
                        currentProcess.TimeLeft--;
                    }

                    // Increment clock and reset the loop
                    clock++;
                }
                waitingQ.Enqueue(currentProcess);
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
