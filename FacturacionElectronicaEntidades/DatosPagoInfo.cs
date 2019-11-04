using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class DatosPagoInfo
    {
        private string _fechaPago;
        private string _numeroPago;
        private decimal? _importePago;
        private string _monedaPago;

        public string FechaPago
        {
            get { return _fechaPago;}
            set { this._fechaPago = value; }
        }

        public string NumeroPago
        {
            get { return _numeroPago; }
            set { this._numeroPago = value; }
        }

        public decimal? ImportePago
        {
            get { return this._importePago; }
            set { this._importePago = value; }
        }

        public string MonedaPago
        {
            get { return _monedaPago; }
            set { _monedaPago = value; }
        }
    }
}
