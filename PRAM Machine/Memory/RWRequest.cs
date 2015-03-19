using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRAM_Machine.Memory {
    /// <summary>
    /// This class represents R/W request to a cell
    /// </summary>
    public class RWRequest {
        #region ClassMembers
        //Number of processor that posted request
        private readonly int from;
        //Address of cell that request is directed to
        private readonly MemoryAddress where;
        //Data of write request (its value is not analyzed in read requests)
        private readonly dynamic data;
        //If request is empty the it won't be processed
        private readonly bool empty;
        #endregion

        #region Constructors
        /// <summary>
        /// Builds empty request that won't be processed
        /// </summary>
        public RWRequest() {
            this.empty = true;
        }

        /// <summary>
        /// Builds a request that will be processed
        /// </summary>
        /// <param name="from">Number of processor that sent request</param>
        /// <param name="where">Address of cell that request is directed to</param>
        /// <param name="data">Data of request (it only is analyzed in write requests)</param>
        public RWRequest(int from, MemoryAddress where, dynamic data) {
            this.from = from;
            this.where = where;
            this.data = data;
            this.empty = where.Empty;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Read only property that indicates sender of this request.
        /// </summary>
        public int From {
            get { return from; }
        }

        /// <summary>
        /// Address of memory cell that will receive this request.
        /// </summary>
        public MemoryAddress Where {
            get { return where; }
        }

        /// <summary>
        /// Data of the request (it will only be processed in write requests).
        /// </summary>
        public dynamic Data {
            get { return data; }
        }

        /// <summary>
        /// If this property is true then request won't be processed.
        /// </summary>
        public bool Empty {
            get { return empty; }
        }
        #endregion
    }
}
