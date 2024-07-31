using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkClasses
{
    public class Input
    {
        
        double[] _value= null;
        double[,] hiWeight = null;
        double[,] ihPreWeightsDelta = null;

        public Input(int number, int hiddenNodes_number)
        {
            _value = new double[number];
            hiWeight = new double[number,hiddenNodes_number];
            ihPreWeightsDelta = new double[number, hiddenNodes_number];
        }

        public double[] Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }

        public double[,] Weight
        {
            get
            {
                return hiWeight;
            }
            set
            {
                hiWeight = value;
            }
        }

        public double[,] IhPreWeightsDelta
        {
            get
            {
                return ihPreWeightsDelta;
            }
            set
            {
                ihPreWeightsDelta = value;
            }
        }
    }
}
