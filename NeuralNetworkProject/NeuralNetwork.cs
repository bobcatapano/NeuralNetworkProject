using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkProject
{

    //This is obsolete
    [Obsolete]
    public class NeuralNetwork
    {
        private int numInput;
        private int numHidden;
        private int numInnerHidden;
        private int numOutput;

        private double[] inputs;
        private double[] outputs;
        private double[] hOutputs;
        private double[] innerHiddenOutputs;

        private double[][] ihWeights;
        private double[][] hoWeights;
        //private double[][] innerHiddenWeights;
        private double[][] input_innerHiddenWeights;
        private double[][] output_innerHiddenWeights;

        private double[] hBiases;
        private double[] oBiases;
        private double[] innerHiddenBiases;

        
        // New for Back Propagation 
        private double[] oGrads; // Output gradients
        private double[] hGrads; // Hidden gradients
        private double[] innerHiddenGrads; // Not sure if this is needed

        // For Momentum with back-propagation
        private double[][] ihPreWeightsDelta; 
        private double[][] hoPreWeightsDelta;

        private double[][] input_innerHiddenPreWeightsDelta; // Not sure if this is needed
        private double[][] output_innerHiddenPreWeightsDelta;

        private double[] hPreBiasesDelta;
        private double[] oPreBiasesDelta;
        private double[] innerHiddenPreBiasesDelta; // Not sure if this is needed
 

        public NeuralNetwork(int numberInput, int numInnerHidden, int numHidden, int numOutput)
        {

            this.numInput = numberInput;
            this.numHidden = numHidden;
            this.numOutput = numOutput;

            this.numInnerHidden = numInnerHidden; // new

            this.inputs = new double[numInput];
            this.ihWeights = MakeMatrix(numInput, numHidden);

           // this.innerHiddenWeights = MakeMatrix(numHidden, numInnerHidden); // new
            this.input_innerHiddenWeights = MakeMatrix(numHidden, numInnerHidden); // new
            this.output_innerHiddenWeights = MakeMatrix(numInnerHidden, numOutput); // new

            this.hBiases = new double[numHidden];
            this.hOutputs = new double[numHidden];
           //this.hoWeights = MakeMatrix(numHidden, numOutput);
            this.hoWeights = MakeMatrix(numHidden, numInnerHidden);

            this.innerHiddenBiases = new double[numInnerHidden]; // new
            this.innerHiddenOutputs = new double[numInnerHidden]; // new

            this.oBiases = new double[numOutput];
            this.outputs = new double[numOutput];

            oGrads = new double[numOutput];
            hGrads = new double[numHidden];

            this.innerHiddenGrads = new double[numInnerHidden]; // new

            // Note: Look this over.. Make sure this makes sense..

            ihPreWeightsDelta = MakeMatrix(numInput, numHidden);
            hoPreWeightsDelta = MakeMatrix(numHidden, numInnerHidden); // Changed from output to numInnerHidden

            input_innerHiddenPreWeightsDelta = MakeMatrix(numHidden, numInnerHidden); // new
            output_innerHiddenPreWeightsDelta = MakeMatrix(numHidden, numOutput); // new


            hPreBiasesDelta = new double[numHidden]; 
            oPreBiasesDelta = new double[numOutput];

            innerHiddenPreBiasesDelta = new double[numInnerHidden]; // new

            /// TO DO: Make sure above is correct!
            /// 
            
            /// TO Do: Still need to complete below...

            //InitMatrix(ihPreWeightsDelta, 0.011);
            //InitVector(hPreBiasesDelta, 0.011);

            //InitMatrix(hoPreWeightsDelta, 0.011);
            //InitVector(oPreBiasesDelta, 0.011);

            InitMatrix(input_innerHiddenPreWeightsDelta,0.0012); // new
            InitVector(innerHiddenPreBiasesDelta,0.0012); // new

            InitMatrix(ihPreWeightsDelta, 0.0012); // orignal
            InitVector(hPreBiasesDelta, 0.0014); // orignal


            InitMatrix(output_innerHiddenPreWeightsDelta, 0.0015); //new

            InitMatrix(hoPreWeightsDelta, 0.0015); // orignal
            InitVector(oPreBiasesDelta, 0.013); // orignal


            InitMatrix(input_innerHiddenWeights, 0.0015); // new
            InitMatrix(output_innerHiddenWeights, 0.0015); // new

            InitMatrix(ihWeights, 0.0010); // orignal
            InitMatrix(hoWeights, 0.0017); // orignal

            InitVector(innerHiddenBiases, 0.0015); // new

            InitVector(hBiases, 0.0013); // orignal
            InitVector(oBiases, 0.0019); // orignal
        }


        //private static void InitVector(double[] vector, double value) 
        //{
        //    for (int i = 0; i < vector.Length; ++i)
        //        vector[i] = value; 
        //}

        private static void InitVector(double[] vector, double scale=1.0)
        {
           // DateTime theDateTime = new DateTime();
           // int seedTime = theDateTime.Millisecond;  
            Random variable = null;
            double value = 0;

            //for (int I = 0; I < x; I++)
            //{
            //    variable = new Random((int)DateTime.Now.Ticks & (0x0000FFFF + x));
            //    value = Convert.ToDouble(variable.Next(1, 5));
            //}
            int counter = 0;

           for (int i = 0; i < vector.Length; ++i)
            {
                counter++;
                value = GetNextRandomNumber(70, counter);
                vector[i] = value * scale;
                if (vector[i] > 1.0)
                {
                    vector[i] = vector[i] / 10;
                }
           }
        }

        //private static void InitMatrix(double[][] matrix, double value)
        //{ 
        //    int rows = matrix.Length;
        //    int cols = matrix[0].Length;

        //    for (int i = 0; i < rows; ++i)
        //        for (int j = 0; j < cols; ++j)
        //            matrix[i][j] = value;
        //}

        private static void InitMatrix(double[][] matrix, double scale=1.0)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;
            double value = 0;
            Random variable = null;

          //  DateTime theDateTime = new DateTime();
          //  int seedTime = theDateTime.Millisecond;

            //for (int I = 0; I < x; I++)
            //{
            //    variable = new Random((int)DateTime.Now.Ticks & (0x0000FFFF + x));
            //    value = Convert.ToDouble(variable.Next(1, 10));
            //}
            int counter = 0;
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                {
                    counter++;
                    value = GetNextRandomNumber(90, counter);
                    matrix[i][j] = value * scale;
                    if (matrix[i][j] > 1.0)
                    {
                        matrix[i][j] = matrix[i][j] / 10;
                    }
                }
        } 

        private static double GetNextRandomNumber(int max, int counter)
        {
            double value = 0.0;

            Random anotherRandom = new Random();
            int addto;

           // for (int I = 0; I < variable; I++)
           // {
                addto = anotherRandom.Next(1, 1000000);
                Random newNumber  = new Random((int)DateTime.Now.Ticks + counter+addto);
                value = Convert.ToDouble(newNumber.Next(1, max));
          //  }

            return value;
        }



        private void UpdateWeights(double[] tValues, double learnRate, double momentum)
        {
            Util.ShowConsoleMessage("Updating Weights");
          
            // Assumes that SetWeights and ComputeOutputs have been called.
            if (tValues.Length != numOutput)
                throw new Exception("target values not same Length as output in UpdateWeights");

            // 1. Compute output gradients. Assumes softmax. 
            for (int i = 0; i < oGrads.Length; ++i)
            {
               // double derivative = (1 - outputs[i]) * (1 + outputs[i]);
                double derivative = (1 - outputs[i]) * outputs[i]; // Derivative of softmax is y(1y).
                if (double.IsNegativeInfinity(derivative))
                    derivative = 0;

               oGrads[i] = derivative * (tValues[i] - outputs[i]); // oGrad = (1 - O)(O) * (T-O)
            }

            //NEW. Compute innerHidden Gradients. This uses tanh for now. Maybe use Sigmol Log later
            // TO Do: Place in For while loop once everything else in in place
            ///
            //NEW. Update input to Inner Hidden Weights
            // To Do: Place in for while loop once this is ready
            for (int i = 0; i < innerHiddenGrads.Length; i++)
            {
                double derivate = (1 - innerHiddenOutputs[i]) * (1 + innerHiddenOutputs[i]); // f' of tanh
                double sum = 0.0;
                for (int j = 0; j < numOutput; j++)
                    sum += oGrads[j] * output_innerHiddenWeights[i][j];
                    //sum += oGrads[j] * hoWeights[i][j];

                innerHiddenGrads[i] = derivate * sum;
            }

                ///////


                // 2. Compute hidden gradients. Assumes tanh!
                // To DO: This has to be changed.. Going from InnerHidden to Hidden
                // oGrads will have to change to?? innerHiddenGrads?
                for (int i = 0; i < hGrads.Length; ++i)
                {
                    double derivative = (1 - hOutputs[i]) * (1 + hOutputs[i]); // f' of tanh is (1y)(1+y).
                    double sum = 0.0;
                    for (int j = 0; j < numInnerHidden; ++j) // Each hidden delta is the sum of numOutput terms.
                        sum += innerHiddenGrads[j] * hoWeights[i][j]; // may change hoWeights...
                       // sum += oGrads[j] * hoWeights[i][j]; // Each downstream gradient * outgoing weight.  

                    hGrads[i] = derivative * sum; // hGrad = (1-O)(1+O) * Sum(oGrads*oWts) 
                }

            

            // 3. Update input to hidden weights.
            for (int i = 0; i < ihWeights.Length; ++i) 
            {  
                for (int j = 0; j < ihWeights[i].Length; ++j) 
                { 
                    double delta = learnRate * hGrads[j] * inputs[i];
                    ihWeights[i][j] += delta; // Update.
                    ihWeights[i][j] += momentum * ihPreWeightsDelta[i][j]; // Add momentum factor.  
                    ihPreWeightsDelta[i][j] = delta; // Save the delta for next time.
                } 
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // I think the above code is correct
            // Finish the remain code. This is going to need allot of changes

            //New. Update inner Hidden Biases
            // To Do: place in for while loop


            for (int i = 0; i < innerHiddenBiases.Length; ++i)
            {
                double delta = learnRate * innerHiddenGrads[i] * 1.0; // The 1.0 is a dummy value; it could be left out.
                hBiases[i] += delta;
                hBiases[i] += momentum * innerHiddenPreBiasesDelta[i]; // To Do: change hPreBiaseDelta to ?
                innerHiddenPreBiasesDelta[i] = delta; // Save delta.
            }  


            //////

            // 4. Update hidden biases.
            for (int i = 0; i < hBiases.Length; ++i)
            { 
                double delta = learnRate * hGrads[i] * 1.0; // The 1.0 is a dummy value; it could be left out.
                hBiases[i] += delta;
                hBiases[i] += momentum * hPreBiasesDelta[i];
                hPreBiasesDelta[i] = delta; // Save delta.
            }  
 
              // 5. Update hidden to output weights.
            // Update this to hidden to innerHidden weights
            
            for (int i = 0; i < innerHiddenOutputs.Length; ++i)
            {
                for (int j = 0; j < output_innerHiddenWeights[i].Length; ++j) // TO DO: Check this. A guess..
                { 
                 double delta = learnRate * oGrads[j] * innerHiddenOutputs[i];
                 output_innerHiddenWeights[i][j] += delta;

                 hoWeights[i][j] += momentum * output_innerHiddenPreWeightsDelta[i][j];
                 output_innerHiddenPreWeightsDelta[i][j] = delta; // Save delta. 
                }   
             }

            // 6. Update output biases.
            // This should remain the same
            for (int i = 0; i < oBiases.Length; ++i)
            {
                double delta = learnRate * oGrads[i] * 1.0; 
                oBiases[i] += delta;
                oBiases[i] += momentum * oPreBiasesDelta[i];
                oPreBiasesDelta[i] = delta; // Save delta.
            }   
        }


     

        public void FindWeights(double[] tValues, double[] xValues, double learnRate,   double momentum, int maxEpochs)
        {   
            int epoch = 0;
            while (epoch <= maxEpochs)
            {  
                double[] yValues = ComputeOutputs(xValues);
                UpdateWeights(tValues, learnRate, momentum); 

          //   if (epoch % 100 == 0) 
          //   {     
               //  Console.Write("epoch = " + epoch.ToString().PadLeft(5) + "   curr outputs = ");  
               ///  BackPropProgram.ShowVector(yValues, 2, 4, true);
          //   }  
                ++epoch;
            } // Find loop.
        } 

        public double[] GetWeights() {


           // int numWeights = (numInput * numHidden) + numHidden + (numHidden * numOutput) + numOutput;

            int numWeights = (numInput * numHidden) + numHidden + (numHidden * numInnerHidden) + numInnerHidden + (numInnerHidden * numOutput) + numOutput;

            double[] result = new double[numWeights];

            int k = 0;  // Pointer into results array.  

            for (int i = 0; i < numInput; ++i)
                for (int j = 0; j < numHidden; ++j)
                    result[k++] = ihWeights[i][j];

            for (int i = 0; i < numHidden; ++i)
                result[k++] = hBiases[i];


            // Changed numOutput to numHiddenOutput
            for (int i = 0; i < numHidden; ++i)
                //for (int j = 0; j < numOutput; ++j)
                for (int j = 0; j < numInnerHidden; ++j)
                    result[k++] = hoWeights[i][j];


            for (int i = 0; i < numInnerHidden; i++)
                for (int j = 0; j < numOutput; ++j)
                    result[k++] = output_innerHiddenWeights[i][j];

                for (int i = 0; i < numOutput; ++i)
                    result[k++] = oBiases[i];

            return result; 
        }

        public void SetWeights(double[] weights)
        {
           // int numWeights = (numInput * numHidden) + numHidden + (numHidden * numInput) + numOutput;

            // This can't be right.. Check this over
            int numWeights = (numInput * numHidden) + numHidden + (numHidden * numInnerHidden) + numInnerHidden + (numInnerHidden * numOutput) + numOutput;

            if (weights.Length != numWeights)
                throw new Exception("Bad Weights array");

            int k = 0; // Pointer into weights parameter

            for (int i = 0; i < numInput; i++)
                for (int j = 0; j < numHidden; j++)
                    ihWeights[i][j] = weights[k++];

            for (int i = 0; i < numHidden; i++)
                hBiases[i] = weights[k++];

            // Changed from numOutput to numHidden
            for (int i = 0; i < numHidden; i++)
                //for (int j = 0; j < numOutput; j++)
                for (int j = 0; j < numInnerHidden; j++)
                    hoWeights[i][j] = weights[k++];

            // Changed numOutput to numInnerHidden
            for (int i = 0; i < numInnerHidden; i++)
                oBiases[i] = weights[k++];

            // This is new
            for (int i = 0; i < numInnerHidden; i++)
                for (int j = 0; j < numOutput; j++)
                    input_innerHiddenWeights[i][j] = weights[k++];

            for (int i = 0; i < numInnerHidden; i++)
                innerHiddenBiases[i] = weights[k++];

        }

        public double[] ComputeOutputs(double[] xValues)
        {
            // I think this is complete.. 
            Util.ShowConsoleMessage("Computing Outputs");
       
            if (xValues.Length != numInput)
                throw new Exception("Bad xValues array");

            double[] hSums = new double[numHidden];
            double[] oSums = new double[numOutput];
            double[] result = new double[numOutput];

            double[] inner_HiddenSums = new double[numInnerHidden]; // new

            for (int i = 0; i < xValues.Length; ++i)
                inputs[i] = xValues[i];

            //// The inputs become the 'Inputs for the InnerHiddenNetwork

            // Don't forget the Bias Values for the InnderHiddenNetwork

            /// The outputs of the InnerHiddenNetwork will the new Inputs for the Output Network


            for (int j = 0; j < numHidden; ++j)
                for (int i = 0; i < numInput; ++i)
                    hSums[j] += inputs[i] * ihWeights[i][j];

            for (int i = 0; i < numHidden; ++i)
                hSums[i] += hBiases[i];


            for (int i = 0; i < numHidden; ++i) 
                hOutputs[i] = HyperTan(hSums[i]);

            // Inner Hidden Network
            for (int j = 0; j < numInnerHidden; j++)
                for (int i = 0; i < numHidden; i++)
                    inner_HiddenSums[j] += hSums[i] * input_innerHiddenWeights[i][j];

            for (int i = 0; i < numHidden; i++)
                inner_HiddenSums[i] += innerHiddenBiases[i];

            ////////////////
            // New Output based on InnerHidden Sumation
            for (int j = 0; j < numOutput; ++j) 
               for (int i = 0; i < numInnerHidden; ++i)
                    oSums[j] += inner_HiddenSums[i] * hoWeights[i][j];

            ////////

            // This is orginal outputs

            //for (int j = 0; j < numOutput; ++j) 
            //    for (int i = 0; i < numHidden; ++i)
            //        oSums[j] += hSums[i] * hoWeights[i][j];

            for (int i = 0; i < numOutput; ++i)
            {
               
                oSums[i] += oBiases[i];
                if (oSums[i] > 100)
                {
                    int hould = 1;
                }
            }

            // Softmax does all outputs at once.
          ///  double[] softOut = Softmax(oSums);

            /////////////////////////////////////

            for (int i = 0; i < outputs.Length; ++i)
            {
                outputs[i] = LogSigmoid(oSums[i]);
                //    outputs[i] = softOut[i];
                int old = 1;
            }
            
 
            for (int i = 0; i < outputs.Length; ++i)
                result[i] = outputs[i];

            return result; 
        }

        private static double[][] MakeMatrix(int rows, int cols)
        {
            double[][] result = new double[rows][];

            for (int i = 0; i < rows; i++)
            {
                result[i] = new double[cols];
            }
            return result;
        }

        public void Train(double[][] trainData, int maxEpochs, double learnRate, double momentum)
        {
            // Train a back-propagation style NN classifier using learning rate and momentum. 
            int epoch = 0;  
            double[] xValues = new double[numInput]; 
            // Inputs.  
            double[] tValues = new double[numOutput];
            // Target values.  

            int[] sequence = new int[trainData.Length]; 

            for (int i = 0; i < sequence.Length; ++i) 
                sequence[i] = i;

            while (epoch < maxEpochs)
            {
                Util.ShowConsoleMessage("Epoch Number: " + epoch.ToString());
                double mse = MeanSquaredError(trainData);
                //double mse = MeanCrossEntropyError(trainData);

                Util.ShowConsoleMessage(" ... Mean Spuare set to: " + mse.ToString());
                if (mse < 0.040)
                    break;
                // Consider passing value in as parameter.
                Shuffle(sequence); // Visit each training data in random order. 

                for (int i = 0; i < trainData.Length; ++i)
                {
                    int idx = sequence[i];
                    Array.Copy(trainData[idx], xValues, numInput);
                    Array.Copy(trainData[idx], numInput, tValues, 0, numOutput);
                    ComputeOutputs(xValues); // Copy xValues in, compute outputs (store them internally). 
                    UpdateWeights(tValues, learnRate, momentum); // Find better weights.
                } // Each training item.
                ++epoch;
                Util.ShowConsoleMessage(" ");
            }
 
        }

        public static void Shuffle(int[] sequence)
        {
            Random rnd = new Random(0);

            for (int i = 0; i < sequence.Length; ++i)
            {
                int r = rnd.Next(i, sequence.Length);
                int tmp = sequence[r];
                sequence[r] = sequence[i];
                sequence[i] = tmp;
            } 
        }

        private double MeanCrossEntropyError(double[][] trainData)
        {
            double sumError = 0.0;
            double[] xValues = new double[numInput]; // First numInput values in trainData.
            double[] tValues = new double[numOutput]; // Last numOutput values. 
 
            for (int i = 0; i < trainData.Length; ++i) // Training data: (6.9 3.2 5.7 2.3) (0 0 1).
            {
                Array.Copy(trainData[i], xValues, numInput); // Get xValues.
                Array.Copy(trainData[i], numInput, tValues, 0, numOutput); // Get target values.
                double[] yValues = this.ComputeOutputs(xValues); // Compute output using current weights.
                for (int j = 0; j < numOutput; ++j)
                {
                    sumError += Math.Log(yValues[j]) * tValues[j]; // CE error for one training data.
                }
            }

            return -1.0 * sumError / trainData.Length;
        } 

        public double MeanSquaredError(double[][] trainData)
        {
            // Average squared error per training item. 
            double sumSquaredError = 0.0; 
            double[] xValues = new double[numInput];
            // First numInput values in trainData. 
            double[] tValues = new double[numOutput];
            // Last numOutput values.  
            // Walk through each training case. Looks like (6.9 3.2 5.7 2.3) (0 0 1). 
            for (int i = 0; i < trainData.Length; ++i)  
            {
                Array.Copy(trainData[i], xValues, numInput);
                Array.Copy(trainData[i], numInput, tValues, 0, numOutput); // Get target values. 
                double[] yValues = this.ComputeOutputs(xValues); // Outputs using current weights. 
                for (int j = 0; j < numOutput; ++j)
                {
                    double err = tValues[j] - yValues[j];
                    sumSquaredError += err * err;
                }
            }
  
            return sumSquaredError / trainData.Length; 
        }

        public double Arruracy(double[][] testData)
        {
             // Percentage correct using winner-takes all.
            int numCorrect = 0; 
            int numWrong = 0; 
            double[] xValues = new double[numInput];
            // Inputs.
            double[] tValues = new double[numOutput];
            // Targets.
            double[] yValues; // Computed Y.  

            for (int i = 0; i < testData.Length; ++i)
            {
                Array.Copy(testData[i], xValues, numInput); // Get x-values. 
                Array.Copy(testData[i], numInput, tValues, 0, numOutput); // Get t-values.
                yValues = this.ComputeOutputs(xValues);
                int maxIndex = MaxIndex(yValues);
                // Which cell in yValues has the largest value?  
                if (yValues[maxIndex] == 1.0) // ugly   TO DO: This is a temp fix. It really needs to check against yValue[index] == tValue[index]
                    ++numCorrect; 
                else
                    ++numWrong; 
            }

            return (numCorrect * 1.0) / (numCorrect + numWrong); // No check for divide by zero. 
        }

        public static int MaxIndex(double[] vector)
        {
            // Index of largest value.
            int bigIndex = 0;
            double biggestVal = vector[0];
            for (int i = 0; i < vector.Length; ++i)
            {
                if (vector[i] > biggestVal)
                {
                    biggestVal = vector[i];
                    bigIndex = i;
                }
            }

            return bigIndex; 
        }



        private static double HyperTan(double v)
        {
            if (v < -20.0) return -1.0;
            else if (v > 20.0) return 1.0;
            else return Math.Tanh(v); 
        }

        private static double[] Softmax(double[] oSums)
        {
            // Does all output nodes at once.
            // Determine max oSum. 
      
            double max = oSums[0];
            
         
          
            for (int i = 0; i < oSums.Length; ++i)
               if (oSums[i] > max)
                  max = oSums[i];
            
            // Determine scaling factor -- sum of exp(each val - max). 
            double scale = 0.0;

          
            for (int i = 0; i < oSums.Length; ++i)
             scale += Math.Exp(oSums[i] - max);
           

            double[] result = new double[oSums.Length];

       
            for (int i = 0; i < oSums.Length; ++i)
                result[i] = Math.Exp(oSums[i] - max) / scale;
         

            return result; // Now scaled so that xi sums to 1.0. 
        }

        public static double[] SoftmaxNaive(double[] oSums)
        {
            double denom = 0.0;
            for (int i = 0; i < oSums.Length; ++i)
                denom += Math.Exp(oSums[i]);

            double[] result = new double[oSums.Length];

            for (int i = 0; i < oSums.Length; ++i) 
                result[i] = Math.Exp(oSums[i]) / denom; 

            return result;
        }

        public static double LogSigmoid(double x)
        {
            if (x < -45.0) return 0.0;
            else if (x > 45.0)
                return 1.0;
            else
                return 1.0 / (1.0 + Math.Exp(-x));
        }
    }
}
