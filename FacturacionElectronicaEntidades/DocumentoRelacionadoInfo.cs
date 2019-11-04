using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class DocumentoRelacionadoInfo
    {
        private string _idTipoDocumento;
        private string _numeroDocumento;
        private string _fechaEmision;
        private decimal? _importeTotal;
        private string _monedaImporteTotal;

        public string IdTipoDocumento
        {
            get { return this._idTipoDocumento; }
            set { this._idTipoDocumento = value; }
        }

        public string NumeroDocumento
        {
            get { return this._numeroDocumento; }
            set { this._numeroDocumento = value; }
        }

        public string FechaEmision
        {
            get { return this._fechaEmision; }
            set { this._fechaEmision = value; }
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
