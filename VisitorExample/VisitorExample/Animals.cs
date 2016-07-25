using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorExample
{
    class Animals
    {
        private List<Animal> _animals = new List<Animal>();

        public void Attach(Animal animal)
        {
            _animals.Add(animal);
        }

        public void Detach(Animal animal)
        {
            _animals.Remove(animal);
        }

        public void Accept(IVisitor visitor)
        {
            foreach (Animal a in _animals)
            {
                a.Accept(visitor);
            }
            Console.WriteLine();
        }
    }
}
