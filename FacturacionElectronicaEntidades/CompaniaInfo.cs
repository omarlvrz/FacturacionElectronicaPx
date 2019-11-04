using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class CompaniaInfo
    {
        private String _conjunto;
        private String _nombre;
        private String _ruc;
        private String _direccion;
        private String _pais;
        private String _ubigeo;
        private string _departamento;
        private string _provincia;
        private string _distrito;

        public CompaniaInfo() { }

        /// <summary>
        /// Contructor Compañía
        /// </summary>
        /// <param name="sConjunto">Código de la Compañía</param>
        public CompaniaInfo(String sConjunto)
        {
            _conjunto = sConjunto;
        }

        /// <summary>
        /// Constructor Compañía
        /// </summary>
        /// <param name="sConjunto">Código de la Compañía</param>
        /// <param name="sNombre">Nombre de la Compañía</param>
        /// <param name="sRuc">RUC de la Compañía</param>
        /// <param name="sDireccion">Dirección de la Compañía</param>
        /// <param name="sPais">Código de País de la Compañía</param>
        /// <param name="sUbigeo">Ubigeo de la Compañía</param>
        public CompaniaInfo(String sConjunto, String sNombre, String sRuc, String sDireccion, String sPais, String sUbigeo)
        {
            _conjunto = sConjunto;
            _nombre = sNombre;
            _ruc = sRuc;
            _direccion = sDireccion;
            _pais = sPais;
            _ubigeo = sUbigeo;
        }

        /// <summary>
        /// Código de la Compañía
        /// </summary>
        public String Conjunto { get { return _conjunto; } set { _conjunto = value; } }
        /// <summary>
        /// Nombre de la Compañía
        /// </summary>
        public String Nombre { get { return _nombre; } set { _nombre = value; } }

        /// <summary>
        /// Ruc de la Compañía
        /// </summary>
        public string Ruc
        {
            get { return _ruc; }
            set { _ruc = value; }
        }

        /// <summary>
        /// Dirección de la Compañía
        /// </summary>
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        /// <summary>
        /// Código de País de la Compañía
        /// </summary>
        public string Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }

        /// <summary>
        /// Ubigeo de la Compañía
        /// </summary>
        public string Ubigeo
        {
            get { return _ubigeo; }
            set { _ubigeo = value; }
        }

        public string Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }

        public string Provincia
        {
            get { return _provincia; }
            set { _provincia = value; }
        }

        public string Distrito
        {
            get { return _distrito; }
            set { _distrito = value; }
        }
    }
}
