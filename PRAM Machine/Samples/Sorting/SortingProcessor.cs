using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.Sorting {
    class SortingProcessor : Processor {
        int IndexA;
        int IndexB;
        int Value;
        Stack<int> DataAddresses;
        Stack<dynamic> Values;
        int Count;

        public SortingProcessor(int count, int idxA = 0, int idxB = 0) : base() {
            IndexA = idxA;
            IndexB = idxB;
            DataAddresses = new Stack<int>();
            DataAddresses.Push(IndexB);
            DataAddresses.Push(IndexA);
            Values = new Stack<dynamic>();
            Count = count;

        }

        public override dynamic Run(dynamic data) {
            // Ranking Processor
            if (Number < Count) {
                if (TickCount == 0) {
                    DataToRead = new MemoryAddress("values", Number);
                    return null;
                } else if (TickCount == 1) {
                    Value = data;
                    DataToRead = new MemoryAddress("ranks", Number);
                    return null;
                } else if (TickCount == 3) {
                    DataToWrite = new MemoryAddress("result", data);
                    Stop();
                    return Value;
                }
                return null;
            }
            // Comparing processor
            else {
                Values.Push(data);
                // we still have data to read
                if (DataAddresses.Count != 0) {
                    DataToRead = new MemoryAddress("values", DataAddresses.Pop());
                    return null;
                // we have read all the necessary data
                } else {
                    DataToRead = new MemoryAddress();
                    int a = Values.Pop();
                    int b = Values.Pop();
                    if (a > b) {
                        DataToWrite = new MemoryAddress("ranks", IndexA);
                    } else {
                        DataToWrite = new MemoryAddress("ranks", IndexB);
                    }
                    Stop();
                    return 1;
                }                
            }
        }
    }
}
