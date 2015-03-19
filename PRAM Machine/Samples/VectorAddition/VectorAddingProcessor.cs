using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples {
    public class VectorAddingProcessor : Processor {
        public dynamic a;
        public dynamic b;

        public override dynamic Run(dynamic data) {
            if (this.TickCount == 0) {
                this.DataToRead = new MemoryAddress("a", this.Number);
                this.DataToWrite = new MemoryAddress();
                return null;
            } else if (this.TickCount == 1) {
                this.a = data;
                this.DataToRead = new MemoryAddress("b", this.Number);
                this.DataToWrite = new MemoryAddress();
                return null;
            } else {
                this.b = data;
                this.DataToWrite = new MemoryAddress("c", this.Number);
                this.Stop();
                return a + b;
            }
        }
    }
}
