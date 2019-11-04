using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class NotaAjusteDetInfo
    {
        private String _serieNota;
        private String _nroNota;
        private String _idPlanta;
        private String _idTipoNota;
        private String _idArticulo;
        private ArticuloInfo _articulo;
        private Int32? _item;
        private Double? _cantidad;
        private Double? _subTotal;
        private Double? _igv;
        private Double? _total;
        private Double? _precioUnitario;
        private Double? _valorUnitario;

        public NotaAjusteDetInfo()
        {
        }

        public NotaAjusteDetInfo(String sSerieNota, String sNroNota, String sIdPlanta, String sIdTipoNota)
        {
            _serieNota = sSerieNota;
            _nroNota = sNroNota;
            _idPlanta = sIdPlanta;
            _idTipoNota = sIdTipoNota;
        }

        public NotaAjusteDetInfo(String sSerieNota, String sNroNota, String sIdPlanta, String sIdTipoNota,
                                 String sIdArticulo, ArticuloInfo oArticulo, Int32? nItem, Double? nCantidad,
                                 Double? nSubTotal, Double? nIgv, Double? nTotal)
        {
            _serieNota = sSerieNota;
            _nroNota = sNroNota;
            _idPlanta = sIdPlanta;
            _idTipoNota = sIdTipoNota;
            _idArticulo = sIdArticulo;
            _articulo = oArticulo;
            _item = nItem;
            _cantidad = nCantidad;
            _subTotal = nSubTotal;
            _igv = nIgv;
            _total = nTotal;
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

        public string IdTipoNota
        {
            get { return _idTipoNota; }
            set { _idTipoNota = value; }
        }

        public string IdArticulo
        {
            get { return _idArticulo; }
            set { _idArticulo = value; }
        }

        public ArticuloInfo Articulo
        {
            get { return _articulo; }
            set { _articulo = value; }
        }

        public int? Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public double? Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
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

        /// <summary>
        /// Precio Unitario con IGV
        /// </summary>
        public double? PrecioUnitario
        {
            get { return _precioUnitario; }
            set { _precioUnitario = value; }
        }

        /// <summary>
        /// Precio Unitario sin IGV
        /// </summary>
        public double? ValorUnitario
        {
            get { return _valorUnitario; }
            set { _valorUnitario = value; }
        }
    }
}
