using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkClasses
{
    public class Hidden
    {
        double[] _value= null;
        double[,] hoWeights = null;
        double[] bias = null;
        double[] hgrad = null;
        double[,] hoPreWeightsDelta = null;
        double[] hPreBiasesDelta = null;

        public Hidden(int number, int number_of_nodes_to_connect)
        {
            _value = new double[number];
            hoWeights = new double[number, number_of_nodes_to_connect];
            hgrad = new double[number];
            hoPreWeightsDelta = new double[number, number_of_nodes_to_connect];
            hPreBiasesDelta = new double[number];
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
                return hoWeights;
            }
            set
            {
                hoWeights = value;
            }
        }

        public double[] Bias
        {
            get
            {
                return bias;
            }

            set
            {
                bias = value;
            }
        }

        public double[] HGrad
        {
            get
            {
                return hgrad;
            }

            set
            {
                hgrad = value;
            }
        }

        public double[,] HoPreWeightsDelta
        {
            get
            {
                return hoPreWeightsDelta;
            }

            set
            {
                hoPreWeightsDelta = value;
            }
        }

        public double[] HPreBiasesDelta
        {
            get
            {
                return hPreBiasesDelta;
            }

            set
            {
                hPreBiasesDelta = value;
            }
        }

    }
}
