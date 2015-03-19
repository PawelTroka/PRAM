using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Machine;

namespace PRAM_Machine.Models {
    /// <summary>
    /// Model representing PRAM machine state. 
    /// </summary>
    public class PRAMMachineModel {
        #region ClassMembers
        // List of all the processors in the machine (it contains their models)
        private List<ProcessorModel> processors;
        // Model of the machine memory
        private PRAMModel pram;
        // Global clock ticks elapsed since the start of the machine
        private int tickCount;
        // Current machine state in the work cycle
        private PRAMState state;
        // Whether or not whole machine is stopped
        private bool isStopped;
        // Curent number of read requests
        private int actualReadCount;
        // Curent number of write requests
        private int actualWriteCount;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty PRAM machine model
        /// </summary>
        public PRAMMachineModel() {
            this.processors = new List<ProcessorModel>();
            this.pram = new PRAMModel();
            this.state = PRAMState.Reading;
            this.tickCount = 0;
            this.isStopped = false;
        }

        /// <summary>
        /// Full model built from list of the machines processors, its memory, tick count and state
        /// </summary>
        /// <param name="processors">List of models of the processors from the machine</param>
        /// <param name="pram">Model of the machines memory</param>
        /// <param name="tickCount">Global clock ticks elapsed since the start of the machine</param>
        /// <param name="state">Current machine state in the work cycle</param>
        /// <param name="actualReadCount">Curent number of read requests</param>
        /// <param name="actualWriteCount">Curent number of write requests</param>
        public PRAMMachineModel(List<ProcessorModel> processors, PRAMModel pram, int tickCount, 
                                PRAMState state, bool isStopped,  int actualReadCount, int actualWriteCount) {
            this.processors = processors;
            this.pram = pram;
            this.tickCount = tickCount;
            this.state = state;
            this.isStopped = isStopped;
            this.actualReadCount = actualReadCount;
            this.actualWriteCount = actualWriteCount;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Read only property, memory model
        /// </summary>
        public PRAMModel PRAM {
            get { return pram; }
        }

        /// <summary>
        /// Read only property, processors model
        /// </summary>
        public List<ProcessorModel> Processors {
            get { return processors; }
        }

        /// <summary>
        /// Read only property, machine state
        /// </summary>
        public PRAMState State {
            get { return state; }
        }

        /// <summary>
        /// Read only property, Global clock ticks elapsed since the start of the machine
        /// </summary>
        public int TickCount {
            get { return tickCount; }
        }

        /// <summary>
        /// Read only property indicating whether or not the whole machine is stopped
        /// </summary>
        public bool IsStopped {
            get { return isStopped; }
        }

        /// <summary>
        /// Read only property indicating curent number of read operations in this step
        /// </summary>
        public int ActualReadCount {
            get { return actualReadCount; }
        }

        /// <summary>
        /// Read only property indicating curent number of write operations in this step
        /// </summary>
        public int ActualWriteCount {
            get { return actualWriteCount; }
        }
        #endregion
    }
}
