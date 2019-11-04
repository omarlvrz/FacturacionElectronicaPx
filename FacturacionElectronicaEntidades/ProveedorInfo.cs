using System;
using System.Collections.Generic;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class ProveedorInfo
    {
        private string _razonSocial;
        private string _idTipoDoc;
        private string _nroDocumentoIdentidad;
        private string _direccion;
        private string _departamento;
        private string _provincia;
        private string _distrito;
        private string _ubigeo;

        public string RazonSocial
        {
            get { return this._razonSocial; }
            set { this._razonSocial = value; }
        }

        public string IdTipoDoc
        {
            get { return this._idTipoDoc; }
            set { this._idTipoDoc = value; }
        }

        public string NroDocumentoIdentidad
        {
            get { return this._nroDocumentoIdentidad; }
            set { this._nroDocumentoIdentidad = value; }
        }

        public string Direccion
        {
            get { return this._direccion; }
            set { this._direccion = value; }
        }

        public string Departamento
        {
            get { return this._departamento; }
            set { this._departamento = value; }
        }

        public string Provincia
        {
            get { return this._provincia; }
            set { this._provincia = value; }
        }

        public string Distrito
        {
            get { return this._distrito; }
            set { this._distrito = value; }
        }

        public string Ubigeo
        {
            get { return this._ubigeo; }
            set { this._ubigeo = value; }
        }
    }
}
