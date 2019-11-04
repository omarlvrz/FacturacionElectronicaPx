using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class TipoDocumentoInfo
    {
        private String _idTipoDoc;
        private String _idSubtipoDoc;
        private String _descripcion;
        private String _idTipoSunat;

        public string IdTipoDoc
        {
            get { return _idTipoDoc; }
            set { _idTipoDoc = value; }
        }

        public string IdSubtipoDoc
        {
            get { return _idSubtipoDoc; }
            set { _idSubtipoDoc = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public string IdTipoSunat
        {
            get { return _idTipoSunat; }
            set { _idTipoSunat = value; }
        }
    }
}
