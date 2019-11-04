using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class VehiculoInfo
    {
        private String _placaVehiculo;
        private String _placaTanque;
        private String _mtc;
        private String _marca;

        /// <summary>
        /// Constructor de la Entidad VehiculoInfo
        /// </summary>
        public VehiculoInfo()
        {
        }

        /// <summary>
        /// Constructor de la Entidad VehiculoInfo
        /// </summary>
        /// <param name="sPlacaVehiculo">Placa del Vehículo</param>
        /// <param name="sPlacaTanque">Placa del Tanque (Cisterna) del Vehículo</param>
        /// <param name="sMtc">Certificado MTC del Vehículo</param>
        public VehiculoInfo(String sPlacaVehiculo, String sPlacaTanque, String sMtc)
        {
            _placaVehiculo = sPlacaVehiculo;
            _placaTanque = sPlacaTanque;
            _mtc = sMtc;
        }

        /// <summary>
        /// Placa del Vehículo
        /// </summary>
        public string PlacaVehiculo
        {
            get { return _placaVehiculo; }
            set { _placaVehiculo = value; }
        }

        /// <summary>
        /// Placa del Tanque (Cisterna) del Vehículo
        /// </summary>
        public string PlacaTanque
        {
            get { return _placaTanque; }
            set { _placaTanque = value; }
        }

        /// <summary>
        /// Certificado MTC del Vehículo
        /// </summary>
        public string Mtc
        {
            get { return _mtc; }
            set { _mtc = value; }
        }

        public string Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }
    }
}
