using System;
using System.Data;

namespace DBControl
{
    public class DBOrden
    {

        Conexion oConexion;

        public void EliminarDeBistacoraOrdenesAEnviar()
        {
            oConexion = new Conexion();
            string query = "";
            oConexion.Consultas(query);
        }

        public void eliminarRegistroOrdenRepartidor(string uidRelacionOrdenRepartidor)
        {
            oConexion = new Conexion();
            string Query = "delete from BitacoraDeEstatusOrdenRepartidor where UidOrdenRepartidor in (select UidRelacionOrdenTarifario from OrdenTarifario where UidRelacionOrdenTarifario in (select UidOrden from OrdenRepartidor where UidRelacionOrdenRepartidor = '" + uidRelacionOrdenRepartidor + "')); delete from OrdenRepartidor where UidRelacionOrdenRepartidor = '" + uidRelacionOrdenRepartidor + "'; ";
            oConexion.Consultas(Query);
        }

        public DataTable BuscarOrden(string strcodigo, string licencia)
        {
            oConexion = new Conexion();
            string query = "select ot.UidOrden, s.Identificador,os.IntFolio, dbo.EstatusActualDeOrden(ot.UidOrden) as estatus, u.Nombre from OrdenTarifario ot inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario inner join ZonaDeRecoleccion ZDR on ZDR.UidZonaDeRecolecta = t.UidRelacionZonaRecolecta inner join Sucursales s on s.UidSucursal = ZDR.UidSucursal inner join OrdenSucursal os on os.UidRelacionOrdenSucursal = ot.UidOrden inner join OrdenRepartidor orep on orep.UidOrden = ot.UidRelacionOrdenTarifario inner join TurnoRepartidor tr on tr.UidTurnoRepartidor = orep.UidTurnoRepartidor inner join Usuarios u on u.UidUsuario = tr.UidUsuario where ot.UidCodigo = '" + strcodigo + "' ";
            return oConexion.Consultas(query);
        }

        public DataTable VerificarExistenciaDeNota(string uidRegistroDeProdutoEnOrden)
        {
            oConexion = new Conexion();
            string query = "Select * from NotasProductosEnOrden where UidListaDeProducto = '" + uidRegistroDeProdutoEnOrden + "' ";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerNotaDeProducto(Guid uidProductoEnOrden)
        {
            oConexion = new Conexion();
            string query = "Select * from NotasProductosEnOrden where UidListaDeProducto = '" + uidProductoEnOrden.ToString() + "' ";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerUidUsuarioPorUidOrdenSucursal(Guid uidOrden)
        {
            oConexion = new Conexion();
            string query = "select u.UidUsuario from Usuarios u inner join OrdenUsuario ou on ou.UidUsuario = u.UidUsuario inner join Ordenes o on o.UidOrden = ou.UidOrden inner join OrdenSucursal os on os.UidOrden = o.UidOrden where os.UidRelacionOrdenSucursal = '" + uidOrden.ToString() + "' ";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerOrdenRepartidor(Guid UidTurnoRepartidor)
        {
            oConexion = new Conexion();
            string query = " select top 1 OrdR.UidRelacionOrdenRepartidor, ot.UidOrden,ot.UidRelacionOrdenTarifario,os.UidRelacionOrdenSucursal, s.UidSucursal, s.Identificador,os.IntFolio,o.UidDireccion as DireccionCliente," +
                " dbo.asp_ObtenerUltimoEstatusOrdenTarifario(ot.UidRelacionOrdenTarifario) as EstatusOrdenTarifario,dbo.EstatusActualDeOrden(os.UidRelacionOrdenSucursal) as EstatusOrdenGeneral, dbo.asp_ObtenerUltimoEstatusOrdenRepartidor(OrdR.UidRelacionOrdenRepartidor) as EstatusOrdenRepartidor from OrdenTarifario ot " +
                " inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario inner " +
                " join ZonaDeRecoleccion ZDR on ZDR.UidZonaDeRecolecta = t.UidRelacionZonaRecolecta inner " +
                " join OrdenSucursal os on os.UidRelacionOrdenSucursal = ot.UidOrden inner " +
                " join Sucursales s on s.UidSucursal = os.UidSucursal inner " +
                " join OrdenRepartidor OrdR on ot.UidRelacionOrdenTarifario = OrdR.UidOrden inner join ordenes o on o.UidOrden = os.UidOrden " +
                "where OrdR.UidTurnoRepartidor = '" + UidTurnoRepartidor.ToString() + "' order by OrdR.dtmFechaAsignacion desc";
            return oConexion.Consultas(query);
        }

        public DataTable RecuperarCodigoOrdenTarifario(Guid uidOrdenTarifario)
        {
            oConexion = new Conexion();
            string query = "Select UidCodigo from OrdenTarifario where UidRelacionOrdenTarifario = '" + uidOrdenTarifario.ToString() + "'";
            return oConexion.Consultas(query);
        }

        public DataTable VerificaCodigoDeEntrega(string strCodigo,string UidTurnoRepartidor)
        {
            oConexion = new Conexion();
            string query = "select * from ordensucursal os inner join ordentarifario ot on ot.uidorden = os.uidrelacionordensucursal inner join OrdenRepartidor orep on orep.UidOrden = ot.UidRelacionOrdenTarifario where os.BIntCodigoEntrega = "+strCodigo+ " and orep.UidTurnoRepartidor = '" + UidTurnoRepartidor+ "' and dbo.asp_ObtenerUltimoEstatusOrdenRepartidor(orep.UidRelacionOrdenRepartidor) != '12748F8A-E746-427D-8836-B54432A38C07'";
            return oConexion.Consultas(query);
        }

        public DateTime ObtenerHoraSucursal(string UidOrdenSucursal)
        {
            DateTime dt = new DateTime();

            oConexion = new Conexion();
            string query = "select dbo.ObtenerHoraCliente(s.uiddireccion) as hora from sucursales s inner join OrdenSucursal os on os.UidSucursal = s.UidSucursal where os.UidRelacionOrdenSucursal = '"+ UidOrdenSucursal + "'";
            foreach (DataRow item in oConexion.Consultas(query).Rows)
            {
                dt = DateTime.Parse(item["hora"].ToString());
            };
            return dt;
        }
    }
}
