using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DBControl
{
    public class DBTurno
    {
        Conexion oConexion;

        public DataTable VerificaUltimoTurno(Guid UidUsuario)
        {
            string query = "select top 1 * from TurnoRepartidor where UidUsuario = '"+ UidUsuario.ToString() + "' order by DtmHoraInicio desc ";
            oConexion = new Conexion();
            return oConexion.Consultas(query);
        }

        public DataTable InformacionDeOrdenesPorTurno(Guid uidTurno)
        {
            string query = " select count(*) as TotalOrdenes, sum(os.MTotalSucursal) as totalSucursal,sum(t.MCosto) as totalEnvio from OrdenSucursal os   inner join OrdenTarifario ot on ot.UidOrden = os.UidRelacionOrdenSucursal inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario  inner join OrdenRepartidor orep on orep.UidOrden = ot.UidRelacionOrdenTarifario inner join TurnoRepartidor tr on tr.UidTurnoRepartidor = orep.UidTurnoRepartidor where tr.UidTurnoRepartidor = '"+ uidTurno .ToString()+ "' and dbo.asp_ObtenerUltimoEstatusOrdenRepartidor(orep.UidRelacionOrdenRepartidor) != '12748F8A-E746-427D-8836-B54432A38C07' ";
            oConexion = new Conexion();
            return oConexion.Consultas(query);
        }

        public DataTable HistoricoTurno(Guid uidUsuario)
        {
            string query = " select * from turnorepartidor where uidusuario = '"+ uidUsuario.ToString() + "' and dtmhorafin is not null order by lngFolio desc ";
            oConexion = new Conexion();
            return oConexion.Consultas(query);
        }

        public DataTable HistoricoOrdenes(Guid UidTurno)
        {
            string query = " select os.IntFolio,os.MTotalSucursal,t.MCosto from OrdenSucursal os inner join OrdenTarifario ot on ot.UidOrden = os.UidRelacionOrdenSucursal inner join OrdenRepartidor orep on orep.UidOrden = ot.UidRelacionOrdenTarifario inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario  inner join TurnoRepartidor tr  on orep.UidTurnoRepartidor = tr.UidTurnoRepartidor  where tr.UidTurnoRepartidor = '"+ UidTurno .ToString()+ "' order by orep.dtmFechaAsignacion desc ";
            oConexion = new Conexion();
            return oConexion.Consultas(query);
        }
    }
}
