using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NeuralNetworkProject
{
    static public class CreateTrainingMatrix
    {
        // I think this is Obsolete.. Check in Util. This may come out..
        //static public Array ConvertDataArrayToOneDimensionMatric(double[,] trainData, int NumberOfTrainingSets = 1, int Result = 1)
        //{

        //    // Change this method to return type Array....
        //    // Return AnArray

        //    Array AnArray = Array.CreateInstance(typeof(double), trainData.Length + 1);
        //    double[][] trainingData = new double[NumberOfTrainingSets][];

        //    // This is duplicate code. Turn into a static method in a Util class
        //    int height = 0;
        //    int length = 0;

        //    int numberOfDimensions = trainData.Rank;

        //    for (int dimension = 1; dimension <= numberOfDimensions; dimension++)
        //    {
        //        if (dimension == 1)
        //            length = trainData.GetUpperBound(dimension - 1) + 1;
        //        if (dimension == 2)
        //            height = trainData.GetUpperBound(dimension - 1) + 1;
        //    }
        //    ////////////////////////////////////////////////

        //    // Convert 2 Dimension data array into 1 dimension Matrice 
        //    int count = 0;

        //    for (int x = 0; x < length; x++)
        //    {
        //        for (int y = 0; y < height; y++)
        //        {
        //            // var value = trtrainData[x,y]ainData[x, y];
        //            AnArray.SetValue(trainData[x, y], count);
        //            count++;
        //        }
        //    }
        //    return AnArray;
        //}


        // This is going to be obsolete soon..
        // TO Do: Make this more generic to handle multiple Training Data Sets. Right now 1 is all that is needed
        //static public double[][] TheTrainingMatrix(double[,] trainData, int NumberOfTrainingSets=1, int Result=1)
        //{

        //    // Change this method to return type Array....
        //    // Return AnArray

        //    Array AnArray = Array.CreateInstance(typeof(double), trainData.Length + 1);
        //    double[][] trainingData = new double[NumberOfTrainingSets][];

        //    // This is duplicate code. Turn into a static method in a Util class
        //    int height = 0;
        //    int length = 0;

        //    int numberOfDimensions = trainData.Rank;

        //    for (int dimension = 1; dimension <= numberOfDimensions; dimension++)
        //    {
        //        if (dimension == 1)
        //            length = trainData.GetUpperBound(dimension - 1) + 1;
        //        if (dimension == 2)
        //            height = trainData.GetUpperBound(dimension - 1) + 1;
        //    }
        //    ////////////////////////////////////////////////

        //    // Convert 2 Dimension data array into 1 dimension Matrice 
        //    int count = 0;

        //    for (int x = 0; x < length; x++)
        //    {
        //        for (int y = 0; y < height; y++)
        //        {
        //            // var value = trtrainData[x,y]ainData[x, y];
        //            AnArray.SetValue(trainData[x, y], count);
        //            count++;
        //        }
        //    }


        //    //////////////////////////////////////////////////////////

        //    // Take this out.. Create a new Class to Create the Training Matrix
        //    AnArray.SetValue(Result, count);
        //    trainingData.SetValue(AnArray, 0); // Using First Elemet.. TO Do: Make this more generic so it can define Multiple Training Sets
        //    return trainingData;
        //}

        public static Array CreateTraingSetArray(Array AnArray, List<double> Result)
        {
            int InputArrayLength = AnArray.Length;
            int ResultListLength = Result.Count;

            int TotalNewLength = InputArrayLength + ResultListLength; // May need to use this instead

            Array NewArray = Array.CreateInstance(typeof(double), TotalNewLength); // Was InputArrayLength
            Array.Copy(AnArray, NewArray, InputArrayLength);

            int NewIndexLocation = (InputArrayLength - 1);

            foreach (var myResult in Result)
            {
                NewIndexLocation++;
                NewArray.SetValue(Result, NewIndexLocation);
            }

            return NewArray;
        
        }

        public static Array CreateTrainingSetArray(Array AnArray, int Index, double Result)
        {
            int InputArrayLength = AnArray.Length;
            int NewArrayLength = InputArrayLength + Index;

            Array NewArray = Array.CreateInstance(typeof(double), NewArrayLength);
            Array.Copy(AnArray, NewArray, InputArrayLength);

            int NewIndexLocation = (InputArrayLength - 1) + Index;

            NewArray.SetValue(Result, NewIndexLocation);

            return NewArray;
        }


        // This should be wrapped around a Loop statement to create the entire Training set.
        public static double[][] CreateTrainingData(Array AnArray, double[][] TrainingData, int Index)
        {

            TrainingData.SetValue(AnArray, Index);
            return TrainingData;

        }

        public static double[][] CreateTraingDataSet(Array AnArray, int DataSetIndex, double Result, double[][] TrainingData, int TraingSetIndex)
        {
            //CreateTrainingSetArray
            AnArray = CreateTrainingSetArray(AnArray, DataSetIndex, Result);
 
            // CreateTrainingData
            TrainingData = CreateTrainingData(AnArray, TrainingData, TraingSetIndex);

            return TrainingData;
        }

        public static double[][] CreateTraingDataSet(Array AnArray, List<List<double>> ResultList, double[][] TrainingData, int TraingSetIndex)
        {
            //For Each Result List -- 0.0 0.0 1.0 etc..
            //CreateTrainingSetArray
            int I = 1;
            foreach(var resultinList in ResultList)
            {
                foreach (double theResult in resultinList)
                {
                    AnArray = CreateTrainingSetArray(AnArray, I, theResult);
                    I++;
                }
            }


            // CreateTrainingData
            TrainingData = CreateTrainingData(AnArray, TrainingData, TraingSetIndex);

            return TrainingData;
        }

        public static double[][] CreateTraingDataSet(List<Array> ListOfAnArrays, List<List<double>> ResultList, double[][] TraingData, int InitialStartingIndex = 0)
        {
            int TheLengthofDataSetArray = ListOfAnArrays[0].Length;
            int TheLenghtofResultList = ResultList[0].Count;
            int TheTraininfDataLength = TraingData.Length;
            int TraingDataSetToCompete = 0;


            // For each in Array List .1 .2 .3 .4 .5
            //                        .1 .6 .7 .8 .9
            //                        .4 .2 .9 .4 .2

            // Note: This two list have to match up  
            // To Do: Place in Error handling to make sure dimensions of List of the same

            //For each in Result List 1.0 0.0 0.0
            //                        0.0 1.0 0.0
            //                        0.0 0.0 1.0

           int NewArrayLength = TheLengthofDataSetArray + TheLenghtofResultList;
           //Array AnArray = Array.CreateInstance(typeof(double), TraingData.Length); // To Do: Change TraingData.Length to NewArrayLength
           Array AnArray = Array.CreateInstance(typeof(double), NewArrayLength);

           int A = InitialStartingIndex;

            foreach (var arrayListMember in ListOfAnArrays)
            {
                int I = 1;

                foreach (var resultinList in ResultList)
                {
                    foreach (double theResult in resultinList)
                    {
                        AnArray = CreateTrainingSetArray(arrayListMember, I, theResult); // TO DO: place in resultinList
                        I++;
                    }
                    I = 1;
                   // TraingData = CreateTrainingData(AnArray, TraingData, A);
                   // A++;
                }

                TraingData = CreateTrainingData(AnArray, TraingData, A);
                TraingDataSetToCompete = TheTraininfDataLength - (A+1);

                Util.ShowConsoleMessage(string.Format("Creating {0} Training Set Data. {1} Data Sets to go", (A+1).ToString(),TraingDataSetToCompete.ToString()));
                A++;
            }

            return TraingData;
        }

        public static double[][] CreaetUniqueTraingSetFromImageFile(double[][] TrainingDataToCreate, int TraingSetSize, int InitialStartingIndex, List<double> listofRestults, List<string> ImageFileNames, WaveLets.Wavelet myWaveLet)
        {
            List<List<double>> ListOfResults = new List<List<double>>();
            List<Array> ListOfArrays = new List<Array>();

            foreach (var myImageFile in ImageFileNames)
            {
                Util.ShowConsoleMessage("Loading Image file: " + myImageFile, false);
                Bitmap bitmap = new Bitmap(myImageFile);
                // Bitmap bitmap_greyscaled = ConvertImageToGreyScale.ConvertUsingAverages(bitmap);
                Util.ShowConsoleMessage("..Loading Complete", false);
                Util.ShowConsoleMessage("");
                Util.ShowConsoleMessage(" ");

                Util.ShowConsoleMessage("Peforming Noise Filter on Image: " + myImageFile, false);
                Bitmap ConvertedEdgeDetectedToClearOutNoise = Util.MedianFilter(bitmap, 2);
                Util.ShowConsoleMessage("... Completed Noise Filter", false);
                Util.ShowConsoleMessage("");
                Util.ShowConsoleMessage(" ");

                Util.ShowConsoleMessage("Peforming Edge Detection on Image: " + myImageFile, false);
                Bitmap EdgeDetectionofImage = myWaveLet.DetectEdges(ConvertedEdgeDetectedToClearOutNoise, 0);
                Util.ShowConsoleMessage("... Completed Edge Detection", false);
                Util.ShowConsoleMessage("");
                Util.ShowConsoleMessage(" ");

                Util.ShowConsoleMessage("Inverting Image: " + myImageFile, false);
                Bitmap EdgeDetectionImageInverted = myWaveLet.InvertImage(EdgeDetectionofImage);
                Util.ShowConsoleMessage("... Completed  Invertion", false);
                Util.ShowConsoleMessage("");
                Util.ShowConsoleMessage(" ");

                double[,] image_array = ConvertImageToFormat.ConvertToDoubleArray(bitmap);

                Util.ShowConsoleMessage("Normalizing Image Data: " + myImageFile, false);
                double[,] normalizedData = Normalize.GassNormal(image_array);
                Util.ShowConsoleMessage("...Normalization Compete", false);
                Util.ShowConsoleMessage("");
                Util.ShowConsoleMessage(" ");

                Util.ShowConsoleMessage("Building One Dimension Array", false);
                Array OneDimensionNormalizedData = Util.ReturnOneDimensionArray(normalizedData);
                Util.ShowConsoleMessage("...Completed", false);
                Util.ShowConsoleMessage("");
                Util.ShowConsoleMessage(" ");

                //BuildListOfArrays
                Util.ShowConsoleMessage("Building Array List", false);
                ListOfArrays = Util.BuildListOfArrays(OneDimensionNormalizedData, TraingSetSize);
                Util.ShowConsoleMessage("...Done");
                Util.ShowConsoleMessage("");
                Util.ShowConsoleMessage(" ");

                //BuildListOfResults
                Util.ShowConsoleMessage("Building List of Results", false);
                ListOfResults = Util.BuildListofAssociatedResults(listofRestults, TraingSetSize);
                Util.DisplayResultsList(listofRestults);
                Util.ShowConsoleMessage("...Completed Building List of Results", false);
                Util.ShowConsoleMessage("");
                Util.ShowConsoleMessage(" ");

  

            }

            //Create the Training Data
            return CreateTrainingMatrix.CreateTraingDataSet(ListOfArrays, ListOfResults, TrainingDataToCreate, InitialStartingIndex);
        }

        /// <summary>
        /// Method to create the Over all Trainging Set from an Image File. Peforms all the necessary loading, normalization, and the creation of the Training Set to be used in the Network.
        /// </summary>
        /// <param name="TrainingSetSize"></param>
        /// <param name="InitialStartingIndex"></param>
        /// <param name="ImageFileName"></param>
        /// <returns></returns>
        public static double[][] CreaetUniqueTraingSetFromImageFile(double[][] TrainingDataToCreate, int TrainingSetSize, int InitialStartingIndex, List<double> listofRestults, string ImageFileName, WaveLets.Wavelet myWaveLet)
        {
            Util.ShowConsoleMessage("Loading Image file: " + ImageFileName, false);  
            Bitmap bitmap = new Bitmap(ImageFileName);
           // Bitmap bitmap_greyscaled = ConvertImageToGreyScale.ConvertUsingAverages(bitmap);
            Util.ShowConsoleMessage("..Loading Complete", false);
            Util.ShowConsoleMessage("");
            Util.ShowConsoleMessage(" ");

            Util.ShowConsoleMessage("Peforming Noise Filter on Image: " + ImageFileName, false);
            Bitmap ConvertedEdgeDetectedToClearOutNoise = Util.MedianFilter(bitmap, 2);
            Util.ShowConsoleMessage("... Completed Noise Filter", false);
            Util.ShowConsoleMessage("");
            Util.ShowConsoleMessage(" ");

            ConvertedEdgeDetectedToClearOutNoise.Save("C:\\temp\\myimage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            Util.ShowConsoleMessage("Peforming Edge Detection on Image: " + ImageFileName, false);
            Bitmap EdgeDetectionofImage = myWaveLet.DetectEdges(ConvertedEdgeDetectedToClearOutNoise, 0);
            Util.ShowConsoleMessage("... Completed Edge Detection", false);
            Util.ShowConsoleMessage("");
            Util.ShowConsoleMessage(" ");

            EdgeDetectionofImage.Save("C:\\temp\\myimage2.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            Util.ShowConsoleMessage("Inverting Image: " + ImageFileName, false);
            Bitmap EdgeDetectionImageInverted = myWaveLet.InvertImage(EdgeDetectionofImage);
            Util.ShowConsoleMessage("... Completed  Invertion", false);
            Util.ShowConsoleMessage("");
            Util.ShowConsoleMessage(" ");

            EdgeDetectionImageInverted.Save("C:\\temp\\myimage3.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);


            double[,] image_array = ConvertImageToFormat.ConvertToDoubleArray(EdgeDetectionImageInverted);
            //Util.ShowConsoleMessage("Grey Scaled Image Compelted");

            Util.ShowConsoleMessage("Normalizing Image Data: " + ImageFileName, false);
            double[,] normalizedData = Normalize.GassNormal(image_array);
            Util.ShowConsoleMessage("...Normalization Compete", false);
            Util.ShowConsoleMessage("");
            Util.ShowConsoleMessage(" ");

            Util.WriteImageInformationToDisk(ImageFileName, image_array, "C:\\temp");

            Util.ShowConsoleMessage("Building One Dimension Array", false);
            Array OneDimensionNormalizedData = Util.ReturnOneDimensionArray(normalizedData);
            Util.ShowConsoleMessage("...Completed", false);
            Util.ShowConsoleMessage("");
            Util.ShowConsoleMessage(" ");

            //BuildListOfArrays
            Util.ShowConsoleMessage("Building Array List", false);
            var ListOfArrays = Util.BuildListOfArrays(OneDimensionNormalizedData, TrainingSetSize);
            Util.ShowConsoleMessage("...Done");
            Util.ShowConsoleMessage("");
            Util.ShowConsoleMessage(" ");
         
            //BuildListOfResults
            Util.ShowConsoleMessage("Building List of Results", false);
            var ListOfResults = Util.BuildListofAssociatedResults(listofRestults, TrainingSetSize);
            Util.DisplayResultsList(listofRestults);
            Util.ShowConsoleMessage("...Completed Building List of Results", false);
            Util.ShowConsoleMessage("");
            Util.ShowConsoleMessage(" ");

            //Create the Training Data
            return CreateTrainingMatrix.CreateTraingDataSet(ListOfArrays, ListOfResults, TrainingDataToCreate, InitialStartingIndex);
        }

    }
}
