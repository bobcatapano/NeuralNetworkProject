using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WaveLets;

namespace NeuralNetworkProject
{
    class Program
    {
        static void Main(string[] args)
        {
            double[][] TrainingData = new double[10][];
           
            string ImageFileName1 = "C:\\Workspaces\\New1.jpg";
            string ImageFileName2 = "C:\\Workspaces\\New2.jpg";

            // New code to handle reading Data from Directories

            string outputpath1 = "..\\TrainingSet\\DataSet1";
            string outputpath2 = "..\\TrainingSet\\DataSet2";

            string readoutpath1 = "..\\TraingSet\\TrainedData\\outputNode1"; // This needs to be created Programmatically to handle more output Nodes
            string readoutpath2 = "..\\TraingSet\\TrainedData\\outputNode2";

       //     IEnumerable<string> listofTrainingData1 = Util.GetAllFilesFromDirectory(outputpath1);
       //     IEnumerable<string> listofTrainingData2 = Util.GetAllFilesFromDirectory(outputpath2);

       ///     int NumberofTraingingData1 = listofTrainingData1.Count();
        //    int NumberofTraingingData2 = listofTrainingData2.Count();

       //     double[][] TraingData2 = new double[NumberofTraingingData1 + NumberofTraingingData2][];

            /////////////////////////////////////////////////////////


            //BuildListOfResults
            List<double> listofRestults = new List<double>(){1.0};

            int NumberofOutputNodes = listofRestults.Count;
            Util.ShowConsoleMessage("Number of Output Nodes will be set to " + NumberofOutputNodes.ToString());
            Util.ShowConsoleMessage(" ");
           
            //Create Trainging Data
             TrainingData = CreateTrainingMatrix.CreaetUniqueTraingSetFromImageFile(TrainingData, 10, 0, listofRestults, ImageFileName1, new Wavelet());
             Util.ShowConsoleMessage(" ");

           //  List<double> listofRestults2 = new List<double>() {0.0, 1.0};

          //   TrainingData = CreateTrainingMatrix.CreaetUniqueTraingSetFromImageFile(TrainingData, 5, 5, listofRestults2, ImageFileName2, new Wavelet());

             double[][] trainData = null;
             double[][] testData = null;

             TrainClass.MakeTrainTest(TrainingData, 72, out trainData, out testData); 

             int numInput = TrainingData[0].Length - NumberofOutputNodes;
             int numOutput = NumberofOutputNodes;
             int numHidden = numInput+1;
             int numInnerHidden = numHidden;

             Util.ShowConsoleMessage(string.Format("Number of Inputs {0}, Number of Output {1}, Hidden Network set to {2}, and Inner Hidden set to {3}", numInput, numOutput, numHidden, numInnerHidden));
             Util.ShowConsoleMessage(" ");

             Util.ShowConsoleMessage("Initializing Neural Network...", false);
             NeuralNetwork nn = new NeuralNetwork(numInput, numInnerHidden, numHidden, numOutput);
             Util.ShowConsoleMessage("Complete", false);
             Util.ShowConsoleMessage(" ");

           // double alpha = 0.001;
            int maxEpochs = 100;
            double learnRate = 0.9;
            double momentum = 0.3;

            Util.ShowConsoleMessage(string.Format("Max Epochs {0}, Learning Rate set to {1} and Momentum", maxEpochs, learnRate, momentum));
            Util.ShowConsoleMessage(" ");

            Util.ShowConsoleMessage("Training Started");
            Util.ShowConsoleMessage(" ");

            nn.Train(trainData, maxEpochs, learnRate, momentum);
            Util.ShowConsoleMessage("Training Completed");
            Util.ShowConsoleMessage(" ");

            double trainAcc = nn.Arruracy(trainData);
            
            Util.ShowConsoleMessage("Accuracy on training data = " + trainAcc.ToString("F4"));
            Util.ShowConsoleMessage(" ");

            double testAcc = nn.Arruracy(testData);

            Util.ShowConsoleMessage("Accuracy on test data = " + testAcc.ToString("F4"));
            Util.ShowConsoleMessage(" ");
 
            trainData = null;
            testData = null;
            trainData = null;
           
            double[] weights = nn.GetWeights();

            Util.ShowConsoleMessage("Writing weights info to disk", false);
            string[] lines = Array.ConvertAll(weights, i => i.ToString());
            System.IO.File.WriteAllLines(@"C:\Users\ktrg47\Documents\Visual Studio 2013\Projects\NN_RunTimeEngine\NN_RunTimeEngine\bin\Weights\weigts_bias.txt", lines);
            Util.ShowConsoleMessage("...Completed writing to disk");
            Util.ShowConsoleMessage(" ");

                
        }
    }
}
