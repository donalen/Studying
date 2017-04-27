using System;

namespace Eigenvalue
{
    class ComplexNumbers
    {
        private double _realPart;
        private double _imaginaryPart;

        public double Real
        {
            get { return _realPart; }
            set { _realPart = value; }
        }
        
        public double Imag
        {
            get { return _imaginaryPart; }
            set { _imaginaryPart = value; }
        }
        
        public ComplexNumbers(double real, double imag)
        {
            _realPart = real;
            _imaginaryPart = imag;
        }
        
        // Переопределение операции сложения для комплексных чисел
        public static ComplexNumbers operator +(ComplexNumbers c1, ComplexNumbers c2)
        {
            return new ComplexNumbers(c1._realPart + c2._realPart, c1._imaginaryPart + c2._imaginaryPart);
        }
        
        // Переопределение операции вычитания для комплексных чисел
        public static ComplexNumbers operator -(ComplexNumbers c1, ComplexNumbers c2)
        {
            return new ComplexNumbers(c1._realPart - c2._realPart, c1._imaginaryPart - c2._imaginaryPart);
        }
        
        // Переопределение операции умножения для комплексных чисел
        public static ComplexNumbers operator *(ComplexNumbers c1, ComplexNumbers c2)
        {
            return new ComplexNumbers(c1._realPart * c2._realPart - c1._imaginaryPart * c2._imaginaryPart, c1._realPart * c2._imaginaryPart + c1._imaginaryPart * c2._realPart);
        }
        
        // Переопределение операции деления для комплексных чисел
        public static ComplexNumbers operator /(ComplexNumbers c1, ComplexNumbers c2)
        {
            return new ComplexNumbers((c1._realPart * c2._realPart + c1._imaginaryPart * c2._imaginaryPart) / (Math.Pow(c2._realPart, 2) + Math.Pow(c2._imaginaryPart, 2)),
                (c2._realPart * c1._imaginaryPart - c1._realPart * c2._imaginaryPart) / (Math.Pow(c2._realPart, 2) + Math.Pow(c2._imaginaryPart, 2)));
        }
        
        // Возвращает модуль комплексного числа
        public double Abc()
        {
            return Math.Sqrt(Math.Pow(_realPart, 2) + Math.Pow(_imaginaryPart, 2));
        }

        // Печать комплексного числа
        public string Print()
        {
            if (_imaginaryPart == 0)
                return "" + _realPart;
            if (_realPart == 0)
                return _imaginaryPart + "i";

            return _realPart + " + " + _imaginaryPart + "i";
        }

    }
}
