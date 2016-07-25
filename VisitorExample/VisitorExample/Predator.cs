using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorExample
{
    class Predator : Animal
    {
        public Predator()
            : base("Lion", 200, 8)
        {
        }
        
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
