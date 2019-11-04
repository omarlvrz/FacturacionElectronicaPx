using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class EstadoInfo
    {
        private String _idEstado;
        private String _descripcion;

        public string IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
    }
}
