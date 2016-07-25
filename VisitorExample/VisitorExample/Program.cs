using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorExample
{
    class Program
    {
        static void Main()
        {
            Animals a = new Animals();

            a.Attach(new Predator());
            a.Attach(new Herbivorous());

            a.Accept(new WeightVisitor());
            a.Accept(new AgeVisitor());
        }
    }
}
