using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Nhibernate.Base
{
    public class ProcedureParameter
    {
        #region Constractors
        
        public ProcedureParameter()
        {
 
        }

        public ProcedureParameter(string parameterName,object value,ParameterDirection direction)
        {
            ParameterName = parameterName;
            Value = value;
            Direction = direction;
        }

        public ProcedureParameter(string parameterName, object value, ParameterDirection direction,int size)
        {
            ParameterName = parameterName;
            Value = value;
            Direction = direction;
            Size = size;
        }

        public ProcedureParameter(string parameterName, object value, ParameterDirection direction, int size, DbType dbType)
        {
            ParameterName = parameterName;
            Value = value;
            Direction = direction;
            Size = size;
            DataType = dbType;
        }
        #endregion

        #region Properties
        
        /// <summary>
        /// 参数名
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 方向
        /// </summary>
        public ParameterDirection Direction { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType DataType { get; set; }

        #endregion
    }
}
