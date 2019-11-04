using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class MonedaInfo
    {
        private String _idMoneda;
        private String _descripcion;
        private String _codigoIso;

        public string IdMoneda
        {
            get { return _idMoneda; }
            set { _idMoneda = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public string CodigoIso
        {
            get { return _codigoIso; }
            set { _codigoIso = value; }
        }
    }
}
