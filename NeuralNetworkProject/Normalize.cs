using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkProject
{
    public class Normalize
    {
        static public double[,] GassNormal(double[,] data)
        {
            int height = 0;
            int length = 0;

            int numberOfDimensions = data.Rank; 

            for (int dimension = 1; dimension <= numberOfDimensions; dimension++)
            {
                if (dimension == 1)
                    length = data.GetUpperBound(dimension - 1) + 1;
                if (dimension ==2)
                    height = data.GetUpperBound(dimension - 1) + 1;
            }
            
            double sum = 0.0;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < height; j++)
                    sum += data[i,j];
            }

            double mean = sum / data.Length;
            double sumSquares = 0.0;
 
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < height; j++ )
                    sumSquares += (data[i,j] - mean) * (data[i,j] - mean);
            }

            double stdDev = Math.Sqrt(sumSquares / data.Length);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < height; j++ )
                    data[i,j] = (data[i,j] - mean) / stdDev;
            }

            return data;
        }

        //static double[][] MinMaxNormal(double[][] data, int column)
        //{
        //    int j = column;
        //    double min = data[0][j];
        //    double max = data[0][j];

        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        if (data[i][j] < min)
        //            min = data[i][j];
        //        if (data[i][j] > max)
        //            max = data[i][j];
        //    }

        //    double range = max - min;

        //    if (range == 0.0)
        //    {
        //        for (int i = 0; i < data.Length; i++)
        //        {
        //            data[i][j] = 0.5;
        //        }
        //        return data;
        //    }

        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        data[i][j] = (data[i][j] - min) / range;
        //    }

        //    return data;
        //}

    }
}
