using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples
{
    public class VectorAddingProcessor : Processor
    {
        public dynamic a;
        public dynamic b;

        public override dynamic Run(dynamic data)
        {
            if (TickCount == 0)
            {
                DataToRead = new MemoryAddress("a", Number);
                DataToWrite = new MemoryAddress();
                return null;
            }
            if (TickCount == 1)
            {
                a = data;
                DataToRead = new MemoryAddress("b", Number);
                DataToWrite = new MemoryAddress();
                return null;
            }
            b = data;
            DataToWrite = new MemoryAddress("c", Number);
            Stop();
            return a + b;
        }
    }
}