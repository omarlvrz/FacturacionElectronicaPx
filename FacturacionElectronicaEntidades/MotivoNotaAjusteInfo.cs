using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class MotivoNotaAjusteInfo
    {
        private String _idTipoNota;
        private String _idMotivoNota;
        private String _descripcion;
        private String _idMotivoSunat;

        public MotivoNotaAjusteInfo()
        {
        }

        public MotivoNotaAjusteInfo(String sIdTipoNota, String sIdMotivoNota, String sDescripcion, String sIdMotivoSunat)
        {
            _idTipoNota = sIdTipoNota;
            _idMotivoNota = sIdMotivoNota;
            _descripcion = sDescripcion;
            _idMotivoSunat = sIdMotivoSunat;
        }

        public string IdTipoNota
        {
            get { return _idTipoNota; }
            set { _idTipoNota = value; }
        }

        public string IdMotivoNota
        {
            get { return _idMotivoNota; }
            set { _idMotivoNota = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public string IdMotivoSunat
        {
            get { return _idMotivoSunat; }
            set { _idMotivoSunat = value; }
        }
    }
}
