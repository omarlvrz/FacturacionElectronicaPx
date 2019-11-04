using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class DocumentoPrecintosInfo
    {
        private String _serie;
        private String _nroDocumento;
        private String _idPlanta;
        private String _idTipoFacturacion;
        private String _idTipoDoc;
        private String _nroPrecinto;

        public DocumentoPrecintosInfo()
        {
        }

        public DocumentoPrecintosInfo(String sSerie, String sNroDocumento, String sIdPlanta, String sIdTipoFacturacion,
                                  String sIdTipoDoc, String sNroPrecinto)
        {
            _serie = sSerie;
            _nroDocumento = sNroDocumento;
            _idPlanta = sIdPlanta;
            _idTipoFacturacion = sIdTipoFacturacion;
            _idTipoDoc = sIdTipoDoc;
            _nroPrecinto = sNroPrecinto;
        }

        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }

        public string NroDocumento
        {
            get { return _nroDocumento; }
            set { _nroDocumento = value; }
        }

        public string IdPlanta
        {
            get { return _idPlanta; }
            set { _idPlanta = value; }
        }

        public string IdTipoFacturacion
        {
            get { return _idTipoFacturacion; }
            set { _idTipoFacturacion = value; }
        }

        public string IdTipoDoc
        {
            get { return _idTipoDoc; }
            set { _idTipoDoc = value; }
        }

        public string NroPrecinto
        {
            get { return _nroPrecinto; }
            set { _nroPrecinto = value; }
        }
    }
}
