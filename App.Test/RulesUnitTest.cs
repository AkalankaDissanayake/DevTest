using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.Rules.Abstract;
using App.Rules.Concrete;
using App.Entity;

namespace App.Test
{
    [TestClass]
    public class RulesUnitTest
    {
        IBaseRules ibaserules;
        public RulesUnitTest()
        {
            this.ibaserules = new BaseRules();
        }
        [TestMethod]
        public void GetTextRuleTestMethod()
        {
            ErrorObject err = new ErrorObject();
            var rst = ibaserules.GetText(out err, new Text());
            Assert.AreEqual(true, rst != null && rst.GetEnumerator().MoveNext());
        }
    }
}
