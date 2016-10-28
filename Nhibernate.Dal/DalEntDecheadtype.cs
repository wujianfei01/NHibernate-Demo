using Nhibernate.Entities;
using NHibernate.Expression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nhibernate.Base;

namespace XN.Transmit.DataAccess
{
    public class DalEntDecheadtype
    {
        /// <summary>
        /// 获得待发送列表
        /// </summary>
        /// <returns></returns>
        public IList<EntDecheadtype> GetUnsentData()
        {
            IList<ICriterion> lstCriterion = new List<ICriterion>();
            lstCriterion.Add(Expression.Eq("ProcessStatus", 0));

            return Helper.GetEntityList<EntDecheadtype>(lstCriterion);
        }

        /// <summary>
        /// 获得一次申报单(DEC)头数据
        /// </summary>
        /// <param name="sqeNo"></param>
        /// <returns></returns>
        public EntDecheadtype GetEntDecheadtype(string seqNo)
        {
            IList<ICriterion> lstCriterion = new List<ICriterion>();
            lstCriterion.Add(Expression.Eq("Seqno", seqNo));

            return Helper.GetEntityList<EntDecheadtype>(lstCriterion).FirstOrDefault();
        }

        public string SaveEntDecheadtype(EntDecheadtype t)
        {
            return Helper.Save<EntDecheadtype>(t);
        }

        public string UpdateEntDecheadtype(EntDecheadtype t)
        {
            return Helper.Update<EntDecheadtype>(t);
        }

        public string DeleteEntDecheadtype(EntDecheadtype t)
        {
            return Helper.Delete<EntDecheadtype>(t);
        }


    }
}
