using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eigenvalue
{
    /// <summary>
    ///Класс для поиска наибольшего по модулю собственного значения
    /// </summary>
    class Method 
    {
        double _h = 0.000132; //погрешность
       // double _h = 0.001;
        int size; //размер матрицы
        private Complex[] vector;
        private Matrix matrix;

        /// <summary>
        /// Заносит матрицу для поиска собственного значения
        /// </summary>
        /// <param name="size">размер матрицы</param>
        /// <param name="Matrix">элементы матрицы</param>
        public Method(int size, Matrix Matrix)
        {
            this.size = size;
            this.matrix = Matrix;
            this.vector = new Complex[size];
            for (int i = 0; i < size; i++)
                this.vector[i] = new Complex(1, 0);
        }

        /// <summary>
        /// Главный метод для поиска собственного значения
        /// </summary>
        /// <returns>найденное собственное значение</returns>
        public Complex MainMethod()
        {
            return ComplexEigenvalue();
        }

        /// <summary>
        /// Нахождение скалярного произведения двух векторов
        /// </summary>
        /// <param name="vec1">первый вектор </param>
        /// <param name="vec2">второй вектор </param>
        /// <returns>скалярное произведение (комплексное число)</returns>
        private Complex ScalarProduct(Complex[] vec1, Complex[] vec2)
        {
            Complex newVec = new Complex(0, 0);

            for (int i = 0; i < size; i++)
                newVec += vec1[i] * vec2[i];
            return newVec;
        }

        /// <summary>
        /// Метод находит следующую итерацию для каждого вектора
        /// </summary>
        /// <param name="PreviousVector"> вектор на предыдущей итерации</param>
        /// <param name="Vector"> вектор на текущей итерации</param>
        /// <param name="NextVector">вектор на следующей итерации</param>
        /// <param name="Next2Vector">вектор через итерацию</param>
        private void VectorToNextVector(ref Complex[] PreviousVector, ref Complex[] Vector, ref Complex[] NextVector, ref Complex[] Next2Vector)
        {
            PreviousVector = Vector;
            Vector = NextVector;
            NextVector = Next2Vector;
            Next2Vector = this.matrix.Multiplication(NextVector);
        }
        /// <summary>
        /// Метод нахождения следующих значений коэффициентов p и q уравнения h^2+ph+q=0 при итерационном вычислении комплексного собственного значения 
        /// </summary>
        /// <param name="p">куда записать коэффициент р</param>
        /// <param name="q">куда записать коэффициент q</param>
        /// <param name="PreviousVector"> вектор на предыдущей итерации</param>
        /// <param name="Vector">вектор на текущей итерации</param>
        /// <param name="NextVector">вектор на следующей итерации</param>
        /// <param name="Next2Vector">вектор через итерацию</param>
        private void EigenvalueIsComplex(out Complex p, out Complex q, Complex[] PreviousVector, Complex[] Vector, Complex[] NextVector, Complex[] Next2Vector)
        {
            p = (PreviousVector[0] * Next2Vector[0] - Vector[0] * NextVector[0]) /
                   (PreviousVector[0] * NextVector[0] - Vector[0] * Vector[0]);
            q = (Vector[0] * Next2Vector[0] - NextVector[0] * NextVector[0]) /
                (PreviousVector[0] * NextVector[0] - Vector[0] * Vector[0]);
        }

        /// <summary>
        ///  Метод нахождения следующих значений коэффициентов p и q уравнения h^2+ph+q=0 
        ///  при итерационном вычислении действительного собственного значения кратности 2
        /// </summary>
        /// <param name="p">куда записать коэффициент р</param>
        /// <param name="q">куда записать коэффициент q</param>
        /// <param name="PreviousVector"> вектор на предыдущей итерации</param>
        /// <param name="Vector">вектор на текущей итерации</param>
        /// <param name="NextVector">вектор на следующей итерации</param>
        private void EigenvalueOfMultiplicity2(out Complex p, out Complex q, Complex[] PreviousVector, Complex[] Vector, Complex[] NextVector)
        {
            p = (PreviousVector[0] * NextVector[1] - PreviousVector[1] * NextVector[0]) /
                        (PreviousVector[0] * Vector[1] - PreviousVector[1] * Vector[0]);//р для кратного действительного значения
            q = (Vector[0] * NextVector[1] - Vector[1] * NextVector[0]) /
                (PreviousVector[0] * Vector[1] - Vector[0] * PreviousVector[1]);//q для кратного действительного значения
        }

        /// <summary>
        /// Метод ищет комплексное значение и если не находит, то вызывает метод для поиска действительного собственного значения 
        /// </summary>
        /// <returns>максимальное по модулю собственное значение</returns>
        private Complex ComplexEigenvalue()
        {
            Complex p = new Complex(0, 0);
            Complex pNext;
            Complex q = new Complex(0, 0);
            Complex qNext;

            Complex[] PreviousVector = this.vector;
            Complex[] Vector = this.matrix.Multiplication(PreviousVector);
            Complex[] NextVector = this.matrix.Multiplication(Vector);
            Complex[] Next2Vector = this.matrix.Multiplication(NextVector);
            for (int i = 0; i < 1000; i++)//комплексный случай
            {
                EigenvalueIsComplex(out p, out q, PreviousVector, Vector, NextVector, NextVector);
                VectorToNextVector(ref PreviousVector, ref Vector, ref NextVector, ref Next2Vector);
                EigenvalueIsComplex(out pNext, out qNext, PreviousVector, Vector, NextVector, NextVector);
                if ((p - pNext).Abc() < _h && (q - qNext).Abc() < _h)
                    break;
            }
            if (-(p.Real * p.Real - 4 * q.Real) > 0)
                return new Complex(p.Real / 2, Math.Sqrt(-(p.Real * p.Real - 4 * q.Real)) / 2);
            else
            {
                for(int i = 0; i<1000; i++)//действительный случай с кратностью 2
                {
                    EigenvalueOfMultiplicity2(out p, out q, PreviousVector, Vector, NextVector);
                    VectorToNextVector(ref PreviousVector, ref Vector, ref NextVector, ref Next2Vector);
                    EigenvalueOfMultiplicity2(out pNext, out qNext, PreviousVector, Vector, NextVector);
                    if ((p - pNext).Abc() < _h && (q - qNext).Abc() < _h)
                        break;
                }
                if ((p * p / 4 - q).Abc() <= _h)
                {
                    return new Complex(p.Real / 2, 0);
                }
                else//действительный случай
                {
                    Complex res = new Complex(0,0);
                    res = RealEigenvalue();//действительный случай 
                    return res.Real == 0 ? TMethod() : res;
                } 
            }

        }

        /// <summary>
        /// / Метод ищет действительное максимальное по модулю собственное значение 
        /// </summary>
        /// <returns> действительное максимальное по модулю собственное значение </returns>
        private Complex RealEigenvalue()    
        {
            Complex value;
            Complex nextValue;

            Complex[] vector = this.vector;
            Complex[] nextVector = this.matrix.Multiplication(vector);
            vector = this.vector;
            nextVector = this.matrix.Multiplication(vector);

            for (int i = 1; i < 1000; i++)
            {
                /*value = ScalarProduct(nextVector, vector) / ScalarProduct(vector, vector);
                vector = nextVector;
                nextVector = this.matrix.Multiplication(vector);
                nextValue = ScalarProduct(nextVector, vector) / ScalarProduct(vector, vector);*/

                value = nextVector[0] / vector[0];
                vector = nextVector;
                nextVector = this.matrix.Multiplication(vector);
                nextValue = nextVector[0] / vector[0];
                if ((nextValue - value).Abc() < _h)
                    return nextValue / (int)Math.Pow(-1, i);
            }
            return new Complex(0, 0);
        }

        private Complex TMethod()
        {
            Complex value;
            Complex nextValue;
            Complex [] prev = this.vector;
            Complex[] Vector = this.vector;
            Complex[] NextVector = this.matrix.Multiplication(Vector);
            Complex[] Next2Vector = this.matrix.Multiplication(NextVector);
            double resault, nextResault;
            for (int i = 1; i < 1000; i++)
            {
                value = ScalarProduct(Next2Vector, vector) / ScalarProduct(vector, vector);
                //vector = NextVector;
                //NextVector = this.matrix.Multiplication(vector);
                VectorToNextVector(ref prev, ref Vector, ref NextVector, ref Next2Vector);

                nextValue = ScalarProduct(NextVector, vector) / ScalarProduct(vector, vector);

                resault = Math.Sqrt(value.Real);
                nextResault = Math.Sqrt(nextValue.Real);

                if (Math.Abs(nextResault - resault) < _h)
                {
                    //MessageBox.Show(i.ToString());
                    return new Complex(nextResault, 0) / (int)Math.Pow(-1, i);
                }
            }
            return new Complex(0,0);
        }
    }
}
