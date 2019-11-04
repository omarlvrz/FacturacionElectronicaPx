using System;
using System.Collections.Generic;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class PercepcionInfo
    {
        private String _seriePercepcion;
        private String _numeroPercepcion;
        private String _fechaPercepcion;
        private String _regimenPercepcion;
        private double? _valorTasaPercepcion;
        private double? _importeTotalPercibido;
        private String _monedaImporteTotalPercibido;
        private double? _importeTotalCobrado;
        private String _monedaImporteTotalCobrado;
        private String _observaciones;
        private ClienteInfo _cliente;
        private List<DetallePercepcionInfo> _detalle;

        public string SeriePercepcion
        {
            get { return _seriePercepcion; }
            set { _seriePercepcion = value; }
        }

        public string NumeroPercepcion
        {
            get { return _numeroPercepcion; }
            set { _numeroPercepcion = value; }
        }

        public String FechaPercepcion
        {
            get { return _fechaPercepcion; }
            set { _fechaPercepcion = value; }
        }

        public string RegimenPercepcion
        {
            get { return _regimenPercepcion; }
            set { _regimenPercepcion = value; }
        }

        public double? ValorTasaPercepcion
        {
            get { return _valorTasaPercepcion; }
            set { _valorTasaPercepcion = value; }
        }

        public double? ImporteTotalPercibido
        {
            get { return _importeTotalPercibido; }
            set { _importeTotalPercibido = value; }
        }

        public string MonedaImporteTotalPercibido
        {
            get { return _monedaImporteTotalPercibido; }
            set { _monedaImporteTotalPercibido = value; }
        }

        public double? ImporteTotalCobrado
        {
            get { return _importeTotalCobrado; }
            set { _importeTotalCobrado = value; }
        }

        public string MonedaImporteTotalCobrado
        {
            get { return _monedaImporteTotalCobrado; }
            set { _monedaImporteTotalCobrado = value; }
        }

        public string Observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        public ClienteInfo Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }

        public List<DetallePercepcionInfo> Detalle
        {
            get { return _detalle; }
            set { _detalle = value; }
        }
    }
}
