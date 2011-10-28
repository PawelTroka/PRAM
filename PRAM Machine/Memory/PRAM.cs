using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Models;

namespace PRAM_Machine.Memory {
    /// <summary>
    /// Class representing entire PRAM. It contains multiple named rows, each of them containing a number of indexed cells.
    /// </summary>
    /// <typeparam name="MemoryType">Memory type parameter indicates cell reaction to R/W operations</typeparam>
    public class PRAM<MemoryType> where MemoryType : IMemoryType, new() {
        #region ClassMembers
        //Dictionary containing rows indexed with row names.
        private Dictionary<string, List<MemoryCell<MemoryType>>> Memory;
        #endregion

        #region Constructors
        /// <summary>
        /// Build empty PRAM. You have to add memory rows to it.
        /// </summary>
        public PRAM() {
            Memory = new Dictionary<string, List<MemoryCell<MemoryType>>>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method that adds named memory row of count cells, each of them initialized with nulls.
        /// </summary>
        /// <param name="name">Name of added row</param>
        /// <param name="count">Number of cells in added row</param>
        public void AddNamedMemory(string name, int count) {
            AddNamedMemory<dynamic>(name, count, (dynamic)null);
        }

        /// <summary>
        /// Method that adds named memory row, containing count cells, each of them initialized with value.
        /// </summary>
        /// <typeparam name="DataType">Type of added data</typeparam>
        /// <param name="name">Name of added row</param>
        /// <param name="count">Number of cells in added row</param>
        /// <param name="value">Value that every added cell will be initialized with</param>
        public void AddNamedMemory<DataType>(string name, int count, DataType value) {
            List<dynamic> values = new List<dynamic>();
            for (int i = 0; i < count; i++) {
                values.Add((dynamic)value);
            }
            this.AddNamedMemory(name, values);
        }

        /// <summary>
        /// Method that adds named memory row, initialized with a list of values.
        /// There will be a memory cell for each element of initialization list.
        /// </summary>
        /// <typeparam name="DataType">Type of added data</typeparam>
        /// <param name="name">Name of added row</param>
        /// <param name="values">List containing values to initialize row</param>
        public void AddNamedMemory<DataType>(string name, List<DataType> values) where DataType : new() {
            List<MemoryCell<MemoryType>> cells = new List<MemoryCell<MemoryType>>();
            foreach (DataType data in values) {
                dynamic value = (dynamic)data;
                MemoryCell<MemoryType> cell = new MemoryCell<MemoryType>(value);
                cells.Add(cell);
            }
            this.Memory.Add(name, cells);
        }

        /// <summary>
        /// This method posts all read requests to their respective cells.
        /// </summary>
        /// <param name="requests">List of all read requests posted to memory in this cycle</param>
        public void PostReadRequests(List<RWRequest> requests) {
            foreach (RWRequest request in requests) {
                Memory[request.Where.MemoryName][request.Where.Address].PostReadRequest(request);
            }
        }

        /// <summary>
        /// This method posts all write requests to their respective cells.
        /// </summary>
        /// <param name="requests">List of all write requests posted to memory in this cycle</param>
        public void PostWriteRequests(List<RWRequest> requests) {
            foreach (RWRequest request in requests) {
                //Memory[request.Where.MemoryName][request.Where.Address].PostWriteRequest(request);
            }
        }

        /// <summary>
        /// This method reads data from a cell, upon a read request. 
        /// This is performed after all read requests have been posted to cells
        /// </summary>
        /// <param name="request">A read request directed to a specific cell</param>
        /// <returns>Data returned by cell upon analyzing request</returns>
        public dynamic ReadData(RWRequest request) {
            return Memory[request.Where.MemoryName][request.Where.Address].ReadData(request);
        }

        /// <summary>
        /// This method writes data to cell, upon a write request.
        /// This is performed after all write requests have been posted to cells.
        /// </summary>
        /// <param name="request">A write request directed to a specific cell</param>
        public void WriteData(RWRequest request) {
            Memory[request.Where.MemoryName][request.Where.Address].WriteData(request);
        }

        /// <summary>
        /// This method is called at the start of the cycle, 
        /// and it clears all R/W requests, from previous cycle, in all cells
        /// </summary>
        public void ClearRequests() {
            foreach (var row in Memory) {
                foreach (var cell in row.Value) {
                    cell.ClearRequests();
                }
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Read only property representing number of read operations, performed on all cells in memory.
        /// </summary>
        public int ReadCount {
            get {
                int count = 0;
                foreach (var row in Memory) {
                    foreach (MemoryCell<MemoryType> cell in row.Value) {
                        count += cell.ReadCount;
                    }
                }
                return count;
            }
        }

        /// <summary>
        /// Read only property representing number of write operations, performed on all cells in memory.
        /// </summary>
        public int WriteCount {
            get {
                int count = 0;
                foreach (var row in Memory) {
                    foreach (MemoryCell<MemoryType> cell in row.Value) {
                        count += cell.WriteCount;
                    }
                }
                return count;
            }
        }

        /// <summary>
        /// This read only property returns raw data contained by the PRAM
        /// </summary>
        public Dictionary<string, List<dynamic>> DataRows {
            get {
                Dictionary<string, List<dynamic>> dict = new Dictionary<string,List<dynamic>>();
                foreach (KeyValuePair<string, List<MemoryCell<MemoryType>>> row in this.Memory) {
                    dict.Add(row.Key, new List<dynamic>());
                    foreach (MemoryCell<MemoryType> cell in row.Value) {
                        dict[row.Key].Add(cell.ShowData);
                    }
                }
                return dict;
            }
        }

        /// <summary>
        /// This read only property returns data model of memory contained by the PRAM
        /// </summary>
        public PRAMModel Model {
            get {
                PRAMModel model = new PRAMModel();
                foreach (KeyValuePair<string, List<MemoryCell<MemoryType>>> row in this.Memory) {
                    model.Add(row.Key, new List<dynamic>());
                    foreach (MemoryCell<MemoryType> cell in row.Value) {
                        model[row.Key].Add(cell.ShowData);
                    }
                }
                return model;
            }
        }
        #endregion
    }
}
