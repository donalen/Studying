using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorExample
{
    class Herbivorous : Animal
    {
        public Herbivorous()
            : base("Bunny", 1.5, 2)
        {
        }
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
