using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.ListRanking
{
    internal class ListRankingProcessor : Processor
    {
        private int pointer;
        private int rank;

        public ListRankingProcessor(int number) : base(number)
        {
            DataToRead = new MemoryAddress("next", number);
            rank = 0;
        }

        public override dynamic Run(dynamic data)
        {
            if (TickCount == 0)
            {
                pointer = data;
                if (data != -1)
                {
                    rank = 1;
                }
                DataToRead = new MemoryAddress();
                DataToWrite = new MemoryAddress("ranks", Number);
                return rank;
            }
            if (TickCount == 1)
            {
                if (pointer != -1)
                {
                    DataToRead = new MemoryAddress("ranks", pointer);
                }
                else
                {
                    Stop();
                }
                DataToWrite = new MemoryAddress("pointers", Number);
                return pointer;
            }
            if (TickCount%2 == 0)
            {
                rank += data;
                DataToWrite = new MemoryAddress("ranks", Number);
                DataToRead = new MemoryAddress("pointers", pointer);
                return rank;
            }
            pointer = data;
            if (pointer != -1)
            {
                DataToRead = new MemoryAddress("ranks", pointer);
            }
            else
            {
                Stop();
            }
            DataToWrite = new MemoryAddress("pointers", Number);
            return pointer;
        }
    }
}