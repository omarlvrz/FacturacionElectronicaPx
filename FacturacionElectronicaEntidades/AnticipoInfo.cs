using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class AnticipoInfo
    {
        private String _idTipoDocAnticipo;
        private String _nroDocAnticipo;
        private Double? _montoSumaAnticipos;
        private String _idTipoDocClienteAnticipo;
        private String _nroDocClienteAnticipo;

        public string IdTipoDocAnticipo
        {
            get { return _idTipoDocAnticipo; }
            set { _idTipoDocAnticipo = value; }
        }

        public string NroDocAnticipo
        {
            get { return _nroDocAnticipo; }
            set { _nroDocAnticipo = value; }
        }

        public double? MontoSumaAnticipos
        {
            get { return _montoSumaAnticipos; }
            set { _montoSumaAnticipos = value; }
        }

        public string IdTipoDocClienteAnticipo
        {
            get { return _idTipoDocClienteAnticipo; }
            set { _idTipoDocClienteAnticipo = value; }
        }

        public string NroDocClienteAnticipo
        {
            get { return _nroDocClienteAnticipo; }
            set { _nroDocClienteAnticipo = value; }
        }
    }
}
