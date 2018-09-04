using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CoffeeMachine
{
    public class CoffeeMachine
    {
        private DataSet myDataSet;
        private static List<Coffee> Products = new List<Coffee>();
        private static List<Ingridient> Ingridients = new List<Ingridient>();
        private int coffeeNum;
        public int CoffeeNum
        {
            set
            {
                if (value >= 1 && value <= 10)
                {
                    coffeeNum = value;
                }
                else
                {
                    throw new Exception();
                }
            }
            get
            {
                return coffeeNum;
            }
        }
        private int money;
        public int Money
        {
            set
            {
                if (value % 50 == 0)
                {
                    money = value;
                }
                else
                {
                    throw new Exception();
                }
            }
            get
            {
                return money;
            }
        }
        public CoffeeMachine()
        {
            var fs = new FileStream("CoffeeMachine.bin", FileMode.Open);
            var bformat = new BinaryFormatter();
            myDataSet = (DataSet)bformat.Deserialize(fs);
            fs.Close();
            foreach (DataRow row in myDataSet.Tables["Products"].Rows)
            {
                Products.Add(new Coffee
                {
                    Id = (int)row["ProductID"],
                    Name = (string)row["Name"],
                    Price = (int)row["Price"],
                    CoffeeIngrid = (decimal)row["CoffeeIngrid"],
                    WaterIngrid = (decimal)row["WaterIngrid"],
                    SugarIngrid = (decimal)row["SugarIngrid"]
                });
            }

            foreach (DataRow row in myDataSet.Tables["Ingridients"].Rows)
            {
                Ingridients.Add(new Ingridient
                {
                    Id = (int)row["IngridID"],
                    Name = (string)row["Name"],
                    Quantity = (decimal)row["Quantity"]
                });
            }
        }
        private bool IsEnoughIngridients(Coffee coffee)
        {
            if (Ingridients[0].Quantity >= coffee.CoffeeIngrid &&
                    Ingridients[1].Quantity >= coffee.SugarIngrid &&
                    Ingridients[2].Quantity >= coffee.WaterIngrid)
            {
                return true;
            }
            return false;
        }
        public Coffee MakeCoffee()
        {
            Coffee usersCoffee = new Coffee();
            foreach (Coffee coffee in Products)
            {
                if (coffee.Id == coffeeNum)
                {
                    usersCoffee = coffee;
                }
            }
            if (!IsEnoughIngridients(usersCoffee))
            {
                IngridientException ex = new IngridientException("There Are Not Enough Ingridients To Make Your Coffee");
                throw ex;
            }
            else if (Money < usersCoffee.Price)
            {
                MoneyException ex = new MoneyException("There Is Not Enough Money");
                throw ex;
            }
            else
            {
                foreach (Ingridient ingrid in Ingridients)
                {
                    if (ingrid.Name == "Coffee")
                    {
                        ingrid.Quantity -= usersCoffee.CoffeeIngrid;
                    }
                    else if (ingrid.Name == "Sugar")
                    {
                        ingrid.Quantity -= usersCoffee.SugarIngrid;
                    }
                    else
                    {
                        ingrid.Quantity -= usersCoffee.WaterIngrid;
                    }
                }
                return usersCoffee;
            }
        }

        public void SaveChangesInFile()
        {
            int i = 0;
            foreach (DataRow row in myDataSet.Tables["Ingridients"].Rows)
            {
                row["Quantity"] = Ingridients[i].Quantity;
                i++;
            }
            myDataSet.RemotingFormat = SerializationFormat.Binary;
            var fs = new FileStream("CoffeeMachine.bin", FileMode.Create);
            var bFormat = new BinaryFormatter();
            bFormat.Serialize(fs, myDataSet);
            fs.Close();
        }
        public void PrintProducts()
        {
            foreach(Coffee coffee in Products)
            {
                Console.Write(coffee.Id + "    " + coffee.Name + " - " + coffee.Price);
                Console.WriteLine();
            }
        }
    }
}
