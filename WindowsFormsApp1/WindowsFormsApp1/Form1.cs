using Filters_1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;




namespace WindowsFormsApp1
{
  public partial class Form1 : Form
  {
    Bitmap image;

    private Bitmap imageHistory;

    bool[,] kernel = { { false, true, false }, { true, true, true }, { false, true, false } };


    public Form1()
    {
      InitializeComponent();
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
      
    }

    private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();

      dialog.Filter = "Image files | *.png; *.jpg; *.bmp | All Files (*.*) | *.*";

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        image = new Bitmap(dialog.FileName);

        pictureBox1.Image = image;

        pictureBox1.Refresh();
      }
    }

    private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      InvertFillers filter = new InvertFillers();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
      Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);

      if (backgroundWorker1.CancellationPending != true)
        image = newImage;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      backgroundWorker1.CancelAsync();
    }

    private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      progressBar1.Value = e.ProgressPercentage;
    }

    private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (!e.Cancelled)
      {
        pictureBox1.Image = image;

        pictureBox1.Refresh();
      }
      progressBar1.Value = 0;
    }

    private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new BlurFilter();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void фильтрГауссаToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new GaussianFilter();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void чернобелоеToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      GrayScaleFilter filter = new GrayScaleFilter();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SepiaFilter filter = new SepiaFilter();

      AddFilterToHistory();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void большеЯркостиToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      BrightnessFilter filter = new BrightnessFilter();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void фильтрСобеляToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new SobelFilter();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void повышениеРезкостиToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new SharpnessFilter();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void переносToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new RelocateFilter();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void поворотToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new TurnFilter();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
      saveFileDialog1.Title = "Save an Image File";
      saveFileDialog1.ShowDialog();

      if (saveFileDialog1.FileName != "")
      {
        System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();

        switch (saveFileDialog1.FilterIndex)
        {
          case 1:
            this.pictureBox1.Image.Save(fs,
              System.Drawing.Imaging.ImageFormat.Jpeg);
            break;

          case 2:
            this.pictureBox1.Image.Save(fs,
              System.Drawing.Imaging.ImageFormat.Bmp);
            break;

          case 3:
            this.pictureBox1.Image.Save(fs,
              System.Drawing.Imaging.ImageFormat.Gif);
            break;
        }

        fs.Close();
      }
    }

    private void toolStripMenuItem2_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new WaveFilter1();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void toolStripMenuItem3_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new WaveFilter2();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void операторЩарраToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new SharraFilter();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void операторПрюиттаToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new PriuttaFilter();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void серыйМирToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new GrayWorldFilter();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void медианныйToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new MedianFilter();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void button2_Click(object sender, EventArgs e)
    {
      UndoLastAction();
    }

    private void UndoLastAction()
    {
      if (imageHistory != null)
      {
        pictureBox1.Image = imageHistory;
        image = imageHistory;
        imageHistory = null;
        MessageBox.Show("Отменено последнее действие");
        
      }
      else
      {
        MessageBox.Show("Можно отменить только одно действие");
      }
    }

    private void AddFilterToHistory()
    {
      imageHistory = image;
    }

    private void гистограммаЯркостиToolStripMenuItem_Click(object sender, EventArgs e)
    {
      BrightnessHistogramViewer view = new BrightnessHistogramViewer();

      view.ShowBrightnessHistogram(image);

      backgroundWorker1.RunWorkerAsync(view);
    }

    private void растяжениеГистограммыToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new LinearHistogramStretchingFilter();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void motionBlurToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new MotionBlur();

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void расширениеToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new Dilation(kernel);

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void сужениеToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new Erosion(kernel);

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void закрытиеToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new Closing(kernel);

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void раскрытиеToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new Opening(kernel);

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void градиентToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddFilterToHistory();

      Filters filter = new Grad(kernel);

      backgroundWorker1.RunWorkerAsync(filter);
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (comboBox1.SelectedIndex == 0)
      {
        kernel = new bool[,] { { false, true, false }, { true, true, true }, { false, true, false } };
      }
      if (comboBox1.SelectedIndex == 1)
      {
        kernel = new bool[,] { { true, true, true }, { true, false, true }, { true, true, true } };
      }
      if (comboBox1.SelectedIndex == 2)
      {
        kernel = new bool[,] { { true, true, true }, { true, true, true }, { true, true, true } };
      }
      if (comboBox1.SelectedIndex == 3)
      {
        kernel = new bool[,] { { true, false, true }, { false, true, false }, { true, false, true } };
      }
    }
  }
}



namespace Filters_1
{
  abstract class Filters
  {
    protected abstract Color calculateNewPixelColor(Bitmap sourceImage, int x, int y);

    public virtual Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
    {
      Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

      for (int i = 0; i < resultImage.Width; i++)
      {
        worker.ReportProgress((int)((float)i / resultImage.Width * 100));

        if (worker.CancellationPending)
          return null;

        for (int j = 0; j < resultImage.Height; j++)
        {
          resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
        }
      }
      return resultImage;
    }

    public int Clamp(int value, int min, int max)
    {
      if (value < min)
        return min;
      if (value > max)
        return max;
      return value;
    }
  }

  class MatrixFilter : Filters
  {
    protected float[,] kernel = null;
    protected MatrixFilter() { }
    public MatrixFilter(float[,] kernel)
    {
      this.kernel = kernel;
    }
    protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
    {
      int radiusX = kernel.GetLength(0) / 2;
      int radiusY = kernel.GetLength(1) / 2;

      float resultR = 0;
      float resultG = 0;
      float resultB = 0;

      for (int l = -radiusY; l <= radiusY; l++)
      {
        for (int k = -radiusX; k <= radiusX; k++)
        {
          int idX = Clamp(x + k, 0, sourceImage.Width - 1);
          int idY = Clamp(y + l, 0, sourceImage.Height - 1);
          Color neighborColor = sourceImage.GetPixel(idX, idY);
          resultR += neighborColor.R * kernel[k + radiusX, l + radiusY];
          resultG += neighborColor.G * kernel[k + radiusX, l + radiusY];
          resultB += neighborColor.B * kernel[k + radiusX, l + radiusY];
        }
      }
      return Color.FromArgb(Clamp((int)resultR, 0, 255), Clamp((int)resultG, 0, 255), Clamp((int)resultB, 0, 255));
    }
  }

  class BlurFilter : MatrixFilter
  {
    public BlurFilter() 
    {
      int sizeX = 3;
      int sizeY = 3;
      kernel = new float[sizeX, sizeY];
      for (int i = 0; i < sizeX; i++)
        for (int j = 0; j < sizeY; j++)
          kernel[i, j] = 1.0f / (float)(sizeX * sizeY);
    }
  }


  class InvertFillers : Filters
  {
    protected override Color calculateNewPixelColor(Bitmap sourceImage1, int x, int y)
    {
      Color sourceColor = sourceImage1.GetPixel(x, y);

      Color resultColor = Color.FromArgb(255 - sourceColor.R, 255 - sourceColor.G, 255 - sourceColor.B);

      return resultColor;
    }
  }

  class GaussianFilter : MatrixFilter
  {
    public GaussianFilter()
    {
      createGaussianKernel(3, 2);
    }
    public void createGaussianKernel(int radius, float sigma)
    {
      int size = 2 * radius + 1;

      kernel = new float[size, size];

      float norm = 0;

      for (int i = -radius;i < radius;i++)
      {
        for (int j = -radius; j <= radius; j++)
        {
          kernel[i + radius, j + radius] = (float)(Math.Exp(-(i * i + j * j) / (sigma * sigma)));

          norm += kernel[i + radius, j + radius];
        }
      }

      for (int i = 0; i < size; i++)
        for (int j = 0; j < size; j++)
          kernel[i, j] /= norm;
    }
  }

  class GrayScaleFilter : Filters
  {
    protected override Color calculateNewPixelColor(Bitmap sourceImage1, int x, int y)
    {
      Color sourceColor = sourceImage1.GetPixel(x, y);

      int Intensity = (int)(0.36 * sourceColor.R) + (int)(0.53 * sourceColor.G) + (int)(0.11 * sourceColor.B);

      Color resultColor = Color.FromArgb(Clamp(Intensity, 0, 255), Clamp(Intensity, 0, 255), Clamp(Intensity, 0, 255));

      return resultColor;
    }
  }

  class SepiaFilter : Filters
  {
    protected override Color calculateNewPixelColor(Bitmap sourceImage1, int x, int y)
    {
      Color sourceColor = sourceImage1.GetPixel(x, y);

      int Intensity = (int)(0.36 * sourceColor.R) + (int)(0.53 * sourceColor.G) + (int)(0.11 * sourceColor.B);

      Color resultColor = Color.FromArgb(Clamp((int)(Intensity + 2 * 15.8), 0, 255), Clamp((int)(Intensity + 0.5 * 15.8), 0, 255), Clamp((int)(Intensity - 1 * 15.8), 0, 255));

      return resultColor;
    }
  }

  class BrightnessFilter : Filters
  {
    protected override Color calculateNewPixelColor(Bitmap sourceImage1, int x, int y)
    {
      Color sourceColor = sourceImage1.GetPixel(x, y);

      Color resultColor = Color.FromArgb(Clamp(sourceColor.R + 70, 0, 255), Clamp(sourceColor.G + 70, 0, 255), Clamp(sourceColor.B + 70, 0, 255));

      return resultColor;
    }
  }

  class SobelFilter : Filters
  {
    float[,] kernelX = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };

    float[,] kernelY = { { -1, 2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

    public SobelFilter()
    {
    }

    protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
    {
      int radiusX = kernelX.GetLength(0) / 2;
      int radiusY = kernelY.GetLength(1) / 2;

      float resultRX = 0;
      float resultGX = 0;
      float resultBX = 0;

      float resultRY = 0;
      float resultGY = 0;
      float resultBY = 0;

      for (int l = -radiusY; l <= radiusY; l++)
      {
        for (int k = -radiusX; k <= radiusX; k++)
        {
          int idX = Clamp(x + k, 0, sourceImage.Width - 1);
          int idY = Clamp(y + l, 0, sourceImage.Height - 1);

          Color neighborColor = sourceImage.GetPixel(idX, idY);

          resultRX += neighborColor.R * kernelX[k + radiusX, l + radiusY];
          resultGX += neighborColor.G * kernelX[k + radiusX, l + radiusY];
          resultBX += neighborColor.B * kernelX[k + radiusX, l + radiusY];

          resultRY += neighborColor.R * kernelX[k + radiusX, l + radiusY];
          resultGY += neighborColor.G * kernelX[k + radiusX, l + radiusY];
          resultBY += neighborColor.B * kernelX[k + radiusX, l + radiusY];
        }
      }

      int newR = (int)(Math.Sqrt(Math.Pow(resultRX, 2) + Math.Pow(resultRY, 2)));
      int newG = (int)(Math.Sqrt(Math.Pow(resultGX, 2) + Math.Pow(resultGY, 2)));
      int newB = (int)(Math.Sqrt(Math.Pow(resultBX, 2) + Math.Pow(resultBY, 2)));

      return Color.FromArgb(Clamp(newR, 0, 255), Clamp(newG, 0, 255), Clamp(newB, 0, 255));
    }
  }

  class SharpnessFilter : MatrixFilter
  {
    private float[,] kernel1 = { { -1, -1, -1 }, { -1, 9, -1 }, { -1, -1, -1 } }; 
    public SharpnessFilter()
    {
      this.kernel = kernel1;
    }
  }

  class RelocateFilter : Filters
  {
    protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
    {

      int newX = x + 50; 

      if (newX >= 0 && newX < sourceImage.Width)
      {
        return sourceImage.GetPixel(newX, y);
      }
      else
      {
        return Color.Black; 
      }
    }
  }

  class TurnFilter : Filters
  {
    protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
    {
      int midX = sourceImage.Width / 2;
      int midY = sourceImage.Height / 2;
      double mu = Math.PI / 6;

      int newX = (int)((x - midX) * Math.Cos(mu) - (y - midY) * Math.Sin(mu) + midX);
      int newY = (int)((x - midX) * Math.Sin(mu) + (y - midY) * Math.Cos(mu) + midY);

      if (newX >= 0 && newX < sourceImage.Width && newY >= 0 && newY < sourceImage.Height)
      {
        return sourceImage.GetPixel(newX, newY);
      }
      else
      {
        return Color.Black;
      }
    }
  }

  class WaveFilter1 : Filters
  {
    protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
    {
      int newX, newY;
      newX = Clamp((int)(x + 20 * Math.Sin(2 * Math.PI * y / 60)), 0, sourceImage.Width - 1);
      newY = Clamp(y, 0, sourceImage.Height - 1);
      Color sourceColor = sourceImage.GetPixel(newX, newY);
      Color resultColor = Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);

      return resultColor;
    }
  }

  class WaveFilter2 : Filters
  {
    protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
    {
      int newX, newY;
      newX = Clamp((int)(x + 20 * Math.Sin(2 * Math.PI * x / 30)), 0, sourceImage.Width - 1);
      newY = Clamp(y, 0, sourceImage.Height - 1);
      Color sourceColor = sourceImage.GetPixel(newX, newY);
      Color resultColor = Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);

      return resultColor;
    }
  }

  class SharraFilter : Filters
  {
    float[,] kernelX = { { 3, 10, 3 }, { 0, 0, 0 }, { -3, -10, -3 } };

    float[,] kernelY = { { 3, 0, -3 }, { 10, 0, -10 }, { 3, 0, -3 } };

    public SharraFilter()
    {
    }

    protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
    {
      int radiusX = kernelX.GetLength(0) / 2;
      int radiusY = kernelY.GetLength(1) / 2;

      float resultRX = 0;
      float resultGX = 0;
      float resultBX = 0;

      float resultRY = 0;
      float resultGY = 0;
      float resultBY = 0;

      for (int l = -radiusY; l <= radiusY; l++)
      {
        for (int k = -radiusX; k <= radiusX; k++)
        {
          int idX = Clamp(x + k, 0, sourceImage.Width - 1);
          int idY = Clamp(y + l, 0, sourceImage.Height - 1);

          Color neighborColor = sourceImage.GetPixel(idX, idY);

          resultRX += neighborColor.R * kernelX[k + radiusX, l + radiusY];
          resultGX += neighborColor.G * kernelX[k + radiusX, l + radiusY];
          resultBX += neighborColor.B * kernelX[k + radiusX, l + radiusY];

          resultRY += neighborColor.R * kernelX[k + radiusX, l + radiusY];
          resultGY += neighborColor.G * kernelX[k + radiusX, l + radiusY];
          resultBY += neighborColor.B * kernelX[k + radiusX, l + radiusY];
        }
      }

      int newR = (int)(Math.Sqrt(Math.Pow(resultRX, 2) + Math.Pow(resultRY, 2)));
      int newG = (int)(Math.Sqrt(Math.Pow(resultGX, 2) + Math.Pow(resultGY, 2)));
      int newB = (int)(Math.Sqrt(Math.Pow(resultBX, 2) + Math.Pow(resultBY, 2)));

      return Color.FromArgb(Clamp(newR, 0, 255), Clamp(newG, 0, 255), Clamp(newB, 0, 255));
    }
  }

  class PriuttaFilter : Filters
  {
    float[,] kernelX = { { -1, -1, -1 }, { 0, 0, 0 }, { 1, 1, 1 } };

    float[,] kernelY = { { -1, 0, 1 }, { -1, 0, 1 }, { -1, 0, 1 } };

    public PriuttaFilter()
    {
    }

    protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
    {
      int radiusX = kernelX.GetLength(0) / 2;
      int radiusY = kernelY.GetLength(1) / 2;

      float resultRX = 0;
      float resultGX = 0;
      float resultBX = 0;

      float resultRY = 0;
      float resultGY = 0;
      float resultBY = 0;

      for (int l = -radiusY; l <= radiusY; l++)
      {
        for (int k = -radiusX; k <= radiusX; k++)
        {
          int idX = Clamp(x + k, 0, sourceImage.Width - 1);
          int idY = Clamp(y + l, 0, sourceImage.Height - 1);

          Color neighborColor = sourceImage.GetPixel(idX, idY);

          resultRX += neighborColor.R * kernelX[k + radiusX, l + radiusY];
          resultGX += neighborColor.G * kernelX[k + radiusX, l + radiusY];
          resultBX += neighborColor.B * kernelX[k + radiusX, l + radiusY];

          resultRY += neighborColor.R * kernelX[k + radiusX, l + radiusY];
          resultGY += neighborColor.G * kernelX[k + radiusX, l + radiusY];
          resultBY += neighborColor.B * kernelX[k + radiusX, l + radiusY];
        }
      }

      int newR = (int)(Math.Sqrt(Math.Pow(resultRX, 2) + Math.Pow(resultRY, 2)));
      int newG = (int)(Math.Sqrt(Math.Pow(resultGX, 2) + Math.Pow(resultGY, 2)));
      int newB = (int)(Math.Sqrt(Math.Pow(resultBX, 2) + Math.Pow(resultBY, 2)));

      return Color.FromArgb(Clamp(newR, 0, 255), Clamp(newG, 0, 255), Clamp(newB, 0, 255));
    }
  }

  class GrayWorldFilter : Filters
  {
    protected int avg;
    protected int r, g, b;

    protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
    {
      Color sourceColor = sourceImage.GetPixel(x, y);
      Color resultColor;
      if (r > 0 && g > 0 && b > 0)
      {
        resultColor = Color.FromArgb(Clamp((int)(sourceColor.R * avg / r), 0, 255), Clamp((int)(sourceColor.G * avg / g), 0, 255), Clamp((int)(sourceColor.B * avg / b), 0, 255));
        return resultColor;
      }

      return sourceColor;
    }

    public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
    {
      Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
      r = 0;
      g = 0;
      b = 0;
      avg = 0;

      for (int i = 0; i < sourceImage.Width; i++)
      {
        for (int j = 0; j < sourceImage.Height; j++)
        {
          Color sourceColor = sourceImage.GetPixel(i, j);
          r += sourceColor.R;
          g += sourceColor.G;
          b += sourceColor.B;
        }
      }

      r = (int)(r / (sourceImage.Width * sourceImage.Height));
      g = (int)(g / (sourceImage.Width * sourceImage.Height));
      b = (int)(b / (sourceImage.Width * sourceImage.Height));

      avg = (r + g + b) / 3;

      for (int i = 0; i < sourceImage.Width; i++)
      {
        worker.ReportProgress((int)((float)i / resultImage.Width * 100));
        for (int j = 0; j < sourceImage.Height; j++)
        {
          resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
        }
      }

      return resultImage;
    }
  }

  class MedianFilter : Filters
  {
    protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
    {
      int radius = 3; 
      int size = 2 * radius + 1;
      int[] redValues = new int[size * size];
      int[] greenValues = new int[size * size];
      int[] blueValues = new int[size * size];
      int index = 0;

      for (int i = -radius; i <= radius; i++)
      {
        for (int j = -radius; j <= radius; j++)
        {
          int newX = Clamp(x + i, 0, sourceImage.Width - 1);
          int newY = Clamp(y + j, 0, sourceImage.Height - 1);
          Color neighborColor = sourceImage.GetPixel(newX, newY);
          redValues[index] = neighborColor.R;
          greenValues[index] = neighborColor.G;
          blueValues[index] = neighborColor.B;
          index++;
        }
      }

      Array.Sort(redValues);
      Array.Sort(greenValues);
      Array.Sort(blueValues);

      int medianIndex = size * size / 2;

      return Color.FromArgb(Clamp(redValues[medianIndex], 0, 255), Clamp(greenValues[medianIndex], 0, 255), Clamp(blueValues[medianIndex], 0, 255));
    }
  }
  
  class BrightnessHistogramViewer : Filters
  {
    public void ShowBrightnessHistogram(Bitmap sourceImage)
    {
      int[] histogram = new int[256]; 


      for (int i = 0; i < sourceImage.Width; i++)
      {
        for (int j = 0; j < sourceImage.Height; j++)
        {
          Color pixelColor = sourceImage.GetPixel(i, j);
          int brightness = (int)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B);
          histogram[brightness]++;
        }
      }

      Chart chart = new Chart();
      chart.Size = new Size(600, 400);
      chart.ChartAreas.Add(new ChartArea());

      Series series = new Series();
      series.ChartType = SeriesChartType.Column;

      for (int i = 0; i < histogram.Length; i++)
      {
        series.Points.AddXY(i, histogram[i]);
      }

      chart.Series.Add(series);

      chart.ChartAreas[0].AxisX.Minimum = 0;

      Form histogramForm = new Form();
      histogramForm.Text = "Гистограмма яркости";
      histogramForm.Size = new Size(618, 425);
      histogramForm.Controls.Add(chart);

      histogramForm.ShowDialog();
    }

    protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
    {
      return sourceImage.GetPixel(x, y);
    }
  }

  class LinearHistogramStretchingFilter : Filters
  {
    protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
    {
        Color pixelColor = sourceImage.GetPixel(x, y);

        int intensity = (int)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B);

        return StretchIntensity(sourceImage, pixelColor, intensity);
    }

    private Color StretchIntensity(Bitmap sourceImage, Color originalColor, int intensity)
    {
        int minIntensity = 255;
        int maxIntensity = 0;

        for (int i = 0; i < sourceImage.Width; i++)
        {
            for (int j = 0; j < sourceImage.Height; j++)
            {
                Color srcColor = sourceImage.GetPixel(i, j);
                int srcIntensity = (int)(0.299 * srcColor.R + 0.587 * srcColor.G + 0.114 * srcColor.B);

                if (srcIntensity < minIntensity)
                    minIntensity = srcIntensity;
                if (srcIntensity > maxIntensity)
                    maxIntensity = srcIntensity;
            }
        }

        int newIntensity = (int)((255.0 / (maxIntensity - minIntensity)) * (intensity - minIntensity));
        newIntensity = Clamp(newIntensity, 0, 255);

        int newRed = originalColor.R * newIntensity / intensity;
        int newGreen = originalColor.G * newIntensity / intensity;
        int newBlue = originalColor.B * newIntensity / intensity;

        return Color.FromArgb(Clamp(newRed, 0, 255), Clamp(newGreen, 0, 255), Clamp(newBlue, 0, 255));
    }
  }

  class MotionBlur : MatrixFilter
  {
    public void createMotionBlurCernel(int n)
    {
      int size = n;
      kernel = new float[size, size];

      for (int i = 0; i < size; i++)
        for (int j = 0; j < size; j++)
        {
          if (i == j)
            kernel[i, j] = ((float)1 / n);
          else
            kernel[i, j] = 0;
        }
    }

    public MotionBlur()
    {
      createMotionBlurCernel(9); 
    }
  }

  class MathMorphology : Filters
  {
    protected bool isDilation;
    protected bool[,] kernel = null;

    protected MathMorphology()
    { 
    }

    public MathMorphology(bool[,] kernel)
    {
      this.kernel = kernel;
    }

    protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
    {
      int max = 0;
      int min = int.MaxValue;
      Color clr = Color.Black;
      int radiusX = kernel.GetLength(0) / 2;
      int radiusY = kernel.GetLength(1) / 2;
      for (int l = -radiusY; l <= radiusY; l++)
        for (int k = -radiusX; k <= radiusX; k++)
        {
          int idX = Clamp(x + k, 0, sourceImage.Width - 1);
          int idY = Clamp(y + l, 0, sourceImage.Height - 1);
          Color sourceColor = sourceImage.GetPixel(idX, idY);
          int intensity = (int)(0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B);
          if (isDilation)
          {
            if ((kernel[k + radiusX, l + radiusY]) && (intensity > max))
            {
              max = intensity;
              clr = sourceColor;
            }
          }
          else
          {
            if (intensity < min)
            {
              min = intensity;
              clr = sourceColor;
            }
          }
        }

      return clr;
    }

  }

  class Dilation : MathMorphology
  {
    public Dilation(bool[,] _kernel)
    {
      isDilation = true;
      this.kernel = _kernel;
    }
  }

  class Erosion : MathMorphology
  {
    public Erosion(bool[,] _kernel)
    {
      isDilation = false;
      this.kernel = _kernel;
    }
  }

  class Opening : MathMorphology
  {
    public Opening(bool[,] _kernel)
    {
      this.kernel = _kernel;
    }

    public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
    {
      Bitmap currImage = new Bitmap(sourceImage.Width, sourceImage.Height);
      Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
      isDilation = false;
      for (int i = 0; i < sourceImage.Width; i++)
      {
        worker.ReportProgress((int)((float)i / resultImage.Width * 100));
        for (int j = 0; j < sourceImage.Height; j++)
        {
          currImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
        }
      }
      isDilation = true;
      for (int i = 0; i < currImage.Width; i++)
      {
        worker.ReportProgress((int)((float)i / resultImage.Width * 100));
        for (int j = 0; j < currImage.Height; j++)
        {
          resultImage.SetPixel(i, j, calculateNewPixelColor(currImage, i, j));
        }
      }
      return resultImage;
    }
  }

  class Closing : MathMorphology
  {
    public Closing(bool[,] _kernel)
    {
      this.kernel = _kernel;
    }

    public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
    {
      Bitmap currImage = new Bitmap(sourceImage.Width, sourceImage.Height);
      Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
      isDilation = true;
      for (int i = 0; i < sourceImage.Width; i++)
      {
        worker.ReportProgress((int)((float)i / resultImage.Width * 100));
        for (int j = 0; j < sourceImage.Height; j++)
        {
          currImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
        }
      }
      isDilation = false;
      for (int i = 0; i < currImage.Width; i++)
      {
        worker.ReportProgress((int)((float)i / resultImage.Width * 100));
        for (int j = 0; j < currImage.Height; j++)
        {
          resultImage.SetPixel(i, j, calculateNewPixelColor(currImage, i, j));
        }
      }
      return resultImage;
    }
  }

  class Grad : MathMorphology
  {
    public Grad(bool[,] _kernel)
    {
      isDilation = true;
      this.kernel = _kernel;
    }

    protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
    {
      int max = 0;
      int min = int.MaxValue;
      Color gradColor = Color.Black;
      int radiusX = kernel.GetLength(0) / 2;
      int radiusY = kernel.GetLength(1) / 2;

      for (int l = -radiusY; l <= radiusY; l++)
      {
        for (int k = -radiusX; k <= radiusX; k++)
        {
          int idX = Clamp(x + k, 0, sourceImage.Width - 1);
          int idY = Clamp(y + l, 0, sourceImage.Height - 1);

          Color sourceColor = sourceImage.GetPixel(idX, idY);
          int intensity = (int)(0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B);

          if ((kernel[k + radiusX, l + radiusY]) && (intensity > max))
          {
            max = intensity;
            gradColor = sourceColor;
          }

          if (intensity < min)
          {
            min = intensity;
          }
        }
      }

      int gradIntensity = Clamp(max - min, 0, 255);

      return Color.FromArgb(gradIntensity, gradIntensity, gradIntensity);
    }
  }
}