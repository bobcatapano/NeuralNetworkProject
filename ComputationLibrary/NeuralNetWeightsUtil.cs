using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkClasses;
using NeuralNetworkProject;

namespace ComputationLibrary
{
    /// <summary>
    /// This class handles the common util functions for setting, getting, finding and updating weights
    /// </summary>
    public static class NeuralNetWeightsUtil
    {
        // double[] tValues, double[] matrixValues are obsolete. Have to figure out how these map to Input, List<Hidden>, Output
        public static void FindWeights(Input inputNodes, List<Hidden> listOfHiddenNodes, Output outputNodes,  double[] tValues, double[] matrixValues, double learnRate, double momentum, int maxEpochs)
        {
            // TO DO: come up with a better names then tValues and xValues
            int epoch = 0;
            while (epoch <= maxEpochs)
            {
               // double[] OutPutValues = ComputeoutputLibary.ComputeOutput(matrixValues);
                double[] OutPutValues = ComputeoutputLibary.ComputeOutput(inputNodes, listOfHiddenNodes, outputNodes);
                UpdateWeights(tValues, learnRate, momentum, ref inputNodes, ref listOfHiddenNodes, ref outputNodes);

                if (epoch % 100 == 0)
                {
                    Console.Write("epoch = " + epoch.ToString().PadLeft(5) + "   curr outputs = ");
                    Util.ShowVector(OutPutValues, 2, 4, true);
                }  
                ++epoch;
            } 
        }

        public static double[] GetWeights(int numWeights, Input inputNodes, List<Hidden> listOfHiddenNodes, Output outputNodes)
        {
          int NumberOfHiddenNetworks = listOfHiddenNodes.Count;
          int TheLastHiddenNetworkIndex = NumberOfHiddenNetworks -1;

          double[] result = new double[numWeights];

          int k = 0;  // Pointer into results array.  

        // This is for the First Hidden Network
          for (int i = 0; i < inputNodes.Value.Length; ++i)
            for (int j = 0; j < listOfHiddenNodes[0].Value.Length; ++j)
                result[k++] = inputNodes.Weight[i,j];

          for (int i = 0; i < listOfHiddenNodes[0].Value.Length; ++i)
            result[k++] = listOfHiddenNodes[0].Bias[i];
         //////////////////////////////////////////////

        // IF there are more than 1 Hidden Nodes

        if (NumberOfHiddenNetworks > 1)
        {
            for (int i = 1; i < (NumberOfHiddenNetworks - 1); i++)
            {
                for (int a = 0; a < listOfHiddenNodes[i].Value.Length; a++)
                {
                    for (int b = 0; b < listOfHiddenNodes[i++].Value.Length; b++)
                    {
                        result[k++] = listOfHiddenNodes[i].Weight[a, b];
                    }
                }
            }

            for (int i = 1; i < (NumberOfHiddenNetworks); i++)
            {
                for (int a = 0; a < listOfHiddenNodes[i].Value.Length; a++)
                {
                    result[k++] = listOfHiddenNodes[i].Bias[a];
                }

            }
        }
        ///////////////////////////////////////////////


        // This is for the Last Hidden Network connected to the Output Network
       
        for (int i = 0; i < listOfHiddenNodes[TheLastHiddenNetworkIndex].Weight.Length; ++i)
            for (int j = 0; j < outputNodes.Value.Length; ++j)
                result[k++] = listOfHiddenNodes[TheLastHiddenNetworkIndex].Weight[i,j];


        for (int i = 0; i < outputNodes.Value.Length; ++i)
            result[k++] = outputNodes.Bias[i];

        return result; 
  
        }


        private static void UpdateWeights(double[] tValues, double learnRate, double momentum, ref Input InputNodes, ref List<Hidden> HiddenNodes, ref Output OutputNodes)
        {
            double derivative = 0.0;
            double sum = 0;
            double delta = 0;

            int NumberOfHiddenLayers = HiddenNodes.Count;
            int LastHiddenNodeIndex = NumberOfHiddenLayers - 1;

            //// 1. Compute output gradients. Assumes softmax. // This checks out..
            for (int i = 0; i < OutputNodes.OGrad.Length; i++)
            {
                // double derivative = (1 - outputs[i]) * (1 + outputs[i]);
                derivative = (1 - OutputNodes.Value[i]) * OutputNodes.Value[i]; // Derivative of softmax is y(1y).

                if (double.IsNegativeInfinity(derivative))
                    derivative = 0;

                OutputNodes.OGrad[i] = derivative * (tValues[i] - OutputNodes.Value[i]); // oGrad = (1 - O)(O) * (T-O)
            }


            // 2. Compute from Output to Last Hidden Layer -- Compute Hidden Gradient from Output layer to Last Hidden layer // This checks out..
            for (int i = 0; i < HiddenNodes[LastHiddenNodeIndex].Value.Length; i++)
            {
                derivative = (1 - HiddenNodes[LastHiddenNodeIndex].Value[i]) * (1 + HiddenNodes[LastHiddenNodeIndex].Value[i]);
              //  derivative = (1 - innerHiddenOutputs[i]) * (1 + innerHiddenOutputs[i]); // f' of tanh
                sum = 0.0;
                for (int j = 0; j < OutputNodes.OGrad.Length; j++)
                {
                    //    sum += oGrads[j] * output_innerHiddenWeights[i][j];
                    sum += OutputNodes.OGrad[j] * HiddenNodes[LastHiddenNodeIndex].Weight[i,j];
                }

                 HiddenNodes[LastHiddenNodeIndex].HGrad[i] = derivative * sum;
            }


            /// NOTE: CHECK WHICH ONE IS CORRECT.. The bottom may next obsolete but I wrote this just to compare


            ////NEW. Compute innerHidden Gradients. This uses tanh for now. Maybe use Sigmol Log later
            //// TO Do: Place in For while loop once everything else in in place
            /////
            ////NEW. Update input to Inner Hidden Weights
            //// To Do: Place in for while loop once this is ready

            // Place in for loop to transverse through hidden nodes

            //3. Update input to hidden weights ---- Check this next - I think this is fine ... 1_20_17
            for (int x = LastHiddenNodeIndex-1; x == 0; x--)
            {
                for (int i = 0; i < HiddenNodes[x].HGrad.Length; i++)
                {
                    double derivate = (1 - HiddenNodes[x].Value[i]) * (1 + HiddenNodes[x].Value[i]); // f' of tanh
                    sum = 0.0;
                    for (int j = 0; j < HiddenNodes[x+1].Value.Length; j++)
                        sum += HiddenNodes[x + 1].HGrad[j] * HiddenNodes[x + 1].Weight[i,j];
                    //sum += oGrads[j] * hoWeights[i][j];

                    HiddenNodes[x].HGrad[i] = derivate * sum;
                }
            }

            /////////


            //// 2. Compute hidden gradients. Assumes tanh!
            //// To DO: This has to be changed.. Going from InnerHidden to Hidden
            //// oGrads will have to change to?? innerHiddenGrads?
            //for (int i = 0; i < hGrads.Length; ++i)
            //{
            //    double derivative = (1 - hOutputs[i]) * (1 + hOutputs[i]); // f' of tanh is (1y)(1+y).
            //    double sum = 0.0;
            //    for (int j = 0; j < numInnerHidden; ++j) // Each hidden delta is the sum of numOutput terms.
            //        sum += innerHiddenGrads[j] * hoWeights[i][j]; // may change hoWeights...
            //    // sum += oGrads[j] * hoWeights[i][j]; // Each downstream gradient * outgoing weight.  

            //    hGrads[i] = derivative * sum; // hGrad = (1-O)(1+O) * Sum(oGrads*oWts) 
            //}


            /// TO DO: Update all of the Hidden Nodes from Last to First -- Cheek this over 1_20_17 - This will seriously screw things up if this isn't correct
            /// Treat each Hidden Node to the left as an Input
            /// for int(i=lastinputNde to secondnode in this case 1)
            ///   for int(j=0 to Number of Inputs from HiddenNode[i]
            ///      delta = learnRate * HiddenNodes[0].HGrad[j] * InputNodes.Value[i];
            //         HiddenNodes[0].Weight[i, j] += delta; // Update.
            //         HiddenNodes[0].Weight[i, j] += momentum * HiddenNodes[0].HoPreWeightsDelta[i, j]; // Add momentum factor.  
            //         HiddenNodes[0].HoPreWeightsDelta[i, j] = delta; // Save the delta for next time.




            // This is fine. This is the last computation needed from the First Hidden Nodes to the Inputs
            //// 3. Update input to hidden weights.
            //for (int x = 0; x < NumberOfHiddenLayers; x++)
            //{
            for (int i = 0; i < HiddenNodes[0].Weight.Length; i++)
                { 
                    for (int j = 0; j < InputNodes.Weight.Length; j++)
                    {
                          delta = learnRate * HiddenNodes[0].HGrad[j] * InputNodes.Value[i];
                          HiddenNodes[0].Weight[i,j] += delta; // Update.
                          HiddenNodes[0].Weight[i, j] += momentum * HiddenNodes[0].HoPreWeightsDelta[i,j]; // Add momentum factor.  
                          HiddenNodes[0].HoPreWeightsDelta[i, j] = delta; // Save the delta for next time.
                    }
                }
            //}

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            //// I think the above code is correct
            //// Finish the remain code. This is going to need allot of changes

            ////New. Update inner Hidden Biases
            //// To Do: place in for while loop


            //for (int i = 0; i < innerHiddenBiases.Length; ++i)
            //{
            //    double delta = learnRate * innerHiddenGrads[i] * 1.0; // The 1.0 is a dummy value; it could be left out.
            //    hBiases[i] += delta;
            //    hBiases[i] += momentum * innerHiddenPreBiasesDelta[i]; // To Do: change hPreBiaseDelta to ?
            //    innerHiddenPreBiasesDelta[i] = delta; // Save delta.
            //}


            ////////

            

            //// 4. Update hidden biases.  this is ok for the First Hidden Node
            for (int i = 0; i < HiddenNodes[0].Bias.Length; i++)
            {
                  delta = learnRate * HiddenNodes[0].HGrad[i] * 1.0; // The 1.0 is a dummy value; it could be left out.
                  HiddenNodes[0].Bias[i] += delta;
                  HiddenNodes[0].Bias[i] += momentum * HiddenNodes[0].HPreBiasesDelta[i];
                  HiddenNodes[0].HPreBiasesDelta[i] = delta; // Save delta.
            }


            // JORDAN: updating hidden nodws

            // TO DO: Perform the same steps as above in relation to All Hidden Nodes from second to Second to Last Node

            // for I = 1 to LastNodeIndex-1
            //    for A = 0 to HiddenNode[I].value.length
            //       for B = 0 to HiddenNode[I+1].value.length ?? think about this some more 


            //// 5. Update hidden to output weights.
            //// Update this to hidden to innerHidden weights. This is ok for the Last Node 

            for (int i = 0; i < HiddenNodes[LastHiddenNodeIndex].Value.Length; i++)
            {
                for (int j = 0; j < OutputNodes.Value.Length; ++j) // TO DO: Check this. A guess..
                {
                      delta = learnRate * OutputNodes.OGrad[j] * HiddenNodes[LastHiddenNodeIndex].Value[i];
                      HiddenNodes[LastHiddenNodeIndex].Weight[i,j] += delta;

                      HiddenNodes[LastHiddenNodeIndex].Weight[i, j] += momentum * HiddenNodes[LastHiddenNodeIndex].HoPreWeightsDelta[i,j];
                      HiddenNodes[LastHiddenNodeIndex].HoPreWeightsDelta[i, j] = delta; // Save delta. 
                }
            }

            //// 6. Update output biases.
            //// This should remain the same -- this is ok
            for (int i = 0; i < OutputNodes.Bias.Length; ++i)
            {
                  delta = learnRate * OutputNodes.OGrad[i] * 1.0;
                  OutputNodes.Bias[i] += delta;
                  OutputNodes.Bias[i] += momentum * OutputNodes.OPreBiasesDelta[i];
                  OutputNodes.OPreBiasesDelta[i] = delta; // Save delta.
            }


           // What I think is needed is as follows:
           // 1: Start from Last hidden Node - treat this as the output.
           // 2: then go backwards treating the Last - 1 Hidden as the Input...
           // 3: Update all the values until I reached the first Hidden node
           // 1_20_17 - already started

        }
    }
}
