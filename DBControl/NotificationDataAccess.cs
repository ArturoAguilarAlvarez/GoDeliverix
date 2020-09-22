using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControl
{
    public class NotificationDataAccess
    {
        protected Conexion _Conexion { get; set; }

        public NotificationDataAccess()
        {
            this._Conexion = new Conexion();
        }

        public DataTable ReadAllNotifications(int target, Guid uid)
        {
            string query = $"SELECT * FROM [Notifications] WHERE [Target] = {target} AND [TargetUid] = '{uid}' ORDER BY [CreatedDate] DESC";

            DataTable data = this._Conexion.Consultas(query);
            return data;
        }

        public bool Add(int target, Guid targetUid, string jsonTitle, string jsonContent, int type, Guid typeUid)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = $"INSERT INTO [Notifications] VALUES (NEWID(), {target}, '{targetUid}', '{jsonTitle}','{jsonContent}', {type}, '{typeUid}', GETDATE())"; 
            sqlCommand.CommandType = CommandType.Text;

            return this._Conexion.ModificarDatos(sqlCommand);
        }
    }
}
