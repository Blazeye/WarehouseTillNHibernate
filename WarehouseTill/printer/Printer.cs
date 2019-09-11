using System;
using System.Collections.Generic;
using WarehouseTill.model;

namespace WarehouseTill.printer
{
    class Printer
    {

        public Dictionary<string, OrdersProduct> CheckoutDict = new Dictionary<string, OrdersProduct>();

        public void PrintItemList(Dictionary<string, OrdersProduct> dict)
        {
            Console.WriteLine("================ PRINTING: ===============");
            foreach(string item in dict.Keys)
            {
                switch (item)
                {
                    case "1234":
                        PrintButter(dict[item].Amount);
                        break;
                    case "9902":
                        PrintCheese(dict[item].Amount);
                        break;
                    case "3568":
                        PrintMilk(dict[item].Amount);
                        break;
                    case "7324":
                        PrintEggs(dict[item].Amount);
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("==========================================\n");
        }

        public void PrintButter(int amount)
        {
            Console.Out.WriteLine("|                                         |");
            Console.Out.WriteLine($"|    _________________________" + PaddedAmount(amount, 12));
            Console.Out.WriteLine("|   /                        /|           |");
            Console.Out.WriteLine("|  /     /3onko-/3oter      / |           |");
            Console.Out.WriteLine("| /_______________________ /  :           |");
            Console.Out.WriteLine("| |                        |  /           |");
            Console.Out.WriteLine("| |                        | /            |");
            Console.Out.WriteLine("| |________________________|/             |");
            Console.Out.WriteLine("|                                         |");
        }

        public void PrintEggs(int amount)
        {
        Console.Out.WriteLine("|                .-~-.                    |");
        Console.Out.WriteLine($"|              .\'     \'." + PaddedAmount(amount, 18));
        Console.Out.WriteLine("|             /         \\                 |");
        Console.Out.WriteLine("|     .-~-.  :           ;                |");
        Console.Out.WriteLine("|   .\'     \'.|           |                |");
        Console.Out.WriteLine("|  /         \\           :                |");
        Console.Out.WriteLine("| :           ; .-~\"\"~-,/                 |"); 
        Console.Out.WriteLine("| |           /`        `\'.               |");
        Console.Out.WriteLine("| :          |             \\              |"); 
        Console.Out.WriteLine("|  \\         |             /              |");
        Console.Out.WriteLine("|   `.     .\' \\          .\'               |");
        Console.Out.WriteLine("|     `~~~`    \'-.____.-\'                 |");
        Console.Out.WriteLine("|                                         |");
        }
        public void PrintCheese(int amount)
        {
        Console.Out.WriteLine("|     _--\"-.                              |");
        Console.Out.WriteLine("|  .-\"      \"-." + PaddedAmount(amount, 27));
        Console.Out.WriteLine("| | \"\"--..      \'-.                       |");
        Console.Out.WriteLine("| |       \"\"--..  \'-.                     |");
        Console.Out.WriteLine("| |.-.  .-\".   \"\"--..\".                   |");
        Console.Out.WriteLine("| |'./  -_ \' .-.      |                   |");
        Console.Out.WriteLine("| |      .-. \'.-\'   .-\'                   |");
        Console.Out.WriteLine("| '--..  \'.\'    .-  -.                    |");
        Console.Out.WriteLine("|     \"\"--..    \'_\'   :                   |");
        Console.Out.WriteLine("|          \"\"--..     |                   |");
        Console.Out.WriteLine("|                \"\" - \'                   |");
        Console.Out.WriteLine("|                                         |");

        }
        public void PrintMilk(int amount)
        {
        Console.Out.WriteLine("|     ________                            |");
            Console.Out.WriteLine("|    j________j" + PaddedAmount(amount, 27));
        Console.Out.WriteLine("|   /________/_\\                          |");
        Console.Out.WriteLine("|  |         |  |                         |");
        Console.Out.WriteLine("|  | |\\/|ELK |  |                         |");
        Console.Out.WriteLine("|  |         |  |                         |");
        Console.Out.WriteLine("|  |  _(~)_  |  |                         |");
        Console.Out.WriteLine("|  |   )\"(   |  |                         |");
        Console.Out.WriteLine("|  |  (@_@)  |  |                         |");
        Console.Out.WriteLine("|  |         |  |                         |");
        Console.Out.WriteLine("|  | _______ |,/\'                         |");
        Console.Out.WriteLine("|                                         |");
        }

        private string PaddedAmount(int amount, int length)
        {
            string thing = $"AMOUNT: {amount}";
            return thing.PadLeft(length, ' ')+'|'
                ;
        }
        public void HandlePrintItems(object s, OrdersListEventArgs e)
        {
            CheckoutDict = e.OrderList;
            PrintItemList(CheckoutDict);
        }
    }
}
