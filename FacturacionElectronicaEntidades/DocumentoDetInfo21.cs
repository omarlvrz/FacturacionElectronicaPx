﻿using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class DocumentoDetInfo21
    {
        private String _serie;
        private String _nroDocumento;
        private String _idPlanta;
        private String _idTipoFacturacion;
        private String _idTipoDoc;
        private String _idArticulo;
        private ArticuloInfo _articulo;
        private Int32? _item;
        private Double? _precioUnitario;
        private Double? _precioUnitarioIgv;
        private Double? _cantidad;
        private Double? _subTotal;
        private Double? _igv;
        private Double? _factorDescuento;
        private Double? _descuento;
        private Double? _total;
        private Double? _api;
        private Double? _temperatura;
        private Double? _isc;
        private Double? _montoGratuito;
        private String _placaAtencion;

        /// <summary>
        /// Serie del Documento
        /// </summary>
        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }

        /// <summary>
        /// Número del Documento
        /// </summary>
        public string NroDocumento
        {
            get { return _nroDocumento; }
            set { _nroDocumento = value; }
        }

        /// <summary>
        /// Id de la Planta del Documento
        /// </summary>
        public string IdPlanta
        {
            get { return _idPlanta; }
            set { _idPlanta = value; }
        }

        /// <summary>
        /// Id del Tipo de Facturación del Documento
        /// </summary>
        public string IdTipoFacturacion
        {
            get { return _idTipoFacturacion; }
            set { _idTipoFacturacion = value; }
        }

        /// <summary>
        /// Id del Tipo de Documento del Documento
        /// </summary>
        public string IdTipoDoc
        {
            get { return _idTipoDoc; }
            set { _idTipoDoc = value; }
        }

        /// <summary>
        /// Id del Artículo del Detalle
        /// </summary>
        public string IdArticulo
        {
            get { return _idArticulo; }
            set { _idArticulo = value; }
        }

        /// <summary>
        /// Artículo del Detalle
        /// </summary>
        public ArticuloInfo Articulo
        {
            get { return _articulo; }
            set { _articulo = value; }
        }

        /// <summary>
        /// Nro. de Item del Detalle
        /// </summary>
        public int? Item
        {
            get { return _item; }
            set { _item = value; }
        }

        /// <summary>
        /// Precio Unitario del Detalle
        /// </summary>
        public double? PrecioUnitario
        {
            get { return _precioUnitario; }
            set { _precioUnitario = value; }
        }

        /// <summary>
        /// Cantidad del Detalle
        /// </summary>
        public double? Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        /// <summary>
        /// SubTotal del Detalle
        /// </summary>
        public double? SubTotal
        {
            get { return _subTotal; }
            set { _subTotal = value; }
        }

        /// <summary>
        /// IGV del Detalle
        /// </summary>
        public double? Igv
        {
            get { return _igv; }
            set { _igv = value; }
        }

        /// <summary>
        /// Factor de Descuento del Detalle
        /// </summary>
        public double? FactorDescuento
        {
            get { return _factorDescuento; }
            set { _factorDescuento = value; }
        }

        /// <summary>
        /// Descuento del Detalle
        /// </summary>
        public double? Descuento
        {
            get { return _descuento; }
            set { _descuento = value; }
        }

        /// <summary>
        /// Total del Detalle
        /// </summary>
        public double? Total
        {
            get { return _total; }
            set { _total = value; }
        }

        /// <summary>
        /// Precio Unitario con IGV
        /// </summary>
        public double? PrecioUnitarioIgv
        {
            get { return _precioUnitarioIgv; }
            set { _precioUnitarioIgv = value; }
        }

        /// <summary>
        /// API del producto
        /// </summary>
        public double? Api
        {
            get { return _api; }
            set { _api = value; }
        }

        /// <summary>
        /// Temperatura del producto
        /// </summary>
        public double? Temperatura
        {
            get { return _temperatura; }
            set { _temperatura = value; }
        }

        /// <summary>
        /// Impuesto Selectivo al Consumo
        /// </summary>
        public double? Isc
        {
            get { return _isc; }
            set { _isc = value; }
        }

        public double? MontoGratuito
        {
            get { return _montoGratuito; }
            set { _montoGratuito = value; }
        }

        public string PlacaAtencion
        {
            get { return _placaAtencion; }
            set { _placaAtencion = value; }
        }
    }
}
