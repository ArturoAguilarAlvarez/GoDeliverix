using Dapper;
using DataAccess.Common;
using Modelo.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserSignInRewardCodeDb : BaseDapper
    {
        public UserSignInRewardCodeDb()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns> Error (-5), No encontrado (-2), Utilizado (-1), Caducado (0), Valido(1) </returns>
        public int VerifyCode(string code)
        {
            try
            {
                string query = "[sp_VerifySignInRewardCode]";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Code", code);
                parameters.Add("@Output", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                this.Execute(query, parameters, System.Data.CommandType.StoredProcedure);

                return parameters.Get<int>("@Output");
            }
            catch (Exception ex)
            {
                return -5;
            }
        }

        /// <summary>
        /// Obtener datos del codigo de promocion de registro del usuario
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public UserSignInRewardCode GetByUserUid(Guid uid)
        {
            string query = "sp_UserSignInRewardCode_FindByUserId";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserUid", uid);

            return this.QuerySingleOrDefault<UserSignInRewardCode>(query, parameters, System.Data.CommandType.StoredProcedure);
        }

        public int VerifyAndApplyCode(Guid uid, string code)
        {
            string query = "sp_UserSignInRewardCode_VerifyAndApply";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UidUser", uid);
            parameters.Add("@Code", code);
            parameters.Add("@Result", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            this.Execute(query, parameters, System.Data.CommandType.StoredProcedure);

            return parameters.Get<int>("@Result");
        }
    }
}
