using System;
using System.Collections.Generic;
using System.Linq;
using Modelo;
using DBControl;
using System.Data.SqlClient;
using System.Data;

namespace VistaDelModelo
{
    public class VMContrato
    {
        #region Propiedades

        protected DBContrato Datos = new DBContrato();
        private Guid _UidContrato;

        public Guid Uid
        {
            get { return _UidContrato; }
            set { _UidContrato = value; }
        }
        private Guid _UidSucursalSuministradora;

        public Guid UidSucursalSuministradora
        {
            get { return _UidSucursalSuministradora; }
            set { _UidSucursalSuministradora = value; }
        }
        private Guid _UidSucursalDistribuidora;

        public Guid UidSucursalDistribuidora
        {
            get { return _UidSucursalDistribuidora; }
            set { _UidSucursalDistribuidora = value; }
        }
        private Guid _UidEstatus;

        public Guid UidEstatus
        {
            get { return _UidEstatus; }
            set { _UidEstatus = value; }
        }
        private bool _blConfirmacionSuministradora;

        public bool BlConfirmacionSuministadora
        {
            get { return _blConfirmacionSuministradora; }
            set { _blConfirmacionSuministradora = value; }
        }
        private bool _blConfirmacionDistribuidora;

        public bool BlConfirmacionDistribuidora
        {
            get { return _blConfirmacionDistribuidora; }
            set { _blConfirmacionDistribuidora = value; }
        }

        private bool _BlPagoAlRecolectar;

        public bool BiPagoAlRecoletar
        {
            get { return _BlPagoAlRecolectar; }
            set { _BlPagoAlRecolectar = value; }
        }

        private int _PorcentajeComisionContrato;

        public int IntPorcentajeComisionContrato
        {
            get { return _PorcentajeComisionContrato; }
            set { _PorcentajeComisionContrato = value; }
        }
        private int _ComisionDistribuidora;

        public int IntComisionDistribuidora
        {
            get { return _ComisionDistribuidora; }
            set { _ComisionDistribuidora = value; }
        }
        private int _IntComisionEnvioGodeliverix;

        public int IntComisionEnvioGoDeliverix
        {
            get { return _IntComisionEnvioGodeliverix; }
            set { _IntComisionEnvioGodeliverix = value; }
        }


        public List<VMContrato> ListaDeSucursalesEnContrato = new List<VMContrato>();

        #endregion
        #region Metodo
        public void SeleccionarEmpresa(Guid UidContrato, Guid UidSuministradora, Guid UidDistribuidora, Guid UidEstatus, bool ConfirmacionSuministradora, bool ConfirmacionDistribuidora, bool PagoAlRecolectar, int comisionProducto)
        {
            if (!ListaDeSucursalesEnContrato.Exists(sucursal => sucursal.UidSucursalSuministradora == UidSuministradora && sucursal.UidSucursalDistribuidora == UidDistribuidora))
            {
                ListaDeSucursalesEnContrato.Add(new VMContrato() { Uid = UidContrato, UidSucursalSuministradora = UidSuministradora, UidSucursalDistribuidora = UidDistribuidora, UidEstatus = UidEstatus, BlConfirmacionSuministadora = ConfirmacionSuministradora, BlConfirmacionDistribuidora = ConfirmacionDistribuidora, BiPagoAlRecoletar = PagoAlRecolectar, IntPorcentajeComisionContrato = comisionProducto });
            }
        }
        public void DeseleccionarEmpresa(Guid UidSucursal)
        {
            if (ListaDeSucursalesEnContrato.Exists(sucursal => sucursal.UidSucursalSuministradora == UidSucursal || sucursal.UidSucursalDistribuidora == UidSucursal))
            {
                var Sucursal = ListaDeSucursalesEnContrato.Find(sucursal => sucursal.UidSucursalSuministradora == UidSucursal || sucursal.UidSucursalDistribuidora == UidSucursal);
                ListaDeSucursalesEnContrato.Remove(Sucursal);
            }
        }
        public void ObtenRelacionContrato(string valor)
        {
            foreach (DataRow item in Datos.RecuperarRelacionContrato(valor).Rows)
            {
                Guid UidContrato = new Guid(item["UidContrato"].ToString());
                Guid Suministradora = new Guid(item["UidSucursalSuministradora"].ToString());
                Guid Distribuidora = new Guid(item["UidSucursalDistribuidora"].ToString());
                Guid UidEstatus = new Guid(item["UidEstatusContrato"].ToString());
                bool blConfirmacionSuministradora = bool.Parse(item["BlConfirmacionSuministradora"].ToString());
                bool blConfirmacionDistribuidora = bool.Parse(item["BlConfirmacionDistribuidora"].ToString());
                bool PagoAlRecolectar = false;
                int ComisionDelSistema = 0;
                if (!string.IsNullOrEmpty(item["BiPagaAlRecogerOrdenes"].ToString()))
                {
                    PagoAlRecolectar = bool.Parse(item["BiPagaAlRecogerOrdenes"].ToString());
                }
                if (!string.IsNullOrEmpty(item["ComisionDistribuidora"].ToString()))
                {
                    ComisionDelSistema = int.Parse(item["ComisionDistribuidora"].ToString());
                }
                SeleccionarEmpresa(UidContrato, Suministradora, Distribuidora, UidEstatus, blConfirmacionSuministradora, blConfirmacionDistribuidora, PagoAlRecolectar, ComisionDelSistema);
            }
        }
        public void GuardaRelacionDeContrato()
        {
            foreach (var item in ListaDeSucursalesEnContrato)
            {
                Datos.GuardaRelacionDeContrato(item.Uid, item.UidSucursalSuministradora, item.UidSucursalDistribuidora, item.UidEstatus, item.BlConfirmacionSuministadora, item.BlConfirmacionDistribuidora, item.BiPagoAlRecoletar,item.IntPorcentajeComisionContrato);
            }
        }

        public void borrarSucursalSuministradora(string UidSucursal)
        {
            Datos.borrarRelacionSuministradora(UidSucursal);
        }

        public void borrarSucursalDistribuidora(string UidSucursal)
        {
            Datos.borrarRelacionDistribuidora(UidSucursal);
        }
        /// <summary>
        /// Verifica si el contrato de esa orden es para pagar al recolectar
        /// devuelve true si tiene que pagar de lo contrario retorna false
        /// </summary>
        /// <param name="uidSucursal"></param>
        /// <param name="licencia"></param>
        /// <returns></returns>
        public bool VerificaPagoARecolectar(string uidSucursal = "", string licencia = "", string UidOrden = "")
        {
            bool resultado = false;
            if (string.IsNullOrEmpty(UidOrden))
            {
                if (Datos.PagaAlRecolectar(uidSucursal, licencia).Rows.Count == 1)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }
            else
            {
                if (Datos.PagaAlRecolectarLaOrden(UidOrden).Rows.Count == 1)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }

            return resultado;
        }


        #endregion
    }
}
