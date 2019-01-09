using System;
using Drachenhorn.Xml.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drachenhorn.UnitTest.DataTests
{
    [TestClass]
    public class CurrencyTest
    {
        private Currency _currency;

        [TestInitialize]
        public void InitializeCurrencyTest()
        {
            _currency = new Currency();
            _currency.Name = "test";
            _currency.CurrencyParts.Add(new CurrencyPart() {Name = "test1", Symbol = "% t1", Value = 1});
            _currency.CurrencyParts.Add(new CurrencyPart() {Name = "test2", Symbol = " t2", Value = 10});
            _currency.CurrencyParts.Add(new CurrencyPart() {Name = "test3", Symbol = "% t3", Value = 100});
        }


        [TestMethod]
        public void CurrencyToStringTest()
        {
            var value = 522;

            var result = _currency.ToString(value, 'p');

            Assert.AreEqual("5 t3 2 t2 2 t1", result);
        }

        [TestMethod]
        public void CurrencyParseTest()
        {
            var value = "5 t3 2 t2 2 t1";

            var result = _currency.Parse(value);

            Assert.AreEqual(522, result);
        }
    }
}
