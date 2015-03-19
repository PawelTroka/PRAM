using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Models {
    /// <summary>
    /// Class representing model of parallel processor. 
    /// It contains all necessary fields to represent processor state at the moment.
    /// </summary>
    public class ProcessorModel {
        #region ClassMembers
        //If processor is stopped
        private bool isStopped;
        //Number of this processor in PRAM machine
        private int number;
        //This is address of data that will be passed to the processor at the next cycle
        private MemoryAddress dataToRead;
        //This is address of memory cell that the processor will store its result into.
        private MemoryAddress dataToWrite;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty model
        /// </summary>
        public ProcessorModel() {
            this.isStopped = false;
            this.number = 0;
            this.dataToRead = new MemoryAddress();
            this.dataToWrite = new MemoryAddress();
        }

        /// <summary>
        /// Model of the processor passed as parameter
        /// </summary>
        /// <param name="processor">Processor from which the model is constructed</param>
        public ProcessorModel(Processor processor) {
            this.number = processor.Number;
            this.isStopped = processor.IsStopped;
            this.dataToRead = processor.DataToRead;
            this.dataToWrite = processor.DataToWrite;
        }

        /// <summary>
        /// Model built from separately passed parameters
        /// </summary>
        /// <param name="number">Processor number</param>
        /// <param name="isStopped">Whether or not processor is stopped</param>
        /// <param name="dataToRead">Current processor read request</param>
        /// <param name="dataToWrite">Current processor write request</param>
        public ProcessorModel(int number, bool isStopped, MemoryAddress dataToRead, MemoryAddress dataToWrite) {
            this.number = number;
            this.isStopped = isStopped;
            this.dataToRead = dataToRead;
            this.dataToWrite = dataToWrite;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Read only property indicating whether or not processor is stopped
        /// </summary>
        public bool IsStopped {
            get { return isStopped; }
        }

        /// <summary>
        /// Read only property, number of the processor
        /// </summary>
        public int Number {
            get { return number; }
        }

        /// <summary>
        /// Read only property, current read request
        /// </summary>
        public MemoryAddress DataToRead {
            get { return dataToRead; }
        }

        /// <summary>
        /// Read only property, current write request
        /// </summary>
        public MemoryAddress DataToWrite {
            get { return dataToWrite; }
        }
        #endregion
    }
}
