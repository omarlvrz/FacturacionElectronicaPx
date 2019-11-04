using System;

namespace FacturacionElectronicaEntidades
{
    [Serializable]
    public class DocumentoCpInfo
    {
        private String _idProveedor;
        private String _idTipoDoc;
        private String _idSubtipoDoc;
        private String _nroDocumento;
        private DateTime? _fechaDocumento;
        private Double? _subtotal;
        private Double? _impuesto;
        private Double? _inafecto;
        private Double? _total;
        private String _idMoneda;
        private String _glosa;
        private String _idTipoProveedor;
        private String _idCliente;
        private String _centroSap;
        private String _ordenCompra;
        private String _documentoReferencia;

        public string IdProveedor
        {
            get { return _idProveedor; }
            set { _idProveedor = value; }
        }

        public string IdTipoDoc
        {
            get { return _idTipoDoc; }
            set { _idTipoDoc = value; }
        }

        public String IdSubtipoDoc
        {
            get { return _idSubtipoDoc; }
            set { _idSubtipoDoc = value; }
        }

        public string NroDocumento
        {
            get { return _nroDocumento; }
            set { _nroDocumento = value; }
        }

        public DateTime? FechaDocumento
        {
            get { return _fechaDocumento; }
            set { _fechaDocumento = value; }
        }

        public double? Subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; }
        }

        public double? Impuesto
        {
            get { return _impuesto; }
            set { _impuesto = value; }
        }

        public double? Inafecto
        {
            get { return _inafecto; }
            set { _inafecto = value; }
        }

        public double? Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public string IdMoneda
        {
            get { return _idMoneda; }
            set { _idMoneda = value; }
        }

        public string Glosa
        {
            get { return _glosa; }
            set { _glosa = value; }
        }

        public string IdTipoProveedor
        {
            get { return _idTipoProveedor; }
            set { _idTipoProveedor = value; }
        }

        public string IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        public string CentroSap
        {
            get { return _centroSap; }
            set { _centroSap = value; }
        }

        public string OrdenCompra
        {
            get { return _ordenCompra; }
            set { _ordenCompra = value; }
        }

        public string DocumentoReferencia
        {
            get { return _documentoReferencia; }
            set { _documentoReferencia = value; }
        }
    }
}
