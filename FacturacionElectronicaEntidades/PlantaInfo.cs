using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class PlantaInfo
    {
        private String _idPlanta;
        private String _descripcion;
        private String _idEstado;
        private String _idTipoFacturacion;
        private String _direccion;
        private String _idDepartamento;
        private String _idProvincia;
        private String _idDistrito;
        private String _ubigeo;
        private String _flagFepe;

        /// <summary>
        /// Contructor de la Entidad PlantaInfo
        /// </summary>
        public PlantaInfo()
        {
        }

        /// <summary>
        /// Contructor de la Entidad PlantaInfo
        /// </summary>
        /// <param name="sIdPlanta">Código de la Planta</param>
        public PlantaInfo(String sIdPlanta)
        {
            _idPlanta = sIdPlanta;
        }

        /// <summary>
        /// Contructor de la Entidad PlantaInfo
        /// </summary>
        /// <param name="sIdPlanta">Código de la Planta</param>
        /// <param name="sDescripcion">Descripción de la Planta</param>
        /// <param name="sIdEstado">Estado de la Planta</param>
        /// <param name="sIdTipoFacturacion">Tipo de Facturación por defecto de la Planta</param>
        /// <param name="sDireccion">Dirección de la Planta</param>
        /// <param name="sIdDepartamento">Departamento de la Planta</param>
        /// <param name="sIdProvincia">Provincia de la Planta</param>
        /// <param name="sIdDistrito">Distrito de la Planta</param>
        /// <param name="sUbigeo">Ubigeo de la Planta</param>
        public PlantaInfo(String sIdPlanta, String sDescripcion, String sIdEstado, String sIdTipoFacturacion, String sDireccion, String sIdDepartamento, String sIdProvincia, String sIdDistrito, String sUbigeo)
        {
            _idPlanta = sIdPlanta;
            _descripcion = sDescripcion;
            _idEstado = sIdEstado;
            _idTipoFacturacion = sIdTipoFacturacion;
            _direccion = sDireccion;
            _idDepartamento = sIdDepartamento;
            _idProvincia = sIdProvincia;
            _idDistrito = sIdDistrito;
            _ubigeo = sUbigeo;
        }

        /// <summary>
        /// Código de la Planta
        /// </summary>
        public string IdPlanta
        {
            get { return _idPlanta; }
            set { _idPlanta = value; }
        }

        /// <summary>
        /// Descripción de la Planta
        /// </summary>
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        /// <summary>
        /// Estado de la Planta
        /// </summary>
        public string IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }

        /// <summary>
        /// Tipo de Facturación por defecto de la Planta
        /// </summary>
        public string IdTipoFacturacion
        {
            get { return _idTipoFacturacion; }
            set { _idTipoFacturacion = value; }
        }

        /// <summary>
        ///  Dirección de la Planta
        /// </summary>
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        /// <summary>
        /// Departamento de la Planta
        /// </summary>
        public string IdDepartamento
        {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }

        /// <summary>
        /// Provincia de la Planta
        /// </summary>
        public string IdProvincia
        {
            get { return _idProvincia; }
            set { _idProvincia = value; }
        }

        /// <summary>
        /// Distrito de la Planta
        /// </summary>
        public string IdDistrito
        {
            get { return _idDistrito; }
            set { _idDistrito = value; }
        }

        /// <summary>
        /// Ubigeo de la Planta
        /// </summary>
        public string Ubigeo
        {
            get { return _ubigeo; }
            set { _ubigeo = value; }
        }

        public string FlagFepe
        {
            get { return _flagFepe; }
            set { _flagFepe = value; }
        }
    }
}
