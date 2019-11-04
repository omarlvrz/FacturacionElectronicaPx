using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class DetallePercepcionInfo
    {
        private ComprobanteRelacionadoInfo _comprobanteRelacionado;
        private CobroPercepcionInfo _cobroPercepcion;
        private DatosPercepcionInfo _datosPercepcion;
        private TipoCambioInfo _tipoCambio;

        public ComprobanteRelacionadoInfo ComprobanteRelacionado
        {
            get { return _comprobanteRelacionado; }
            set { _comprobanteRelacionado = value; }
        }

        public CobroPercepcionInfo CobroPercepcion
        {
            get { return _cobroPercepcion; }
            set { _cobroPercepcion = value; }
        }

        public DatosPercepcionInfo DatosPercepcion
        {
            get { return _datosPercepcion; }
            set { _datosPercepcion = value; }
        }

        public TipoCambioInfo TipoCambio
        {
            get { return _tipoCambio; }
            set { _tipoCambio = value; }
        }
    }
}
