using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkClasses;

namespace SetUpNetworkFromMatrix
{
    public class SetUpFromMatrixFromAGeneralSource
    {
        public static void SetWeights(double[] weights, ref Input inputNodes, ref List<Hidden> listOfHiddenNodes, ref Output outputNodes)
        {
            int numberOfInputNodes = inputNodes.Value.Length;
            int numberOfHiddenNetworks = listOfHiddenNodes.Count;
            int numberOfOutPutNodes = outputNodes.Value.Length;
            int numberOfFirstNetworkHiddenNetworkNodes = listOfHiddenNodes[0].Value.Length;
            int theLastIndexMatrixNumber = numberOfInputNodes - 1;

            int k = 0; // Pointer into weights parameter

            // This handles the Input Node list
            for (int i = 0; i < numberOfInputNodes; i++)
                for (int j = 0; j < numberOfFirstNetworkHiddenNetworkNodes; j++)
                    inputNodes.Weight[i, j] = weights[k++];

            //// This handles the Fist Network Matrix
            for (int i = 0; i < numberOfFirstNetworkHiddenNetworkNodes; i++)
                listOfHiddenNodes[0].Bias[i] = weights[k++];


            if (numberOfHiddenNetworks > 1)
            {
                for (int i = 1; i < numberOfHiddenNetworks - 1; i++)
                {
                    for (int a = 0; a < listOfHiddenNodes[i].Weight.Length; a++)
                    {
                        for (int b = 0; b < listOfHiddenNodes[i++].Weight.Length; b++)
                            listOfHiddenNodes[i].Weight[a, b] = weights[k++];
                    }

                    for (int a = 0; a < listOfHiddenNodes[i].Bias.Length; a++)
                        listOfHiddenNodes[i].Bias[a] = weights[k++];
                }
            }


            // Need to include the Output Node
            // I don't think this is necessary... Check this over. It may produce a bug..

            for (int i = 0; i < listOfHiddenNodes[theLastIndexMatrixNumber].Weight.Length; i++)
            {
                for (int a = 0; a < outputNodes.Value.Length; a++)
                {
                    listOfHiddenNodes[theLastIndexMatrixNumber].Weight[i, a] = weights[k++];
                }
            }

            // Set this Bais values for the OutPutNodes
            for (int i=0; i < outputNodes.Value.Length;i++)
                outputNodes.Bias[i] = weights[k++];

        }
    }
}
