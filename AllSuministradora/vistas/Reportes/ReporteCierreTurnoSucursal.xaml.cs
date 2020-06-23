using AllSuministradora.Recursos;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using VistaDelModelo;
using DataSet = Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.DataSet;

namespace AllSuministradora.vistas.Reportes
{
    /// <summary>
    /// Interaction logic for ReporteCierreTurnoSucursal.xaml
    /// </summary>
    public partial class ReporteCierreTurnoSucursal : Window
    {
        string Licencia = "";
        public ReporteCierreTurnoSucursal(string licencia)
        {
            InitializeComponent();
            Licencia = licencia;
        }

        private void RVCierreTurnoSuministradora_Load(object sender, System.EventArgs e)
        {
            VMTurno TUrno = new VMTurno();
            TUrno.InformacionDeCierreDeTurnoSucursalSuministradora("Suministradora", UidLicencia: Licencia);
            DataSet.InformacionCierreTurnoSuministradora turno = new DataSet.InformacionCierreTurnoSuministradora();

            foreach (var item in TUrno.ListaDeInformacionDeTurno)
            {
                turno.LsInformacionOrdenes.Rows.Add(
                  item.LngFolio.ToString(), item.DPagoDeComision, item.DPagosASucursal, item.DPrecioOrden, item.IntComisionSistema, item.DGananciasSucursal);
                if ((item.DGananciasSucursal - item.DPagosASucursal) > 0)
                {
                    turno.InformacionOrdenesPorCobrar.Rows.Add(item.StrEmpresaDistribuidora, item.LngFolio, item.DGananciasSucursal - item.DPagosASucursal);
                }

            }
            var instance = ControlGeneral.GetInstance();
            TUrno.ConsultarUltimoTurnoSuministradora(Licencia);
            var sucursal = instance.VMSucursalesLocal.ListaDeSucursales.Where(x => x.Licencia.ToString() == Licencia).FirstOrDefault();
            turno.InformacionTurno.Rows.Add(sucursal.NombreSucursal, instance.Principal.StrNombre, TUrno.LngFolio, TUrno.DtmHoraInicio, TUrno.DtmHoraFin, TUrno.StrNombreEmpresa, instance.Principal.oTurno.LngFolio);

            DataTable LsInformacionOrdenes = turno.LsInformacionOrdenes;
            DataTable InformacionTurno = turno.InformacionTurno;
            DataTable InformacionOrdenesPorCobrar = turno.InformacionOrdenesPorCobrar;
            ReportDataSource reporte1 = new ReportDataSource("LsInformacionOrdenes", LsInformacionOrdenes);
            ReportDataSource reporte2 = new ReportDataSource("InformacionTurno", InformacionTurno);
            ReportDataSource reporte3 = new ReportDataSource("InformacionOrdenesPorCobrar", InformacionOrdenesPorCobrar);
            RVCierreTurnoSuministradora.LocalReport.DataSources.Add(reporte1);
            RVCierreTurnoSuministradora.LocalReport.DataSources.Add(reporte2);
            RVCierreTurnoSuministradora.LocalReport.DataSources.Add(reporte3);
            RVCierreTurnoSuministradora.LocalReport.ReportEmbeddedResource = "AllSuministradora.vistas.Reportes.RvCierreTurnoSuministradora.rdlc";
            RVCierreTurnoSuministradora.ZoomMode = ZoomMode.Percent;
            RVCierreTurnoSuministradora.ZoomPercent = 100;
            RVCierreTurnoSuministradora.RefreshReport();
        }
    }
}
