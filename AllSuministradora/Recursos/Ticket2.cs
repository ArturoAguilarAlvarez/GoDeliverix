// Decompiled with JetBrains decompiler
// Type: LibPrintTicket.Ticket
// Assembly: LibPrintTicket, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0314F60F-8D02-4A73-9D48-3297CA17E79B
// Assembly location: C:\Users\Abimael Serralta\Downloads\Telegram Desktop\LibPrintTicket.dll

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using VistaDelModelo;
using LibPrintTicket;
namespace TestLib
{
    public class Ticket2
    {
        private ArrayList headerLines = new ArrayList();
        private ArrayList subHeaderLines = new ArrayList();
        private ArrayList items = new ArrayList();
        private ArrayList totales = new ArrayList();
        private ArrayList footerLines = new ArrayList();
        private int maxChar = 35;
        private int maxCharDescription = 20;
        private float topMargin = 1f;
        private string fontName = "Arial";
        private float fontSize = 6.5F;
        private SolidBrush myBrush = new SolidBrush(Color.Black);
        private Image headerImage;
        private Image footerImage;
        private int count;
        private int imageHeight;
        private float leftMargin;
        private Font printFont;
        private Graphics gfx;
        private string line;
        StringBuilder linea = new StringBuilder();

        string Posicion = string.Empty;
        int fontSizePosicion = 12;
        string var1 = string.Empty;
        string var2 = string.Empty;
        string var3 = string.Empty;

        public Image HeaderImage
        {
            get
            {
                return this.headerImage;
            }
            set
            {
                if (this.headerImage == value)
                    return;
                this.headerImage = value;
            }
        }

        public Image FooterImage
        {
            get
            {
                return this.footerImage;
            }
            set
            {
                if (this.footerImage == value)
                    return;
                this.footerImage = value;
            }
        }

        public int MaxChar
        {
            get
            {
                return this.maxChar;
            }
            set
            {
                if (value == this.maxChar)
                    return;
                this.maxChar = value;
            }
        }

        public int MaxCharDescription
        {
            get
            {
                return this.maxCharDescription;
            }
            set
            {
                if (value == this.maxCharDescription)
                    return;
                this.maxCharDescription = value;
            }
        }

        public float FontSize
        {
            get
            {
                return this.fontSize;
            }
            set
            {
                if (value == this.fontSize)
                    return;
                this.fontSize = value;
            }
        }

        public string FontName
        {
            get
            {
                return this.fontName;
            }
            set
            {
                if (!(value != this.fontName))
                    return;
                this.fontName = value;
            }
        }

        public float VariablesGlobal { get; private set; }
        Font f = null;
        public Ticket2()
        {
            VariablesGlobal = 10;
            var cvt = new FontConverter();
            string s = cvt.ConvertToString(fontName);
            f = cvt.ConvertFromString(s) as Font;
        }
        public void AddHeaderLine(string line)
        {
            this.headerLines.Add((object)line);
        }

        public void AddSubHeaderLine(string line)
        {
            this.subHeaderLines.Add((object)line);
        }

        public void AddItem(string cantidad, string item, string price)
        {
            this.items.Add((object)new OrderItem('?').GenerateItem(cantidad, item, price));
        }

        public void AddTotal(string name, string price)
        {
            this.totales.Add((object)new OrderTotal('?').GenerateTotal(name, price));
        }

        public void AddFooterLine(string line)
        {
            this.footerLines.Add((object)line);
        }

        private string AlignRightText(int lenght)
        {
            string str = "";
            int num = this.maxChar - lenght;
            for (int index = 0; index < num; ++index)
                str += " ";
            return str;
        }

        private string DottedLine()
        {
            string str = "";
            for (int index = 0; index < this.maxChar; ++index)
                str += "=";
            return str;
        }

        public bool PrinterExists(string impresora)
        {
            foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
            {
                if (impresora == installedPrinter)
                    return true;
            }
            return false;
        }

        public void PrintTicket(string impresora)
        {
            this.printFont = new Font(this.fontName, (float)this.fontSize, FontStyle.Regular);
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings.PrinterName = impresora;
            printDocument.PrintPage += new PrintPageEventHandler(this.pr_PrintPage);
            printDocument.Print();
        }

        private void pr_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            this.gfx = e.Graphics;
            this.DrawImage();
            this.DrawHeader();
            this.DrawSubHeader();
            this.DrawItems();
            this.DrawTotales();
            this.DrawFooter();
            this.DrawImageFooter();
            if (this.headerImage == null)
                return;
            this.HeaderImage.Dispose();
            this.headerImage.Dispose();
        }

        private float YPosition()
        {
            return this.topMargin + ((float)this.count * this.printFont.GetHeight(this.gfx) + (float)this.imageHeight);
        }

        private void DrawImage()
        {
            if (this.headerImage == null)
                return;
            try
            {
                this.gfx.DrawImage(this.headerImage, new Point((int)this.leftMargin, (int)this.YPosition()));
                this.imageHeight = (int)Math.Round((double)this.headerImage.Height / 58.0 * 15.0) + 3;
            }
            catch (Exception ex)
            {
            }
        }

        private void DrawImageFooter()
        {
            if (this.footerImage == null)
                return;
            try
            {
                this.gfx.DrawImage(this.footerImage, new Point((int)this.leftMargin, (int)this.YPosition()));
                this.imageHeight = (int)Math.Round((double)this.footerImage.Height / 58.0 * 15.0) + 3;
            }
            catch (Exception ex)
            {
            }
        }

        private void DrawHeader()
        {
            foreach (string headerLine in this.headerLines)
            {
                if (headerLine.Length > this.maxChar)
                {
                    int startIndex = 0;
                    for (int length = headerLine.Length; length > this.maxChar; length -= this.maxChar)
                    {
                        this.line = headerLine.Substring(startIndex, this.maxChar);
                        this.gfx.DrawString(this.line, this.printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        ++this.count;
                        startIndex += this.maxChar;
                    }
                    this.line = headerLine;
                    this.gfx.DrawString(this.line.Substring(startIndex, this.line.Length - startIndex), this.printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    ++this.count;
                }
                else
                {
                    this.line = headerLine;
                    this.gfx.DrawString(this.line, this.printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    ++this.count;
                }
            }
            //this.DrawEspacio();
            this.gfx.DrawString("       " + Posicion, new Font(fontName, fontSizePosicion, FontStyle.Regular), (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
        }

        private void DrawSubHeader()
        {

            Font printFont = new Font(f.FontFamily, VariablesGlobal, FontStyle.Regular);

            foreach (string subHeaderLine in this.subHeaderLines)
            {
                if (subHeaderLine.Length > this.maxChar)
                {
                    int startIndex = 0;
                    for (int length = subHeaderLine.Length; length > this.maxChar; length -= this.maxChar)
                    {
                        this.line = subHeaderLine;
                        this.gfx.DrawString(this.line.Substring(startIndex, this.maxChar), printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        ++this.count;
                        startIndex += this.maxChar;
                    }
                    this.line = subHeaderLine;
                    this.gfx.DrawString(this.line.Substring(startIndex, this.line.Length - startIndex), printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    ++this.count;
                }
                else
                {
                    this.line = subHeaderLine;
                    this.gfx.DrawString(this.line, printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    ++this.count;

                    if (this.line != string.Empty)
                    {
                        this.line = this.DottedLine();
                    }


                    this.gfx.DrawString(this.line, printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    ++this.count;
                }
            }
        }

        private void DrawItems()
        {
            Font printFont = new Font(f.FontFamily, VariablesGlobal, FontStyle.Regular);

            OrderItem orderItem1 = new OrderItem('?');

            if (var1 != string.Empty || var2 != string.Empty || var3 != string.Empty)
            {
                this.gfx.DrawString(var1 + "  " + var2 + "           " + var3, printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.DrawEspacio();
            }

            ++this.count;
            foreach (string orderItem2 in this.items)
            {
                this.line = orderItem1.GetItemCantidad(orderItem2);
                this.gfx.DrawString(this.line, printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = orderItem1.GetItemPrice(orderItem2);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                string itemName = orderItem1.GetItemName(orderItem2);
                this.leftMargin = 0.0f;
                if (itemName.Length > this.maxCharDescription)
                {
                    int startIndex = 0;
                    for (int length = itemName.Length; length > this.maxCharDescription; length -= this.maxCharDescription)
                    {
                        this.line = orderItem1.GetItemName(orderItem2);
                        this.gfx.DrawString("      " + this.line.Substring(startIndex, this.maxCharDescription), printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        ++this.count;
                        startIndex += this.maxCharDescription;
                    }
                    this.line = orderItem1.GetItemName(orderItem2);
                    this.gfx.DrawString("      " + this.line.Substring(startIndex, this.line.Length - startIndex), printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    ++this.count;
                }
                else
                {
                    this.gfx.DrawString("      " + orderItem1.GetItemName(orderItem2), printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    ++this.count;
                }
            }
            this.leftMargin = 0.0f;
            if (this.line != string.Empty)
            {
                //this.DrawEspacio();
                this.line = this.DottedLine();
            }
            this.gfx.DrawString(this.line, printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            ++this.count;
        }

        private void DrawTotales()
        {
            OrderTotal orderTotal = new OrderTotal('?');
            foreach (string totale in this.totales)
            {
                this.line = orderTotal.GetTotalCantidad(totale);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, this.printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.leftMargin = 0.0f;
                this.line = "      " + orderTotal.GetTotalName(totale);
                this.gfx.DrawString(this.line, this.printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                ++this.count;
            }
            this.leftMargin = 0.0f;
            //this.DrawEspacio();
            //this.DrawEspacio();
        }

        private void DrawFooter()
        {
            foreach (string footerLine in this.footerLines)
            {
                if (footerLine.Length > this.maxChar)
                {
                    int startIndex = 0;
                    for (int length = footerLine.Length; length > this.maxChar; length -= this.maxChar)
                    {
                        this.line = footerLine;
                        this.gfx.DrawString(this.line.Substring(startIndex, this.maxChar), this.printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        ++this.count;
                        startIndex += this.maxChar;
                    }
                    this.line = footerLine;
                    this.gfx.DrawString(this.line.Substring(startIndex, this.line.Length - startIndex), this.printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    ++this.count;
                }
                else
                {
                    this.line = footerLine;
                    this.gfx.DrawString(this.line, this.printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    ++this.count;
                }
            }
            this.leftMargin = 0.0f;
            this.DrawEspacio();
        }

        private void DrawEspacio()
        {
            this.line = "";
            this.gfx.DrawString(this.line, this.printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            ++this.count;
        }

        #region Metodos Propios
        public void PosicionLlave(string Posicion, int fontSizePosicion)
        {
            this.Posicion = Posicion;
            this.fontSizePosicion = fontSizePosicion;
        }

        public void Cabecera(string var1, string var2, string var3)
        {
            this.var1 = var1;
            this.var2 = var2;
            this.var3 = var3;
        }

        public void TextoCentro()
        {
            string texto = "Esta sucursal no se hace responsable por daños o siniestros para mayor aclaraciones llamar a los hombres de negro, Y SI LOS HOMBRES DE NEGRO NO LO RESUELVEN por favor llama a los desarrolladores con la extension conocida...";
            //string texto = "Esta sucursal no se ";

            //Si la longitud del texto es mayor al numero maximo de caracteres permitidos, realizar el siguiente procedimiento.
            if (texto.Length > maxChar)
            {
                int caracterActual = 0;//Nos indicara en que caracter se quedo al bajar el texto a la siguiente linea
                for (int longitudTexto = texto.Length; longitudTexto > maxChar; longitudTexto -= maxChar)
                {
                    //Agregamos los fragmentos que salgan del texto
                    //linea.AppendLine(texto.Substring(caracterActual, maxChar));
                    this.line = texto.Substring(caracterActual, maxChar);
                    this.gfx.DrawString(this.line, this.printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());

                    caracterActual += maxChar;
                    this.leftMargin = 0.0f;
                    this.DrawEspacio();
                }
                //Variable para poner espacios restntes
                string espacios = "";
                //Obtenemos la longitud del texto restante.
                for (int i = 0; i < (maxChar - texto.Substring(caracterActual, texto.Length - caracterActual).Length); i++)
                {
                    //espacios += " ";//Agrega espacios para alinear a la derecha
                }

                //agregamos el fragmento restante, agregamos antes del texto los espacios
                //linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));
                this.line = espacios + texto.Substring(caracterActual, texto.Length - caracterActual);

                this.gfx.DrawString(this.line, this.printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.leftMargin = 0.0f;
                this.DrawEspacio();
            }
            else
            {
                string espacios = "";
                //Obtenemos la longitud del texto restante.
                for (int i = 0; i < (maxChar - texto.Length); i++)
                {
                    //espacios += " ";//Agrega espacios para alinear a la derecha
                }
                //Si no es mayor solo agregarlo.
                //linea.AppendLine(espacios + texto);
                this.line = espacios + texto;
                this.gfx.DrawString(this.line, this.printFont, (Brush)this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            }
            this.leftMargin = 0.0f;
            this.DrawEspacio();
        }

        public void ImgHead(Bitmap img)
        {

        }
        #endregion
    }
}
