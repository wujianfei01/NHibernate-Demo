using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XN.Transmit.DataAccess;
using Nhibernate.Entities;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        DalEntDecheadtype testDal;
        public UnitTest()
        {
            testDal = new DalEntDecheadtype();
        }

        [TestMethod]
        public void TestSelect()
        {
            IList<EntDecheadtype> s = testDal.GetUnsentData();
        }

        [TestMethod]
        public void TestInsert()
        {
            EntDecheadtype tmp = new EntDecheadtype();
            tmp.Seqno = "mttest001";
            tmp.ProcessStatus = 0;
            string msg = testDal.SaveEntDecheadtype(tmp);
        }

        [TestMethod]
        public void TestUpdate()
        {
            IList<EntDecheadtype> s = testDal.GetUnsentData().Where(p => p.Seqno.Equals("test")).ToList();
            EntDecheadtype c = s.FirstOrDefault();
            c.Tradeareacode = "测试ok";
            string msg = testDal.UpdateEntDecheadtype(c);
        }

        [TestMethod]
        public void TestDelete()
        {
            EntDecheadtype d = testDal.GetUnsentData().Where(p => p.Seqno.Equals("mttest001")).ToList().FirstOrDefault();
            string msg;
            if (null != d)
                msg = testDal.DeleteEntDecheadtype(d);
        }
    }
}
