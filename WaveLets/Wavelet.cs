using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;


namespace WaveLets
{
    public class Wavelet
    {
        int m_threshold = 0;
        int[,] orgred;
        int[,] orgblue;
        int[,] orggreen;

        int[,] rowred;
        int[,] rowblue;
        int[,] rowgreen;

        int[,] colred;
        int[,] colblue;
        int[,] colgreen;

        int[,] scalered;
        int[,] scaleblue;
        int[,] scalegreen;

        int[,] recrowred;
        int[,] recrowblue;
        int[,] recrowgreen;

        int[,] recorgred;
        int[,] recorgblue;
        int[,] recorggreen;

        public Bitmap DetectEdges(Bitmap bitmap, int function)
        {
            try
            {
                BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

                orgred = new int[bitmap.Height + 1, bitmap.Width + 1];
                orgblue = new int[bitmap.Height + 1, bitmap.Width + 1];
                orggreen = new int[bitmap.Height + 1, bitmap.Width + 1];

                rowred = new int[bitmap.Height + 1, bitmap.Width + 1];
                rowblue = new int[bitmap.Height + 1, bitmap.Width + 1];
                rowgreen = new int[bitmap.Height + 1, bitmap.Width + 1];

                colred = new int[bitmap.Height + 1, bitmap.Width + 1];
                colblue = new int[bitmap.Height + 1, bitmap.Width + 1];
                colgreen = new int[bitmap.Height + 1, bitmap.Width + 1];

                scalered = new int[bitmap.Height + 1, bitmap.Width + 1];
                scaleblue = new int[bitmap.Height + 1, bitmap.Width + 1];
                scalegreen = new int[bitmap.Height + 1, bitmap.Width + 1];

                recrowred = new int[bitmap.Height + 1, bitmap.Width + 1];
                recrowblue = new int[bitmap.Height + 1, bitmap.Width + 1];
                recrowgreen = new int[bitmap.Height + 1, bitmap.Width + 1];

                recorgred = new int[bitmap.Height + 1, bitmap.Width + 1];
                recorgblue = new int[bitmap.Height + 1, bitmap.Width + 1];
                recorggreen = new int[bitmap.Height + 1, bitmap.Width + 1];

                unsafe
                {
                    byte* imgPtr = (byte*)(bitmapdata.Scan0);

                    for (int i = 0; i < bitmapdata.Height; i++)
                    {
                        for (int j = 0; j < bitmapdata.Width; j++)
                        {
                            orgred[i, j] = (int)*(imgPtr + 0);
                            orggreen[i, j] = (int)*(imgPtr + 1);
                            orgblue[i, j] = (int)*(imgPtr + 2);

                            imgPtr += 3;
                        }

                        imgPtr += bitmapdata.Stride - bitmapdata.Width * 3;
                    }
                }

                //Transform rows

                for (int r = 0; r < bitmapdata.Height; r++)
                {
                    int k = 0;

                    for (int p = 0; p < bitmapdata.Width; p = p + 2)
                    {
                        rowred[r, k] = (int)((double)(orgred[r, p] + orgred[r, p + 1]) / 2);
                        rowred[r, k + (bitmapdata.Width / 2)] = (int)((double)(orgred[r, p] - orgred[r, p + 1]) / 2);

                        rowgreen[r, k] = (int)((double)(orggreen[r, p] + orggreen[r, p + 1]) / 2);
                        rowgreen[r, k + (bitmapdata.Width / 2)] = (int)((double)(orggreen[r, p] - orggreen[r, p + 1]) / 2);

                        rowblue[r, k] = (int)((double)(orgblue[r, p] + orgblue[r, p + 1]) / 2);
                        rowblue[r, k + (bitmapdata.Width / 2)] = (int)((double)(orgblue[r, p] - orgblue[r, p + 1]) / 2);

                        k++;
                    }

                }

                //Transform columns
                for (int c = 0; c < bitmapdata.Width; c++)
                {
                    int k = 0;

                    for (int p = 0; p < bitmapdata.Height; p = p + 2)
                    {
                        colred[k, c] = (int)((double)(rowred[p, c] + rowred[p + 1, c]) / 2);
                        colred[k + bitmapdata.Height / 2, c] = (int)((double)(rowred[p, c] - rowred[p + 1, c]) / 2);

                        colgreen[k, c] = (int)((double)(rowgreen[p, c] + rowgreen[p + 1, c]) / 2);
                        colgreen[k + bitmapdata.Height / 2, c] = (int)((double)(rowgreen[p, c] - rowgreen[p + 1, c]) / 2);

                        colblue[k, c] = (int)((double)(rowblue[p, c] + rowblue[p + 1, c]) / 2);
                        colblue[k + bitmapdata.Height / 2, c] = (int)((double)(rowblue[p, c] - rowblue[p + 1, c]) / 2);

                        k++;
                    }
                }

                //Scale col
                for (int r = 0; r < bitmapdata.Height; r++)
                {
                    for (int c = 0; c < bitmapdata.Width; c++)
                    {
                        if (r >= 0 && r < bitmapdata.Height / 2 && c >= 0 && c < bitmapdata.Width / 2)
                        {
                            scalered[r, c] = colred[r, c];
                            scalegreen[r, c] = colgreen[r, c];
                            scaleblue[r, c] = colblue[r, c];
                        }
                        else
                        {
                            scalered[r, c] = Math.Abs((colred[r, c] - 127));
                            scalegreen[r, c] = Math.Abs((colgreen[r, c] - 127));
                            scaleblue[r, c] = Math.Abs((colblue[r, c] - 127));
                        }
                    }
                }

                //Set LL = 0
                for (int r = 0; r < bitmapdata.Height / 2; r++)
                {
                    for (int c = 0; c < bitmapdata.Width / 2; c++)
                    {
                        colred[r, c] = 0;
                        colgreen[r, c] = 0;
                        colblue[r, c] = 0;
                    }
                }

                //Set LL = 0
                for (int r = 0; r < bitmapdata.Height; r++)
                {
                    for (int c = 0; c < bitmapdata.Width; c++)
                    {
                        if (!(r >= 0 && r < bitmapdata.Height / 2 && c >= 0 && c < bitmapdata.Width / 2))
                        {
                            if (Math.Abs(colred[r, c]) <= m_threshold)
                            {
                                colred[r, c] = 0;
                            }
                            else
                            {
                                //colred[r, c] = 255;
                            }

                            if (Math.Abs(colgreen[r, c]) <= m_threshold)
                            {
                                colgreen[r, c] = 0;
                            }
                            else
                            {
                                //colgreen[r, c] = 255;
                            }

                            if (Math.Abs(colblue[r, c]) <= m_threshold)
                            {
                                colblue[r, c] = 0;
                            }
                            else
                            {
                                //colblue[r, c] = 255;
                            }
                        }
                    }
                }

                //Inverse Transform columns
                for (int c = 0; c < bitmapdata.Width; c++)
                {
                    int k = 0;

                    for (int p = 0; p < bitmapdata.Height; p = p + 2)
                    {
                        recrowred[p, c] = (int)((colred[k, c] + colred[k + bitmapdata.Height / 2, c]));
                        recrowred[p + 1, c] = (int)((colred[k, c] - colred[k + bitmapdata.Height / 2, c]));

                        recrowgreen[p, c] = (int)((colgreen[k, c] + colgreen[k + bitmapdata.Height / 2, c]));
                        recrowgreen[p + 1, c] = (int)((colgreen[k, c] - colgreen[k + bitmapdata.Height / 2, c]));

                        recrowblue[p, c] = (int)((colblue[k, c] + colblue[k + bitmapdata.Height / 2, c]));
                        recrowblue[p + 1, c] = (int)((colblue[k, c] - colblue[k + bitmapdata.Height / 2, c]));

                        k++;
                    }
                }


                //Invers Transform rows

                for (int r = 0; r < bitmapdata.Height; r++)
                {
                    int k = 0;

                    for (int p = 0; p < bitmapdata.Width; p = p + 2)
                    {
                        recorgred[r, p] = (int)((recrowred[r, k] + recrowred[r, k + (bitmapdata.Width / 2)]));
                        recorgred[r, p + 1] = (int)((recrowred[r, k] - recrowred[r, k + (bitmapdata.Width / 2)]));

                        recorggreen[r, p] = (int)((recrowgreen[r, k] + recrowgreen[r, k + (bitmapdata.Width / 2)]));
                        recorggreen[r, p + 1] = (int)((recrowgreen[r, k] - recrowgreen[r, k + (bitmapdata.Width / 2)]));

                        recorgblue[r, p] = (int)((recrowblue[r, k] + recrowblue[r, k + (bitmapdata.Width / 2)]));
                        recorgblue[r, p + 1] = (int)((recrowblue[r, k] - recrowblue[r, k + (bitmapdata.Width / 2)]));

                        k++;
                    }

                }

                unsafe
                {
                    byte* imgPtr = (byte*)(bitmapdata.Scan0);

                    for (int i = 0; i < bitmapdata.Height; i++)
                    {
                        for (int j = 0; j < bitmapdata.Width; j++)
                        {
                            if (function == 0)
                            {
                                *(imgPtr + 0) = (byte)Math.Abs(recorgred[i, j] - 0);
                                *(imgPtr + 1) = (byte)Math.Abs(recorggreen[i, j] - 0);
                                *(imgPtr + 2) = (byte)Math.Abs(recorgblue[i, j] - 0);
                            }
                            else
                            {

                                *(imgPtr + 0) = (byte)scalered[i, j];
                                *(imgPtr + 1) = (byte)scalegreen[i, j];
                                *(imgPtr + 2) = (byte)scaleblue[i, j];
                            }

                            imgPtr += 3;
                        }

                        imgPtr += bitmapdata.Stride - bitmapdata.Width * 3;
                    }
                }

                bitmap.UnlockBits(bitmapdata);

                //bool skip = false;

                //foreach (TabPage t in this.tcEdge.TabPages)
                //{
                //    if (t.Text == itp.Text)
                //    {
                //        skip = true;
                //    }
                //}

               // if (!skip)
               // {
                    //  TabPage etp = new TabPage(itp.Text);

                    // pbEdge = new PictureBox();

                    /// pbEdge.Dock = DockStyle.Fill;

                    // pbEdge.SizeMode = PictureBoxSizeMode.Normal;

                    // pbEdge.Image = bitmap;

                    //bitmap.Save("C:\\temp\\myimage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    //etp.Controls.Add(pbEdge);


                    // this.tcEdge.TabPages.Add(etp);

                    // int index = tcEdge.TabPages.IndexOf(etp);

                    // if (index >= 0)
                    // {
                    //    this.tcEdge.SelectedIndex = index;
                    //}
               // }
                return bitmap;
            } 
            catch (Exception)
            {
                // TO DO: write to log file about this error
                return null;
            }
         
        }

        public Bitmap InvertImage(Bitmap sourceimage)
        {
            Color c;

            for (int i = 0; i < sourceimage.Width; i++)
            {

                for (int j = 0; j < sourceimage.Height; j++)
                {

                    c = sourceimage.GetPixel(i, j);

                    c = Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B);

                    sourceimage.SetPixel(i, j, c);

                }

            }

            return sourceimage;
        }
    }
}
