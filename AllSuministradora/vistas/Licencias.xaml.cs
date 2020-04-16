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
    /// Interaction logic for Licencias.xaml
    /// </summary>
    public partial class Licencias : Page
    {
        public Licencias()
        {
            InitializeComponent();
            var instance = ControlGeneral.GetInstance();
            if (instance.VMLicencia == null)
            {
                instance.VMLicencia = new VMLicencias();
            }
            if (instance.VMSucursalesLocal == null)
            {
                instance.VMSucursalesLocal = new VMSucursalesLocal();
            }
            DataContext = instance.VMLicencia;
            instance.VMSucursalesLocal.ObtenSucursales();
            SucursalesAgregadas.DataContext = instance.VMSucursalesLocal;
        }
    }
}
