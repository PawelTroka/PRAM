using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.Sorting {
    class newCRCW : MemoryTypes.CRCW {
        public override dynamic writeReaction(List<RWRequest> requests, RWRequest currentRequest, dynamic cellData) {
            return cellData + currentRequest.Data;
        }
    }
}
