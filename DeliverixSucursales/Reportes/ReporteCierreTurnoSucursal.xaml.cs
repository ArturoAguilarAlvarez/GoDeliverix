using Microsoft.Reporting.WinForms;
using System.Data;
using System.Windows;
using VistaDelModelo;

namespace DeliverixSucursales.Reportes
{
    /// <summary>
    /// Interaction logic for ReporteCierreTurnoSucursal.xaml
    /// </summary>
    public partial class ReporteCierreTurnoSucursal : Window
    {
        string Licencia = "";
        string Sucursal = "";
        string Folio = "";
        string Supervisor = "";
        public ReporteCierreTurnoSucursal(string licencia,string sucursal,string folio,string supervisor)
        {
            InitializeComponent();
            Licencia = licencia;
            Sucursal = sucursal;
            Folio = folio;
            Supervisor = supervisor;
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
            
            TUrno.ConsultarUltimoTurnoSuministradora(Licencia);
            turno.InformacionTurno.Rows.Add(Sucursal, Supervisor, TUrno.LngFolio, TUrno.DtmHoraInicio, TUrno.DtmHoraFin, TUrno.StrNombreEmpresa);

            DataTable LsInformacionOrdenes = turno.LsInformacionOrdenes;
            DataTable InformacionTurno = turno.InformacionTurno;
            DataTable InformacionOrdenesPorCobrar = turno.InformacionOrdenesPorCobrar;
            ReportDataSource reporte1 = new ReportDataSource("LsInformacionOrdenes", LsInformacionOrdenes);
            ReportDataSource reporte2 = new ReportDataSource("InformacionTurno", InformacionTurno);
            ReportDataSource reporte3 = new ReportDataSource("InformacionOrdenesPorCobrar", InformacionOrdenesPorCobrar);
            RVCierreTurnoSuministradora.LocalReport.DataSources.Add(reporte1);
            RVCierreTurnoSuministradora.LocalReport.DataSources.Add(reporte2);
            RVCierreTurnoSuministradora.LocalReport.DataSources.Add(reporte3);
            RVCierreTurnoSuministradora.LocalReport.ReportEmbeddedResource = "DeliverixSucursales.Reportes.RvCierreTurnoSuministradora.rdlc";
            RVCierreTurnoSuministradora.ZoomMode = ZoomMode.Percent;
            RVCierreTurnoSuministradora.ZoomPercent = 100;
            RVCierreTurnoSuministradora.RefreshReport();
        }
    }
}
