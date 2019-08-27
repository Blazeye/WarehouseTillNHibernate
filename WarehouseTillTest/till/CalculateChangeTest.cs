using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WarehouseTill.till;

namespace EventsTillTest.till
{
    [TestFixture]
    public class CalculateChangeTest
    {
        [Test]
        public void TestConstructor()
        {
            //Prepare
            var data = new SortedDictionary<decimal, int>(new ReversedComparer()) { };

            //Run
            var sut = new CashRegister(data);

            //Analyze
            Assert.NotNull(sut);
            Assert.IsInstanceOf<CashRegister>(sut);
        }
        [Test]
        public void TestEmptyConstructor()
        {
            // Prepare

            // Run & Analyze
            Assert.Throws<System.ArgumentNullException>(() => new CashRegister(null)); // We expect that it throws an exception
        }
        [Test]
        public void TestCountRegister()
        {
            //Prepare
            SortedDictionary<decimal, int> item = StartRegister();

            //Run
            var sut = new CashRegister(item);
            var result = sut.CountRegister();

            //Analyze
            Assert.NotNull(result);
            Assert.IsInstanceOf<decimal>(result);
            Assert.AreEqual(34.71m, result);
        }

        private static SortedDictionary<decimal, int> StartRegister()
        {
            SortedDictionary<decimal, int> item = new SortedDictionary<decimal, int>(new ReversedComparer()) {

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
            return item;
        }

        [TestCase(2.0f,200f, null)]
        [TestCase(2.5f, 2.5f)]
        [TestCase(4.51f, 10f, 5f, 0.20f, 0.20f, 0.05f, 0.02f, 0.01f, 0.01f)]
        public void TestMakeChange(float toPay, float paid, params float?[] expectedChange)
        {
            //SortedDictionary<decimal, int> item = StartRegister();

            IDictionary<decimal, int> expected = CalculateExpected(expectedChange);

            //Run
            //var sut = new CashRegister(item);
            var sut = new CashRegister(StartRegister());
            var result = sut.MakeChange(Convert.ToDecimal(toPay), Convert.ToDecimal(paid));

            //Analyze

            Assert.AreEqual(expected, result);

        }
        [TestCase(4.51f, 10f, 1.30f, 5f, 0.97f, 20f, 10f, 5f, 1f, 1f, 1f, .5f, .2f, .2f, .1f, .05f)]
        [TestCase(1.3f, 20.5f, 0, 0, 1.3f, 2.5f, 1f, .20f)]
        public void TestMakeMultipleChange(float toPay1, float paid1, float toPay2, float paid2, float toPay3, float paid3, params float?[] expectedChange)
        {
            //SortedDictionary<decimal, int> item  = StartRegister();

            SortedDictionary<decimal, int> expected = new SortedDictionary<decimal, int>(CalculateExpected(expectedChange));

            //Run
            var sut = new CashRegister(StartRegister());
            var resultIDict = sut.MakeChange(Convert.ToDecimal(toPay1), Convert.ToDecimal(paid1));
            resultIDict = sut.MakeChange(Convert.ToDecimal(toPay2), Convert.ToDecimal(paid2));
            resultIDict = sut.MakeChange(Convert.ToDecimal(toPay3), Convert.ToDecimal(paid3));
            SortedDictionary<decimal, int> result = new SortedDictionary<decimal, int>(resultIDict);

            //Analyze
            Assert.AreEqual(expected, result);

        }

        private static IDictionary<decimal, int> CalculateExpected(float?[] expectedChange)
        {
            if (expectedChange.Length == 1 && !expectedChange[0].HasValue) { return null; }

            return expectedChange.GroupBy(f => f)
                                          .OrderByDescending(fg => fg.Key)
                                          .ToDictionary(fg => Convert.ToDecimal(fg.Key.Value), fg => fg.Count());
            //SortedDictionary<decimal, int> expected = new SortedDictionary<decimal, int>(new ReversedComparer());
            //foreach (float p in expectedChange)
            //{

            //    if (expected.ContainsKey(Convert.ToDecimal(p)))
            //    {
            //        expected[Convert.ToDecimal(p)] += 1;
            //    }
            //    else
            //    {
            //        expected.Add(Convert.ToDecimal(p), 1);
            //    }
            //}

            //return expected;
        }
    }
}
