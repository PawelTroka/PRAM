using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Memory;
using PRAM_Machine.Models;

namespace PRAM_Machine.Machine {
    /// <summary>
    /// Base class for PRAM machine processor. 
    /// Remember that no methods apart from Run() and properties of this class can be hidden or overridden
    /// </summary>
    public abstract class Processor {
        #region ClassMembers
        //Number of this processor in PRAM machine
        private int number;
        //How many ticks/cycles have elapsed from start till now (starting from 0)
        private int tickCount;
        //This is address of data that will be passed to the processor at the next cycle
        private MemoryAddress dataToRead;
        //This is address of memory cell that the processor will store its result into.
        private MemoryAddress dataToWrite;
        //Result of current processor calculations
        private dynamic processorResult;
        //If processor is stopped
        private bool isStopped;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes base processor class
        /// </summary>
        public Processor() {
            tickCount = 0;
            isStopped = false;
            DataToRead = new MemoryAddress();
            DataToWrite = new MemoryAddress();
            number = -1;
        }

        /// <summary>
        /// Initializes processor with number set
        /// </summary>
        /// <param name="number">Processors number</param>
        public Processor(int number) : this() {
            this.number = number;
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method is called by PRAM machine to run one cycle of processor.
        /// It stores its result and passes correct data to it.
        /// </summary>
        /// <param name="data">Data that will be passed to processor</param>
        public void Execute(dynamic data) {
            if (!this.IsStopped) {
                this.processorResult = Run(data);
                this.tickCount++;
            } 
        }

        /// <summary>
        /// Method that should be called when we don't want the processor to be ran in the next cycle.
        /// </summary>
        public void Stop() {
            this.isStopped = true;
            //As processor is stopped it won't need any new data, it just has to write it's result
            this.DataToRead = new MemoryAddress();
        }

        /// <summary>
        /// This method only works when processor is stopped, and is called by PRAM machine.
        /// This method clears all writing requests by the processor after it managed to write its final result.
        /// </summary>
        public void ClearDataToWrite() {
            if (this.IsStopped) {
                //We allowed it to write it's result, so we stop it from writing it again
                this.DataToWrite = new MemoryAddress();
            }
        }

        /// <summary>
        /// This method has to be overridden in the base class.
        /// It should contain implementation of processor behavior during a single cycle.
        /// This method should set DataToRead and DataToWrite properties.
        /// </summary>
        /// <param name="data">Data that was pointed by DataToRead property in the previous cycle</param>
        /// <returns>The return value of this method will be stored in memory cell pointed by DataToWrite</returns>
        public abstract dynamic Run(dynamic data);
        #endregion

        #region Properties
        /// <summary>
        /// This property stores processor number in PRAM machine
        /// </summary>
        public int Number {
            get { return number; }
            set {
                if (this.number == -1) {
                    this.number = value;
                }
            }
        }

        /// <summary>
        /// This read only property is used by PRAM machine when collecting read reqest from processor.
        /// It builds the request using processors number and data address.
        /// </summary>
        public RWRequest ReadRequest {
            get {
                if (DataToRead.Empty) {
                    //returning empty request
                    return new RWRequest();
                }
                return new RWRequest(this.Number, this.DataToRead, null);
            }
        }

        /// <summary>
        /// This read only property is used by PRAM machine when collecting write request from processor.
        /// It builds the request using processors number, data address, and return value.
        /// </summary>
        public RWRequest WriteRequest {
            get {
                if (DataToWrite.Empty) { 
                    //returning empty request
                    return new RWRequest();
                }
                return new RWRequest(this.Number, this.DataToWrite, this.processorResult);
            }
        }

        /// <summary>
        /// Read only property indicating whether processor is stopped or not
        /// </summary>
        public bool IsStopped {
            get { return this.isStopped; }
        }

        /// <summary>
        /// Read only property that stores number of cycles processor has performed (starting from 0)
        /// </summary>
        public int TickCount {
            get { return this.tickCount;  }
        }

        /// <summary>
        /// This property has to be set to get data from memory. 
        /// It's important to remember to reset this property if memory doesn't support multiple read operations.
        /// </summary>
        public MemoryAddress DataToRead {
            get { return this.dataToRead; }
            set { this.dataToRead = value; }
        }

        /// <summary>
        /// This property has to be set to write data to memory. 
        /// It's important to remember to reset this property if memory doesn't support multiple write operations.
        /// </summary>
        public MemoryAddress DataToWrite {
            get { return this.dataToWrite; }
            set { this.dataToWrite = value; }
        }

        /// <summary>
        /// Read only property returning model of the processor current state
        /// </summary>
        public ProcessorModel Model {
            get { return new ProcessorModel(this); }
        }
        #endregion
    }
}
