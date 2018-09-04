using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine
{
    public class MoneyException:Exception
    {
        public string Message { set; get; }
        public MoneyException(string msg)
        {
            Message = msg;
        }
    }
}
