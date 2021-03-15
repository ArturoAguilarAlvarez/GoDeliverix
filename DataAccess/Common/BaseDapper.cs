using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Common
{
    public class BaseDapper
    {
        protected readonly string _connectionString;

        public BaseDapper()
        {
            this._connectionString = "Data Source=den1.mssql5.gear.host;Initial Catalog=deliverix;Persist Security Info=True;User ID=deliverix;Password=Yj8q4DyP!d!o";
        }

        #region READ
        public T QuerySingleOrDefault<T>(string query, DynamicParameters pars, CommandType? commandType = null)
        {
            T result;

            using (var conn = new SqlConnection(_connectionString))
            {
                result = conn.QuerySingleOrDefault<T>(query, pars, commandType: commandType);
            }

            return result;
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string query, DynamicParameters pars, CommandType? commandType = null)
        {
            T result;

            using (var conn = new SqlConnection(_connectionString))
            {
                result = await conn.QuerySingleOrDefaultAsync<T>(query, pars, commandType: commandType);
            }

            return result;
        }

        public IEnumerable<T> Query<T>(string query, DynamicParameters pars, CommandType? commandType = null)
        {
            IEnumerable<T> result;

            using (var conn = new SqlConnection(_connectionString))
            {
                result = conn.Query<T>(query, pars, commandType: commandType);
            }

            return result;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, DynamicParameters pars, CommandType? commandType = null)
        {
            IEnumerable<T> result;

            using (var conn = new SqlConnection(_connectionString))
            {
                result = await conn.QueryAsync<T>(query, pars, commandType: commandType);
            }

            return result;
        }

        public int Execute(string query, DynamicParameters pars, CommandType? commandType = null)
        {
            int result;

            using (var conn = new SqlConnection(_connectionString))
            {
                result = conn.Execute(query, pars, commandType: commandType);
            }

            return result;
        }
        #endregion

        public SqlConnection GetConnection()
        {
            return new SqlConnection(this._connectionString);
        }
    }
}
