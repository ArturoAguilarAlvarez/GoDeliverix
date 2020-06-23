using AllSuministradora.Recursos;
using AllSuministradora.VistasDelModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AllSuministradora.vistas
{
    /// <summary>
    /// Interaction logic for ControlTurnos.xaml
    /// </summary>
    public partial class ControlTurnos : Page
    {
        public ControlTurnos()
        {
            InitializeComponent();
            var instance = ControlGeneral.GetInstance();
            if (instance.VMSucursalesLocal == null)
            {
                instance.VMSucursalesLocal = new VMSucursalesLocal();
            }
            TurnoSucursal.DataContext = instance.VMSucursalesLocal;
        }

        private void btnTurno_Click(object sender, RoutedEventArgs e)
        {
            Button turno = sender as Button;
            var instance = ControlGeneral.GetInstance();
            var sucursal = instance.VMSucursalesLocal.ListaDeSucursales.Where(x => x.Licencia.ToString() == turno.CommandParameter.ToString()).FirstOrDefault();
            sucursal.ControlTurno();
        }
    }
}
