using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Memory;
using PRAM_Machine.Machine;

namespace PRAM_Machine.Samples.LogicalAnd {
    class LogicalAndProcessor : Processor {
        public LogicalAndProcessor(int number) : base() {
            this.Number = number;
            DataToRead = new MemoryAddress("data", Number);
        }

        public override dynamic Run(dynamic data) {
            if (data == 0) {
                DataToWrite = new MemoryAddress("result", 0);
            }
            Stop();
            return 0;
        }
    }
}
