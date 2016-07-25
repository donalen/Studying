using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorExample
{
    interface IVisitor
    {
        void Visit(Predator predator);
        void Visit(Herbivorous herbivorous);
    }
}
