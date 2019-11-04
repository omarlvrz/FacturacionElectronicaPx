using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class MotivoServicioInfo
    {
        private String _idMotivoServicio;
        private String _descripcion;
        private String _detraccion;

        /// <summary>
        /// Constructor de Entidad MotivoServicioInfo
        /// </summary>
        public MotivoServicioInfo()
        {
        }

        /// <summary>
        /// Constructor de Entidad MotivoServicioInfo
        /// </summary>
        /// <param name="sIdMotivoServicio">Id del Motivo de Servicio</param>
        /// <param name="sDescripcion">Descripción del Motivo de Servicio</param>
        /// <param name="sDetraccion">Flag de Detracción del Motivo de Servicio</param>
        public MotivoServicioInfo(String sIdMotivoServicio, String sDescripcion, String sDetraccion)
        {
            _idMotivoServicio = sIdMotivoServicio;
            _descripcion = sDescripcion;
            _detraccion = sDetraccion;
        }

        /// <summary>
        /// Id del Motivo de Servicio
        /// </summary>
        public string IdMotivoServicio
        {
            get { return _idMotivoServicio; }
            set { _idMotivoServicio = value; }
        }

        /// <summary>
        /// Descripción del Motivo de Servicio
        /// </summary>
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        /// <summary>
        /// Flag de Detracción del Motivo de Servicio
        /// </summary>
        public string Detraccion
        {
            get { return _detraccion; }
            set { _detraccion = value; }
        }
    }
}
