using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form1 : Form
    {
        private Bitmap currentImage;
        private PictureBox pictureBox;
        private string imageFilePath; 

        private void InitializeComponent()
        {
            this.btnLoadImage = new Button();
            this.btnFlipImage = new Button();
            this.pictureBox = new PictureBox();
            this.SuspendLayout();

            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new Point(20, 20);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new Size(120, 30);
            this.btnLoadImage.Text = "Завантажити зображення";
            this.btnLoadImage.Click += new EventHandler(this.BtnLoadImage_Click);

            // 
            // btnFlipImage
            // 
            this.btnFlipImage.Location = new Point(160, 20);
            this.btnFlipImage.Name = "btnFlipImage";
            this.btnFlipImage.Size = new Size(120, 30);
            this.btnFlipImage.Text = "Перевернути";
            this.btnFlipImage.Click += new EventHandler(this.BtnFlipImage_Click);

            // 
            // pictureBox
            // 
            this.pictureBox.Location = new Point(20, 70);
            this.pictureBox.Size = new Size(760, 360);
            this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.btnFlipImage);
            this.Controls.Add(this.pictureBox);
            this.Name = "Form1";
            this.Text = "Зображення";
            this.ResumeLayout(false);
        }

        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.bmp;*.gif;*.jpg;*.jpeg;*.png;*.tiff";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imageFilePath = openFileDialog.FileName; // Store the file path
                    if (IsImageValid(imageFilePath))
                    {
                        currentImage = new Bitmap(imageFilePath);
                        pictureBox.Image = currentImage;
                    }
                    else
                    {
                        MessageBox.Show("Недійсне зображення. Спробуйте вибрати інший файл.");
                    }
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show($"Помилка завантаження зображення: {ex.Message}");
                }
            }
        }

        private bool IsImageValid(string filePath)
        {
            try
            {
                using (Bitmap testImage = new Bitmap(filePath)) { }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void BtnFlipImage_Click(object sender, EventArgs e)
        {
            if (currentImage != null && !string.IsNullOrEmpty(imageFilePath))
            {
                currentImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox.Image = currentImage;

                string saveDirectory = @"C:\Users\koste\source\repos\Lab3_2\Lab3\-mirrored\";
                if (!Directory.Exists(saveDirectory))
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                string fileName = Path.GetFileNameWithoutExtension(imageFilePath); 
                string fileExtension = Path.GetExtension(imageFilePath);
                string savePath = Path.Combine(saveDirectory, fileName + "_mirrored" + fileExtension);

                try
                {
                    currentImage.Save(savePath);
                    MessageBox.Show($"Зображення успішно збережено: {savePath}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при збереженні зображення: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, завантажте зображення перед перевертанням.");
            }
        }

        private Button btnLoadImage;
        private Button btnFlipImage;
    }
}

