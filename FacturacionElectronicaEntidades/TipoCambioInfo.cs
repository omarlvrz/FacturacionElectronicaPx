using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class TipoCambioInfo
    {
        private string _monedaOrigen;
        private string _monedaDestino;
        private decimal? _tipoCambio;
        private string _fechaCambio;

        public string MonedaOrigen
        {
            get { return _monedaOrigen; }
            set { _monedaOrigen = value; }
        }

        public string MonedaDestino
        {
            get { return _monedaDestino; }
            set { _monedaDestino = value; }
        }

        public decimal? TipoCambio
        {
            get { return _tipoCambio; }
            set { _tipoCambio = value; }
        }

        public string FechaCambio
        {
            get { return _fechaCambio; }
            set { _fechaCambio = value; }
        }
    }
}
