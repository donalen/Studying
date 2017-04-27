using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eigenvalue
{
    class IterativeMethod
    {
        private double _h = 0.00001;
        private int _sizeMatrix;
        private ComplexNumbers[] _vector;
        private MatrixOperations _matrix;

        // Заполнение матрицы
        public IterativeMethod(int size, MatrixOperations Matrix)
        {
            _sizeMatrix = size;
            _matrix = Matrix;
            _vector = new ComplexNumbers[size];
            for (int i = 0; i < size; i++)
                _vector[i] = new ComplexNumbers(1, 0);
        }
        
        // Скалярного произведение двух векторов
        private ComplexNumbers ScalarProduct(ComplexNumbers[] vec1, ComplexNumbers[] vec2)
        {
            ComplexNumbers newVec = new ComplexNumbers(0, 0);

            for (int i = 0; i < _sizeMatrix; i++)
                newVec += vec1[i] * vec2[i];
            return newVec;
        }

        /// <summary>
        /// Метод в котором вызываются методы для поиска собственнных значений
        /// </summary>
        /// <returns>найденное собственное значение</returns>
        public ComplexNumbers Method_()
        {
            return ComplexEigenvalue();
        }

        /// <summary>
        /// Метод ищет комплексное значение и если не находит то вызывает 
        /// метод для поиска действительного собственного значения 
        /// </summary>
        /// <returns>найденное собственное значение (если оно комплексно)</returns>
        private ComplexNumbers ComplexEigenvalue()
        {
            ComplexNumbers p = new ComplexNumbers(0, 0);
            ComplexNumbers pNext = new ComplexNumbers(0, 0);
            ComplexNumbers q = new ComplexNumbers(0, 0);
            ComplexNumbers qNext = new ComplexNumbers(0, 0);

            ComplexNumbers[] vector = _vector;
            ComplexNumbers[] nextVector = _matrix.Multiplication(vector);
            ComplexNumbers[] nextNextVector = _matrix.Multiplication(nextVector);
            ComplexNumbers[] nextNextNextVector = _matrix.Multiplication(nextNextVector);
            while ((p - pNext).Abc() < _h)
            {   
                p = (vector[0] * nextNextNextVector[0] - nextVector[0] * nextNextVector[0]) /
                    (vector[0] * nextNextVector[0] - nextVector[0] * nextVector[0]);
                vector = nextVector;
                nextVector = nextNextVector;
                nextNextVector = nextNextNextVector;
                nextNextNextVector = _matrix.Multiplication(nextNextVector);
                pNext = (vector[0] * nextNextNextVector[0] - nextVector[0] * nextNextVector[0]) /
                   (vector[0] * nextNextVector[0] - nextVector[0] * nextVector[0]);
            }

            while ((q - qNext).Abc() >= _h)
            {
                q = (nextVector[0] * nextNextNextVector[0] - nextNextVector[0] * nextNextVector[0]) /
                    (vector[0] * nextNextVector[0] - nextVector[0] * nextVector[0]);
                vector = nextVector;
                nextVector = nextNextVector;
                nextNextVector = nextNextNextVector;
                nextNextNextVector = _matrix.Multiplication(nextNextVector);
                qNext = (nextVector[0] * nextNextNextVector[0] - nextNextVector[0] * nextNextVector[0]) /
                    (vector[0] * nextNextVector[0] - nextVector[0] * nextVector[0]);
            }

            if (-(p.Real * p.Real - 4 * q.Real) > 0)
                return new ComplexNumbers(p.Real / 2, Math.Sqrt(-(p.Real * p.Real - 4 * q.Real)) / 2);
            else
                return RealEigenvalue();

        }
        /// <summary>
        /// Метод ищет действительное значение 
        /// </summary>
        /// <returns>найденное собственное значение (если оно действительно)</returns>
        private ComplexNumbers RealEigenvalue()
        {
            ComplexNumbers eigenvalue;
            ComplexNumbers eigenvalueNext;

            ComplexNumbers[] vector = _vector;
            ComplexNumbers[] nextVector = _matrix.Multiplication(vector);
            vector = _vector;
            nextVector = _matrix.Multiplication(vector);

            for (int i = 0; i < 10000; i++)
            {
                eigenvalue = ScalarProduct(nextVector, vector) / ScalarProduct(vector, vector);
                vector = nextVector;
                nextVector = _matrix.Multiplication(vector);
                eigenvalueNext = ScalarProduct(nextVector, vector) / ScalarProduct(vector, vector);
                if (Math.Abs(eigenvalue.Abc() - eigenvalueNext.Abc()) < _h)
                    return eigenvalue;
            }

            return new ComplexNumbers(0, 0);
        }


    }
}
