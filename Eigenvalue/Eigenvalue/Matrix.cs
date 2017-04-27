using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eigenvalue
{
    // Класс для создания и работы с матрицами
    class Matrix
    {
        private int size;
        private Complex[,] matrix;

        public int Size // Возвращает и задаёт размер матрицы
        {
            get { return size; }
            set { size = value; }
        }
             
        public Complex[,] Matrix_ // Возвращает и задат матрицу
        {
            get { return matrix; }
            set { matrix = value; }
        }

        public Matrix(int size, Complex[,] matrix) // Коструктор
        {
            this.size = size;
            this.matrix = matrix;
        }

        public override string ToString()  // Представление матрицы в виде строки
        {
            string str = "";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    str += matrix[i, j].ToString() + "\t";
            str += "\r\n";
            }

            return str;
        }

        public Complex[] Multiplication(Complex[] vector)//умножение матрицы на вектор
        {
            Complex[] result = new Complex[size];
            for (int i = 0; i < size; i++)
                result[i] = new Complex(0, 0);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++) 
                    result[i] += matrix[i, j] * vector[j];
            return result;
        }

    }
}
