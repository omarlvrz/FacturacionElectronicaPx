using System;
using System.Collections.Generic;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class DocumentoRetencionInfo
    {
        private string _serie;
        private string _numeroRetencion;
        private string _fechaComprobante;
        private string _regimenRetencion;
        private decimal? _valorTasaRetencion;
        private decimal? _montoTotalRetenido;
        private string _monedaMontoRetenido;
        private decimal? _montoTotalPagado;
        private string _monedaMontoPagado;
        private ProveedorInfo _proveedor;

        private List<DetalleRetencionInfo> _detalle;

        public string NumeroRetencion
        {
            get { return this._numeroRetencion; }
            set { this._numeroRetencion = value; }
        }

        public string FechaComprobante
        {
            get { return this._fechaComprobante; }
            set { this._fechaComprobante = value; }
        }

        public string RegimenRetencion
        {
            get { return this._regimenRetencion; }
            set { this._regimenRetencion = value; }
        }

        public decimal? ValorTasaRetencion
        {
            get { return this._valorTasaRetencion; }
            set { this._valorTasaRetencion = value; }
        }

        public decimal? MontoTotalRetenido
        {
            get { return this._montoTotalRetenido; }
            set { this._montoTotalRetenido = value; }
        }

        public string MonedaMontoRetenido
        {
            get { return this._monedaMontoRetenido; }
            set { this._monedaMontoRetenido = value; }
        }

        public decimal? MontoTotalPagado
        {
            get { return this._montoTotalPagado; }
            set { this._montoTotalPagado = value; }
        }

        public string MonedaMontoPagado
        {
            get { return this._monedaMontoPagado; }
            set { this._monedaMontoPagado = value; }
        }

        public ProveedorInfo Proveedor
        {
            get { return this._proveedor; }
            set { this._proveedor = value; }
        }

        public List<DetalleRetencionInfo> Detalle
        {
            get { return this._detalle; }
            set { this._detalle = value; }
        }

        public string Serie
        {
            get { return this._serie; }
            set { this._serie = value; }
        }
    }
}
