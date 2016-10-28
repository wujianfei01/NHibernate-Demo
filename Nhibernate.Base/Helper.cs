using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using System.Data;
using NHibernate.Expression;

namespace Nhibernate.Base
{
    public class Helper
    {
        public static string CurrentLocation
        {
            get
            {
                if (!AppDomain.CurrentDomain.BaseDirectory.EndsWith("\\"))
                {
                    return AppDomain.CurrentDomain.BaseDirectory + "\\";
                }
                else
                {
                    return AppDomain.CurrentDomain.BaseDirectory;
                }
            }
        }

        #region Declarations

        private static ISession _Session = null;

        public static ISession Session
        {
            get
            {
                if (_Session == null)
                {

                    Configuration cfg = new Configuration();

                    // _Session session factory from configuration object
                    _Session = cfg.Configure(CurrentLocation + "Nhibernate.config").BuildSessionFactory().OpenSession();
                }

                return _Session;
            }
        }

        #endregion

        #region IDisposable Members

        public static void Dispose()
        {
            Session.Dispose();
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Close this Persistence Manager and release all resources (connection pools, etc). It is the responsibility of the application to ensure that there are no open Sessions before calling Close().
        /// </summary>
        public static void Close()
        {
            if (_Session != null && _Session.IsOpen)
            {
                _Session.Close();
            }
        }

        /// <summary>
        /// Saves an object and its persistent children.
        /// </summary>
        public static string SaveOrUpdate<T>(T item)
        {
            Helper.Session.Clear();
            string msg = string.Empty;
            try
            {
                using (Helper.Session.BeginTransaction())
                {
                    Helper.Session.SaveOrUpdate(item);
                    Helper.Session.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return msg;
        }


        /// <summary>
        /// Saves an object and its persistent children.
        /// </summary>
        public static string Save<T>(T item)
        {
            Helper.Session.Clear();
            string msg = string.Empty;
            try
            {
                using (Helper.Session.BeginTransaction())
                {
                    Helper.Session.Save(item);
                    Helper.Session.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return msg;
        }

        /// <summary>
        /// Saves an object and its persistent children.
        /// </summary>
        public static string Update<T>(T item)
        {
            Helper.Session.Clear();
            string msg = string.Empty;
            try
            {
                using (Helper.Session.BeginTransaction())
                {
                    Helper.Session.Update(item);
                    Helper.Session.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return msg;
        }

        /// <summary>
        /// Deletes an object of a specified type.
        /// </summary>
        /// <param name="itemsToDelete">The items to delete.</param>
        /// <typeparam name="T">The type of objects to delete.</typeparam>
        public static string Delete<T>(T item)
        {
            Helper.Session.Clear();
            string msg = string.Empty;
            try
            {
                using (Helper.Session.BeginTransaction())
                {
                    Helper.Session.Delete(item);
                    Helper.Session.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return msg;
        }

        /// <summary>
        /// Deletes objects of a specified type.
        /// </summary>
        /// <param name="itemsToDelete">The items to delete.</param>
        /// <typeparam name="T">The type of objects to delete.</typeparam>
        public static string Delete<T>(List<T> itemsToDelete)
        {
            Helper.Session.Clear();
            string msg = string.Empty;
            try
            {
                using (Helper.Session.BeginTransaction())
                {
                    foreach (T item in itemsToDelete)
                    {
                        Helper.Session.Delete(item);
                    }
                    Helper.Session.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return msg;
        }

        /// <summary>
        /// 获取数据实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereCondition">Where条件</param>
        /// <returns></returns>
        public static IList<T> GetEntityList<T>(IList<ICriterion> whereCondition)
        {
            return GetEntityList<T>(whereCondition, null);
        }
        
        /// <summary>
        /// 获取数据实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereCondition">Where条件</param>
        /// <param name="orderColumnList">排序字段</param>
        /// <returns></returns>
        public static IList<T> GetEntityList<T>(IList<ICriterion> whereCondition,IList<string> orderColumnList)
        {
            Helper.Session.Clear();
            ICriteria criteria = Session.CreateCriteria(typeof(T));

            if (whereCondition != null && whereCondition.Count > 0)
            {
                foreach (ICriterion cri in whereCondition)
                {
                    criteria.Add(cri);
                }
            }

            if (orderColumnList != null && orderColumnList.Count > 0)
            {
                foreach (string orderColumn in orderColumnList)
                {
                    criteria.AddOrder(Order.Asc(orderColumn));                    
                }
            }

            return criteria.List<T>();
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="lstParameters"></param>
        public static void ExecuteProcedure(string procedureName, List<ProcedureParameter> lstParameters)
        {
            Helper.Session.Clear();
            try
            {
                var cmd = Session.Connection.CreateCommand();

                cmd.CommandText = procedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var para in lstParameters)
                {
                    var iPara = cmd.CreateParameter();
                    iPara.ParameterName = para.ParameterName;
                    iPara.Value = para.Value;
                    iPara.Direction = para.Direction;
                    iPara.DbType = para.DataType;
                    if (para.Size != 0)
                    {
                        iPara.Size = para.Size;
                    }
                    cmd.Parameters.Add(iPara);
                }
                cmd.ExecuteNonQuery();

                foreach (var p in lstParameters)
                {
                    if (p.Direction == ParameterDirection.Output)
                    {
                        p.Value = ((System.Data.Common.DbParameter)cmd.Parameters[p.ParameterName]).Value;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
