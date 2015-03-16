using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRAM_Machine.Memory {
    /// <summary>
    /// Interface that contains memory cell reactions to R/W operations
    /// </summary>
    public interface IMemoryType {
        /// <summary>
        /// This interface member describes memory cell reaction to read requests.
        /// </summary>
        /// <param name="requests">It's a list of all requests to this cell in current cycle</param>
        /// <param name="currentRequest">This is currently analyzed request</param>
        /// <param name="cellData">This is data contained by the cell</param>
        /// <returns>The data that will be returned upon request</returns>
        dynamic readReaction(List<RWRequest> requests, RWRequest currentRequest, dynamic cellData);

        /// <summary>
        /// This interface member describes memory cell reaction to write requests.
        /// </summary>
        /// <param name="requests">It's a list of all requests to this cell in current cycle</param>
        /// <param name="currentRequest">This is currently analyzed request</param>
        /// <param name="cellData">This is data contained by the cell</param>
        /// <returns>The data that will be stored in cell</returns>
        dynamic writeReaction(List<RWRequest> requests, RWRequest currentRequest, dynamic cellData);
    }
}
