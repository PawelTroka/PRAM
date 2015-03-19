using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Memory;
using PRAM_Machine.Models;

namespace PRAM_Machine.Machine {
    ///<summary>
    ///Class representing entire PRAM machine
    ///It contains both processors and memory
    ///</summary>
    ///<typeparam name="MemoryType">
    ///This parameter of IMemoryType represents memory reaction to multiple R/W attempts
    ///</typeparam>
    public class PRAMMachine<MemoryType> : IPRAMMachine where MemoryType : IMemoryType, new() {
        #region ClassMembers
        private PRAMState state;
        //All processors forming the machine
        private List<Processor> processors;
        //Memory available to processors
        private PRAM<MemoryType> memory;
        //How many cycles machine has performed up to date (starting from 0)
        private int tickCount;
        //Whether or not whole machine is stopped
        private bool isStopped;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of PRAM machine
        /// </summary>
        /// <param name="processors">List of processors that will be used by machine</param>
        /// <param name="memory">Machines memory</param>
        public PRAMMachine(List<Processor> processors, PRAM<MemoryType> memory) {
            this.state = PRAMState.Reading;
            this.tickCount = 0;
            this.processors = processors;
            for (int i = 0; i < processors.Count; i++) {
                processors[i].Number = i;
            }
            this.memory = memory;
            this.isStopped = false;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method that executes single cycle of entire machine
        /// 1. All processors read data
        /// 2. All processors execute computations
        /// 3. All processors write their results
        /// </summary>
        /// <returns>Returns true if there are any working processors and false if all of them are stopped</returns>
        private bool Tick() {
            memory.ClearRequests();
            int workingProcessorsCount;
            List<RWRequest> readRequests = GetReadRequests();
            List<RWRequest> writeRequests = new List<RWRequest>();
            //Step 1. We post read requests to memory
            state = PRAMState.Reading;
            this.memory.PostReadRequests(readRequests);
            //Step 2. Now all processors are called with data they have read or null if they havent posted any new reqeuests
            state = PRAMState.Processing;
            workingProcessorsCount = this.RunProcessors(readRequests);
            //Step 3. We now collect all write requests from processors
            state = PRAMState.Writing;
            writeRequests = this.GetWriteRequests();
            //Step 4. And post them to memory
            memory.PostWriteRequests(writeRequests);
            //Step 5. Now we write data to memory
            foreach (RWRequest request in writeRequests) {
                memory.WriteData(request);
            }
            tickCount++;
            if (workingProcessorsCount > 0) {
                return true;
            }
            this.isStopped = true;
            return false;
        }

        /// <summary>
        /// This method performs consecutively each step of PRAM machine workcycle
        /// </summary>
        /// <returns>true if machine is working and false otherwise</returns>
        public bool Step() {
            memory.ClearRequests();
            if (state == PRAMState.Reading) {
                state = PRAMState.Processing;
                Read();
                return true;
            }
            if (state == PRAMState.Processing) {
                state = PRAMState.Writing;
                int workingProcessorsCount = Process(GetReadRequests());
                if (workingProcessorsCount > 0) {
                    return true;
                }
                // When machine stops all remaining write requests are processed
                Write();
                this.isStopped = true;
                return false;
            }
            state = PRAMState.Reading;
            Write();
            tickCount++;
            return true;
        }

        /// <summary>
        /// This method performs first (reading data) step of PRAM machine workcycle
        /// </summary>
        private void Read() {
            List<RWRequest> readRequests = GetReadRequests();
            //Step 1. We post read requests to memory
            this.memory.PostReadRequests(readRequests);
        }

        /// <summary>
        /// This method performs second (processing) step of PRAM machine workcycle
        /// </summary>
        private int Process(List<RWRequest> readRequests) {
            //Step 2. Now all processors are called with data they have read or null if they havent posted any new reqeuests
            return this.RunProcessors(readRequests);
        }

        /// <summary>
        /// This method performs third (writing results) step of PRAM machine workcycle
        /// </summary>
        private void Write() {
            //Step 3. We now collect all write requests from processors
            List<RWRequest> writeRequests = this.GetWriteRequests();
            //Step 4. And post them to memory
            memory.PostWriteRequests(writeRequests);
            //Step 5. Now we write data to memory
            foreach (RWRequest request in writeRequests) {
                memory.WriteData(request);
            }
        }

        /// <summary>
        /// This method runs all processors with data they requested
        /// </summary>
        /// <param name="readRequests">All data requests from this cycle</param>
        /// <returns>Returns number of working processors</returns>
        private int RunProcessors(List<RWRequest> readRequests) {
            int workingProcessorsCount = processors.Count;
            foreach (Processor processor in processors) {
                if (!processor.IsStopped) {
                    // TODO: sprawdzić czy nie da się tego ifa rozwiązać lepiej np. !processor.ReadRequest.Empty
                    if (readRequests.Any(request => request.From == processor.Number)) {
                        RWRequest req = readRequests.First(request => request.From == processor.Number);
                        processor.Execute(memory.ReadData(req));
                    } else {
                        processor.Execute(null);
                    }
                    //If it was the las operation processor executed
                    if (processor.IsStopped) {
                        workingProcessorsCount--;
                    }
                } else {
                    //The processor allready managed to write its data co we stop it from trying that again
                    processor.ClearDataToWrite();
                    workingProcessorsCount--;
                }
            }
            return workingProcessorsCount;
        }

        /// <summary>
        /// Collects all write requests from all processors
        /// </summary>
        /// <returns>Returns list of all write requests in this cycle</returns>
        private List<RWRequest> GetWriteRequests() {
            List<RWRequest> requests = new List<RWRequest>();
            foreach (Processor processor in processors) {
                if (!processor.WriteRequest.Empty) {
                    requests.Add(processor.WriteRequest);
                }
            }
            return requests;
        }

        /// <summary>
        /// Collects all read requests from all processors
        /// </summary>
        /// <returns>Returns list of all read requests in this cycle</returns>
        private List<RWRequest> GetReadRequests() {
            List<RWRequest> requests = new List<RWRequest>();
            foreach (Processor processor in processors) {
                if (!processor.ReadRequest.Empty) {
                    requests.Add(processor.ReadRequest);
                }
            }
            return requests;
        }

        /// <summary>
        /// Main method of the machine, which loops tick method untill all processors finish their calculations
        /// </summary>
        public void Start() {
            while (Tick());
        }
        #endregion

        #region Properties
        /// <summary>
        /// Read only property that returns number of ticks/cycles machine has performed (starting from 0)
        /// </summary>
        public int TickCount {
            get { return tickCount; }
        }

        /// <summary>
        /// Read only property that returns number of read operations performed by all processors
        /// </summary>
        public int ReadCount {
            get { return memory.ReadCount; }
        }

        /// <summary>
        /// Read only property that returns number of write operations performed by all processors
        /// </summary>
        public int WriteCount {
            get { return memory.WriteCount; }
        }

        /// <summary>
        /// Read only property that returns data stored in machine memory (remember that it's only data not the memory state)
        /// </summary>
        public Dictionary<string, List<dynamic>> Memory {
            get { return memory.DataRows; }
        }

        /// <summary>
        /// This read only property returns PRAM Machine model, representing its current state
        /// </summary>
        public PRAMMachineModel Model {
            get { 
                List<ProcessorModel> processorModels = new List<ProcessorModel>();
                foreach (Processor processor in processors) {
                    processorModels.Add(processor.Model);
                }
                int actualReadCount = 0;
                if (State == PRAMState.Reading) {
                    actualReadCount = processors.Count(p => !p.DataToRead.Empty);
                }
                int actualWriteCount = 0;
                if (State == PRAMState.Writing) {
                    actualWriteCount = processors.Count(p => !p.DataToWrite.Empty);
                }
                PRAMMachineModel model = new PRAMMachineModel(processorModels, memory.Model, TickCount, State, 
                                                              isStopped, actualReadCount, actualWriteCount);
                return model;
            }
        }

        /// <summary>
        /// This readonly property stores current state of PRAM machine
        /// </summary>
        public PRAMState State {
            get { return state; }
        }

        /// <summary>
        /// Read only property indicating whether or not the whole machine is stopped
        /// </summary>
        public bool IsStopped {
            get { return isStopped; }
        }
        #endregion
    }

    /// <summary>
    /// All states of pram machine work cycle
    /// </summary>
    public enum PRAMState { Reading, Processing, Writing };
}
