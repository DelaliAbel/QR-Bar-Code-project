using System.Windows;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Drawing.Printing;
using ZXing;
using ZXing.Windows.Compatibility;

namespace QR___Bar_Code_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
        private Bitmap barcodeBitmap;
        private Bitmap qrcodeBitmap;

        public MainWindow()
		{
			InitializeComponent();
		}


        #region Actions

        //Button generer QR Code
        private void BtnQR_Click(object sender, RoutedEventArgs e)
        {
            qrcodeBitmap = GenerateQRCode(textbox.Text);
            if(qrcodeBitmap != null )
            {
                borderQR.Source = BitmapToImageSource(qrcodeBitmap);
            }
        }

        //Button Generer le Code Barre
        private void BtnCodeBar_Click(object sender, RoutedEventArgs e)
        {
            barcodeBitmap = GenerateBarcode(textbox.Text);
            if(barcodeBitmap != null )
            {
                borderBarCode.Source = BitmapToImageSource(barcodeBitmap);
            }
        }

        //IMPTIMER CODE BARRE
        private void BtnPrintBarCode(object sender, RoutedEventArgs e)
        {
            // Lancer l'impression
            PrintBarcode(barcodeBitmap);
        }
        //IMPTIMER CODE QR
        private void BtnPrintQRCode(object sender, RoutedEventArgs e)
        {
            // Lancer l'impression
            PrintQRcode(qrcodeBitmap);
        }
        //IMPTIMER Text
        private void BtnPrintText(object sender, RoutedEventArgs e)
        {
            // Lancer l'impression
            PrintText("Pour bien apprendre et maîtriser \r\nASP.NET, il est important de \r\nsuivre un parcours structuré \r\nqui couvre à la fois les \r\nfondamentaux et les aspects \r\nplus avancés de ce framework. \r\nVoici un parcours d'apprentissage\r\nétape par étape pour une prise en \r\nmain efficace");

        }

        #endregion

        #region Methodes

        private Bitmap GenerateQRCode(string data)
        {
            // Utiliser ZXing pour générer un QR code
            if (!String.IsNullOrEmpty(data)) { 
                try
                {
                    BarcodeWriter barcodeWriter = new ZXing.Windows.Compatibility.BarcodeWriter
                    {
                        Format = BarcodeFormat.QR_CODE, // Changer ici pour QR_CODE
                        Options = new ZXing.Common.EncodingOptions
                        {
                            Height = 200, // Hauteur du QR code
                            Width = 200,  // Largeur du QR code
                            Margin = 1    // Marge autour du QR code
                        }
                    };

                    return barcodeWriter.Write(data);

                }
                catch (Exception ex)
                {

                    throw;
                }
        }
            return null;
        }

        private Bitmap GenerateBarcode(string data)
        {
            if (!String.IsNullOrEmpty(data))
            {
                try
                {
                    BarcodeWriter barcodeWriter = new ZXing.Windows.Compatibility.BarcodeWriter
                    {
                        Format = BarcodeFormat.CODE_128, // Type de code-barres
                        Options = new ZXing.Common.EncodingOptions
                        {
                            Height = 150, // Hauteur du code-barres
                            Width = 300,  // Largeur du code-barres
                            Margin = 10   // Marge autour du code-barres
                        }
                    };

                    return barcodeWriter.Write(data);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur : ", ex.Message);
                }
                // Générer un code-barres
            }
            return null ;
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            try
            {
                using (var memory = new System.IO.MemoryStream())
                {
                    bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                    memory.Position = 0;
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memory;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    return bitmapImage;
                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private void PrintBarcode(Bitmap barcode)
        {
            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += (sender, e) =>
            {
                e.Graphics.DrawImage(barcode, 0, 0); // Dessiner le code-barres sur la page
            };

            // Définir le nom de l'imprimante Xprinter (changer selon le nom de ton imprimante)
            printDocument.PrinterSettings.PrinterName = "XP-80C";

            // Lancer l'impression
            try
            {
                printDocument.Print();
                MessageBox.Show("Code-barres imprimé avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur d'impression : {ex.Message}");
            }
        }

        private void PrintQRcode(Bitmap qrcode)
        {
            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += (sender, e) =>
            {
                e.Graphics.DrawImage(qrcode, 0, 0); // Dessiner le code-barres sur la page
            };

            // Définir le nom de l'imprimante Xprinter (changer selon le nom de ton imprimante)
            printDocument.PrinterSettings.PrinterName = "XP-80C";

            // Lancer l'impression
            try
            {
                printDocument.Print();
                MessageBox.Show("Code-QR imprimé avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur d'impression : {ex.Message}");
            }
        }

        private void PrintText(string text )
        {
            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += (sender, e) =>
            {
                // Définir la police, la taille, et la couleur du texte
                using (Font font = new Font("consolas",8)) // Police Arial, 14 points, gras
                using (Brush brush = Brushes.DarkBlue) // Couleur bleu foncé
                {
                    // Imprimer le texte à une position spécifique
                    e.Graphics.DrawString(text, font, brush, new PointF(10, 10));
                }
            };

            // Nom de l'imprimante (adapter si nécessaire)
            printDocument.PrinterSettings.PrinterName = "XP-80C";

            // Lancer l'impression
            try
            {
                printDocument.Print();
                MessageBox.Show("Texte formaté imprimé avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur d'impression : {ex.Message}");
            }
        }


        #endregion

    }
}