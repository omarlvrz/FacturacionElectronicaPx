using System;
using System.Collections.Generic;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class DocumentoSapInfo21
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

        private String _codDetraccion;
        private Double? _porcDetraccion;
        private Double? _montoCalculoDtr;

        private String _serieGuiaRemision;
        private String _nroGuiaRemision;
        private DateTime? _fechaGuiaRemision;
        private String _ordenCompra;
        private String _nroPedido;
        private Double? _montoGratuito;
        private Double? _tipoCambio;

        //Campos para Nota de Ajuste
        private String _serieRef;
        private String _nroDocRef;
        private String _idTipoDocRef;
        private String _idMotivoNota;
        private MotivoNotaAjusteInfo _motivoNotaAjuste;

        private Double? _montoFise;
        private Double? _montoSise;

        private String _flagAnticipo;
        private AnticipoInfo _anticipo;

        private String _telefonoCsc1;
        private String _telefonoCsc2;
        private String _telefonoCsc3;
        private String _instruccionEntrega;
        private String _codigoPago;
        private String _condicionPago;

        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }

        public string NroDocumento
        {
            get { return _nroDocumento; }
            set { _nroDocumento = value; }
        }

        public string IdPlanta
        {
            get { return _idPlanta; }
            set { _idPlanta = value; }
        }

        public PlantaInfo Planta
        {
            get { return _planta; }
            set { _planta = value; }
        }

        public string IdTipoFacturacion
        {
            get { return _idTipoFacturacion; }
            set { _idTipoFacturacion = value; }
        }

        public string IdTipoDoc
        {
            get { return _idTipoDoc; }
            set { _idTipoDoc = value; }
        }

        public DateTime? FechaDocumento
        {
            get { return _fechaDocumento; }
            set { _fechaDocumento = value; }
        }

        public DateTime? FechaVencimiento
        {
            get { return _fechaVencimiento; }
            set { _fechaVencimiento = value; }
        }

        public string IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        public ClienteInfo Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }

        public string IdPuntoVenta
        {
            get { return _idPuntoVenta; }
            set { _idPuntoVenta = value; }
        }

        public PuntoVentaInfo PuntoVenta
        {
            get { return _puntoVenta; }
            set { _puntoVenta = value; }
        }

        public string IdMotivoVenta
        {
            get { return _idMotivoVenta; }
            set { _idMotivoVenta = value; }
        }

        public MotivoVentaInfo MotivoVenta
        {
            get { return _motivoVenta; }
            set { _motivoVenta = value; }
        }

        public string IdMotivoServicio
        {
            get { return _idMotivoServicio; }
            set { _idMotivoServicio = value; }
        }

        public MotivoServicioInfo MotivoServicio
        {
            get { return _motivoServicio; }
            set { _motivoServicio = value; }
        }

        public string IdMoneda
        {
            get { return _idMoneda; }
            set { _idMoneda = value; }
        }

        public string IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }

        public double? Subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; }
        }

        public double? Descuento
        {
            get { return _descuento; }
            set { _descuento = value; }
        }

        public double? Flete
        {
            get { return _flete; }
            set { _flete = value; }
        }

        public double? Igv
        {
            get { return _igv; }
            set { _igv = value; }
        }

        public double? Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public double? Percepcion
        {
            get { return _percepcion; }
            set { _percepcion = value; }
        }

        public double? PorcPercepcion
        {
            get { return _porcPercepcion; }
            set { _porcPercepcion = value; }
        }

        public string PlacaVehiculo
        {
            get { return _placaVehiculo; }
            set { _placaVehiculo = value; }
        }

        public string PlacaTanque
        {
            get { return _placaTanque; }
            set { _placaTanque = value; }
        }

        public VehiculoInfo Vehiculo
        {
            get { return _vehiculo; }
            set { _vehiculo = value; }
        }

        public string HashFe
        {
            get { return _hashFe; }
            set { _hashFe = value; }
        }

        public string IdEmpresaTransporte
        {
            get { return _idEmpresaTransporte; }
            set { _idEmpresaTransporte = value; }
        }

        public string Scop
        {
            get { return _scop; }
            set { _scop = value; }
        }

        public double? Isc
        {
            get { return _isc; }
            set { _isc = value; }
        }

        public List<DocumentoDetInfo> Detalle
        {
            get { return _detalle; }
            set { _detalle = value; }
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

        public string SerieRef
        {
            get { return _serieRef; }
            set { _serieRef = value; }
        }

        public string NroDocRef
        {
            get { return _nroDocRef; }
            set { _nroDocRef = value; }
        }

        public string IdTipoDocRef
        {
            get { return _idTipoDocRef; }
            set { _idTipoDocRef = value; }
        }

        public string IdMotivoNota
        {
            get { return _idMotivoNota; }
            set { _idMotivoNota = value; }
        }

        public MotivoNotaAjusteInfo MotivoNotaAjuste
        {
            get { return _motivoNotaAjuste; }
            set { _motivoNotaAjuste = value; }
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

        public string CodDetraccion
        {
            get { return _codDetraccion; }
            set { _codDetraccion = value; }
        }

        public double? PorcDetraccion
        {
            get { return _porcDetraccion; }
            set { _porcDetraccion = value; }
        }

        public double? MontoCalculoDtr
        {
            get { return _montoCalculoDtr; }
            set { _montoCalculoDtr = value; }
        }

        public string FlagAnticipo
        {
            get { return _flagAnticipo; }
            set { _flagAnticipo = value; }
        }

        public AnticipoInfo Anticipo
        {
            get { return _anticipo; }
            set { _anticipo = value; }
        }

        public string TelefonoCsc1 { get => _telefonoCsc1; set => _telefonoCsc1 = value; }
        public string TelefonoCsc2 { get => _telefonoCsc2; set => _telefonoCsc2 = value; }
        public string TelefonoCsc3 { get => _telefonoCsc3; set => _telefonoCsc3 = value; }
        public string InstruccionEntrega { get => _instruccionEntrega; set => _instruccionEntrega = value; }
        public string CodigoPago { get => _codigoPago; set => _codigoPago = value; }
        public string CondicionPago { get => _condicionPago; set => _condicionPago = value; }
    }
}
