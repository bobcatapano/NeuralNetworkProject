using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace NeuralNetworkProject
{
    public static class ConvertImageToFormat
    {
        public static double[,] ConvertToDoubleArray(Bitmap bt)
        {
            int Width = bt.Size.Width;
            int Height = bt.Size.Height;

            double[,] data = new double[Width,Height];
            
            for (int x=0; x < Width; x++)
            {
                for (int y=0; y < Height; y++)
                {
                    data[x,y] = Convert.ToDouble(bt.GetPixel(x, y).ToArgb());
                }
            }

            return data;
        }
    }
}
