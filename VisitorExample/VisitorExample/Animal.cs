using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorExample
{
    abstract class Animal
    {
        private string _name;
        private double _weight;
        private int _age;

        public Animal(string name, double weight, int age)
        {
            this._name = name;
            this._weight = weight;
            this._age = age;
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public double Weight
        {
            get
            {
                return _weight;
            }

            set
            {
                _weight = value;
            }
        }

        public int Age
        {
            get
            {
                return _age;
            }

            set
            {
                _age = value;
            }
        }

        public abstract void Accept(IVisitor visitor);
    }
}
