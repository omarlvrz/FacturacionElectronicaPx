using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class DatosRetencionInfo
    {
        private decimal? _importeRetenido;
        private string _monedaImporteRetenido;
        private string _fechaRetencion;
        private decimal? _importeTotal;
        private string _monedaImporteTotal;

        public decimal? ImporteRetenido
        {
            get { return _importeRetenido; }
            set {_importeRetenido = value; }
        }

        public string MonedaImporteRetenido
        {
            get { return this._monedaImporteRetenido; }
            set { this._monedaImporteRetenido = value; }
        }

        public string FechaRetencion
        {
            get { return this._fechaRetencion; }
            set { this._fechaRetencion = value; }
        }

        public decimal? ImporteTotal
        {
            get { return this._importeTotal; }
            set { this._importeTotal = value; }
        }

        public string MonedaImporteTotal
        {
            get { return this._monedaImporteTotal; }
            set { this._monedaImporteTotal = value; }
        }
    }
}
