using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class ReceptorTerceroInfo
    {
        private String _razonSocial;
        private String _idTipoDoc;
        private String _nroDocumentoIdentidad;

        public string RazonSocial { get => _razonSocial; set => _razonSocial = value; }
        public string IdTipoDoc { get => _idTipoDoc; set => _idTipoDoc = value; }
        public string NroDocumentoIdentidad { get => _nroDocumentoIdentidad; set => _nroDocumentoIdentidad = value; }
    }
}
