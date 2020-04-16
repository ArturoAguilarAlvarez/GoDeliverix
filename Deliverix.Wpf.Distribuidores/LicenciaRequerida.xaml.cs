using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
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
using System.Windows.Shapes;

namespace DeliverixSucursales
{
    /// <summary>
    /// Interaction logic for LicenciaRequerida.xaml
    /// </summary>
    public partial class LicenciaRequerida : Window
    {
        public LicenciaRequerida(String QR = "")
        {
            InitializeComponent();
            if (String.IsNullOrEmpty(QR))
            {
                lblMensaje.Visibility = Visibility.Visible;
                CodigoQr.Visibility = Visibility.Hidden;
            }
            else
            {
                lblMensaje.Visibility = Visibility.Hidden;
                CodigoQr.Visibility = Visibility.Visible;

                QrEncoder encoder = new QrEncoder(ErrorCorrectionLevel.M);
                QrCode qrCode;
                encoder.TryEncode(QR, out qrCode);
                WriteableBitmapRenderer wRenderer = new WriteableBitmapRenderer(new FixedModuleSize(2, QuietZoneModules.Two), Colors.Black, Colors.White);
                WriteableBitmap wBitmap = new WriteableBitmap(70, 70, 96, 96, PixelFormats.Gray8, null);
                wRenderer.Draw(wBitmap, qrCode.Matrix);

                CodigoQr.Source = wBitmap;
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
