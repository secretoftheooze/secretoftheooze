// COIS 3320 A1: Task Class
// Colin A. Marshall(0533528) and Brandon Root(0564499)
// The general data structure that stores all the data for a specific job being processed

using System;

namespace Scheduler
{
    class Task
    {
        // Values
        int runTime;            // Time it takes to process the task
        int arriveTime;         // The time the task arrives to be processed
        int startTime;          // The time that the task starts processing. Initially set to -1.
        int timeLeft;           // The time left for the task to process, initially set as runTime and ticked down from there
        int endTime;            // The time that the task finished processing. Initially set to -1.

        // Stride specific values
        int tickets;        // How many tickets are alloted to the task for Stride scheduling
        int stride;         // Stride value, determined by stride algorithm
        int passCount;      


        // Constructors
        public Task()
        {
            // Initialize variables
            runTime = -1;
            arriveTime = -1;
            startTime = -1;
            endTime = -1;
        }

        // Set initial values for run time and arrival time
        public Task (int runTime, int arriveTime)
        {
            this.runTime = runTime;
            this.arriveTime = arriveTime;

            timeLeft = runTime; // Sets the initial value for timeLeft

            // Initialize start and end times
            startTime = -1;
            endTime = -1;
        }

        // Properties
        // Read-only
        public int RunTime { get { return runTime; } }          // Read-only property for the time it takes to process the task
        public int ArriveTime { get { return arriveTime; } }    // Read-only property for the time the task arrives to be processed

        // Read-Write
        // To be manipulated by the algorithms
        public int StartTime
        {
            get { return startTime; }
            set { startTime = value;  }
        }

        public int TimeLeft
        {
            get { return timeLeft; }
            set { timeLeft = value; }
        }

        public int EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        // Stride specific properties
        public int Tickets
        {
            get { return tickets; }
            set { tickets = value; }
        }

        public int Stride
        {
            get { return stride; }
            set { stride = value; }
        }

        public int PassCount
        {
            get { return passCount; }
            set { passCount = value; }
        }
    }
}
