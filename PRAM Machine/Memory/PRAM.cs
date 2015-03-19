using System;
using System.Collections.Generic;
using System.Linq;
using PRAM_Machine.ComplexDataTypes;
using PRAM_Machine.Models;

namespace PRAM_Machine.Memory
{
    /// <summary>
    ///     Class representing entire PRAM. It contains multiple named rows, each of them containing a number of indexed cells.
    /// </summary>
    /// <typeparam name="MemoryType">Memory type parameter indicates cell reaction to R/W operations</typeparam>
    public class PRAM<MemoryType> where MemoryType : IMemoryType, new()
    {
        #region ClassMembers

        //Dictionary containing rows indexed with row names.
        private readonly Dictionary<string, MemoryCellCollection<MemoryType>> Memory;

        #endregion

        #region Constructors

        /// <summary>
        ///     Build empty PRAM. You have to add memory rows to it.
        /// </summary>
        public PRAM()
        {
            Memory = new Dictionary<string, MemoryCellCollection<MemoryType>>();
        }

        #endregion

        #region Methods

        //  public void AddNamedMemory(string name, int count) {
        //     AddNamedMemory<dynamic>(name, count, (dynamic)null);
        // }
        /// <summary>
        ///     Method that adds named memory row of count cells, each of them initialized with nulls.
        /// </summary>
        /// <param name="name">Name of added row</param>
        /// <param name="count">Number of cells in added row</param>
        /// <summary>
        ///     Method that adds named memory row, containing count cells, each of them initialized with value.
        /// </summary>
        /// <typeparam name="DataType">Type of added data</typeparam>
        /// <param name="name">Name of added row</param>
        /// <param name="count">Number of cells in added row</param>
        /// <param name="value">Value that every added cell will be initialized with</param>
        public void AddNamedMemory<DataType>(string name, int count, DataType value)
        {
            var values = new List<dynamic>();
            for (int i = 0; i < count; i++)
            {
                values.Add((dynamic) value);
            }
            AddNamedMemory(name, values);
        }

        /// <summary>
        ///     Method that adds named memory row, initialized with a list of values.
        ///     There will be a memory cell for each element of initialization list.
        /// </summary>
        /// <typeparam name="DataType">Type of added data</typeparam>
        /// <param name="name">Name of added row</param>
        /// <param name="values">List containing values to initialize row</param>
        public void AddNamedMemory<DataType>(string name, List<DataType> values) where DataType : new()
        {
            var cells = new MemoryCellCollection<MemoryType>();
            cells.AddRange(values.Select(data => (dynamic) data).Select(value => new MemoryCell<MemoryType>(value)));
            Memory.Add(name, cells);
        }

        public void AddNamedMemory<DataType>(string name, DataType value) where DataType : new()
        {
            var cells = new MemoryCellCollection<MemoryType> {Representation = MemoryCellRepresentation.Variable};
            cells.Add(new MemoryCell<MemoryType>(value));

            Memory.Add(name, cells);
        }

        public void AddNamedMemory<DataType>(string name, Matrix<DataType> values) where DataType : new()
        {
            var cells = new MemoryCellCollection<MemoryType>(values.RowsCount,values.ColumnsCount) { Representation = MemoryCellRepresentation.Matrix };
            cells.Add(new MemoryCell<MemoryType>(values));

            Memory.Add(name, cells);
        }


        /*  public void AddNamedMemory<DataType>(string name, Matrix<DataType> values) where DataType : new()
        {
            var cells = new MemoryCellCollection<MemoryType>(values.RowsCount,values.ColumnsCount){Representation = MemoryCellRepresentation.Matrix};       

            for(int i=0;i<values.RowsCount;i++)
                for (int j = 0; j < values.ColumnsCount; j++)
                {
                    var cell = new MemoryCell<MemoryType>(values[i,j]);
                    cells.Add(cell);
                }

            AddNamedMemory(name, values);
            //this.Memory.Add(name, cells);
        }*/

        /// <summary>
        ///     This method posts all read requests to their respective cells.
        /// </summary>
        /// <param name="requests">List of all read requests posted to memory in this cycle</param>
        public void PostReadRequests(List<RWRequest> requests)
        {
            foreach (RWRequest request in requests)
            {
                if (IsInstanceOfGenericType(typeof(Matrix<>),Memory[request.Where.MemoryName].First().ShowData))
                {
                    Memory[request.Where.MemoryName][0].PostReadRequest(request);
                }
                else
                Memory[request.Where.MemoryName][request.Where.Address].PostReadRequest(request);
            }
        }

        public static bool IsInstanceOfGenericType(Type genericType, object instance)
        {
            Type type = instance.GetType();
            while (type != null)
            {
                if (type.IsGenericType &&
                    type.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }

        /// <summary>
        ///     This method posts all write requests to their respective cells.
        /// </summary>
        /// <param name="requests">List of all write requests posted to memory in this cycle</param>
        public void PostWriteRequests(List<RWRequest> requests)
        {
            foreach (RWRequest request in requests)
            {
                //Memory[request.Where.MemoryName][request.Where.Address].PostWriteRequest(request);
            }
        }

        /// <summary>
        ///     This method reads data from a cell, upon a read request.
        ///     This is performed after all read requests have been posted to cells
        /// </summary>
        /// <param name="request">A read request directed to a specific cell</param>
        /// <returns>Data returned by cell upon analyzing request</returns>
        public dynamic ReadData(RWRequest request)
        {
            return Memory[request.Where.MemoryName][request.Where.Address].ReadData(request);
        }

        /// <summary>
        ///     This method writes data to cell, upon a write request.
        ///     This is performed after all write requests have been posted to cells.
        /// </summary>
        /// <param name="request">A write request directed to a specific cell</param>
        public void WriteData(RWRequest request)
        {
            Memory[request.Where.MemoryName][request.Where.Address].WriteData(request);
        }

        /// <summary>
        ///     This method is called at the start of the cycle,
        ///     and it clears all R/W requests, from previous cycle, in all cells
        /// </summary>
        public void ClearRequests()
        {
            foreach (var row in Memory)
            {
                foreach (var cell in row.Value)
                {
                    cell.ClearRequests();
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Read only property representing number of read operations, performed on all cells in memory.
        /// </summary>
        public int ReadCount
        {
            get
            {
                int count = 0;
                foreach (var row in Memory)
                {
                    foreach (var cell in row.Value)
                    {
                        count += cell.ReadCount;
                    }
                }
                return count;
            }
        }

        /// <summary>
        ///     Read only property representing number of write operations, performed on all cells in memory.
        /// </summary>
        public int WriteCount
        {
            get
            {
                int count = 0;
                foreach (var row in Memory)
                {
                    foreach (var cell in row.Value)
                    {
                        count += cell.WriteCount;
                    }
                }
                return count;
            }
        }

        /// <summary>
        ///     This read only property returns raw data contained by the PRAM
        /// </summary>
        public Dictionary<string, List<dynamic>> DataRows
        {
            get
            {
                var dict = new Dictionary<string, List<dynamic>>();
                foreach (var row in Memory)
                {
                    dict.Add(row.Key, new List<dynamic>());
                    foreach (var cell in row.Value)
                    {
                        dict[row.Key].Add(cell.ShowData);
                    }
                }
                return dict;
            }
        }

        /// <summary>
        ///     This read only property returns data model of memory contained by the PRAM
        /// </summary>
        public PRAMModel Model
        {
            get
            {
                var model = new PRAMModel();
                foreach (var row in Memory)
                {
                    model.Add(row.Key, new List<dynamic>());
                    foreach (var cell in row.Value)
                    {
                        model[row.Key].Add(cell.ShowData);
                    }
                }
                return model;
            }
        }

        #endregion
    }
}