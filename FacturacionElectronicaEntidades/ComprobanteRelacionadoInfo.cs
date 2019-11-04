using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class ComprobanteRelacionadoInfo
    {
        private String _idTipoDocumento;
        private String _serieDocumento;
        private String _numeroDocumento;
        private String _fechaEmision;
        private double? _importeTotal;
        private String _monedaDocumento;

        public string IdTipoDocumento
        {
            get { return _idTipoDocumento; }
            set { _idTipoDocumento = value; }
        }

        public string SerieDocumento
        {
            get { return _serieDocumento; }
            set { _serieDocumento = value; }
        }

        public string NumeroDocumento
        {
            get { return _numeroDocumento; }
            set { _numeroDocumento = value; }
        }

        public double? ImporteTotal
        {
            get { return _importeTotal; }
            set { _importeTotal = value; }
        }

        public string MonedaDocumento
        {
            get { return _monedaDocumento; }
            set { _monedaDocumento = value; }
        }

        public string FechaEmision
        {
            get { return _fechaEmision; }
            set { _fechaEmision = value; }
        }
    }
}
