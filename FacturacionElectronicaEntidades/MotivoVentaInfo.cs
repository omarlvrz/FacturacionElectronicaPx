using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class MotivoVentaInfo
    {
        private String _idMotivoVenta;
        private String _descripcion;
        private String _gratuito;

        public MotivoVentaInfo()
        {
        }

        public MotivoVentaInfo(String sIdMotivoVenta, String sDescripcion, String sGratuito)
        {
            _idMotivoVenta = sIdMotivoVenta;
            _descripcion = sDescripcion;
            _gratuito = sGratuito;
        }

        public string IdMotivoVenta
        {
            get { return _idMotivoVenta; }
            set { _idMotivoVenta = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public string Gratuito
        {
            get { return _gratuito; }
            set { _gratuito = value; }
        }
    }
}
