using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;

namespace PoS.Tickets
{
    public class Ticket
    {
        ArrayList headerLines = new ArrayList();
        ArrayList subHeaderLines = new ArrayList();
        ArrayList items = new ArrayList();
        ArrayList totals = new ArrayList();
        ArrayList footerLines = new ArrayList();
        private Image headerImage = null;

        int count = 0;

        int maxChar = 35;
        int maxCharDescription = 20;

        int imageHeight = 0;

        float leftMargin = 0;
        float topMargin = 3;

        string fontName = "Lucida Console";
        int fontSize = 9;

        Font printFont = null;
        SolidBrush myBrush = new SolidBrush(Color.Black);

        Graphics gfx = null;

        string line = null; 

        public Ticket()
        {
           
        }

        public Image HeaderImage
        {
            get { return headerImage; }
            set { if (headerImage != value) headerImage = value; }
        }

        public int MaxChar
        {
            get { return maxChar; }
            set { if (value != maxChar) maxChar = value; }
        }

        public int MaxCharDescription
        {
            get { return maxCharDescription; }
            set { if (value != maxCharDescription) maxCharDescription = value; }
        }

        public int FontSize
        {
            get { return fontSize; }
            set { if (value != fontSize) fontSize = value; }
        }

        public string FontName
        {
            get { return fontName; }
            set { if (value != fontName) fontName = value; }
        }

        public void AddHeaderLine(string line)
        {
            headerLines.Add(line);
        }

        public void AddSubHeaderLine(string line)
        {
            subHeaderLines.Add(line);
        }

        public void AddItem(string qty,string item, string price)
        {
            OrderItem newItem = new OrderItem('?');
            items.Add(newItem.GenerateItem(qty,item, price));
        }

        public void AddTotal(string name,string price)
        {
            OrderTotal newTotal = new OrderTotal('?');
            totals.Add(newTotal.GenerateTotal(name, price));
        }

        public void AddFooterLine(string line)
        {
            footerLines.Add(line);
        }   

        private string AlignRightText(int lenght)
        {
            string blanks = "";
            int spaces = maxChar - lenght;
            for (int x = 0; x < spaces; x++)
                blanks += " ";
            return blanks;
        }

        private string DottedLine()
        {
            string dotted = "";
            for (int x = 0; x < maxChar; x++)
                dotted += "=";
            return dotted;
        }

        public bool PrinterExists(string printer)
        {
            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                if (printer == strPrinter)
                    return true;
            }
            return false;
        }

        public void PrintTicket(string printer)
        {
            printFont = new Font(fontName, fontSize, FontStyle.Regular);
            PrintDocument pr = new PrintDocument();
            pr.PrinterSettings.PrinterName = printer;
            pr.PrintPage += new PrintPageEventHandler(pr_PrintPage);
            pr.Print();
        }

        private void pr_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            gfx = e.Graphics;

            DrawImage();
            DrawHeader();
            DrawSubHeader();
            DrawItems();
            DrawTotals();
            DrawFooter();

            if (headerImage != null)
            {
                HeaderImage.Dispose();
                headerImage.Dispose();
            }
        }

        private float YPosition()
        {
            return topMargin + (count * printFont.GetHeight(gfx) + imageHeight);
        }

        private void DrawImage()
        {
            if (headerImage != null)
            {
                try
                {
                    gfx.DrawImage(headerImage, new Point((int)leftMargin, (int)YPosition()));
                    double height = ((double)headerImage.Height / 58) * 15;
                    imageHeight = (int)Math.Round(height) + 3;
                }
                catch (Exception)
                {
                }
            }
        }

        private void DrawHeader()
        {
            foreach(string header in headerLines)
            {
                if (header.Length > maxChar)
                {
                    int currentChar = 0;
                    int headerLenght = header.Length;

                    while (headerLenght > maxChar)
                    {
                        line = header.Substring(currentChar, maxChar);
                        gfx.DrawString(line, printFont, myBrush, leftMargin,YPosition(), new StringFormat());

                        count++;
                        currentChar += maxChar;
                        headerLenght -= maxChar;
                    }
                    line = header;
                    gfx.DrawString(line.Substring(currentChar, line.Length - currentChar), printFont, myBrush, leftMargin,YPosition(), new StringFormat());
                    count++;
                }
                else
                {
                    line = header;
                    gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                    count++;
                }
            }
            DrawBlank();
        }   

        private void DrawSubHeader()
        {
            foreach (string subHeader in subHeaderLines)
            {
                if (subHeader.Length > maxChar)
                {
                    int currentChar = 0;
                    int subHeaderLenght = subHeader.Length;

                    while (subHeaderLenght > maxChar)
                    {
                        line = subHeader;
                        gfx.DrawString(line.Substring(currentChar, maxChar), printFont, myBrush, leftMargin,YPosition(), new StringFormat());

                        count++;
                        currentChar += maxChar;
                        subHeaderLenght -= maxChar;
                    }
                    line = subHeader;
                    gfx.DrawString(line.Substring(currentChar, line.Length - currentChar), printFont, myBrush, leftMargin, YPosition(), new StringFormat());
                    count++;
                }
                else
                {
                    line = subHeader;

                    gfx.DrawString(line, printFont, myBrush, leftMargin,YPosition(), new StringFormat());

                    count++;

                    line = DottedLine();

                    gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                    count++;
                }
            }
            DrawBlank();
        }

        private void DrawItems()
        {
            OrderItem ordIt = new OrderItem('?');

            gfx.DrawString("Qty  Description           Amount", printFont, myBrush, leftMargin, YPosition(), new StringFormat());

            count++;
            DrawBlank();

            foreach (string item in items)
            {
                line = ordIt.GetItemQty(item);

                gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                line = ordIt.GetItemPrice(item);
                line = AlignRightText(line.Length) + line;

                gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                string name = ordIt.GetItemName(item);

                leftMargin = 0;
                if (name.Length > maxCharDescription)
                {
                    int currentChar = 0;
                    int itemLenght = name.Length;

                    while (itemLenght > maxCharDescription)
                    {
                        line = ordIt.GetItemName(item);
                        gfx.DrawString("      " + line.Substring(currentChar, maxCharDescription), printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                        count++;
                        currentChar += maxCharDescription;
                        itemLenght -= maxCharDescription;
                    }

                    line = ordIt.GetItemName(item);
                    gfx.DrawString("      " + line.Substring(currentChar, line.Length - currentChar), printFont, myBrush, leftMargin, YPosition(), new StringFormat());
                    count++;
                }
                else
                {
                    gfx.DrawString("      " + ordIt.GetItemName(item), printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                    count++;
                }
            }

            leftMargin = 0;
            DrawBlank();
            line = DottedLine();

            gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

            count++;
            DrawBlank();
        }

        private void DrawTotals()
        {
            OrderTotal ordTot = new OrderTotal('?');

            foreach (string total in totals)
            {
                line = ordTot.GetTotalCantidad(total);
                line = AlignRightText(line.Length) + line;

                gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());
                leftMargin = 0;

                line = "      "+ordTot.GetTotalName(total);
                gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());
                count++;
            }
            leftMargin = 0;
            DrawBlank();
            DrawBlank();
        }

        private void DrawFooter()
        {
            foreach (string footer in footerLines)
            {
                if (footer.Length > maxChar)
                {
                    int currentChar = 0;
                    int footerLenght = footer.Length;

                    while (footerLenght > maxChar)
                    {
                        line = footer;
                        gfx.DrawString(line.Substring(currentChar, maxChar), printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                        count++;
                        currentChar += maxChar;
                        footerLenght -= maxChar;
                    }
                    line = footer;
                    gfx.DrawString(line.Substring(currentChar, line.Length - currentChar), printFont, myBrush, leftMargin, YPosition(), new StringFormat());
                    count++;
                }
                else
                {
                    line = footer;
                    gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

                    count++;
                }
            }
            leftMargin = 0;
            DrawBlank();
        }

        private void DrawBlank()
        {
            line = "";

            gfx.DrawString(line, printFont, myBrush, leftMargin, YPosition(), new StringFormat());

            count++;
        }
    }

    public class OrderItem
    {
        char[] delimiter = new char[] { '?' };

        public OrderItem(char delimit)
        {
            delimiter = new char[] {delimit };
        }

        public string GetItemQty(string orderItem)
        {
            string [] delimited = orderItem.Split(delimiter);
            return delimited[0];
        }

        public string GetItemName(string orderItem)
        {
            string [] delimited = orderItem.Split(delimiter);
            return delimited[1];
        }

        public string GetItemPrice(string orderItem)
        {
            string[] delimited = orderItem.Split(delimiter);
            return delimited[2];
        }

        public string GenerateItem(string qty, string itemName, string price)
        {
            return qty + delimiter[0] + itemName + delimiter[0] + price;
        }
    }

    public class OrderTotal
    {
        char[] delimiter = new char[] { '?' };

        public OrderTotal(char delimit)
        {
            delimiter = new char[] { delimit };
        }

        public string GetTotalName(string totalItem)
        {
            string[] delimited = totalItem.Split(delimiter);
            return delimited[0];
        }

        public string GetTotalCantidad(string totalItem)
        {
            string[] delimited = totalItem.Split(delimiter);
            return delimited[1];
        }

        public string GenerateTotal(string totalName, string price)
        {
            return totalName + delimiter[0] + price;
        }
    }
}