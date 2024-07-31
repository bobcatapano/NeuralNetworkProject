using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkClasses;


namespace ComputationLibrary
{
    public class ComputeoutputLibary
    {
         public static double[] ComputeOutput(Input inputnodes, List<Hidden> hiddenNodes, Output outputNodes)
         {
             double sumValue = 0.0;
             double biasValue = 0.0;
             double[] finalOutPutValue = new double[outputNodes.Value.Length];
             
             for (int I = 0; I < hiddenNodes[0].Weight.Length; I++)
             {
                 for (int A= 0; A < inputnodes.Value.Length; A++)
                 {
                     sumValue += inputnodes.Value[A] * hiddenNodes[0].Weight[A, I];
                 }

                biasValue = hiddenNodes[0].Bias[I];
                hiddenNodes[0].Value[I] = HyperTan(sumValue + biasValue);
             }

             if (hiddenNodes.Count == 1)
             {
                 finalOutPutValue = ComputeForHiddenNetwork.ComputeForSingleLayerHiddenNetwork(hiddenNodes[0], outputNodes);
             }
             else
             {
                 finalOutPutValue = ComputeForHiddenNetwork.ComputeForMultiLayerHiddenNetwork(hiddenNodes, outputNodes);
             }

             return finalOutPutValue;           
         }

        // Create the TanH Function
         private static double HyperTan(double v)
         {
             if (v < -20.0) return -1.0;
             else if (v > 20.0) return 1.0;
             else return Math.Tanh(v);
         }

        // Create the SigM Function

    }
}
