using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace CreateTrainAndTestMatrix
{
    public static class Util
    {
        //private int height = 0;
        //private int length = 0;


        public static IEnumerable<string> GetAllFilesFromDirectory(string path)
        {
            var files = Directory.GetFiles(path);
            return files.ToList<string>();
        
        }

        [DllImport("msvcrt.dll")]
        private static extern int memcmp(IntPtr b1, IntPtr b2, long count);

        public static bool CompareMemCmp(Bitmap b1, Bitmap b2)
        {
            if ((b1 == null) != (b2 == null)) return false;
            if (b1.Size != b2.Size) return false;

            var bd1 = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bd2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                IntPtr bd1scan0 = bd1.Scan0;
                IntPtr bd2scan0 = bd2.Scan0;

                int stride = bd1.Stride;
                int len = stride * b1.Height;

                return memcmp(bd1scan0, bd2scan0, len) == 0;
            }
            finally
            {
                b1.UnlockBits(bd1);
                b2.UnlockBits(bd2);
            }
        }

        private static Dimensions returnImageDimensions(double[,] trainData)
        { // This is duplicate code. Turn into a static method in a Util class
            int height = 0;
            int length = 0;
            Dimensions theDimensions = new Dimensions();
            int numberOfDimensions = trainData.Rank; 

            for (int dimension = 1; dimension <= numberOfDimensions; dimension++)
            {
                if (dimension == 1)
                    length = trainData.GetUpperBound(dimension - 1) + 1;
                if (dimension ==2)
                    height = trainData.GetUpperBound(dimension - 1) + 1;
            }
            ////////////////////////////////////////////////
            theDimensions.length = length;
            theDimensions.height = height;

            return theDimensions;
        }

            // Convert 2 Dimension data array into 1 dimension Matrice 
        public static Array ReturnOneDimensionArray(double[,] trainData)
        {
           var theDimensions = returnImageDimensions(trainData);
           int height = theDimensions.height;
           int length = theDimensions.length;

            Array AnArray = Array.CreateInstance(typeof(double), trainData.Length);
            int count = 0;

            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < height; y++)
                {

                    // var value = trtrainData[x,y]ainData[x, y];
                    AnArray.SetValue(trainData[x, y], count);
                    count++;
                }
            }

            return AnArray;
            //////////////////////////////////////////////////////////
        }

        public static double[] convertToDoubleTraditionalArray(Array myArray)
        {
            int numInput = myArray.Length;
            double[] xValues = new double[numInput];
            Array.Copy(myArray, xValues, numInput);

            return xValues;
        }

        public static double[] JustGetTheWeights(double[] theWeightsAndBais)
        {
            int theLengthToGetAlltheWeights = theWeightsAndBais.Length - 1;
            double[] theWeights = new double[theLengthToGetAlltheWeights];
            
            Array.Copy(theWeightsAndBais, theWeights, theLengthToGetAlltheWeights);
            return theWeights;
        }

        public static void ShowMatrix(double[][] matrix, int numRows, int decimals, bool newLine)
        {
            for (int i = 0; i < numRows; ++i)
            {
                Console.Write(i.ToString().PadLeft(3) + ": ");

                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    if (matrix[i][j] >= 0.0)
                        Console.Write(" ");
                    else
                        Console.Write("-");

                    Console.Write(Math.Abs(matrix[i][j]).ToString("F" + decimals) + " ");

                }
                Console.WriteLine("");
            }
            if (newLine == true)
                Console.WriteLine("");
        }
        

        public static void ShowConsoleMessage(string text, Boolean WriteLn=true, Boolean StayAtCurrentLine=false, int moveToLeft=0)
        {
            if (WriteLn)
                Console.WriteLine(text);
            else
            {
                Console.Write(text);
            }

            if (StayAtCurrentLine)
            {
                Console.SetCursorPosition(Console.CursorLeft - moveToLeft, Console.CursorTop);
               // Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
        }

     

        public static void ShowVector(double[] vector, int valsPerRow, int decimals, bool newLine)
        {
            for (int i = 0; i < vector.Length; ++i)
            {
                if (i % valsPerRow == 0)
                    Console.WriteLine("");

                Console.Write(vector[i].ToString("F" + decimals).PadLeft(decimals + 4) + " ");
            }
            if (newLine == true)
                Console.WriteLine("");
        }

        public static List<Array> BuildListOfArrays(Array AnArray, int NumberOfArraysCreate)
        {
            List<Array> TheListofArrays = new List<Array>();

            for (int I=0; I < NumberOfArraysCreate; I++)
            {
                TheListofArrays.Add(AnArray);
            }

            return TheListofArrays;
        }

        public static void DisplayResultsList(List<double> theresults)
        {
            int I = 1;
            Console.WriteLine("");
            Console.WriteLine("");

            foreach (var result in theresults)
            {
               // Console.WriteLine(string.Format("Result Node {0} value set to: {1}", I, result.ToString()));
                ShowConsoleMessage(string.Format("Result Node {0} value set to: {1}", I, result.ToString(), true));
                I++;
            }
            ShowConsoleMessage("", true);
           // Console.WriteLine("");
        }

        public static List<List<double>> BuildListofAssociatedResults(List<double> ResultsList, int NumberofResultsCreate)
        {
            List<List<double>> TheListofResults = new List<List<double>>();

            for (int I=0; I < NumberofResultsCreate; I++)
            {
                TheListofResults.Add(ResultsList);
            }

            return TheListofResults;
        }

        public static void WriteImageInformationToDisk(string ImageName, double[,] ImageInformation, string Path)
        {
            int Length = ImageInformation.GetLength(0);
            int Height = ImageInformation.GetLength(1);

            string lines = string.Format("Image Name: {0}\r\n Length: {1}\r\n Height: {2}\r\n", ImageName, Length, Height);

            System.IO.StreamWriter file = new System.IO.StreamWriter(Path + "\\ImageInformation.txt");

            file.WriteLine(lines);
            file.Close();
        }
        ////
        public static Bitmap MedianFilter(Bitmap Image, int Size)
        {
          System.Drawing.Bitmap TempBitmap = Image;
          System.Drawing.Bitmap NewBitmap = new System.Drawing.Bitmap(TempBitmap.Width, TempBitmap.Height);
          System.Drawing.Graphics NewGraphics = System.Drawing.Graphics.FromImage(NewBitmap);
          NewGraphics.DrawImage(TempBitmap, new System.Drawing.Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), new System.Drawing.Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), System.Drawing.GraphicsUnit.Pixel);
          NewGraphics.Dispose();
          Random TempRandom = new Random();
          int ApetureMin = -(Size / 2);
          int ApetureMax = (Size / 2);
          for (int x = 0; x < NewBitmap.Width; ++x)
          {
              for (int y = 0; y < NewBitmap.Height; ++y)
              {
                  List<int> RValues = new List<int>();
                  List<int> GValues = new List<int>();
                  List<int> BValues = new List<int>();
                  for (int x2 = ApetureMin; x2 < ApetureMax; ++x2)
                  {
                      int TempX = x + x2;
                      if (TempX >= 0 && TempX < NewBitmap.Width)
                      {
                          for (int y2 = ApetureMin; y2 < ApetureMax; ++y2)
                          {
                              int TempY = y + y2;
                              if (TempY >= 0 && TempY < NewBitmap.Height)
                              {
                                  Color TempColor = TempBitmap.GetPixel(TempX, TempY);
                                  RValues.Add(TempColor.R);
                                  GValues.Add(TempColor.G);
                                  BValues.Add(TempColor.B);
                             }
                         }
                     }
                  }
                  RValues.Sort();
                  GValues.Sort();
                  BValues.Sort();
                  Color MedianPixel = Color.FromArgb(RValues[RValues.Count / 2],
                      GValues[GValues.Count / 2], 
                      BValues[BValues.Count / 2]);
                      NewBitmap.SetPixel(x, y, MedianPixel);
             }
          }
         return NewBitmap;
        }

        private static Bitmap FillImage(this Image img, int div, Color[] colors)
        {
         
            if (img == null) throw new ArgumentNullException();
            if (div < 1) throw new ArgumentOutOfRangeException();
            if (colors == null) throw new ArgumentNullException();
            if (colors.Length < 1) throw new ArgumentException();

            int xstep = img.Width / div;
            int ystep = img.Height / div;

            List<SolidBrush> brushes = new List<SolidBrush>();
            foreach (Color color in colors)
                brushes.Add(new SolidBrush(color));

            using (Graphics g = Graphics.FromImage(img))
            {
                for (int x = 0; x < div; x++)
                    for (int y = 0; y < div; y++)
                        g.FillRectangle(brushes[(y * div + x) % colors.Length],
                            new Rectangle(x * xstep, y * ystep, xstep, ystep));

                Bitmap myBitMap = new Bitmap(xstep, ystep, g);
                return myBitMap;
            }
            
        }

        public static Bitmap FillTheImage(Bitmap orignal)
        {
            Bitmap newBitmap = new Bitmap(orignal).FillImage(1, new Color[] { Color.Blue });
            return newBitmap;
        }
        ///
      //  public static Bitmap SNNBlur(Bitmap OriginalImage, int Size)
      //  {
      //    Bitmap NewBitmap = new Bitmap(OriginalImage.Width, OriginalImage.Height);
      //    BitmapData NewData = Image.LockImage(NewBitmap);
      //    BitmapData OldData = Image.LockImage(OriginalImage);
      //    int NewPixelSize = Image.GetPixelSize(NewData);
      //    int OldPixelSize = Image.GetPixelSize(OldData);
      //    int ApetureMinX = -(Size / 2);
      //    int ApetureMaxX = (Size / 2);
      //    int ApetureMinY = -(Size / 2);
      //    int ApetureMaxY = (Size / 2);
      //    for (int x = 0; x < NewBitmap.Width; ++x)
      //    {
      //        for (int y = 0; y < NewBitmap.Height; ++y)
      //        {
      //            int RValue = 0;
      //            int GValue = 0;
      //            int BValue = 0;
      //            int NumPixels = 0;
      //            for (int x2 = ApetureMinX; x2 < ApetureMaxX; ++x2)
      //            {
      //                int TempX1 = x + x2;
      //                int TempX2 = x - x2;
      //                if (TempX1 >= 0 && TempX1 < NewBitmap.Width && TempX2 >= 0 && TempX2 < NewBitmap.Width)
      //                {
      //                    for (int y2 = ApetureMinY; y2 < ApetureMaxY; ++y2)
      //                    {
      //                        int TempY1 = y + y2;
      //                        int TempY2 = y - y2;
      //                        if (TempY1 >= 0 && TempY1 < NewBitmap.Height && TempY2 >= 0 && TempY2 < NewBitmap.Height)
      //                        {
                  
      //                            Color TempColor = Image.GetPixel(OldData, x, y, OldPixelSize);
      //                            Color TempColor2 = Image.GetPixel(OldData, TempX1, TempY1, OldPixelSize);
      //                            Color TempColor3 = Image.GetPixel(OldData, TempX2, TempY2, OldPixelSize);
      //                            if (Distance(TempColor.R, TempColor2.R, TempColor.G, TempColor2.G, TempColor.B, TempColor2.B) <
      //                            Distance(TempColor.R, TempColor3.R, TempColor.G, TempColor3.G, TempColor.B, TempColor3.B))
      //                            {
      //                                RValue += TempColor2.R;
      //                                GValue += TempColor2.G;
      //                                BValue += TempColor2.B;
      //                            }
      //                            else
      //                            {
      //                                RValue += TempColor3.R;
      //                                GValue += TempColor3.G;
      //                                BValue += TempColor3.B;
      //                            }
      //                           ++NumPixels;
      //                       }
      //                   }
      //               }
      //           }
      //           Color MeanPixel = Color.FromArgb(RValue / NumPixels,
      //                GValue / NumPixels,
      //                BValue / NumPixels);
      //           Image.SetPixel(NewData, x, y, MeanPixel, NewPixelSize);
      //      }
      //    }
      //    Image.UnlockImage(NewBitmap, NewData);
      //    Image.UnlockImage(OriginalImage, OldData);
      //   return NewBitmap;
      //}
      private static double Distance(int R1, int R2, int G1, int G2, int B1, int B2)
      {
        return System.Math.Sqrt(((R1 - R2) * (R1 - R2)) + ((G1 - G2) * (G1 - G2)) + ((B1 - B2) * (B1 - B2)));
      }

        ////
           
    }
}
