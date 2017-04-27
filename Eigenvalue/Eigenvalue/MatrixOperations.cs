namespace Eigenvalue
{
    class MatrixOperations
    {
        private int _sizeMatrix;
        private ComplexNumbers[,] _matrix;

        public int Size
        {
            get { return _sizeMatrix; }
            set { _sizeMatrix = value; }
        }

        public ComplexNumbers[,] Matrix_
        {
            get { return _matrix; }
            set { _matrix = value; }
        }

        public MatrixOperations(int size, ComplexNumbers[,] matrix)
        {
            _sizeMatrix = size;
            _matrix = matrix;
        }
        
        // Умножает матрицу на вектор столбец
        public ComplexNumbers[] Multiplication(ComplexNumbers[] vector)
        {
            ComplexNumbers[] newVector = new ComplexNumbers[_sizeMatrix];

            for (int i = 0; i < _sizeMatrix; i++)
                newVector[i] = new ComplexNumbers(0, 0);

            for (int i = 0; i < _sizeMatrix; i++)
                for (int j = 0; j < _sizeMatrix; j++) 
                    newVector[i] += _matrix[i, j] * vector[j];

            return newVector;
        }

        // Печать матрицы
        public string Print()
        {
            string matrixLine = "";

            for (int i = 0; i < _sizeMatrix; i++)
            {
                for (int j = 0; j < _sizeMatrix; j++)
                    matrixLine += _matrix[i, j].Print() + "\t";
                matrixLine += "\r\n";
            }

            return matrixLine;
        }
    }
}
