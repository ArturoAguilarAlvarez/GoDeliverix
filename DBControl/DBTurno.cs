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
            string query = " select count(*) as TotalOrdenes, sum(os.MTotalSucursal) as totalSucursal,sum(t.MCosto) as totalEnvio, SUM(ot.MPropina) as propina  from OrdenSucursal os   inner join OrdenTarifario ot on ot.UidOrden = os.UidRelacionOrdenSucursal inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario  inner join OrdenRepartidor orep on orep.UidOrden = ot.UidRelacionOrdenTarifario inner join TurnoRepartidor tr on tr.UidTurnoRepartidor = orep.UidTurnoRepartidor left join (select top 1 * from LiquidacionRepartidor where UidTurnoRepartidor = '" + uidTurno.ToString() + "' order by dtmfecharegistro desc) lr on lr.UidTurnoRepartidor = tr.uidturnorepartidor    where tr.UidTurnoRepartidor = '" + uidTurno .ToString()+ "' and(lr.DtmFechaRegistro is not null and orep.dtmFechaAsignacion > lr.DtmFechaRegistro or lr.DtmFechaRegistro is null) and dbo.asp_ObtenerUltimoEstatusOrdenRepartidor(orep.UidRelacionOrdenRepartidor) != '12748F8A-E746-427D-8836-B54432A38C07' ";
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
        public DataTable ObtenerRepartidoresParaLiquidar(object licencia)
        {
            oConexion = new Conexion();
            string query = " select u.UidUsuario,u.Nombre,u.usuario,dbo.asp_ObtenerUtimoEstatus(tr.UidUsuario) as estatus, tr.DtmHoraFin, tr.UidTurnoRepartidor, sum(os.MTotalSucursal) as Ordenes, dbo.GD_ObtenerUltimoEstatusTurnoRepartidor(tr.UidTurnoRepartidor) as EstatusTurno,sum(t.MCosto) as Envio from Usuarios u inner join TurnoRepartidor tr on tr.UidUsuario = u.UidUsuario  inner join OrdenRepartidor orep on orep.UidTurnoRepartidor = tr.UidTurnoRepartidor inner join OrdenTarifario ot on ot.UidRelacionOrdenTarifario = orep.UidOrden inner join OrdenSucursal os on os.UidRelacionOrdenSucursal = ot.UidOrden inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario left join (select distinct top 1 * from LiquidacionRepartidor order by DtmFechaRegistro desc) lr on lr.UidTurnoRepartidor = tr.uidturnorepartidor where (lr.DtmFechaRegistro is not null and orep.dtmFechaAsignacion > lr.DtmFechaRegistro or lr.DtmFechaRegistro is null) and tr.UidUsuario in (select UidUsuario from VehiculoUsuario where UidVehiculo in (select UidVehiculo from vehiculoSucursal where uidsucursal in (select UidSucursal from SucursalLicencia where UidLicencia = '" + licencia + "'))) and dbo.asp_ObtenerUltimoEstatusOrdenRepartidor(orep.UidRelacionOrdenRepartidor) = '7DA3A42F-2271-47B4-B9B8-EDD311F56864' and dbo.GD_ObtenerUltimoEstatusTurnoRepartidor(tr.UidTurnoRepartidor)= 'AE28F243-AA0D-43BD-BF10-124256B75B00' group by u.uidusuario,u.Nombre,u.Usuario,tr.UidUsuario,tr.DtmHoraFin, tr.UidTurnoRepartidor, orep.UidTurnoRepartidor, tr.DtmHoraInicio order by tr.DtmHoraInicio desc  ";
            return oConexion.Consultas(query);
        }
    }
}
