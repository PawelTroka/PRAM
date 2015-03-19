using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.LogicalAnd
{
    internal class LogicalAndProcessor : Processor
    {
        public LogicalAndProcessor(int number)
        {
            Number = number;
            DataToRead = new MemoryAddress("data", Number);
        }

        public override dynamic Run(dynamic data)
        {
            if (data == 0)
            {
                DataToWrite = new MemoryAddress("result", 0);
            }
            Stop();
            return 0;
        }
    }
}