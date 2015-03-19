using System;

namespace PRAM_Machine.ComplexDataTypes
{
    public class Matrix<T>
    {
        private readonly T[,] _data;

        public Matrix()
        {
        }

        public Matrix(T[,] data)
        {
            _data = data;
        }

        public Matrix(int rows, int columns)
        {
            _data = new T[rows, columns];
        }

        public T this[int row, int column]
        {
            get { return _data[row, column]; }
            set { _data[row, column] = value; }
        }

        public int RowsCount
        {
            get { return _data.GetLength(0); }
        }

        public int ColumnsCount
        {
            get { return _data.GetLength(1); }
        }

        public Type UnderlyingType
        {
            get { return typeof (T); }
        }
    }
}