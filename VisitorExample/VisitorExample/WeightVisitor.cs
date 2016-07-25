using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorExample
{
    class WeightVisitor : IVisitor
    {
        public void Visit(Predator predator)
        {
            Predator p = predator as Predator;

            p.Weight = p.Weight * 1.1;
            Console.WriteLine("{0}: {1}'s new weight: {2}", p.GetType().Name, p.Name, p.Weight);
        }
        public void Visit(Herbivorous herbivorous)
        {
            Herbivorous h = herbivorous as Herbivorous;

            h.Weight = h.Weight * 1.05;
            Console.WriteLine("{0}: {1}'s new weight: {2}", h.GetType().Name, h.Name, h.Weight);
        }

    }
}
