using System;

namespace Eigenvalue
{
    //Класс для поиска наибольшего по модулю собственного значения
    class IterativeMethod
    {
        double _h = 0.000132; //погрешность
                              // double _h = 0.001;
        int size; //размер матрицы
        private ComplexNumbers[] vector;
        private MatrixOperations matrix;

        // Заносит матрицу для поиска собственного значения
        public IterativeMethod(int size, MatrixOperations Matrix)
        {
            this.size = size;
            this.matrix = Matrix;
            this.vector = new ComplexNumbers[size];
            for (int i = 0; i < size; i++)
                this.vector[i] = new ComplexNumbers(1, 0);
        }

        // Главный метод для поиска собственного значения
        public ComplexNumbers Method_()
        {
            return ComplexNumbersEigenvalue();
        }

        // Нахождение скалярного произведения двух векторов
        private ComplexNumbers ScalarProduct(ComplexNumbers[] vec1, ComplexNumbers[] vec2)
        {
            ComplexNumbers newVec = new ComplexNumbers(0, 0);

            for (int i = 0; i < size; i++)
                newVec += vec1[i] * vec2[i];
            return newVec;
        }

        // Метод находит следующую итерацию для каждого вектора
        private void VectorToNextVector(ref ComplexNumbers[] PreviousVector, ref ComplexNumbers[] Vector, ref ComplexNumbers[] NextVector, ref ComplexNumbers[] Next2Vector)
        {
            PreviousVector = Vector;
            Vector = NextVector;
            NextVector = Next2Vector;
            Next2Vector = this.matrix.Multiplication(NextVector);
        }
        
        // Метод нахождения следующих значений коэффициентов p и q уравнения h^2+ph+q=0 при итерационном вычислении комплексного собственного значения 
        private void EigenvalueIsComplexNumbers(out ComplexNumbers p, out ComplexNumbers q, ComplexNumbers[] PreviousVector, ComplexNumbers[] Vector, ComplexNumbers[] NextVector, ComplexNumbers[] Next2Vector)
        {
            p = (PreviousVector[0] * Next2Vector[0] - Vector[0] * NextVector[0]) /
                   (PreviousVector[0] * NextVector[0] - Vector[0] * Vector[0]);
            q = (Vector[0] * Next2Vector[0] - NextVector[0] * NextVector[0]) /
                (PreviousVector[0] * NextVector[0] - Vector[0] * Vector[0]);
        }

        //  Метод нахождения следующих значений коэффициентов p и q уравнения h^2+ph+q=0 
        //  при итерационном вычислении действительного собственного значения кратности 2
        private void EigenvalueOfMultiplicity2(out ComplexNumbers p, out ComplexNumbers q, ComplexNumbers[] PreviousVector, ComplexNumbers[] Vector, ComplexNumbers[] NextVector)
        {
            p = (PreviousVector[0] * NextVector[1] - PreviousVector[1] * NextVector[0]) /
                        (PreviousVector[0] * Vector[1] - PreviousVector[1] * Vector[0]);//р для кратного действительного значения
            q = (Vector[0] * NextVector[1] - Vector[1] * NextVector[0]) /
                (PreviousVector[0] * Vector[1] - Vector[0] * PreviousVector[1]);//q для кратного действительного значения
        }

        // Метод ищет комплексное значение и если не находит, то вызывает метод для поиска действительного собственного значения 
        private ComplexNumbers ComplexNumbersEigenvalue()
        {
            ComplexNumbers p = new ComplexNumbers(0, 0);
            ComplexNumbers pNext;
            ComplexNumbers q = new ComplexNumbers(0, 0);
            ComplexNumbers qNext;

            ComplexNumbers[] PreviousVector = this.vector;
            ComplexNumbers[] Vector = this.matrix.Multiplication(PreviousVector);
            ComplexNumbers[] NextVector = this.matrix.Multiplication(Vector);
            ComplexNumbers[] Next2Vector = this.matrix.Multiplication(NextVector);
            for (int i = 0; i < 1000; i++)//комплексный случай
            {
                EigenvalueIsComplexNumbers(out p, out q, PreviousVector, Vector, NextVector, NextVector);
                VectorToNextVector(ref PreviousVector, ref Vector, ref NextVector, ref Next2Vector);
                EigenvalueIsComplexNumbers(out pNext, out qNext, PreviousVector, Vector, NextVector, NextVector);
                if ((p - pNext).Abc() < _h && (q - qNext).Abc() < _h)
                    break;
            }
            if (-(p.Real * p.Real - 4 * q.Real) > 0)
                return new ComplexNumbers(p.Real / 2, Math.Sqrt(-(p.Real * p.Real - 4 * q.Real)) / 2);
            else
            {
                for (int i = 0; i < 1000; i++)//действительный случай с кратностью 2
                {
                    EigenvalueOfMultiplicity2(out p, out q, PreviousVector, Vector, NextVector);
                    VectorToNextVector(ref PreviousVector, ref Vector, ref NextVector, ref Next2Vector);
                    EigenvalueOfMultiplicity2(out pNext, out qNext, PreviousVector, Vector, NextVector);
                    if ((p - pNext).Abc() < _h && (q - qNext).Abc() < _h)
                        break;
                }
                if ((p * p / 4 - q).Abc() <= _h)
                {
                    return new ComplexNumbers(p.Real / 2, 0);
                }
                else//действительный случай
                {
                    ComplexNumbers res = new ComplexNumbers(0, 0);
                    res = RealEigenvalue();//действительный случай 
                    return res.Real == 0 ? TMethod() : res;
                }
            }

        }

        // Метод ищет действительное максимальное по модулю собственное значение 
        private ComplexNumbers RealEigenvalue()
        {
            ComplexNumbers value;
            ComplexNumbers nextValue;

            ComplexNumbers[] vector = this.vector;
            ComplexNumbers[] nextVector = this.matrix.Multiplication(vector);
            vector = this.vector;
            nextVector = this.matrix.Multiplication(vector);

            for (int i = 1; i < 1000; i++)
            {
                value = nextVector[0] / vector[0];
                vector = nextVector;
                nextVector = this.matrix.Multiplication(vector);
                nextValue = nextVector[0] / vector[0];
                if ((nextValue - value).Abc() < _h)
                    return nextValue / (int)Math.Pow(-1, i);
            }
            return new ComplexNumbers(0, 0);
        }

        private ComplexNumbers TMethod()
        {
            ComplexNumbers value;
            ComplexNumbers nextValue;
            ComplexNumbers[] prev = this.vector;
            ComplexNumbers[] Vector = this.vector;
            ComplexNumbers[] NextVector = this.matrix.Multiplication(Vector);
            ComplexNumbers[] Next2Vector = this.matrix.Multiplication(NextVector);
            double result, nextResult;
            for (int i = 1; i < 1000; i++)
            {
                value = ScalarProduct(Next2Vector, vector) / ScalarProduct(vector, vector);
                VectorToNextVector(ref prev, ref Vector, ref NextVector, ref Next2Vector);

                nextValue = ScalarProduct(NextVector, vector) / ScalarProduct(vector, vector);

                result = Math.Sqrt(value.Real);
                nextResult = Math.Sqrt(nextValue.Real);

                if (Math.Abs(nextResult - result) < _h)
                {
                    return new ComplexNumbers(nextResult, 0) / (int)Math.Pow(-1, i);
                }
            }
            return new ComplexNumbers(0, 0);
        }
    }
}
