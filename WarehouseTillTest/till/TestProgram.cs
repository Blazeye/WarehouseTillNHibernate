using System;
using System.Collections.Generic;
using WarehouseTill.till;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsTillTest.till
{
    class TestProgram
    {
        private SortedDictionary<decimal, int> initialContent { get; } = null;

        public SortedDictionary<decimal, int> StartRegister()
        {
            SortedDictionary<decimal, int> initialContent = new SortedDictionary<decimal, int>(new ReversedComparer()) {

                      {   50m,  0 }, // no bills of 50 euro
                      {   20m,  1 }, //  1 bill  of 20 euro
                      {   10m,  0 }, // no bills of 10 euro
                      {    5m,  1 }, //  1 bill  of  5 euro
                      {    2m,  1 }, //  1 coin  of  2 euro
                      {    1m,  4 }, //  4 coins of  1 euro
                      {  0.5m,  2 }, //  2 coins of 50 cent
                      {  0.2m, 10 }, // 10 coins of 20 cent
                      {  0.1m,  3 }, //  3 coins of 10 cent
                      { 0.05m,  7 }, //  7 coins of  5 cent
                      { 0.02m,  1 }, //  1 coin  of  2 cent
                      { 0.01m,  4 }, //  4 coins of  1 cent

                };

            return initialContent;
        }
    }
}
