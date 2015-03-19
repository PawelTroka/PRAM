using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRAM_Machine.Memory {
    /// <summary>
    /// Class containing four basic memory types that can be used as base classes for more complex reactions.
    /// </summary>
    public class MemoryTypes {
        /// <summary>
        /// Type of memory allowing concurrent reads and concurrent writes,
        /// although data from multiple writes will only be written if they all want to write the same value.
        /// </summary>
        public class CRCW : IMemoryType {
            public virtual dynamic readReaction(List<RWRequest> requests, RWRequest currentRequest, dynamic cellData) {
                return cellData;
            }

            public virtual dynamic writeReaction(List<RWRequest> requests, RWRequest currentRequest, dynamic cellData) {
                if (requests.All(request => request.Data == currentRequest.Data)) {
                    // If all requests are the same we allow them to write
                    return currentRequest.Data;
                }
                // Otherwise data remains unchanged
                return cellData;
            }
        }

        /// <summary>
        /// Type of memory with concurrent reads and exclusive writes,
        /// </summary>
        public class CREW : IMemoryType {
            public virtual dynamic readReaction(List<RWRequest> requests, RWRequest currentRequest, dynamic cellData) {
                return cellData;
            }

            public virtual dynamic writeReaction(List<RWRequest> requests, RWRequest currentRequest, dynamic cellData) {
                if (requests.Count > 1) {
                    throw new ForbiddenMemoryOperationException();
                }
                return currentRequest.Data;
            }
        }

        /// <summary>
        /// Type of memory with exclusive reads and concurrent writes,
        /// although data from multiple writes will only be written if they all want to write the same value.
        /// </summary>
        public class ERCW : IMemoryType {
            public virtual dynamic readReaction(List<RWRequest> requests, RWRequest currentRequest, dynamic cellData) {
                if (requests.Count > 1) {
                    throw new ForbiddenMemoryOperationException();
                }
                return cellData;
            }

            public virtual dynamic writeReaction(List<RWRequest> requests, RWRequest currentRequest, dynamic cellData) {
                if (requests.All(request => request.Data == currentRequest.Data)) {
                    // If all requests are the same we allow them to write
                    return currentRequest.Data;
                }
                // Otherwise data remains unchanged
                return cellData;
            }
        }

        /// <summary>
        /// Type of memory with exclusive reads and exclusive writes,
        /// </summary>
        public class EREW : IMemoryType {
            public virtual dynamic readReaction(List<RWRequest> requests, RWRequest currentRequest, dynamic cellData) {
                if (requests.Count > 1) {
                    throw new ForbiddenMemoryOperationException();
                }
                return cellData;
            }

            public virtual dynamic writeReaction(List<RWRequest> requests, RWRequest currentRequest, dynamic cellData) {
                if (requests.Count > 1) {
                    throw new ForbiddenMemoryOperationException();
                }
                return currentRequest.Data;
            }
        }
    }

    /// <summary>
    /// Exception that is thrown when forbidden memory operation is performed.
    /// </summary>
    public class ForbiddenMemoryOperationException : Exception {
        public ForbiddenMemoryOperationException() { }
    }
}
