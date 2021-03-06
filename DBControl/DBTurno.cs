﻿using System;
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
            string query = "select top 1 tr.UidTurnoRepartidor,tr.UidUsuario,tr.DtmHoraInicio,tr.DtmHoraFin,tr.LngFolio,vu.mfondo from TurnoRepartidor tr inner join VehiculoUsuario vu on vu.UidUsuario = tr.UidUsuario where vu.UidUsuario = '" + UidUsuario.ToString() + "' order by tr.DtmHoraInicio desc ";
            oConexion = new Conexion();
            return oConexion.Consultas(query);
        }

        public DataTable InformacionDeOrdenesPorTurno(Guid uidTurno)
        {
            string query = " select count(*) as TotalOrdenes, sum(os.MTotalSucursal) as totalSucursal,sum(ot.MtotalEnvio) as totalEnvio, dbo.asp_ObtenerLaCantidadDeEfectivoEnMano(tr.UidTurnoRepartidor) as Efectivo, dbo.ObtenerLiquidacionesTurnoRepartidor(tr.uidturnorepartidor) as liquidacion, dbo.ObtenerLasGananciasRepartidor(tr.Uidturnorepartidor) as ganancias, dbo.ObtenerMontoDePagosDeOrdenesEnTurnoRepartidor(tr.uidturnorepartidor) as PagosSucursales, dbo.ObtenerCantidadOrdenesPagadasTurnoRepartidor(tr.uidturnorepartidor) as CantidadDePagos, dbo.ObtenerRecargasTurnoRepartidor(tr.uidturnorepartidor) as recarga, SUM(ot.MPropina) as propina  from OrdenSucursal os inner join OrdenTarifario ot on ot.UidOrden = os.UidRelacionOrdenSucursal inner join OrdenRepartidor orep on orep.UidOrden = ot.UidRelacionOrdenTarifario inner join TurnoRepartidor tr on tr.UidTurnoRepartidor = orep.UidTurnoRepartidor and dbo.asp_ObtenerUltimoEstatusOrdenRepartidor(orep.UidRelacionOrdenRepartidor) != '12748F8A-E746-427D-8836-B54432A38C07' and dbo.asp_ObtenerUltimoEstatusOrdenRepartidor(orep.UidRelacionOrdenRepartidor) = '7DA3A42F-2271-47B4-B9B8-EDD311F56864' where tr.UidTurnoRepartidor = '" + uidTurno.ToString() + "' group by tr.UidTurnoRepartidor";
            oConexion = new Conexion();
            return oConexion.Consultas(query);
        }

        public DataTable HistoricoTurno(Guid uidUsuario)
        {
            string query = " select * from turnorepartidor where uidusuario = '" + uidUsuario.ToString() + "' and dtmhorafin is not null order by lngFolio desc ";
            oConexion = new Conexion();
            return oConexion.Consultas(query);
        }

        public DataTable HistoricoOrdenes(Guid UidTurno)
        {
            string query = " select os.IntFolio,os.MTotalSucursal,t.MCosto,ot.MPropina from OrdenSucursal os inner join OrdenTarifario ot on ot.UidOrden = os.UidRelacionOrdenSucursal inner join OrdenRepartidor orep on orep.UidOrden = ot.UidRelacionOrdenTarifario inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario  inner join TurnoRepartidor tr  on orep.UidTurnoRepartidor = tr.UidTurnoRepartidor  where tr.UidTurnoRepartidor = '" + UidTurno.ToString() + "' and dbo.asp_ObtenerUltimoEstatusOrdenRepartidor(orep.UidRelacionOrdenRepartidor) = '7DA3A42F-2271-47B4-B9B8-EDD311F56864' order by orep.dtmFechaAsignacion desc ";
            oConexion = new Conexion();
            return oConexion.Consultas(query);
        }
        public DataTable ObtenerRepartidoresParaLiquidar(object licencia)
        {
            oConexion = new Conexion();
            string query = " select u.UidUsuario,(u.Nombre + ' '+ u.ApellidoPaterno +' '+ u.ApellidoMaterno) as Nombre,u.usuario,dbo.asp_ObtenerUtimoEstatus(tr.UidUsuario) as estatus, tr.DtmHoraFin, tr.UidTurnoRepartidor, dbo.asp_ObtenerLaCantidadDeEfectivoEnMano(tr.UidTurnoRepartidor) as Ordenes,dbo.asp_PrecioDeEnvioConComision(tr.UidTurnoRepartidor) as envio, sum(ot.MPropina) as propina, dbo.GD_ObtenerUltimoEstatusTurnoRepartidor(tr.UidTurnoRepartidor) as EstatusTurno from Usuarios u inner join TurnoRepartidor tr on tr.UidUsuario = u.UidUsuario  inner join OrdenRepartidor orep on orep.UidTurnoRepartidor = tr.UidTurnoRepartidor inner join OrdenTarifario ot on ot.UidRelacionOrdenTarifario = orep.UidOrden inner join OrdenSucursal os on os.UidRelacionOrdenSucursal = ot.UidOrden inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario where  tr.UidUsuario in (select UidUsuario from VehiculoUsuario where UidVehiculo in (select UidVehiculo from vehiculoSucursal where uidsucursal in (select UidSucursal from SucursalLicencia where UidLicencia = '" + licencia + "'))) and dbo.asp_ObtenerUltimoEstatusOrdenRepartidor(orep.UidRelacionOrdenRepartidor) = '7DA3A42F-2271-47B4-B9B8-EDD311F56864' and  dbo.GD_ObtenerUltimoEstatusTurnoRepartidor(tr.UidTurnoRepartidor)= 'AE28F243-AA0D-43BD-BF10-124256B75B00' group by u.uidusuario,u.Nombre,u.Usuario,tr.UidUsuario,tr.DtmHoraFin, tr.UidTurnoRepartidor, orep.UidTurnoRepartidor, tr.DtmHoraInicio,u.ApellidoPaterno,u.ApellidoMaterno order by tr.DtmHoraInicio desc  ";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerRepartidoresParaRecargar(string uidLicencia)
        {
            oConexion = new Conexion();
            string query = " select u.UidUsuario,(u.Nombre + ' '+ u.ApellidoPaterno +' '+ u.ApellidoMaterno) as Nombre,u.usuario,dbo.asp_ObtenerUtimoEstatus(tr.UidUsuario) as estatus, tr.DtmHoraFin, tr.UidTurnoRepartidor, dbo.asp_ObtenerLaCantidadDeEfectivoEnMano(tr.UidTurnoRepartidor) as Ordenes,sum(dbo.asp_PrecioDeEnvioConComision(tr.UidTurnoRepartidor)) as envio, sum(ot.MPropina) as propina, dbo.GD_ObtenerUltimoEstatusTurnoRepartidor(tr.UidTurnoRepartidor) as EstatusTurno from Usuarios u inner join TurnoRepartidor tr on tr.UidUsuario = u.UidUsuario  inner join OrdenRepartidor orep on orep.UidTurnoRepartidor = tr.UidTurnoRepartidor inner join OrdenTarifario ot on ot.UidRelacionOrdenTarifario = orep.UidOrden inner join OrdenSucursal os on os.UidRelacionOrdenSucursal = ot.UidOrden inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario where tr.UidUsuario in (select UidUsuario from VehiculoUsuario where UidVehiculo in (select UidVehiculo from vehiculoSucursal where uidsucursal in (select UidSucursal from SucursalLicencia where UidLicencia = '" + uidLicencia.ToString() + "'))) and dbo.asp_ObtenerUltimoEstatusOrdenRepartidor(orep.UidRelacionOrdenRepartidor) = '7DA3A42F-2271-47B4-B9B8-EDD311F56864' and dbo.GD_ObtenerUltimoEstatusTurnoRepartidor(tr.UidTurnoRepartidor)= 'B03E3407-F76D-4DFA-8BF9-7F059DC76141' group by u.uidusuario,u.Nombre,u.Usuario,tr.UidUsuario,tr.DtmHoraFin, tr.UidTurnoRepartidor, orep.UidTurnoRepartidor, tr.DtmHoraInicio,u.ApellidoPaterno,u.ApellidoMaterno order by tr.DtmHoraInicio desc  ";
            return oConexion.Consultas(query);
        }
    }
}
