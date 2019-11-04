using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class ArticuloInfo
    {
        private String _idArticulo;
        private String _descripcion;
        private String _unidadMedida;
        private String _codigoCubso;
        private String _codigoGs1;
        private String _codigoGtin;

        /// <summary>
        /// Constructor de Entidad ArticuloInfo
        /// </summary>
        public ArticuloInfo()
        {
        }

        /// <summary>
        /// Constructor de Entidad ArticuloInfo
        /// </summary>
        /// <param name="sIdArticulo">Id del Artículo</param>
        /// <param name="sDescripcion">Descripción del Artículo</param>
        /// <param name="sUnidadMedida">Unidad de Medida del Artículo</param>
        public ArticuloInfo(String sIdArticulo, String sDescripcion, String sUnidadMedida)
        {
            _idArticulo = sIdArticulo;
            _descripcion = sDescripcion;
            _unidadMedida = sUnidadMedida;
        }

        /// <summary>
        /// Id del Artículo
        /// </summary>
        public string IdArticulo
        {
            get { return _idArticulo; }
            set { _idArticulo = value; }
        }

        /// <summary>
        /// Descripción del Artículo
        /// </summary>
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        /// <summary>
        /// Unidad de Medida del Artículo
        /// </summary>
        public string UnidadMedida
        {
            get { return _unidadMedida; }
            set { _unidadMedida = value; }
        }

        /// <summary>
        /// Código de Producio SUNAT (CUBSO)
        /// </summary>
        public string CodigoCubso
        {
            get { return _codigoCubso; }
            set { _codigoCubso = value; }
        }

        /// <summary>
        /// Código de producto GS1
        /// </summary>
        public string CodigoGs1
        {
            get { return _codigoGs1; }
            set { _codigoGs1 = value; }
        }

        /// <summary>
        /// Código de producto GTIN
        /// </summary>
        public string CodigoGtin
        {
            get { return _codigoGtin; }
            set { _codigoGtin = value; }
        }
    }
}
