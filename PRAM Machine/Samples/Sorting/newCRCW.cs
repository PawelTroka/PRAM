using System.Collections.Generic;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.Sorting
{
    internal class newCRCW : MemoryTypes.CRCW
    {
        public override dynamic writeReaction(List<RWRequest> requests, RWRequest currentRequest, dynamic cellData)
        {
            return cellData + currentRequest.Data;
        }
    }
}