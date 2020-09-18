using Dapper;
using System.Collections.Generic;
using System.Data;
using System;
using System.Data.SqlClient;

namespace GoDeliverix.Services.Data.Common
{
    public class BaseDapper
    {
        protected readonly string _connString;

        public BaseDapper()
        {
            this._connString = "";
        }

        public IEnumerable<T> Query<T>(string query, DynamicParameters pars, CommandType? commandType = null)
        {
            IEnumerable<T> result;

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                result = conn.Query<T>(query, pars, commandType: commandType);
            }

            return result;
        }

        /// <summary>
        /// Inserta el elemento a la base de datos y retorna su uid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public Guid Insert<T>(T Entity)
        {
            Guid uid;

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                uid = conn.Insert<Guid,T>(Entity);
                conn.Close();
            }

            return uid;
        }
    }
}
