using System.Collections.Generic;

namespace PRAM_Machine.Memory
{
    /// <summary>
    ///     Class representing single memory cell of PRAM
    /// </summary>
    /// <typeparam name="MemoryType">Memory type parameter indicates cell reaction to R/W operations</typeparam>
    internal class MemoryCell<MemoryType> where MemoryType : IMemoryType, new()
    {
        #region ClassMembers

        //Data stored by cell
        //Instance of memory type used to analyze R/W requests
        private readonly IMemoryType memoryType;
        //List of all read requests in current cycle
        private readonly List<RWRequest> readRequests;
        //List of all write requests in current cycle
        private readonly List<RWRequest> writeRequests;
        private dynamic data;
        //Number of read operations performed on cell
        private int readCount;
        //Number of write operations performed on cell
        private int writeCount;

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructs memory cell initialized with data.
        /// </summary>
        /// <param name="data">Data that will be initially contained in memory cell</param>
        public MemoryCell(dynamic data) : this()
        {
            this.data = data;
        }

        /// <summary>
        ///     Initializes empty memory cell with data property set to null.
        /// </summary>
        public MemoryCell()
        {
            data = null;
            memoryType = new MemoryType();
            readCount = 0;
            writeCount = 0;
            readRequests = new List<RWRequest>();
            writeRequests = new List<RWRequest>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     This method is for analyzing single read request, after all read requests have been posted.
        /// </summary>
        /// <param name="request">Request that will be analyzed</param>
        /// <returns>Result of processing this request (usually it will be data stored in cell)</returns>
        public dynamic ReadData(RWRequest request)
        {
            readCount++;
            return memoryType.readReaction(readRequests, request, data);
        }

        /// <summary>
        ///     This is method for analyzing single write request, after all write requests have been posted.
        /// </summary>
        /// <param name="request">Request that will be analyzed</param>
        public void WriteData(RWRequest request)
        {
            writeCount++;
            data = memoryType.writeReaction(writeRequests, request, data);
        }

        /// <summary>
        ///     This method posts a read request to the cell.
        ///     All requests from a cycle are posted before they get to be analyzed.
        /// </summary>
        /// <param name="request">Posted request</param>
        public void PostReadRequest(RWRequest request)
        {
            readRequests.Add(request);
        }

        /// <summary>
        ///     This method posts a write request to the cell.
        ///     All requests from a cycle are posted before they get to be analyzed.
        /// </summary>
        /// <param name="request">Posted request</param>
        public void PostWriteRequest(RWRequest request)
        {
            writeRequests.Add(request);
        }

        /// <summary>
        ///     This method is called at start of the cycle and clears requests from former cycle.
        /// </summary>
        public void ClearRequests()
        {
            readRequests.Clear();
            writeRequests.Clear();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Read only property indicating number of read operations performed on a cell.
        /// </summary>
        public int ReadCount
        {
            get { return readCount; }
        }

        /// <summary>
        ///     Read only property indicating number of write operations performed on a cell.
        /// </summary>
        public int WriteCount
        {
            get { return writeCount; }
        }

        /// <summary>
        ///     This read only property gets data from cell. It is only used by PRAM machine method to return memory state.
        /// </summary>
        public dynamic ShowData
        {
            get { return data; }
        }

        #endregion
    }
}