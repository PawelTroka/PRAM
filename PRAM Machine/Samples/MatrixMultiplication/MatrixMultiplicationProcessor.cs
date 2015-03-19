using System;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.MatrixMultiplication
{
    internal class MatrixMultiplicationProcessor : Processor
    {
        private int clusterNumber;
        private int column;
        private int num;
        private int row;
        private int size;
        private int sum;
        private int value;

        public MatrixMultiplicationProcessor(int number, int row, int column, int size) : base()
        {
            this.row = row;
            this.column = column;
            num = number;
            this.size = size;
            sum = 0;
            clusterNumber = row*size + column;
            DataToRead = new MemoryAddress("MatrixA", num + row*size);
        }

        public override dynamic Run(dynamic data)
        {
            if (TickCount < 2)
            {
                if (TickCount == 0)
                {
                    value = data;
                    DataToRead = new MemoryAddress("MatrixB", column + num*size);
                    return null;
                }
                else
                {
                    DataToWrite = new MemoryAddress("temp" + clusterNumber.ToString(), num);
                    DataToRead = new MemoryAddress("temp" + clusterNumber.ToString(), num);
                    return value*data;
                }
            }
            else
            {
                if (num + (int) Math.Pow(2, TickCount - 3) < size)
                {
                    sum += data;
                }
                // If first processor should work
                if ((int) Math.Pow(2, TickCount - 2) < size)
                {
                    // If any other processor should work
                    if (num%Math.Pow(2, TickCount - 1) == 0)
                    {
                        if (num + (int) Math.Pow(2, TickCount - 2) < size)
                        {
                            DataToRead = new MemoryAddress("temp" + clusterNumber.ToString(),
                                num + (int) Math.Pow(2, TickCount - 2));
                        }
                        else
                        {
                            DataToRead = new MemoryAddress();
                        }
                        DataToWrite = new MemoryAddress("temp" + clusterNumber.ToString(), num);
                        return sum;
                    }
                    else
                    {
                        Stop();
                        DataToWrite = new MemoryAddress();
                        return sum;
                    }
                }
                else
                {
                    if (num == 0)
                    {
                        DataToWrite = new MemoryAddress("MatrixC", clusterNumber);
                    }
                    Stop();
                    return sum;
                }
            }
        }
    }
}