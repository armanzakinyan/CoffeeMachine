using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine
{
    public class Coffee
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int Price { set; get; }
        public decimal CoffeeIngrid { set; get; }
        public decimal SugarIngrid { set; get; }
        public decimal WaterIngrid { set; get; }
        public override string ToString()
        {
            return Id + "\t" + Name + "\t" + Price;
        }
    }
}
