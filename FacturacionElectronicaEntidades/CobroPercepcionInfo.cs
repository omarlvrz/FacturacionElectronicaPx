using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class CobroPercepcionInfo
    {
        private String _fechaCobro;
        private String _numeroCobro;
        private double? _importeCobroSinPerc;
        private String _monedaCobro;

        public String FechaCobro
        {
            get { return _fechaCobro; }
            set { _fechaCobro = value; }
        }

        public string NumeroCobro
        {
            get { return _numeroCobro; }
            set { _numeroCobro = value; }
        }

        public double? ImporteCobroSinPerc
        {
            get { return _importeCobroSinPerc; }
            set { _importeCobroSinPerc = value; }
        }

        public string MonedaCobro
        {
            get { return _monedaCobro; }
            set { _monedaCobro = value; }
        }
    }
}
