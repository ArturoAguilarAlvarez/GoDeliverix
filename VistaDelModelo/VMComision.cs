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

        private List<VMComision> _ListaDeTipoDeComision;

        public List<VMComision> ListaDeTipoDeComision
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
                FValor = int.Parse(oComosion.FValor.ToString());
                BAbsorveComision = oComosion.BAbsorveComision;
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion
    }
}
