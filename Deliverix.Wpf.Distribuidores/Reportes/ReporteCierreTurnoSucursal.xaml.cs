
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using VistaDelModelo;
using DataSet = Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.DataSet;

namespace Deliverix.Wpf.Distribuidores.Reportes
{
    /// <summary>
    /// Interaction logic for ReporteCierreTurnoSucursal.xaml
    /// </summary>
    public partial class ReporteCierreTurnoSucursal : Window
    {
        string Licencia = "";
        string UidUsuario = "";
        string UidTurnoSucursal = "";
        public ReporteCierreTurnoSucursal(string licencia, string uidturnosucursal, string uidusuario)
        {
            InitializeComponent();
            Licencia = licencia;
            UidTurnoSucursal = uidturnosucursal;
            UidUsuario = uidusuario;
        }

        private void RVCierreTurnoSuministradora_Load(object sender, System.EventArgs e)
        {
            VMTurno TUrno = new VMTurno();
            TUrno.InformacionDeCierreDeTurnoSucursalSuministradora("Distribuidora", UidLicencia: Licencia);
            DataSet.InformacionCierreTurnoDistribuidora ordenes = new DataSet.InformacionCierreTurnoDistribuidora();
            decimal ingresos = 0;
            foreach (var item in TUrno.ListaDeInformacionDeTurno)
            {
                ordenes.InformacionDeOrdenes.Rows.Add(item.LngFolioGeneral, item.LngFolio, item.DPagosASucursal, item.DGananciasSucursal, item.DGanancias, item.DPagoDeComision, item.StrTipoDePagoDeOrden, item.StrEmpresaDistribuidora, item.strNombreSucursal, item.strUsuario, item.DPropina, item.DTotalEnvio, item.BPagoAlRecolectar);
                ingresos += item.DGananciasSucursal;
            }
            var MVTurno = new VMTurno();
            
            var MVusuario = new VMUsuarios();
            MVusuario.obtenerDatosDeSupervisor(new Guid(UidUsuario));



            MVTurno.ConsultarUltimoTurnoDistribuidora(Licencia);

            ordenes.InformacionDeTurno.Rows.
                Add(
                MVusuario.StrNombre,
                MVusuario.NombreEmpresa,
                MVusuario.Sucursal,
                ingresos.ToString(),
                MVTurno.LngFolio,
                MVTurno.DtmHoraFin,
                MVTurno.DtmHoraInicio,
                MVTurno.DLiquidacion,
                MVTurno.DRecarga
                );
            DataTable InformacionDeOrdenes = ordenes.InformacionDeOrdenes;
            DataTable InformacionDeTurno = ordenes.InformacionDeTurno;
            ReportDataSource reporte1 = new ReportDataSource("InformacionDeOrdenes", InformacionDeOrdenes);
            ReportDataSource reporte2 = new ReportDataSource("InformacionDeTurno", InformacionDeTurno);

            RVCierreTurnoSuministradora.LocalReport.DataSources.Add(reporte1);
            RVCierreTurnoSuministradora.LocalReport.DataSources.Add(reporte2);
            RVCierreTurnoSuministradora.LocalReport.ReportEmbeddedResource = "Deliverix.Wpf.Distribuidores.Reportes.RVCierreDeTurnoDistribuidora.rdlc";
            RVCierreTurnoSuministradora.ZoomMode = ZoomMode.Percent;
            RVCierreTurnoSuministradora.ZoomPercent = 100;
            RVCierreTurnoSuministradora.RefreshReport();
        }
    }
}
