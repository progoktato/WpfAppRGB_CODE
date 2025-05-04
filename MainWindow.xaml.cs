using Microsoft.Win32;
using RGBSzinek.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppRGB.Models;

namespace WpfAppRGB;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    ObservableCollection<ViewModelRGB> keppontok = new();
    RGBpicManager picManager = new RGBpicManager();

    public MainWindow()
    {
        InitializeComponent();
        dgPixelek.ItemsSource = keppontok;
        cbSzuro.SelectedIndex = 0;
    }


    private void sliderRed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        UpdateRGBTextAndColor();
    }

    private void UpdateRGBTextAndColor()
    {
        // TODO: Update the RGB text and color of the rectangle
    }

    private void sliderBlue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        UpdateRGBTextAndColor();
    }

    private void sliderGreen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        UpdateRGBTextAndColor();
    }

    private void btnLoad_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement the Load button functionality

        // TODO: Handle exceptions
        // Create an OpenFileDialog to select a file

        // picManager.LoadFromTXT(fileName);

        // Update the pixel collection with the loaded data
        // UpdatePixelCollection();

        // TODO: Update the UI with the loaded data

    }

    private void UpdatePixelCollection()
    {
        keppontok.Clear();
        for (int sorIndex = 1; sorIndex <= RGBpicManager.PIC_HEIGHT; sorIndex++)
        {
            for (int oszlopIndex = 1; oszlopIndex <= RGBpicManager.PIC_WIDTH; oszlopIndex++)
            {
                var pixel = picManager.Pixelek[oszlopIndex, sorIndex];
                var ujPixel = new ViewModelRGB(oszlopIndex, sorIndex, pixel);
                keppontok.Add(ujPixel);
            }
        }
        DisplayImageOnCanvas();
    }

    private void DisplayImageOnCanvas()
    {
        WriteableBitmap bitmap = new WriteableBitmap(
            RGBpicManager.PIC_WIDTH,
            RGBpicManager.PIC_HEIGHT,
            96, // DPI (vízszintes)
            96, // DPI (függőleges)
            PixelFormats.Bgr32, // Pixel formátum
            null // Paletta (nem szükséges Bgr32 esetén)
        );

        int stride = RGBpicManager.PIC_WIDTH * 4; // 4 byte per pixel (Bgr32)
        byte[] pixelData = new byte[RGBpicManager.PIC_HEIGHT * stride];

        for (int sorIndex = 1; sorIndex <= RGBpicManager.PIC_HEIGHT; sorIndex++)
        {
            for (int oszlopIndex = 1; oszlopIndex <= RGBpicManager.PIC_WIDTH; oszlopIndex++)
            {
                var pixel = picManager.Pixelek[oszlopIndex, sorIndex];
                int index = ((sorIndex - 1) * stride) + ((oszlopIndex - 1) * 4);

                pixelData[index] = pixel.Blue;      // Blue
                pixelData[index + 1] = pixel.Green; // Green
                pixelData[index + 2] = pixel.Red;   // Red
                pixelData[index + 3] = 255;         // Alpha (teljesen átlátszatlan)
            }
        }

        bitmap.WritePixels(
            new Int32Rect(0, 0, RGBpicManager.PIC_WIDTH, RGBpicManager.PIC_HEIGHT),
            pixelData,
            stride,
            0
        );

        Image image = new Image
        {
            Source = bitmap,
            Width = RGBpicManager.PIC_WIDTH,
            Height = RGBpicManager.PIC_HEIGHT
        };

        canvasKep.Children.Clear();
        canvasKep.Children.Add(image);
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement the Save button functionality

        // picManager.SaveToTXT(fileName);
    }


    private void comboBoxColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // cbSzuro.SelectedIndex == 0 //nincs szűrés
        // cbSzuro.SelectedIndex == 1 //piros
        // cbSzuro.SelectedIndex == 2 //kék
        // cbSzuro.SelectedIndex == 3 //zöld

        // Mikor van sok egy adott színből? Válasz: ha a szín értéke nagyobb mint 200

        // todo : txtPixelekSzama értékének frissítése

    }

    private void dgPixelek_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // todo: Ha nincs kiválasztva pixel, akkor nem csinál semmit

        // todo: Ha van kiválasztva pixel, akkor frissíti a színértékeket a csúszkákon
    }

    private void btnSzín_Click(object sender, RoutedEventArgs e)
    {
        //Színcsere csak akkor ha:
        // - ha nincs szűrés bekapcsolva
        // "Kérem először állítsa vissza a szűrőt az összes pixelre!"

        // Ha van kiválasztva pixel
        // "Kérem válasszon ki egy pixelt!"

        var selectedPixel = (ViewModelRGB)dgPixelek.SelectedItem;
        RGBpixel pixel = picManager.Pixelek[selectedPixel.Oszlop, selectedPixel.Sor];

        // todo : Innen folytassa!
    }

    private void btnFrissit_Click(object sender, RoutedEventArgs e)
    {
        // Ha van mit frissíteni
        DisplayImageOnCanvas();
    }

    private void btnKor_Click(object sender, RoutedEventArgs e)
    {
        // TODO : Implement the Circle button functionality

        // a) Csak akkor, ha van mire rajzolni
        // "Nincs mire rajzolni!";

        // b) Ha számértéket adtunk meg
        // "Valamelyik adat nem értelmezhető számként!"

        // c) Ha a kör középpontja a képen belül van, egyébként
        // "A kör középpontja nem lehet a képen kívül!"

        // d) A kör sugara 1..300 lehet, egyébként
        // "A kör sugara 1..300 lehet!"

        //picManager.DrawCircle(kx, ky, new RGBpixel((byte)sliderRed.Value, (byte)sliderGreen.Value, (byte)sliderBlue.Value), sugar);
        //UpdatePixelCollection();
    }
}
