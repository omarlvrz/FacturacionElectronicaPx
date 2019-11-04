using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class PuntoVentaInfo
    {
        private String _idCliente;
        private String _idPuntoVenta;
        private String _nombre;
        private String _direccion;
        private String _idDepartamento;
        private String _idProvincia;
        private String _idDistrito;
        private String _ubigeo;

        /// <summary>
        /// Contructor de la Entidad PuntoVentaInfo
        /// </summary>
        public PuntoVentaInfo()
        {
        }

        /// <summary>
        /// Contructor de la Entidad PuntoVentaInfo
        /// </summary>
        /// <param name="sIdCliente">Id del Cliente del Punto de Venta</param>
        /// <param name="sIdPuntoVenta">Id del Punto de Venta</param>
        /// <param name="sNombre">Nombre del Punto de Venta</param>
        /// <param name="sDireccion">Dirección del Punto de Venta</param>
        /// <param name="sIdDepartamento">Id del Departamento del Punto de Venta</param>
        /// <param name="sIdProvincia">Id de la Provincia del Punto de Venta</param>
        /// <param name="sIdDistrito">Id del Distrito del Punto de Venta</param>
        /// <param name="sUbigeo">Ubigeo del Punto de Venta</param>
        public PuntoVentaInfo(String sIdCliente, String sIdPuntoVenta, String sNombre, String sDireccion,
                              String sIdDepartamento, String sIdProvincia, String sIdDistrito, String sUbigeo)
        {
            _idCliente = sIdCliente;
            _idPuntoVenta = sIdPuntoVenta;
            _nombre = sNombre;
            _direccion = sDireccion;
            _idDepartamento = sIdDepartamento;
            _idProvincia = sIdProvincia;
            _idDistrito = sIdDistrito;
            _ubigeo = sUbigeo;
        }

        /// <summary>
        /// Id del Cliente del Punto de Venta
        /// </summary>
        public string IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        /// <summary>
        /// Id del Punto de Venta
        /// </summary>
        public string IdPuntoVenta
        {
            get { return _idPuntoVenta; }
            set { _idPuntoVenta = value; }
        }

        /// <summary>
        /// Nombre del Punto de Venta
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        /// <summary>
        /// Dirección del Punto de Venta
        /// </summary>
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        /// <summary>
        /// Id del Departamento del Punto de Venta
        /// </summary>
        public string IdDepartamento
        {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }

        /// <summary>
        /// Id de la Provincia del Punto de Venta
        /// </summary>
        public string IdProvincia
        {
            get { return _idProvincia; }
            set { _idProvincia = value; }
        }

        /// <summary>
        /// Id del Distrito del Punto de Venta
        /// </summary>
        public string IdDistrito
        {
            get { return _idDistrito; }
            set { _idDistrito = value; }
        }

        /// <summary>
        /// Ubigeo del Punto de Venta
        /// </summary>
        public string Ubigeo
        {
            get { return _ubigeo; }
            set { _ubigeo = value; }
        }
    }
}
