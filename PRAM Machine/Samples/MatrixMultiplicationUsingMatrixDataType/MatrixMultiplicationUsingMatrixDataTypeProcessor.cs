using System;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;

namespace PRAM_Machine.Samples.MatrixMultiplicationUsingMatrixDataType {
    class MatrixMultiplicationUsingMatrixDataTypeProcessor : Processor {
        private readonly int _row;
        private readonly int _column;
        private readonly int _size;
        
        private int ClusterNumber { get { return _row*_size + _column; } }
        private readonly int _num;

        private int _value;
        private int _sum;

        public MatrixMultiplicationUsingMatrixDataTypeProcessor(int number, int row, int column, int size) : base() {
            this._row = row;
            this._column = column;
            this._num = number;
            this._size = size;
            this._sum = 0;
            this.DataToRead = new MemoryAddress("MatrixA",_row,_num);
        }

        public override dynamic Run(dynamic data)
        {
            if (TickCount < 2)
            {
                if (TickCount == 0)
                {
                    _value = data;
                    DataToRead = new MemoryAddress("MatrixB", _num,_column);
                    return null;
                }
                DataToWrite = new MemoryAddress("temp" + ClusterNumber.ToString(), _num);
                DataToRead = new MemoryAddress("temp" + ClusterNumber.ToString(), _num);
                return _value * data;
            }
            if (_num + (int)Math.Pow(2, TickCount - 3) < _size)
            {
                _sum += data;
            }
            // If first processor should work
            if ((int)Math.Pow(2, TickCount - 2) < _size)
            {
                // If any other processor should work
                if (_num % (int)Math.Pow(2, TickCount - 1) == 0)
                {
                    if (_num + (int)Math.Pow(2, TickCount - 2) < _size)
                    {
                        this.DataToRead = new MemoryAddress("temp" + ClusterNumber.ToString(), 
                            _num + (int)Math.Pow(2, TickCount - 2));
                    }
                    else
                    {
                        this.DataToRead = new MemoryAddress();
                    }
                    this.DataToWrite = new MemoryAddress("temp" + ClusterNumber.ToString(), _num);
                    return _sum;
                }
                Stop();
                this.DataToWrite = new MemoryAddress();
                return _sum;
            }
            if (_num == 0)
            {
                DataToWrite = new MemoryAddress("MatrixC", _row,_column);
            }
            Stop();
            return _sum;
        }
    }
}