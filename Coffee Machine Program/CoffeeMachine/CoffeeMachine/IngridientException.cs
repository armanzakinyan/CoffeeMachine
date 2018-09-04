using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine
{
    public class IngridientException:Exception
    {
        public string Message { set; get; }
        public IngridientException(string msg)
        {
            Message = msg;
        }
    }
}
