using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.MatrixMultiplication {
    class MatrixMultiplicationProcessor : Processor {
        private int row;
        private int column;
        private int size;
        private int value;
        private int clusterNumber;
        private int num;
        private int sum;

        public MatrixMultiplicationProcessor(int number, int row, int column, int size) : base() {
            this.row = row;
            this.column = column;
            this.num = number;
            this.size = size;
            this.sum = 0;
            this.clusterNumber = row * size + column;
            this.DataToRead = new MemoryAddress("MatrixA", num + row * size);
        }

        public override dynamic Run(dynamic data) {
            if (TickCount < 2) {
                if (TickCount == 0) {
                    value = data;
                    DataToRead = new MemoryAddress("MatrixB", column + num * size);
                    return null;
                } else {
                    DataToWrite = new MemoryAddress("temp" + clusterNumber.ToString(), num);
                    DataToRead = new MemoryAddress("temp" + clusterNumber.ToString(), num);
                    return value * data;
                }
            } else {
                if (num + (int)Math.Pow(2, TickCount - 3) < size) {
                    sum += data;
                }
                // If first processor should work
                if ((int)Math.Pow(2, TickCount - 2) < size) {
                    // If any other processor should work
                    if (num % Math.Pow(2, TickCount - 1) == 0) {
                        if (num + (int)Math.Pow(2, TickCount - 2) < size) {
                            this.DataToRead = new MemoryAddress("temp" + clusterNumber.ToString(), 
                                                                num + (int)Math.Pow(2, TickCount - 2));
                        } else {
                            this.DataToRead = new MemoryAddress();
                        }
                        this.DataToWrite = new MemoryAddress("temp" + clusterNumber.ToString(), num);
                        return sum;
                    } else {
                        Stop();
                        this.DataToWrite = new MemoryAddress();
                        return sum;
                    }
                } else {
                    if (num == 0) {
                        DataToWrite = new MemoryAddress("MatrixC", clusterNumber);
                    }
                    Stop();
                    return sum;
                }   
            }
        }
    }
}