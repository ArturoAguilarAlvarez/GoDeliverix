using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    public class VMComision
    {
        #region Propiedades
        private Guid _UidComision;

        public Guid UidComision
        {
            get { return _UidComision; }
            set { _UidComision = value; }
        }
        private Guid _UidEmpresa;

        public Guid UidEmpresa
        {
            get { return _UidEmpresa; }
            set { _UidEmpresa = value; }
        }
        private Guid _TipoDeComision;

        public Guid UidTipoDeComision
        {
            get { return _TipoDeComision; }
            set { _TipoDeComision = value; }
        }
        private string _strNombreTipo;

        public string StrNombreTipoDeComision
        {
            get { return _strNombreTipo; }
            set { _strNombreTipo = value; }
        }

        private int _FValor;

        public int FValor
        {
            get { return _FValor; }
            set { _FValor = value; }
        }
        private bool _BAbsorveComision;

        public bool BAbsorveComision
        {
            get { return _BAbsorveComision; }
            set { _BAbsorveComision = value; }
        }
        private bool _BIncluyeComisionTarjeta;

        public bool BIncluyeComisionTarjeta
        {
            get { return _BIncluyeComisionTarjeta; }
            set { _BIncluyeComisionTarjeta = value; }
        }

        private List<VMComision> _ListaDeTipoDeComision;

        public List<VMComision> ListaDeComisiones
        {
            get { return _ListaDeTipoDeComision; }
            set { _ListaDeTipoDeComision = value; }
        }

        private Comision _oComision;

        public Comision oComosion
        {
            get { return _oComision; }
            set { _oComision = value; }
        }

        #endregion


        #region Metodos
        //Metodo par aobtener los tipos de comision
        //public void ObtenerTipoDeComision()
        //{
        //    ListaDeTipoDeComision = new List<VMComision>();
        //    oComosion = new Comision();
        //    try
        //    {
        //        foreach (DataRow item in oComosion.ObtenerTipoDeComision().Rows)
        //        {
        //            ListaDeTipoDeComision.Add(new VMComision() { UidTipoDeComision = new Guid(item["UidTipoDeComision"].ToString()), StrNombreTipoDeComision = item["VchNombre"].ToString() });
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public void ObtenerComisionPorEmpresa(Guid UidEmpresa)
        {
            UidComision = new Guid();
            UidTipoDeComision = new Guid();
            FValor = 0;
            try
            {
                oComosion = new Comision();
                oComosion.ComisionDeEmpresa(UidEmpresa);
                UidComision = oComosion.UidComision;
                UidTipoDeComision = oComosion.UidTipoDeComision;
                BAbsorveComision = oComosion.BAbsorveComision;
                BIncluyeComisionTarjeta = oComosion.BIncluyeComisionTarjeta;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ObtenerComisiones()
        {
            oComosion = new Comision();
            ListaDeComisiones = new List<VMComision>();
            foreach (DataRow item in oComosion.obtenerComisionesGoDeliverix().Rows)
            {
                ListaDeComisiones.Add(new VMComision() { UidComision = new Guid(item["UidComisionEnvio"].ToString()), StrNombreTipoDeComision = item["StrNombreComision"].ToString(), FValor = int.Parse(item["intComision"].ToString()) });
            }
        }
        public bool ActualizaComisionGoDeliverix(int valorComision, string Comision)
        {
            oComosion = new Comision() { FValor = valorComision, StrNombreTipoDeComision = Comision };
            return oComosion.ActualizarComisionGoDeliverix();
        }

        #region Pasarela de cobros
        public void ObtenerComisionPasarelaDeCobro(string StrNombreProvedor)
        {
            oComosion = new Comision();
            foreach (DataRow item in oComosion.ObtenerComisionPasarelaDeCobro(StrNombreProvedor).Rows)
            {
                UidComision = new Guid(item["UidComisionPasarela"].ToString());
                FValor = int.Parse(item["IntComisionUsoPasarela"].ToString());
            }
        }
        public bool ActualizaComisionTarjeta(int valorComision, string Comision)
        {
            oComosion = new Comision() { FValor = valorComision, UidComision = new Guid(Comision) };
            return oComosion.ActualizaComisionTarjeta();
        }

        public string ObtenerGananciasRepartidor(string valor)
        {
            oComosion = new Comision();
            return oComosion.ObtenerComisionDeGananciaRepartidor(valor);
        }

        public void ObtenerComisionGoDeliverix(string StrNombreDeComision)
        {
            oComosion = new Comision();
            foreach (DataRow item in oComosion.ComisionSistema(StrNombreDeComision).Rows)
            {
                FValor = int.Parse(item["intComision"].ToString());
            }
        }

        #endregion

        public decimal ComisionPagoTarjeta { get; set; }
        public void ObtenerComisionDefault()
        {
            oComosion = new Comision();
            DataTable data = this.oComosion.ObtenerComisionDefault();
            foreach (DataRow row in data.Rows)
            {
                ComisionPagoTarjeta = row.IsNull("IntComisionUsoPasarela") ? decimal.Parse("4.06") : (decimal)row["IntComisionUsoPasarela"];
            }
        }
        #endregion
    }
}
