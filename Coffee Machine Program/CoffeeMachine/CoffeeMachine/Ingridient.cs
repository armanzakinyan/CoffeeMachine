using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine
{
    public class Ingridient
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public decimal Quantity { set; get; }
        public override string ToString()
        {
            return Id + "\t" + Name + "\t" + Quantity;
        }
    }
}
