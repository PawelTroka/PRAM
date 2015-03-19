using System;
using System.Collections.Generic;

namespace PRAM_Machine.Memory
{
    internal class MemoryCellCollection<MemoryType> : List<MemoryCell<MemoryType>> where MemoryType : IMemoryType, new()
    {
        public MemoryCellCollection()
        {
            Dim = null;
            Representation = MemoryCellRepresentation.List;
        }

        public MemoryCellCollection(params int[] dimensions)
        {
            Dim = dimensions;
            Representation =
                (MemoryCellRepresentation) Enum.ToObject(typeof (MemoryCellRepresentation), dimensions.Length);
        }

        public int[] Dim { get; private set; }

        public MemoryCellRepresentation Representation { get; set; }
    }
}