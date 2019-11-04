using System;
using System.Collections.Generic;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class NotaAjusteInfo
    {
        private String _serieNota;
        private String _nroNota;
        private String _idPlanta;
        private PlantaInfo _planta;
        private String _idTipoNota;
        private String _idTipoFacturacion;
        private String _serieRef;
        private String _nroDocRef;
        private String _idTipoDocRef;
        private String _idCliente;
        private ClienteInfo _cliente;
        private String _idPuntoVenta;
        private PuntoVentaInfo _puntoVenta;
        private String _idMotivoNota;
        private MotivoNotaAjusteInfo _motivoNotaAjuste;
        private DateTime? _fechaNota;
        private DateTime? _fechaVencimiento;
        private String _idMoneda;
        private Double? _subTotal;
        private Double? _igv;
        private Double? _total;
        private String _hashFe;
        private String _idEstado;
        private List<NotaAjusteDetInfo> _detalle;

        public NotaAjusteInfo()
        {
        }

        public NotaAjusteInfo(String sSerieNota, String sNroNota, String sIdPlanta, String sIdTipoFacturacion,
                              String sIdTipoNota, DateTime? dFechaNota, String sHashFe)
        {
            _serieNota = sSerieNota;
            _nroNota = sNroNota;
            _idPlanta = sIdPlanta;
            _idTipoFacturacion = sIdTipoFacturacion;
            _idTipoNota = sIdTipoNota;
            _fechaNota = dFechaNota;
            _hashFe = sHashFe;
        }

        public NotaAjusteInfo(String sSerieNota, String sNroNota, String sIdPlanta, PlantaInfo oPlanta,
                              String sIdTipoFacturacion, String sIdTipoNota, String sSerieRef, String sNroDocRef,
                              String sIdTipoDocRef, String sIdCliente, ClienteInfo oCliente, String sIdPuntoVenta, PuntoVentaInfo oPuntoVenta,
                              String sIdMotivoNota, MotivoNotaAjusteInfo oMotivoNotaAjuste, DateTime? dFechaNota, DateTime? dFechaVencimiento,
                              String sIdMoneda, Double? nSubTotal, Double? nIgv, Double? nTotal, String sHashFe, String sIdEstado)
        {
            _serieNota = sSerieNota;
            _nroNota = sNroNota;
            _idPlanta = sIdPlanta;
            _planta = oPlanta;
            _idTipoFacturacion = sIdTipoFacturacion;
            _idTipoNota = sIdTipoNota;
            _serieRef = sSerieRef;
            _nroDocRef = sNroDocRef;
            _idTipoDocRef = sIdTipoDocRef;
            _idCliente = sIdCliente;
            _cliente = oCliente;
            _idPuntoVenta = sIdPuntoVenta;
            _puntoVenta = oPuntoVenta;
            _idMotivoNota = sIdMotivoNota;
            _motivoNotaAjuste = oMotivoNotaAjuste;
            _fechaNota = dFechaNota;
            _fechaVencimiento = dFechaVencimiento;
            _idMoneda = sIdMoneda;
            _subTotal = nSubTotal;
            _igv = nIgv;
            _total = nTotal;
            _hashFe = sHashFe;
            _idEstado = sIdEstado;
        }

        public string SerieNota
        {
            get { return _serieNota; }
            set { _serieNota = value; }
        }

        public string NroNota
        {
            get { return _nroNota; }
            set { _nroNota = value; }
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

        public string IdTipoNota
        {
            get { return _idTipoNota; }
            set { _idTipoNota = value; }
        }

        public string IdTipoFacturacion
        {
            get { return _idTipoFacturacion; }
            set { _idTipoFacturacion = value; }
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

        public DateTime? FechaNota
        {
            get { return _fechaNota; }
            set { _fechaNota = value; }
        }

        public DateTime? FechaVencimiento
        {
            get { return _fechaVencimiento; }
            set { _fechaVencimiento = value; }
        }

        public string IdMoneda
        {
            get { return _idMoneda; }
            set { _idMoneda = value; }
        }

        public double? SubTotal
        {
            get { return _subTotal; }
            set { _subTotal = value; }
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

        public string HashFe
        {
            get { return _hashFe; }
            set { _hashFe = value; }
        }

        public List<NotaAjusteDetInfo> Detalle
        {
            get { return _detalle; }
            set { _detalle = value; }
        }

        public string IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }
    }
}
