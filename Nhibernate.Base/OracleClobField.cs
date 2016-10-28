using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace Nhibernate.Base
{
    public class OracleClobField : PatchForOracleLobField
    {
        public override void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if (cmd is OracleCommand)
            {
                OracleParameter param = cmd.Parameters[index] as OracleParameter;
                if (param != null)
                {
                    param.OracleType = OracleType.Clob;  //注意此处
                    param.IsNullable = true;
                }
            }
            NHibernate.NHibernateUtil.StringClob.NullSafeSet(cmd, value, index);
        }
    }
}
