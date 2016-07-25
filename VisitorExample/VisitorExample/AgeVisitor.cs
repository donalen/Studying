using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorExample
{
    class AgeVisitor : IVisitor
    {
        public void Visit(Predator predator)
        {
            Predator p = predator as Predator;

            p.Age = p.Age + 1;
            Console.WriteLine("{0}: {1}'s new age: {2}", p.GetType().Name, p.Name, p.Age);
        }
        public void Visit(Herbivorous herbivorous)
        {
            Herbivorous h = herbivorous as Herbivorous;

            h.Age = h.Age + 1;
            Console.WriteLine("{0}: {1}'s new age: {2}", h.GetType().Name, h.Name, h.Age);
        }
    }
}
