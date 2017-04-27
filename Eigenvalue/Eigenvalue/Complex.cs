using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eigenvalue
{
    class Complex // Класс для создания и работы с комплексными числами
    {
        private double re;
        private double im;

        public double Real    //действительная часть
        {
            get { return re; }
            set { re = value; }
        }

        public double Imag // мнимая часть 
        {
            get { return im; }
            set { im = value; }
        }

        public Complex(double real, double imag)//конструктор
        {
            re = real;
            im = imag;
        }

        public Complex (Complex c)
        {
            re = c.Real;
            im = c.Imag;
        }

        public override string ToString()
        {
            if (im == 0)
                return re+"";
            if (re == 0)
                return im + "i";
            
            return re + " + " + im + "i";
        }

        public static Complex operator +(Complex c1, Complex c2) //сложение
        {
            return new Complex(c1.re + c2.re, c1.im + c2.im);
        }

        public static Complex operator -(Complex c1, Complex c2)//вычитание
        {
            return new Complex(c1.re - c2.re, c1.im - c2.im);
        }

        public static Complex operator *(Complex c1, Complex c2)//умножение
        {
            return new Complex(c1.re * c2.re - c1.im * c2.im, c1.re * c2.im + c1.im * c2.re);
        }

        public static Complex operator /(Complex c1, Complex c2)//деление
        {
            return new Complex((c1.re * c2.re + c1.im * c2.im) / (Math.Pow(c2.re, 2) + Math.Pow(c2.im, 2)),
                (c2.re * c1.im - c1.re * c2.im) / (Math.Pow(c2.re, 2) + Math.Pow(c2.im, 2)));
        }

        public static Complex operator /(Complex c1, int c2)// деление на целое число
        {
            return new Complex(c1.Real / c2, c1.Imag / c2);
        }

        public double Abc()//модуль
        {
            return Math.Sqrt(re*re + im*im);
        }
    }
}
