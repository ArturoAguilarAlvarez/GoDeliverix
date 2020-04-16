using GoDeliverix_TodasLasSucursales.VM;
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

namespace GoDeliverix_TodasLasSucursales
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
            if (instance.MVControlTurno == null)
            {
                instance.MVControlTurno = new VMControlTurno();
            }
            DataContext = instance.MVControlTurno;
        }
    }
}
