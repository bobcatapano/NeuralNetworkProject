using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkProject
{
    public static class TrainClass
    {
        static public void MakeTrainTest(double[][] allData, int seed, out double[][] trainData, out double[][] testData)
        {
            Util.ShowConsoleMessage("Make Training Test");
            Util.ShowConsoleMessage(" ");
            
            // Split allData into 80% trainData
            //Util.ShowConsoleMessage("Creating Random Seed..", false);

            Random rnd = new Random(seed);

            //Util.ShowConsoleMessage(string.Format("The Seed {0} set to", rnd.ToString()), false);
            //Util.ShowConsoleMessage(" ");

            int totRows = allData.Length;
            int numCols = allData[0].Length;
            int trainRows = (int)(totRows * 0.80); // Hard-coded 80-20 split.
            int testRows = totRows - trainRows;  
            trainData = new double[trainRows][]; 
            testData = new double[testRows][];
            double[][] copy = new double[allData.Length][];
            // Make a reference copy.  
            for (int i = 0; i < copy.Length; ++i)
                copy[i] = allData[i];

            // Scramble row order of copy.
            Util.ShowConsoleMessage("Scambling Data...", false);
         
            for (int i = 0; i < copy.Length; ++i)
            { 
                int r = rnd.Next(i, copy.Length);
                double[] tmp = copy[r]; 
                copy[r] = copy[i];
                copy[i] = tmp; 
            }

            Util.ShowConsoleMessage("Scambling Data Complete", false);
            Util.ShowConsoleMessage("");
            Util.ShowConsoleMessage(" ");
 
              // Copy first trainRows from copy[][] to trainData[][]. 
            Util.ShowConsoleMessage("Creating Train Data", false);

            for (int i = 0; i < trainRows; ++i) 
            { 
                trainData[i] = new double[numCols];
                for (int j = 0; j < numCols; ++j)
                {  
                    trainData[i][j] = copy[i][j]; 
                }
            }
            Util.ShowConsoleMessage("...Creating Traing Data Complete");
            Util.ShowConsoleMessage("");
            Util.ShowConsoleMessage(" ");
  
             // Copy testRows rows of allData[] into testData[][]. 
            Util.ShowConsoleMessage("Creating Test Data", false);

             for (int i = 0; i < testRows; ++i) // i points into testData[][].
             { 
                 testData[i] = new double[numCols];
                 for (int j = 0; j < numCols; ++j)
                 { 
                     testData[i][j] = copy[i + trainRows][j];
                 }
             }

             Util.ShowConsoleMessage("...Completed Test Data", false);
             Util.ShowConsoleMessage("");
             Util.ShowConsoleMessage(" ");
        }
        
    }
}
