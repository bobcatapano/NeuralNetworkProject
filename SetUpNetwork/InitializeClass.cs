using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkClasses;

namespace SetUpNetwork
{
    public class Initialize
    {
        Random randomNumber = null;

        // TO DO: This isn't complete. Need to create Methods for Pre* Matrix's
        public Initialize(int seed)
        {
           randomNumber = new Random(seed);
        }
       
        public List<Hidden> SetUpHiddenNetWork(int[] Sequence)
        {     
            List<Hidden> listOfHiddenNetworks = new List<Hidden>();
            
            for (int I = 0; I < Sequence.Length; I++)
            {
                // Note: This may break: Potential Bug if only One Sequence number is entered
                listOfHiddenNetworks.Add(new Hidden(Sequence[I], Sequence[I++]));
            }

            foreach(var hidden_network in listOfHiddenNetworks)
            {
                for (int I = 0; I < hidden_network.Value.Length; I++)
                {
                    hidden_network.Bias[I] = randomNumber.NextDouble();
                    hidden_network.HPreBiasesDelta[I] = randomNumber.NextDouble();
                }

                for (int I = 0; I < hidden_network.Weight.GetUpperBound(0); I++)
                    for (int A = 0; A < hidden_network.Weight.GetUpperBound(1); A++)
                    {
                        hidden_network.Weight[I, A] = randomNumber.NextDouble();
                        hidden_network.HoPreWeightsDelta[I, A] = randomNumber.NextDouble();
                    }
            }

            return listOfHiddenNetworks;
        }

        public Input SetUpInputNodes(int numberOfNodes, int[] Sequence)
        {
            int numberOfNodes_in_first_hiddenNodes = Sequence[0];

            Input inputNodes = new Input(numberOfNodes, numberOfNodes_in_first_hiddenNodes);

            for (int I= 0; I < numberOfNodes; I++)
            {
                for (int A = 0; A < numberOfNodes_in_first_hiddenNodes; A++)
                {
                    inputNodes.Weight[I, A] = randomNumber.NextDouble();
                    inputNodes.IhPreWeightsDelta[I, A] = randomNumber.NextDouble();
                }
                        
            }

            return inputNodes;
        }

        public Output SetUpOutNodes(int numberOfNodes)
        {
            Output outputNodes = new Output(numberOfNodes);

            for (int I=0; I < numberOfNodes; I++)
            {
                outputNodes.Bias[I] = randomNumber.NextDouble();
                outputNodes.OPreBiasesDelta[I] = randomNumber.NextDouble();
            }

            return outputNodes;
        }
    }
}
