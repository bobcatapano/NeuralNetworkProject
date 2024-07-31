using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkClasses;

namespace ComputationLibrary
{
    public static class ComputeForHiddenNetwork
    {
        /// <summary>
        /// This is for Single Layer Networks
        /// </summary>
        /// <param name="singlayerNetwork"></param>
        /// <param name="outputLayer"></param>
        /// <returns></returns>
        public static double[] ComputeForSingleLayerHiddenNetwork(Hidden singlayerNetwork, Output outputLayer)
        {
            double[] finalOutput = new double[outputLayer.Value.Length];

            // TO Do: Add code

            return finalOutput;
        }

        /// <summary>
        /// This is my Multi Layer Networks
        /// </summary>
        /// <param name="multilayerNetwork"></param>
        /// <param name="outputLayer"></param>
        /// <returns></returns>
        public static double[] ComputeForMultiLayerHiddenNetwork(List<Hidden> multilayerNetwork, Output outputLayer)
        {
            double[] finalOutput = new double[outputLayer.Value.Length];
            return finalOutput;
        }
    }
}
