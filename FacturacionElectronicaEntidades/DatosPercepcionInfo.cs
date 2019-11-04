using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class DatosPercepcionInfo
    {
        private double? _importePercibido;
        private String _monedaImportePercibido;
        private String _fechaPercepcion;
        private double? _importeTotalCobrado;
        private String _monedaImporteTotalCobrado;

        public double? ImportePercibido
        {
            get { return _importePercibido; }
            set { _importePercibido = value; }
        }

        public string MonedaImportePercibido
        {
            get { return _monedaImportePercibido; }
            set { _monedaImportePercibido = value; }
        }

        public String FechaPercepcion
        {
            get { return _fechaPercepcion; }
            set { _fechaPercepcion = value; }
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
    }
}
