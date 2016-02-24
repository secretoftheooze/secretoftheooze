// COIS 3320 A1: Task Class
// Colin A. Marshall(0533528) and Brandon Root(0564499)
// The general data structure that stores all the data for a specific job being processed

using System;

namespace Scheduler
{
    struct Task
    {
        // Values
        public int runTime;            // Time it takes to process the task
        public int arriveTime;         // The time the task arrives to be processed
        public int startTime;          // The time that the task starts processing. Initially set to -1.
        public int timeLeft;           // The time left for the task to process, initially set as runTime and ticked down from there
        public int endTime;            // The time that the task finished processing. Initially set to -1.

        // Stride specific values
        public int tickets;        // How many tickets are alloted to the task for Stride scheduling
        public int stride;         // Stride value, determined by stride algorithm
        public int passCount;

        // Set initial values for run time and arrival time
        public Task (int runTime, int arriveTime)
        {
            this.runTime = runTime;
            this.arriveTime = arriveTime;

            timeLeft = runTime; // Sets the initial value for timeLeft

            // Initialize start and end times
            startTime = -1;
            endTime = -1;
        
            tickets = 0;        
            stride = 0;         
            passCount = 0;
        }

        // isEmpty Function
        // Function to check if the structure is empty or not. Used in conditions.
        public bool isEmpty()
        {
            if (runTime == 0)
                return true;
            else
                return false;
        }
    }
}
