using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Models;

namespace PRAM_Machine.Machine {
    public interface IPRAMMachine {
        /// <summary>
        /// This method performs consecutively each step of PRAM machine workcycle
        /// </summary>
        /// <returns>true if machine is working and false otherwise</returns>
        bool Step();

        /// <summary>
        /// Main method of the machine, which loops tick method untill all processors finish their calculations
        /// </summary>
        void Start();

        /// <summary>
        /// Read only property that returns number of ticks/cycles machine has performed (starting from 0)
        /// </summary>
        int TickCount { get; }
        
        /// <summary>
        /// Read only property that returns number of read operations performed by all processors
        /// </summary>
        int ReadCount { get; }
        
        /// <summary>
        /// Read only property that returns number of write operations performed by all processors
        /// </summary>
        int WriteCount { get; }
       
        /// <summary>
        /// Read only property that returns data stored in machine memory (remember that it's only data not the memory state)
        /// </summary>
        Dictionary<string, List<dynamic>> Memory { get; }

        /// <summary>
        /// This read only property returns PRAM Machine model, representing its current state
        /// </summary>
        PRAMMachineModel Model { get; }
        
        /// <summary>
        /// This readonly property stores current state of PRAM machine
        /// </summary>
        PRAMState State { get; }

        /// <summary>
        /// This readonly property indicates whether or not machine is stopped
        /// </summary>
        bool IsStopped { get; }
    }
}
