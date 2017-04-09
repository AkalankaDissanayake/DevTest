using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.Domain.Repo.Abstract;
using App.Domain.Repo.Concrete;
using App.Entity;
namespace App.Test
{
    [TestClass]
    public class RepoUnitTest
    {

        BaseRepository ibrepo;
        public RepoUnitTest()
        {
            this.ibrepo = new BaseRepository();
        }

        [TestMethod]
        public void GetTextRepoTestMethod()
        {
            ErrorObject err = new ErrorObject();
            var rst = ibrepo.GetText(out err, new Text());
            Assert.AreEqual(true, rst != null && rst.GetEnumerator().MoveNext());
        }
    }
}
