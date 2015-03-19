using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.ListRanking {
    class ListRankingProcessor : Processor {
        private int rank;
        private int pointer;

        public ListRankingProcessor(int number) : base(number) {
            this.DataToRead = new MemoryAddress("next", number);
            this.rank = 0;
        }

        public override dynamic Run(dynamic data) {
            if (TickCount == 0) {
                pointer = data;
                if (data != -1) {
                    rank = 1;
                }
                DataToRead = new MemoryAddress();
                DataToWrite = new MemoryAddress("ranks", Number);
                return rank;
            } else if (TickCount == 1) {
                if (pointer != -1) {
                    DataToRead = new MemoryAddress("ranks", pointer);
                } else {
                    Stop();
                }
                DataToWrite = new MemoryAddress("pointers", Number);
                return pointer;
            } else if (TickCount % 2 == 0) {
                rank += data;
                DataToWrite = new MemoryAddress("ranks", Number);
                DataToRead = new MemoryAddress("pointers", pointer);
                return rank;
            } else {
                pointer = data;
                if (pointer != -1) {
                    DataToRead = new MemoryAddress("ranks", pointer);
                } else {
                    Stop();
                }
                DataToWrite = new MemoryAddress("pointers", Number);
                return pointer;
            }
        }
    }
}
