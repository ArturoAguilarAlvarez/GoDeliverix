using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
namespace GoDeliverix_TodasLasSucursales.VM
{
    public class VMControlTurno : NotifyBase
    {
        #region Propiedades


        private List<TurnoItem> _ListaDeSucursales;

        public List<TurnoItem> ListaDeSucursales
        {
            get { return _ListaDeSucursales; }
            set { _ListaDeSucursales = value; OnPropertyChanged("_ListaDeSucursales"); }
        }

        #region Propiedades de la vista del modelo
        VMEmpresas MVEmpresas;
        VMSucursales MVSucursales;
        VMTurno MVTurno;
        #endregion
        #endregion

        #region Metodos
        public VMControlTurno()
        {
            MVEmpresas = new VMEmpresas();
            MVSucursales = new VMSucursales();
            MVTurno = new VMTurno();
            ListaDeSucursales = new List<TurnoItem>();
            MVEmpresas.BuscarEmpresas(tipo: 1, status: 1);
            foreach (var item in MVEmpresas.LISTADEEMPRESAS)
            {
                MVSucursales.BuscarSucursales(Uidempresa: item.UIDEMPRESA.ToString());
                foreach (var sucursal in MVSucursales.LISTADESUCURSALES)
                {
                    if (!MVTurno.TurnoAbierto(sucursal.ID))
                    {
                        TurnoItem control = new TurnoItem()
                        {
                            NombreEmpresa = item.NOMBRECOMERCIAL,
                            NombreSucursal = sucursal.IDENTIFICADOR,
                            HorarioSucursal = sucursal.HORAAPARTURA + " - " + sucursal.HORACIERRE
                        };
                        ListaDeSucursales.Add(control);
                    }
                }
            }
        }
        #endregion
    }
}
