using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class DetalleRetencionInfo
    {
        private DocumentoRelacionadoInfo _documentoRelacionado;
        private DatosPagoInfo _datosPago;
        private DatosRetencionInfo _datosRetencion;
        private TipoCambioInfo _tipoCambio;

        public DocumentoRelacionadoInfo DocumentoRelacionado
        {
            get { return this._documentoRelacionado; }
            set { this._documentoRelacionado = value; }
        }

        public DatosPagoInfo DatosPago
        {
            get { return this._datosPago; }
            set { this._datosPago = value; }
        }

        public DatosRetencionInfo DatosRetencion
        {
            get { return this._datosRetencion; }
            set { this._datosRetencion = value; }
        }

        public TipoCambioInfo TipoCambio
        {
            get { return this._tipoCambio; }
            set { this._tipoCambio = value; }
        }
    }
}
