using System;
using System.Linq;
using System.Collections.Generic;

namespace WarehouseTill.till
{
    public class CashRegister : ICashRegister
    {
        public IDictionary<decimal, int> Register { get; set; }
        public IDictionary<decimal, int> RegisterStartDay { get; set; }

        public CashRegister(IDictionary<decimal, int> initialChange)
        {
                Register = new SortedDictionary<decimal, int>(initialChange, new ReversedComparer());
                RegisterStartDay = new SortedDictionary<decimal, int>(initialChange, new ReversedComparer());
        }

        /// <summary>
        /// total cost of all scanned items
        /// </summary>
        /// <returns>total price</returns>
        public decimal CountRegister()
        {
            return Register.Sum(p => p.Key * p.Value);
        }

        /// <summary>
        /// Changes the money in the register after purchase, and returns the amount of change
        /// </summary>
        /// <param name="itemCost">the cost of the item</param>
        /// <param name="billsUsed">the amount of money that the costumer payed</param>
        /// <returns></returns>
        public IDictionary<decimal, int> MakeChange(decimal itemCost, decimal billsUsed)
        {
            IDictionary<decimal, int> returnChange = new SortedDictionary<decimal, int>(new ReversedComparer());
            IDictionary<decimal, int> addBills = new SortedDictionary<decimal, int>(new ReversedComparer());

            decimal moneyToReturn = billsUsed - itemCost;

            //check if the customer is paying the right amount 
            if (moneyToReturn < 0)
            {
                Console.WriteLine("Customer hasn't paid enough for the item(s)");
                return null;
            }
            //check the amount of money the cashier needs to pay from the register
            foreach (decimal key in this.Register.Keys)
            {
                int amount = (int)(moneyToReturn / key);

                if (billsUsed >= key)
                {
                    //money added to the till
                    int addedToTill = (int)(billsUsed / key);
                    billsUsed -= (addedToTill * key);
                    addBills.Add(key, addedToTill);
                }
                if (amount < 1 || this.Register[key] < 1)
                {
                    continue;
                }
                //if the change you need to give is more than what's in the register
                if (amount > this.Register[key])
                {
                    moneyToReturn -= this.Register[key] * key;
                    returnChange.Add(key, this.Register[key]);
                }
                //if you have enough money in the register
                else
                {
                    moneyToReturn -= amount * key;
                    returnChange.Add(key, amount);
                }
            }



            //check if you have enough 1 and 2 cents to add to make up the lost 5 cent change
            if ((moneyToReturn < 0.05m) && (moneyToReturn > 0m))
            {

                decimal oneCents = returnChange[0.01m] * 0.01m;
                moneyToReturn += oneCents;
                if (returnChange.ContainsKey(0.01m)) { returnChange.Remove(0.01m); }
                int twoCents = (int)((0.05m - oneCents) / 0.02m);
                if (returnChange.ContainsKey(0.02m))
                {
                    if (returnChange[0.02m] > twoCents)
                    {
                        moneyToReturn += twoCents * 0.02m;
                        returnChange[0.02m] -= twoCents;
                    }
                    else
                    {
                        moneyToReturn += returnChange[0.02m] * 0.02m;
                        returnChange.Remove(0.02m);
                    }
                }




                if (moneyToReturn >= 0.03m)
                {
                    if (returnChange.ContainsKey(0.05m))
                    {
                        returnChange[0.05m]++;
                    }
                    else
                    {
                        returnChange.Add(0.05m, 1);
                    }
                }
            }





            //returnChange[0.02m] -= (int)(c / 0.02m);
            //if(returnChange[0.02m] <= 0) { returnChange.Remove(0.02m); }





            if (moneyToReturn >= 0.05m)
            {
                return null;
            }
            //adding money to the register
            foreach (decimal key in addBills.Keys)
            {
                this.Register[key] += addBills[key];
            }
            //removing taking change out of register
            foreach (decimal key in returnChange.Keys)
            {
                this.Register[key] -= returnChange[key];
            }
            return returnChange;
        }

        /// <summary>
        /// Returns the profits for the day and updates the registers thereafter
        /// </summary>
        /// <returns></returns>
        public decimal ProcessEndOfDay()
        {
            decimal sumEnd = CountRegister();
            decimal sum = 0;

            //calculate the sum in the register at the start of the day
            foreach (var item in RegisterStartDay)
            {
                sum += (item.Key * item.Value);
            }
            decimal sumAdded;
            sumAdded = sumEnd - sum;

            // set the amount of money in the register at the start of every new day
            foreach (KeyValuePair<decimal, int> item in Register)
            {
                RegisterStartDay[item.Key] = item.Value;
            }
            return sumAdded;
        }
    }
}

