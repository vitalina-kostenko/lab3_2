using Lab3;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());

        ProcessAndMirrorImages();
    }

    static void ProcessAndMirrorImages()
    {
        string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string inputDirectory = Path.Combine(projectDirectory, "file");

        string outputDirectory = Path.Combine(projectDirectory, "-mirrored");

        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        Regex regexExtForImage = new Regex(@"((bmp)|(gif)|(tiff?)|(jpe?g)|(png))$", RegexOptions.IgnoreCase);

        string[] files = Directory.GetFiles(inputDirectory);

        foreach (var fileName in files)
        {
            try
            {
                if (regexExtForImage.IsMatch(Path.GetExtension(fileName)))
                {
                    using (Bitmap image = new Bitmap(fileName))
                    {
                        image.RotateFlip(RotateFlipType.RotateNoneFlipX);

                        string newFileName = Path.Combine(outputDirectory,
                            Path.GetFileNameWithoutExtension(fileName) + "-mirrored.gif");

                        image.Save(newFileName, System.Drawing.Imaging.ImageFormat.Gif);
                    }
                }
            }
            catch (ArgumentException)
            {
                if (regexExtForImage.IsMatch(Path.GetExtension(fileName)))
                {
                    MessageBox.Show($"{fileName} не є зображенням.");
                }
            }
        }
    }
}
