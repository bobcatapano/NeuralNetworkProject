using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkClasses
{
    public class Output
    {
        double[] _value= null;
        double[] bias = null;
        double[] ograd = null;
        double[] oPreBiasesDelta = null;
        public Output(int number)
        {
            _value = new double[number];
            bias = new double[number];
            ograd = new double[number];
            oPreBiasesDelta = new double[number];
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

        public double[] OGrad
        {
            get
            {
                return ograd;
            }

            set
            {
                ograd = value;
            }
        }

        public double[] OPreBiasesDelta
        { 
            get
            {
                return oPreBiasesDelta;
            }

            set
            {
                oPreBiasesDelta = value;
            }
        }

    }
}
