using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Nhibernate.Base
{
    public abstract class PatchForOracleLobField : IUserType
    {
        public bool IsMutable
        {
            get { return false; }
        }
        public System.Type ReturnedType
        {
            get { return typeof(StringClobSqlType); } //注意此处
        }
        public SqlType[] SqlTypes
        {
            get
            {
                return new SqlType[] { NHibernateUtil.StringClob.SqlType };  //注意此处
            }
        }
        public object DeepCopy(object value)
        {
            return value;
        }
        public new bool Equals(object x, object y)
        {
            return x == y;
        }
        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }
        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }
        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }
        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            return NHibernate.NHibernateUtil.StringClob.NullSafeGet(rs, names[0]);
        }
        public abstract void NullSafeSet(IDbCommand cmd, object value, int index);
        public object Replace(object original, object target, object owner)
        {
            return original;
        }
    }
}
