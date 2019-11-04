using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class TipoFacturacionInfo
    {
        private String _idTipoFacturacion;
        private String _descripcion;

        public TipoFacturacionInfo() { }

        /// <summary>
        /// Constructor TipoFacturacion
        /// </summary>
        /// <param name="sIdTipoFacturacion">Código del Tipo de Facturación</param>
        /// <param name="sDescripcion">Descripción del Tipo de Facturación</param>
        public TipoFacturacionInfo(String sIdTipoFacturacion, String sDescripcion)
        {
            _idTipoFacturacion = sIdTipoFacturacion;
            _descripcion = sDescripcion;
        }

        /// <summary>
        /// Código del Tipo de Facturación
        /// </summary>
        public String IdTipoFacturacion
        {
            get { return _idTipoFacturacion; }
            set { _idTipoFacturacion = value; }
        }

        /// <summary>
        /// Descripción del Tipo de Facturación
        /// </summary>
        public String Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
    }
}
