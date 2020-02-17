using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class ClienteInfo
    {
        private String _idCliente;
        private String _razonSocial;
        private String _idTipoDoc;
        private String _nroDocumentoIdentidad;
        private String _direccion;
        private String _idDepartamento;
        private String _idProvincia;
        private String _idDistrito;
        private String _correoElectronico;

        /// <summary>
        /// Constructor de la Entidad ClienteInfo
        /// </summary>
        public ClienteInfo()
        {
        }

        /// <summary>
        /// Constructor de la Entidad ClienteInfo
        /// </summary>
        /// <param name="sIdCliente">Id del Cliente</param>
        /// <param name="sRazonSocial">Razón Social o Nombre del Cliente</param>
        /// <param name="sIdTipoDoc">Id del Tipo de Documento del Cliente</param>
        /// <param name="sNroDocumentoIdentidad">Nro. de Documento de Identidad del Cliente</param>
        /// <param name="sDireccion">Dirección del Cliente</param>
        /// <param name="sIdDepartamento">Id del Departamento del Cliente</param>
        /// <param name="sIdProvincia">Id de la Provincia del Cliente</param>
        /// <param name="sIdDistrito">Id Distrito del Cliente</param>
        public ClienteInfo(String sIdCliente, String sRazonSocial, String sIdTipoDoc, String sNroDocumentoIdentidad,
                           String sDireccion, String sIdDepartamento, String sIdProvincia, String sIdDistrito)
        {
            _idCliente = sIdCliente;
            _razonSocial = sRazonSocial;
            _idTipoDoc = sIdTipoDoc;
            _nroDocumentoIdentidad = sNroDocumentoIdentidad;
            _direccion = sDireccion;
            _idDepartamento = sIdDepartamento;
            _idProvincia = sIdProvincia;
            _idDistrito = sIdDistrito;
        }

        /// <summary>
        /// Id del Cliente
        /// </summary>
        public string IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        /// <summary>
        /// Razón Social o Nombre del Cliente
        /// </summary>
        public string RazonSocial
        {
            get { return _razonSocial; }
            set { _razonSocial = value; }
        }

        /// <summary>
        /// Id del Tipo de Documento del Cliente
        /// </summary>
        public string IdTipoDoc
        {
            get { return _idTipoDoc; }
            set { _idTipoDoc = value; }
        }

        /// <summary>
        /// Nro. de Documento de Identidad del Cliente
        /// </summary>
        public string NroDocumentoIdentidad
        {
            get { return _nroDocumentoIdentidad; }
            set { _nroDocumentoIdentidad = value; }
        }

        /// <summary>
        /// Dirección del Cliente
        /// </summary>
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        /// <summary>
        /// Id del Departamento del Cliente
        /// </summary>
        public string IdDepartamento
        {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }

        /// <summary>
        /// Id de la Provincia del Cliente
        /// </summary>
        public string IdProvincia
        {
            get { return _idProvincia; }
            set { _idProvincia = value; }
        }

        /// <summary>
        /// Id Distrito del Cliente
        /// </summary>
        public string IdDistrito
        {
            get { return _idDistrito; }
            set { _idDistrito = value; }
        }

        public string CorreoElectronico { get => _correoElectronico; set => _correoElectronico = value; }
    }
}
