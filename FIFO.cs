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
            int currentRT;  // Keeps track of current tasks runtime


            while (completedTasks.Count < taskCount)
            {

                if (currentProcess.StartTime == -1 && taskQ.Count > 0)
                {
                    //waitingQ.Enqueue(taskQ.Dequeue());
                    currentProcess = taskQ.Dequeue();
                currentProcess.StartTime = clock;
                currentRT = currentProcess.RunTime;


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
