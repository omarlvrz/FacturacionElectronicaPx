using System;
using System.Collections.Generic;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class DocumentoInfo
    {
        private String _serie;
        private String _nroDocumento;
        private String _idPlanta;
        private PlantaInfo _planta;
        private String _idTipoFacturacion;
        private String _idTipoDoc;
        private DateTime? _fechaDocumento;
        private DateTime? _fechaVencimiento;
        private String _idCliente;
        private ClienteInfo _cliente;
        private String _idPuntoVenta;
        private PuntoVentaInfo _puntoVenta;
        private String _idMotivoVenta;
        private MotivoVentaInfo _motivoVenta;
        private String _idMotivoServicio;
        private MotivoServicioInfo _motivoServicio;
        private String _idMoneda;
        private String _idEstado;
        private Double? _subtotal;
        private Double? _descuento;
        private Double? _flete;
        private Double? _igv;
        private Double? _total;
        private Double? _percepcion;
        private Double? _porcPercepcion;
        private String _placaVehiculo;
        private String _placaTanque;
        private VehiculoInfo _vehiculo;
        private String _hashFe;
        private String _idEmpresaTransporte;
        private String _scop;
        private Double? _isc;
        private List<DocumentoDetInfo> _detalle;
        private List<DocumentoPrecintosInfo> _precintos;
        private String _facturaGuia;
        private String _licenciaChofer;
        private String _razonSocialEmpresaTransporte;
        private String _observacion;
        private String _direccionEntrega;
        private String _flagDetraccionServ;
        private Double? _montoDetraccionServ;
        private String _ctaBancoDetraccion;
        private String _serieGuiaRemision;
        private String _nroGuiaRemision;
        private DateTime? _fechaGuiaRemision;
        private String _ordenCompra;
        private String _nroPedido;
        private Double? _montoGratuito;
        private Double? _tipoCambio;
        private Double? _montoFise;
        private Double? _montoSise;


        /// <summary>
        /// Constructor de Entidad DocumentoInfo
        /// </summary>
        public DocumentoInfo()
        {
        }

        /// <summary>
        /// Constructor de Entidad DocumentoInfo
        /// </summary>
        /// <param name="sSerie">Serie del Documento</param>
        /// <param name="sNroDocumento">Número del Documento</param>
        /// <param name="sIdPlanta">Id de la Planta del Documento</param>
        /// <param name="sIdTipoFacturacion">Id del Tipo de Facturación del Documento</param>
        /// <param name="sIdTipoDoc">Id del Tipo de Documento del Documento</param>
        /// <param name="sHashFe">Código Hash de Facturación Electrónica del Documento</param>
        /// <param name="dFechaDocumento">Fecha del Documento</param>
        public DocumentoInfo(String sSerie, String sNroDocumento, String sIdPlanta, String sIdTipoFacturacion, String sIdTipoDoc, String sHashFe, DateTime? dFechaDocumento)
        {
            _serie = sSerie;
            _nroDocumento = sNroDocumento;
            _idPlanta = sIdPlanta;
            _idTipoFacturacion = sIdTipoFacturacion;
            _idTipoDoc = sIdTipoDoc;
            _hashFe = sHashFe;
            _fechaDocumento = dFechaDocumento;
        }

        /// <summary>
        /// Constructor de Entidad DocumentoInfo
        /// </summary>
        /// <param name="sSerie">Serie del Documento</param>
        /// <param name="sNroDocumento">Número del Documento</param>
        /// <param name="sIdPlanta">Id de la Planta del Documento</param>
        /// <param name="oPlanta">Planta del Documento</param>
        /// <param name="sIdTipoFacturacion">Id del Tipo de Facturación del Documento</param>
        /// <param name="sIdTipoDoc">Id del Tipo de Documento del Documento</param>
        /// <param name="dFechaDocumento">Fecha del Documento</param>
        /// <param name="dFechaVencimiento">Fecha de Vencimiento</param>
        /// <param name="sIdCliente">Id del Cliente del Documento</param>
        /// <param name="oCliente">Cliente del Documento</param>
        /// <param name="sIdPuntoVenta">Id del Punto de Venta del Documento</param>
        /// <param name="oPuntoVenta">Punto de Venta del Documento</param>
        /// <param name="sIdMotivoVenta">Id del Motivo de Venta del Documento</param>
        /// <param name="oMotivoVenta">Motivo de Venta del Documento</param>
        /// <param name="sIdMotivoServicio">Id del Motivo de Servicio del Documento</param>
        /// <param name="oMotivoServicio">Motivo de Servicio del Documento</param>
        /// <param name="sIdMoneda">Moneda del Documento</param>
        /// <param name="sIdEstado">Estado del Documento</param>
        /// <param name="nSubTotal">SubTotal del Documento</param>
        /// <param name="nDescuento">Descuento del Documento</param>
        /// <param name="nFlete">Monto por Flete del Documento</param>
        /// <param name="nIgv">Igv del Documento</param>
        /// <param name="nTotal">Total del Documento</param>
        /// <param name="nPercepcion">Percepción del Documento</param>
        /// <param name="nPorcPercepcion">Porcentaje de Percepción del Documento</param>
        /// <param name="sPlacaVehiculo">Placa del Vehículo del Documento</param>
        /// <param name="sPlacaTanque">Placa del Tanque del Documento</param>
        /// <param name="oVehiculo">Vehículo del Documento</param>
        /// <param name="sIdEmpresaTransporte">Código de la Empresa de Transporte</param>
        /// <param name="sScop">Código SCOP del Documento</param>
        /// <param name="nIsc">Impuesto Selectivo al Consumo</param>
        public DocumentoInfo(String sSerie, String sNroDocumento, String sIdPlanta, PlantaInfo oPlanta,
                             String sIdTipoFacturacion, String sIdTipoDoc, DateTime? dFechaDocumento, DateTime? dFechaVencimiento,
                             String sIdCliente, ClienteInfo oCliente, String sIdPuntoVenta, PuntoVentaInfo oPuntoVenta,
                             String sIdMotivoVenta, MotivoVentaInfo oMotivoVenta, String sIdMotivoServicio,
                             MotivoServicioInfo oMotivoServicio, String sIdMoneda, String sIdEstado, Double? nSubTotal,
                             Double? nDescuento, Double? nFlete, Double? nIgv, Double? nTotal, Double? nPercepcion,
                             Double? nPorcPercepcion, String sPlacaVehiculo, String sPlacaTanque, VehiculoInfo oVehiculo, String sIdEmpresaTransporte, String sScop,
                             Double? nIsc)
        {
            _serie = sSerie;
            _nroDocumento = sNroDocumento;
            _idPlanta = sIdPlanta;
            _planta = oPlanta;
            _idTipoFacturacion = sIdTipoFacturacion;
            _idTipoDoc = sIdTipoDoc;
            _fechaDocumento = dFechaDocumento;
            _fechaVencimiento = dFechaVencimiento;
            _idCliente = sIdCliente;
            _cliente = oCliente;
            _idPuntoVenta = sIdPuntoVenta;
            _puntoVenta = oPuntoVenta;
            _idMotivoVenta = sIdMotivoVenta;
            _motivoVenta = oMotivoVenta;
            _idMotivoServicio = sIdMotivoServicio;
            _motivoServicio = oMotivoServicio;
            _idMoneda = sIdMoneda;
            _idEstado = sIdEstado;
            _subtotal = nSubTotal;
            _descuento = nDescuento;
            _flete = nFlete;
            _igv = nIgv;
            _total = nTotal;
            _percepcion = nPercepcion;
            _porcPercepcion = nPorcPercepcion;
            _placaVehiculo = sPlacaVehiculo;
            _placaTanque = sPlacaTanque;
            _vehiculo = oVehiculo;
            _idEmpresaTransporte = sIdEmpresaTransporte;
            _scop = sScop;
            _isc = nIsc;
        }

        /// <summary>
        /// Serie del Documento
        /// </summary>
        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }

        /// <summary>
        /// Número del Documento
        /// </summary>
        public string NroDocumento
        {
            get { return _nroDocumento; }
            set { _nroDocumento = value; }
        }

        /// <summary>
        /// Id de la Planta del Documento
        /// </summary>
        public string IdPlanta
        {
            get { return _idPlanta; }
            set { _idPlanta = value; }
        }

        /// <summary>
        /// Planta del Documento
        /// </summary>
        public PlantaInfo Planta
        {
            get { return _planta; }
            set { _planta = value; }
        }

        /// <summary>
        /// Id del Tipo de Facturación del Documento
        /// </summary>
        public string IdTipoFacturacion
        {
            get { return _idTipoFacturacion; }
            set { _idTipoFacturacion = value; }
        }

        /// <summary>
        /// Id del Tipo de Documento del Documento
        /// </summary>
        public string IdTipoDoc
        {
            get { return _idTipoDoc; }
            set { _idTipoDoc = value; }
        }

        /// <summary>
        /// Fecha del Documento
        /// </summary>
        public DateTime? FechaDocumento
        {
            get { return _fechaDocumento; }
            set { _fechaDocumento = value; }
        }

        /// <summary>
        /// Id del Cliente del Documento
        /// </summary>
        public string IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        /// <summary>
        /// Cliente del Documento
        /// </summary>
        public ClienteInfo Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }

        /// <summary>
        /// Id del Punto de Venta del Documento
        /// </summary>
        public string IdPuntoVenta
        {
            get { return _idPuntoVenta; }
            set { _idPuntoVenta = value; }
        }

        /// <summary>
        /// Punto de Venta del Documento
        /// </summary>
        public PuntoVentaInfo PuntoVenta
        {
            get { return _puntoVenta; }
            set { _puntoVenta = value; }
        }

        /// <summary>
        /// Moneda del Documento
        /// </summary>
        public string IdMoneda
        {
            get { return _idMoneda; }
            set { _idMoneda = value; }
        }

        /// <summary>
        /// Estado del Documento
        /// </summary>
        public string IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }

        /// <summary>
        /// SubTotal del Documento
        /// </summary>
        public double? Subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; }
        }

        /// <summary>
        /// Descuento del Documento
        /// </summary>
        public double? Descuento
        {
            get { return _descuento; }
            set { _descuento = value; }
        }

        /// <summary>
        /// IGV del Documento
        /// </summary>
        public double? Igv
        {
            get { return _igv; }
            set { _igv = value; }
        }

        /// <summary>
        /// Total del Documento
        /// </summary>
        public double? Total
        {
            get { return _total; }
            set { _total = value; }
        }

        /// <summary>
        /// Percepción del Documento
        /// </summary>
        public double? Percepcion
        {
            get { return _percepcion; }
            set { _percepcion = value; }
        }

        /// <summary>
        /// Placa del Vehículo del Documento
        /// </summary>
        public string PlacaVehiculo
        {
            get { return _placaVehiculo; }
            set { _placaVehiculo = value; }
        }

        /// <summary>
        /// Placa del Tanque del Documento
        /// </summary>
        public string PlacaTanque
        {
            get { return _placaTanque; }
            set { _placaTanque = value; }
        }

        /// <summary>
        /// Vehículo del Documento
        /// </summary>
        public VehiculoInfo Vehiculo
        {
            get { return _vehiculo; }
            set { _vehiculo = value; }
        }

        /// <summary>
        /// Código Hash de Facturación Electrónica del Documento
        /// </summary>
        public string HashFe
        {
            get { return _hashFe; }
            set { _hashFe = value; }
        }

        /// <summary>
        /// Detalle del Documento
        /// </summary>
        public List<DocumentoDetInfo> Detalle
        {
            get { return _detalle; }
            set { _detalle = value; }
        }

        /// <summary>
        /// Id del Motivo de Venta del Documento
        /// </summary>
        public string IdMotivoVenta
        {
            get { return _idMotivoVenta; }
            set { _idMotivoVenta = value; }
        }

        /// <summary>
        /// Motivo de Venta del Documento
        /// </summary>
        public MotivoVentaInfo MotivoVenta
        {
            get { return _motivoVenta; }
            set { _motivoVenta = value; }
        }

        /// <summary>
        /// Id del Motivo de Servicio del Documento
        /// </summary>
        public string IdMotivoServicio
        {
            get { return _idMotivoServicio; }
            set { _idMotivoServicio = value; }
        }

        /// <summary>
        /// Motivo de Servicio del Documento
        /// </summary>
        public MotivoServicioInfo MotivoServicio
        {
            get { return _motivoServicio; }
            set { _motivoServicio = value; }
        }

        /// <summary>
        /// Porcentaje de Percepción del Documento
        /// </summary>
        public double? PorcPercepcion
        {
            get { return _porcPercepcion; }
            set { _porcPercepcion = value; }
        }

        /// <summary>
        /// Fecha de Vencimiento
        /// </summary>
        public DateTime? FechaVencimiento
        {
            get { return _fechaVencimiento; }
            set { _fechaVencimiento = value; }
        }

        /// <summary>
        /// Código de la Empresa de Transporte
        /// </summary>
        public string IdEmpresaTransporte
        {
            get { return _idEmpresaTransporte; }
            set { _idEmpresaTransporte = value; }
        }

        /// <summary>
        /// Monto por Flete del Documento
        /// </summary>
        public double? Flete
        {
            get { return _flete; }
            set { _flete = value; }
        }

        /// <summary>
        /// Código SCOP del Documento
        /// </summary>
        public string Scop
        {
            get { return _scop; }
            set { _scop = value; }
        }

        /// <summary>
        /// Impuesto Selectivo al Consumo
        /// </summary>
        public double? Isc
        {
            get { return _isc; }
            set { _isc = value; }
        }

        public List<DocumentoPrecintosInfo> Precintos
        {
            get { return _precintos; }
            set { _precintos = value; }
        }

        public string FacturaGuia
        {
            get { return _facturaGuia; }
            set { _facturaGuia = value; }
        }

        public string LicenciaChofer
        {
            get { return _licenciaChofer; }
            set { _licenciaChofer = value; }
        }

        public string RazonSocialEmpresaTransporte
        {
            get { return _razonSocialEmpresaTransporte; }
            set { _razonSocialEmpresaTransporte = value; }
        }

        public string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }

        public string DireccionEntrega
        {
            get { return _direccionEntrega; }
            set { _direccionEntrega = value; }
        }

        public string FlagDetraccionServ
        {
            get { return _flagDetraccionServ; }
            set { _flagDetraccionServ = value; }
        }

        public double? MontoDetraccionServ
        {
            get { return _montoDetraccionServ; }
            set { _montoDetraccionServ = value; }
        }

        public string CtaBancoDetraccion
        {
            get { return _ctaBancoDetraccion; }
            set { _ctaBancoDetraccion = value; }
        }

        public string SerieGuiaRemision
        {
            get { return _serieGuiaRemision; }
            set { _serieGuiaRemision = value; }
        }

        public string NroGuiaRemision
        {
            get { return _nroGuiaRemision; }
            set { _nroGuiaRemision = value; }
        }

        public DateTime? FechaGuiaRemision
        {
            get { return _fechaGuiaRemision; }
            set { _fechaGuiaRemision = value; }
        }

        public string OrdenCompra
        {
            get { return _ordenCompra; }
            set { _ordenCompra = value; }
        }

        public string NroPedido
        {
            get { return _nroPedido; }
            set { _nroPedido = value; }
        }

        public double? MontoGratuito
        {
            get { return _montoGratuito; }
            set { _montoGratuito = value; }
        }

        public double? TipoCambio
        {
            get { return _tipoCambio; }
            set { _tipoCambio = value; }
        }

        public double? MontoFise
        {
            get { return _montoFise; }
            set { _montoFise = value; }
        }

        public double? MontoSise
        {
            get { return _montoSise; }
            set { _montoSise = value; }
        }


    }
}
