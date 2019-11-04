using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Web.Services;
using System.Web.Configuration;
using FacturacionElectronicaEntidades;
using FacturacionElectronicaLogicaNegocio;
using Carvajal.FEPE.PreSC.Core;
using Carvajal.FEPE.PreSC.Entities.PlanoStandard.Segments21;
using Common;
using Common.Entities;
using Carvajal.FEPE.PreSC.Entities.PlanoStandard.Documents;
using Carvajal.FEPE.PreSC.Entities.PlanoStandard.Segments;
using FEPE_Factura = Carvajal.FEPE.PreSC.Entities.PlanoStandard.Documents21.FEPE_Factura;
using FEPE_Nota = Carvajal.FEPE.PreSC.Entities.PlanoStandard.Documents21.FEPE_Nota;

namespace wsFacturacionElectronica
{
    /// <summary>
    /// Métodos para la generación de archivos XML de Facturación Electrónica y consulta de Estado.
    /// </summary>
    [WebService(Namespace = "http://pe.com.pecsa.fepe/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsFacturaElectronica21 : System.Web.Services.WebService
    {

        [WebMethod]
        public string GenerarXml21(CompaniaInfo oEmisor, DocumentoSapInfo21 oDocumentoSap)
        {
            var sHash = "";
            if (oDocumentoSap.IdTipoDoc.Equals("01") || oDocumentoSap.IdTipoDoc.Equals("03"))
            {
                sHash = GenerarXmlFacturaBoleta21(oEmisor, oDocumentoSap);
            }
            if (oDocumentoSap.IdTipoDoc.Equals("07") || oDocumentoSap.IdTipoDoc.Equals("08"))
            {
                sHash = GenerarXmlNotaAjuste21(oEmisor, oDocumentoSap);
            }

            return sHash;
        }

        private String GenerarXmlFacturaBoleta21(CompaniaInfo oEmisor, DocumentoSapInfo21 oDocumentoSap)
        {
            try
            {
                var oDocumentoCarvajal = new FEPE_Factura();
                var oGeneradorHash = new GeneratorCdp21();
                String sMonto = "";

                #region CAB-Cabecera

                //3 - Tipo de Documento
                oDocumentoCarvajal.CAB.TipoDocumento.Value = oDocumentoSap.IdTipoDoc.Equals("01")
                                                                 ? FEPE_Document_Enums.Tipo_Documento.Factura_01
                                                                 : FEPE_Document_Enums.Tipo_Documento.Boleta_Venta_03;

                //4 - Correlativo del Documento (Serie + Número)
                oDocumentoCarvajal.CAB.SerieCorrelativo.Value = oDocumentoSap.Serie + "-0" + oDocumentoSap.NroDocumento;

                //5 - Fecha de Emisión del Documento
                if (oDocumentoSap.FechaDocumento != null)
                    oDocumentoCarvajal.CAB.FechaEmision.Value = (DateTime)oDocumentoSap.FechaDocumento;

                //6 - Hora de Emisión del Documento
                oDocumentoCarvajal.CAB.HoraEmision.Value = DateTime.Now.ToString("hh:mm:ss");

                //7 - Moneda del Documento
                oDocumentoCarvajal.CAB.TipoMoneda.Value = oDocumentoSap.IdMoneda;

                //8 - Total Valor Venta
                if (oDocumentoSap.Subtotal != null)
                {
                    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("1"))
                    {
                        oDocumentoCarvajal.CAB.TotalValorVenta.Value = "0.00";
                    }
                    else
                    {
                        sMonto = ((double)(oDocumentoSap.Subtotal + oDocumentoSap.Flete)).ToString("###0.00");
                        oDocumentoCarvajal.CAB.TotalValorVenta.Value = sMonto;
                    }
                }

                //9 - Total Tributo
                //Pendiente
                if (oDocumentoSap.Igv != null)
                {
                    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("1"))
                    {
                        oDocumentoCarvajal.CAB.MontoTotalImpuestos.Value = "0.00";
                    }
                    else
                    {
                        sMonto = ((double)(oDocumentoSap.Igv + oDocumentoSap.Isc)).ToString("###0.00");
                        oDocumentoCarvajal.CAB.MontoTotalImpuestos.Value = sMonto;
                    }
                }

                //10 - Total Importe incluyendo Impuestos
                if (oDocumentoSap.Total != null)
                {
                    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("1"))
                    {
                        oDocumentoCarvajal.CAB.TotalPrecioVenta.Value = "0.00";
                    }
                    else
                    {
                        sMonto = ((double)oDocumentoSap.Total).ToString("###0.00");
                        oDocumentoCarvajal.CAB.TotalPrecioVenta.Value = sMonto;
                    }
                }

                //11 - Importe Total de Descuentos
                if (oDocumentoSap.Descuento != null)
                {
                    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("1"))
                    {
                        oDocumentoCarvajal.CAB.ImporteTotalDescuentos.Value = "0.00";
                    }
                    else
                    {
                        sMonto = ((double)oDocumentoSap.Descuento).ToString("###0.00");
                        oDocumentoCarvajal.CAB.ImporteTotalDescuentos.Value = sMonto;
                    }
                }

                //12 - Importe Total de Cargos
                if (oDocumentoSap.Percepcion > 0)
                {
                    if (oDocumentoSap.Percepcion != null)
                    {
                        sMonto = ((double)oDocumentoSap.Percepcion).ToString("###0.00");
                        oDocumentoCarvajal.CAB.ImporteTotalCargos.Value = sMonto;
                    }
                }
                else
                {
                    oDocumentoCarvajal.CAB.ImporteTotalCargos.Value = "0.00";
                }


                //13 - Monto Redondeo Importe Total
                //if (oDocumentoSap.Total != null)
                //{
                //    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("1"))
                //    {
                //        oDocumentoCarvajal.CAB.MontoRedondeoImporteTotal.Value = "0.00";
                //    }
                //    else
                //    {
                //        sMonto = ((double)oDocumentoSap.Total).ToString("###0.00");
                //        oDocumentoCarvajal.CAB.MontoRedondeoImporteTotal.Value = sMonto;
                //    }
                //}
                oDocumentoCarvajal.CAB.MontoRedondeoImporteTotal.Value = "0.00";

                //14 - Importe Total a Pagar
                if (oDocumentoSap.Total != null)
                {
                    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("1"))
                    {
                        oDocumentoCarvajal.CAB.ImporteTotalAPagar.Value = "0.00";
                    }
                    else
                    {
                        sMonto = ((double)oDocumentoSap.Total).ToString("###0.00");
                        oDocumentoCarvajal.CAB.ImporteTotalAPagar.Value = sMonto;
                    }
                }

                //15 - Código PDF
                var sPlantilla = "";
                var sResolucionSunat = "";
                if (oEmisor.Ruc.Equals("20259033072"))
                {
                    if (oDocumentoSap.IdTipoDoc.Equals("01"))
                        sPlantilla = WebConfigurationManager.AppSettings["PlantillaPecsaFA"];
                    else
                    {
                        if (oDocumentoSap.IdTipoDoc.Equals("03"))
                            sPlantilla = WebConfigurationManager.AppSettings["PlantillaPecsaBO"];
                    }
                    sResolucionSunat = "0180050001319";
                }
                if (oEmisor.Ruc.Equals("20100132754"))
                {
                    if (oDocumentoSap.IdTipoDoc.Equals("01"))
                        sPlantilla = WebConfigurationManager.AppSettings["PlantillaPdpFA"];
                    else
                    {
                        if (oDocumentoSap.IdTipoDoc.Equals("03"))
                            sPlantilla = WebConfigurationManager.AppSettings["PlantillaPdpBO"];
                    }
                    sResolucionSunat = "0180050001318";
                }

                if (oEmisor.Ruc.Equals("20330033313"))
                {
                    if (oDocumentoSap.IdTipoDoc.Equals("01"))
                        sPlantilla = WebConfigurationManager.AppSettings["PlantillaPesFA"];
                    else
                    {
                        if (oDocumentoSap.IdTipoDoc.Equals("03"))
                            sPlantilla = WebConfigurationManager.AppSettings["PlantillaPesBO"];
                    }
                    sResolucionSunat = "0180050001319";
                }

                oDocumentoCarvajal.CAB.CodigoPlantilla.Value = sPlantilla;

                //16 - Es Baja? -- 17 - Motivo Baja
                if (oDocumentoSap.IdEstado.Equals("2"))
                {
                    oDocumentoCarvajal.CAB.EsBaja.Value = true;
                    oDocumentoCarvajal.CAB.MotivoBaja.Value = "Baja";
                }
                else
                {
                    oDocumentoCarvajal.CAB.EsBaja.Value = false;
                    oDocumentoCarvajal.CAB.MotivoBaja.Value = "";
                }

                //18 - Orden de Compra
                oDocumentoCarvajal.CAB.OrdenCompra.Value = oDocumentoSap.OrdenCompra;

                //20 - Fecha Vencimiento
                if (oDocumentoSap.FechaVencimiento != null)
                    oDocumentoCarvajal.CAB.FechaVencimiento.Value = (DateTime)oDocumentoSap.FechaVencimiento;

                //21 - Resolución SUNAT
                oDocumentoCarvajal.CAB.NumeroResolucionSUNAT.Value = sResolucionSunat;

                //24 - Tipo de Operación
                if (oDocumentoSap.Percepcion > 0)
                    oDocumentoCarvajal.CAB.TipoOperacion.Value = "2001";
                else
                {
                    if ((oDocumentoSap.IdTipoFacturacion.Equals("03") && oDocumentoSap.FlagDetraccionServ.Equals("1") &&
                         oDocumentoSap.Total > oDocumentoSap.MontoDetraccionServ)
                        || (oDocumentoSap.IdTipoFacturacion.Equals("01") && oDocumentoSap.Flete > 400))
                    {
                        oDocumentoCarvajal.CAB.TipoOperacion.Value = "1001";
                    }
                    else
                    {
                        oDocumentoCarvajal.CAB.TipoOperacion.Value = "0101";
                    }
                }




                //Otros
                oDocumentoCarvajal.CAB.TotalAnticipos.Value = "0.00";
                oDocumentoCarvajal.CAB.CantidadCopiasPDFImprimir.Value = "1";
                oDocumentoCarvajal.CAB.INCOTERM.Value = "";
                //oDocumentoCarvajal.CAB.CondicionesPago.Value = "";
                oDocumentoCarvajal.CAB.CodigoImpresora.Value = "IMPEC";

                #endregion


                #region EMI-Emisor

                var oDatosEmisor = new EMI();

                //1 - RUC Emisor
                oDatosEmisor.RUCEmisor.Value = oEmisor.Ruc;

                //2 - Tipo Documento Emisor
                oDatosEmisor.TipoDocumentoEmisor.Value = "6";

                //3 - Nombre Emisor
                oDatosEmisor.NombreEmisor.Value = oEmisor.Nombre;

                //4 - Razón Social Emisor
                oDatosEmisor.RazonSocialEmisor.Value = oEmisor.Nombre;

                //5 - Ubigeo
                oEmisor.Ubigeo = "150140";
                oDatosEmisor.CodigoUbigeoEmisor.Value = oEmisor.Ubigeo;

                //6 - Dirección
                oDatosEmisor.DireccionEmisor.Value = oEmisor.Direccion;

                //8 - Departamento -- 9 - Provincia -- 10 - Distrito
                oDatosEmisor.UrbanizacionEmisor.Value = "";
                oDatosEmisor.DepartamentoEmisor.Value = oEmisor.Ubigeo.Substring(0, 2);
                oDatosEmisor.ProvinciaEmisor.Value = oEmisor.Ubigeo.Substring(2, 2);
                oDatosEmisor.DistritoEmisor.Value = oEmisor.Ubigeo.Substring(4, 2);

                //11 - País
                oDatosEmisor.CodigoPaisEmisor.Value = "PE";

                oDocumentoCarvajal.AddEmisor(oDatosEmisor);

                #endregion

                #region REC-Cliente

                var oDatosCliente = new REC();

                //1 - ID Comprador
                oDatosCliente.IDReceptor.Value = oDocumentoSap.Cliente.NroDocumentoIdentidad;

                //2 - Documento de Identidad
                oDatosCliente.RUCReceptor.Value = oDocumentoSap.Cliente.NroDocumentoIdentidad;

                //3 - Tipo de Documento de Identidad
                oDatosCliente.TipoDocumentoReceptor.Value = oDocumentoSap.Cliente.IdTipoDoc.Equals("01") ? "1" : "6";

                //4 - Nombre Receptor
                oDatosCliente.NombreReceptor.Value = oDocumentoSap.Cliente.RazonSocial;


                //6 - Dirección
                oDatosCliente.DireccionReceptor.Value = oDocumentoSap.Cliente.Direccion;

                oDatosCliente.DepartamentoReceptor.Value = oDocumentoSap.Cliente.IdDepartamento;
                oDatosCliente.ProvinciaReceptor.Value = oDocumentoSap.Cliente.IdProvincia;

                if (oDocumentoSap.Cliente.IdDistrito.Length <= 30)
                {
                    oDatosCliente.DistritoReceptor.Value = oDocumentoSap.Cliente.IdDistrito;
                }
                else
                {
                    oDatosCliente.DistritoReceptor.Value = oDocumentoSap.Cliente.IdDistrito.Substring(0, 30);
                }


                oDatosCliente.CorreoElectronicoReceptor.Value = "";
                oDatosCliente.CodigoPaisReceptor.Value = "PE";

                oDocumentoCarvajal.AddReceptor(oDatosCliente);

                #endregion

                #region NOT-Notas

                //NOTAS
                //GLOSA SAC
                var oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0004" },
                    Descripcion = { Value = @"0-800-20102 / 411-4696 / sac@pecsa.com.pe" }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);

                //Dirección Fiscal del Cliente
                oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0008" },
                    Descripcion = { Value = oDocumentoSap.Cliente.Direccion }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);

                //Dirección de Entrega
                oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0137" },
                    Descripcion = { Value = oDocumentoSap.DireccionEntrega }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);

                //Dirección de Emisión (Planta)
                oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0135" },
                    Descripcion = { Value = oDocumentoSap.Planta.Direccion }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);

                //Planta de Origen
                oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0002" },
                    Descripcion = { Value = oDocumentoSap.Planta.Descripcion }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);

                //Web de PECSA
                oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0006" },
                    Descripcion = { Value = @"www.pecsa.com.pe" }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);

                //Código del Cliente
                oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0007" },
                    Descripcion = { Value = oDocumentoSap.IdCliente }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);


                //SCOP
                if (!String.IsNullOrEmpty(oDocumentoSap.Scop))
                {
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0001" },
                        Descripcion = { Value = oDocumentoSap.Scop }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }

                //Placa Tanque del Vehículo
                if (!String.IsNullOrEmpty(oDocumentoSap.PlacaTanque))
                {
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0011" },
                        Descripcion = { Value = oDocumentoSap.PlacaTanque }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }

                //Percepción
                if (oDocumentoSap.Percepcion > 0)
                {
                    var sRazonSocial = oDocumentoSap.Cliente.RazonSocial.Length > 58
                          ? oDocumentoSap.Cliente.RazonSocial.Substring(0, 59)
                          : oDocumentoSap.Cliente.RazonSocial;

                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0141" },
                        Descripcion = { Value = "COMPROBANTE DE PERCEPCIÓN: " + oDocumentoSap.Serie + "-" + oDocumentoSap.NroDocumento + "-" + sRazonSocial }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }

                //Campos Primax
                //Teléfono CSC1
                if (!String.IsNullOrEmpty(oDocumentoSap.TelefonoCsc1))
                {
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0055" },
                        Descripcion = { Value = oDocumentoSap.TelefonoCsc1 }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }

                //Teléfono CSC2
                if (!String.IsNullOrEmpty(oDocumentoSap.TelefonoCsc2))
                {
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0056" },
                        Descripcion = { Value = oDocumentoSap.TelefonoCsc2 }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }

                //Teléfono CSC3
                if (!String.IsNullOrEmpty(oDocumentoSap.TelefonoCsc3))
                {
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0057" },
                        Descripcion = { Value = oDocumentoSap.TelefonoCsc3 }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }

                //Instrucción Entrega
                if (!String.IsNullOrEmpty(oDocumentoSap.InstruccionEntrega))
                {
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0050" },
                        Descripcion = { Value = oDocumentoSap.InstruccionEntrega }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }

                //Código de Pago
                if (!String.IsNullOrEmpty(oDocumentoSap.CodigoPago))
                {
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0051" },
                        Descripcion = { Value = oDocumentoSap.CodigoPago }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }

                //Condición de Pago
                if (!String.IsNullOrEmpty(oDocumentoSap.CondicionPago))
                {
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0058" },
                        Descripcion = { Value = oDocumentoSap.CondicionPago }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }

                //Precintos

                var sPrecintos = new StringBuilder();
                var sPrecintos2 = new StringBuilder();
                var sPrecintos3 = new StringBuilder();
                if (oDocumentoSap.IdTipoFacturacion.Equals("01"))
                {
                    if (oDocumentoSap.Precintos.Count > 0)
                    {
                        var nContador = 1;
                        foreach (var oPrecinto in oDocumentoSap.Precintos)
                        {
                            if (String.IsNullOrEmpty(oPrecinto.NroPrecinto))
                                break;

                            if (nContador < 10)
                            {
                                sPrecintos.Append(oPrecinto.NroPrecinto);
                                sPrecintos.Append("- ");
                            }
                            else
                            {
                                if (nContador >= 10 && nContador < 20)
                                {
                                    sPrecintos2.Append(oPrecinto.NroPrecinto);
                                    sPrecintos2.Append("- ");
                                }
                                else
                                {
                                    sPrecintos3.Append(oPrecinto.NroPrecinto);
                                    sPrecintos3.Append("- ");
                                }
                            }

                            nContador += 1;
                        }
                    }
                }

                String sPrecinto;
                if (!String.IsNullOrEmpty(sPrecintos.ToString()))
                {
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0124" },
                        Descripcion = { Value = "PRECINTOS: " }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);

                    sPrecinto = String.IsNullOrEmpty(sPrecintos2.ToString()) ? sPrecintos.ToString().Trim().TrimEnd('-') : sPrecintos.ToString().Trim();
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0125" },
                        Descripcion = { Value = sPrecinto }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }
                if (!String.IsNullOrEmpty(sPrecintos2.ToString()))
                {
                    sPrecinto = String.IsNullOrEmpty(sPrecintos3.ToString()) ? sPrecintos2.ToString().Trim().TrimEnd('-') : sPrecintos2.ToString().Trim();
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0126" },
                        Descripcion = { Value = sPrecinto }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }
                if (!String.IsNullOrEmpty(sPrecintos3.ToString()))
                {
                    sPrecinto = sPrecintos3.ToString().Trim().TrimEnd('-');
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0127" },
                        Descripcion = { Value = sPrecinto }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }

                //Hora de Emisión
                oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0142" },
                    Descripcion = { Value = DateTime.Now.ToString("hh:mm tt") }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);

                //Observaciones para Dirección adicional CL
                if (oDocumentoSap.IdTipoFacturacion.Equals("01") && oEmisor.Ruc.Equals("20259033072"))
                {
                    if (!String.IsNullOrEmpty(oDocumentoSap.Observacion))
                    {
                        oNotaDocumento = new Nota
                        {
                            Codigo = { Value = "0114" },
                            Descripcion = { Value = oDocumentoSap.Observacion.Trim() }
                        };
                        oDocumentoCarvajal.AddNota(oNotaDocumento);
                    }
                }

                //Placas en Observaciones para PES
                if (oDocumentoSap.IdTipoFacturacion.Equals("01") && oEmisor.Ruc.Equals("20330033313"))
                {
                    if (!String.IsNullOrEmpty(oDocumentoSap.Observacion))
                    {
                        oNotaDocumento = new Nota
                        {
                            Codigo = { Value = "0114" },
                            Descripcion = { Value = oDocumentoSap.Observacion.Trim() }
                        };
                        oDocumentoCarvajal.AddNota(oNotaDocumento);
                    }
                }

                //Observaciones para Documentos de Servicios con Detracción
                if ((oDocumentoSap.IdTipoFacturacion.Equals("03") && oDocumentoSap.FlagDetraccionServ.Equals("1") &&
                        oDocumentoSap.Total > oDocumentoSap.MontoDetraccionServ)
                        || (oDocumentoSap.IdTipoFacturacion.Equals("01") && oDocumentoSap.Flete > 400))
                {
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0111" },
                        Descripcion = { Value = "Operación Sujeta al Sistema de Pago de Obligaciones Tributarias con el Gobierno Central " }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);

                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0112" },
                        Descripcion = { Value = ". Cta.Cte. MN Banco de la Nación: " + oDocumentoSap.CtaBancoDetraccion }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }

                //Observaciones en Facturas de Misceláneos y Servicios
                if (oDocumentoSap.IdTipoFacturacion.Equals("02") || oDocumentoSap.IdTipoFacturacion.Equals("03"))
                {
                    if (!String.IsNullOrEmpty(oDocumentoSap.Observacion))
                    {
                        oNotaDocumento = new Nota
                        {
                            Codigo = { Value = "0113" },
                            Descripcion = { Value = oDocumentoSap.Observacion }
                        };
                        oDocumentoCarvajal.AddNota(oNotaDocumento);
                    }
                }

                //Nro. Pedido
                if (!String.IsNullOrEmpty(oDocumentoSap.NroPedido))
                {
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0139" },
                        Descripcion = { Value = oDocumentoSap.NroPedido }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }

                #endregion

                #region LEYENDAS

                //TOTAL EN LETRAS
                if (oDocumentoSap.Total != null)
                {
                    var oLeyenda = new Leyenda();
                    oLeyenda.Codigo.Value = "1000";
                    oLeyenda.Descripcion.Value = Util.NumeroALetras((double)oDocumentoSap.Total,
                                                                    oDocumentoSap.IdMoneda);

                    oDocumentoCarvajal.AddLeyenda(oLeyenda);
                }

                //TRANSFERENCIA GRATUITA
                if (oDocumentoSap.MotivoVenta.Gratuito.Equals("1"))
                {
                    var oLeyenda = new Leyenda();
                    oLeyenda.Codigo.Value = "1002";
                    oLeyenda.Descripcion.Value = "TRANSFERENCIA GRATUITA DE UN BIEN Y/O SERVICIO PRESTADO GRATUITAMENTE";

                    oDocumentoCarvajal.AddLeyenda(oLeyenda);
                }

                //PERCEPCIÓN
                if (oDocumentoSap.Percepcion > 0)
                {
                    var oLeyenda = new Leyenda();
                    oLeyenda.Codigo.Value = "2000";
                    //oLeyenda.Descripcion.Value = "COMPROBANTE DE PERCEPCIÓN" + oDocumentoSap.Serie + "-" + oDocumentoSap.NroDocumento;
                    oLeyenda.Descripcion.Value = "COMPROBANTE DE PERCEPCIÓN";

                    oDocumentoCarvajal.AddLeyenda(oLeyenda);
                }

                //DETRACCIÓN
                if ((oDocumentoSap.IdTipoFacturacion.Equals("03") && oDocumentoSap.FlagDetraccionServ.Equals("1") &&
                     oDocumentoSap.Total > oDocumentoSap.MontoDetraccionServ)
                    || (oDocumentoSap.IdTipoFacturacion.Equals("01") && oDocumentoSap.Flete > 400))
                {
                    var oLeyenda = new Leyenda();
                    oLeyenda.Codigo.Value = "2006";
                    oLeyenda.Descripcion.Value = "OPERACIÓN SUJETA A DETRACCIÓN";

                    oDocumentoCarvajal.AddLeyenda(oLeyenda);
                }

                #endregion

                #region REM-Guía de Remisión



                //Guía de Remisión
                if (!String.IsNullOrEmpty(oDocumentoSap.SerieGuiaRemision) && !String.IsNullOrEmpty(oDocumentoSap.NroGuiaRemision))
                {
                    if (!oDocumentoSap.SerieGuiaRemision.Trim().Equals("0") && !oDocumentoSap.NroGuiaRemision.Trim().Equals("0"))
                    {
                        var oGuiaRemision = new REM();

                        String sGuia;

                        if (oDocumentoSap.SerieGuiaRemision.Equals("GR-"))
                        {
                            var sSerieNroGuia = oDocumentoSap.NroGuiaRemision.Split('-');

                            sGuia = sSerieNroGuia[0].Trim() + "-" + sSerieNroGuia[1].Trim();
                        }
                        else
                        {
                            sGuia = oDocumentoSap.SerieGuiaRemision.PadLeft(4, '0') + "-" + oDocumentoSap.NroGuiaRemision;
                        }

                        //1 - Número de la Guía
                        oGuiaRemision.NumeroGuia.Value = sGuia;
                        //2 - Tipo de Documento
                        oGuiaRemision.TipoDocumentoReferencia.Value = "09";
                        //3 - Fecha de Emisión de la Guía
                        oGuiaRemision.FechaEmision.Value = (DateTime)oDocumentoSap.FechaGuiaRemision;
                        //4 - Hora de Emisión de la Guía
                        oGuiaRemision.HoraEmision.Value = DateTime.Now.ToString("hh:mm:ss");

                        oDocumentoCarvajal.AddGuiaRemision(oGuiaRemision);

                    }

                }

                #endregion

                #region REF-Documentos Referencia

                //SCOP
                if (!String.IsNullOrEmpty(oDocumentoSap.Scop) && oDocumentoSap.Scop.Length >= 11)
                {
                    var oReferencia = new REF();
                    oReferencia.NumeroDocumento.Value = oDocumentoSap.Scop;
                    oReferencia.TipoDocumento.Value = "05";

                    oDocumentoCarvajal.AddReferencia(oReferencia);
                }

                #endregion

                #region IMP-Impuestos

                //IGV
                sMonto = oDocumentoSap.MotivoVenta.Gratuito.Equals("1")
                             ? "0.00"
                             : ((double)oDocumentoSap.Igv).ToString("###0.00");
                var oImpuesto = new IMP();
                oImpuesto.ImporteTotal.Value = sMonto;
                oImpuesto.ImporteExplicito.Value = sMonto;

                oImpuesto.TotalVenta.Value = oDocumentoSap.MotivoVenta.Gratuito.Equals("0") ? oDocumentoCarvajal.CAB.TotalValorVenta.Value : ((double)oDocumentoSap.MontoGratuito).ToString("###0.00");

                if (oDocumentoSap.MotivoVenta.Gratuito.Equals("0"))
                {
                    oImpuesto.CatalogoSunat.Value = "1000";
                    oImpuesto.NombreTributo.Value = "IGV";
                    oImpuesto.CodigoTipoTributo.Value = "VAT";

                    if (oDocumentoSap.Igv == 0)
                    {
                        oImpuesto.CatalogoSunat.Value = "9997";
                        oImpuesto.NombreTributo.Value = "EXO";
                        oImpuesto.CodigoTipoTributo.Value = "VAT";
                    }

                }
                else
                {
                    oImpuesto.CatalogoSunat.Value = "9996";
                    oImpuesto.NombreTributo.Value = "GRA";
                    oImpuesto.CodigoTipoTributo.Value = "FRE";
                }



                oDocumentoCarvajal.AddImpuesto(oImpuesto);

                //ISC
                if (oDocumentoSap.Isc > 0)
                {
                    sMonto = oDocumentoSap.MotivoVenta.Gratuito.Equals("1")
                                 ? "0.00"
                                 : ((double)oDocumentoSap.Isc).ToString("###0.00");
                    oImpuesto = new IMP();
                    oImpuesto.ImporteTotal.Value = sMonto;
                    oImpuesto.ImporteExplicito.Value = sMonto;
                    oImpuesto.TotalVenta.Value = oDocumentoCarvajal.CAB.TotalValorVenta.Value;
                    oImpuesto.CatalogoSunat.Value = "2000";
                    oImpuesto.NombreTributo.Value = "ISC";
                    oImpuesto.CodigoTipoTributo.Value = "EXC";

                    oDocumentoCarvajal.AddImpuesto(oImpuesto);


                }

                //FISE
                //if (oDocumentoSap.MontoFise > 0)
                //{
                //    sMonto = ((double)(oDocumentoSap.MontoFise)).ToString("###0.00");
                //    oImpuesto = new IMP();
                //    oImpuesto.ImporteTotal.Value = sMonto;
                //    oImpuesto.ImporteExplicito.Value = sMonto;
                //    oImpuesto.TotalVenta.Value = oDocumentoCarvajal.CAB.TotalValorVenta.Value;
                //    oImpuesto.CatalogoSunat.Value = "9999";
                //    oImpuesto.NombreTributo.Value = "OTR";
                //    oImpuesto.CodigoTipoTributo.Value = "OTH";

                //    oDocumentoCarvajal.AddImpuesto(oImpuesto);
                //}

                #endregion

                #region CYD - Cargos y Descuentos
                //Descuentos
                //if (oDocumentoSap.Descuento > 0 && oDocumentoSap.MotivoVenta.Gratuito.Equals("0"))
                //{
                //    var oCargoDesc = new CYD();
                //    oCargoDesc.EsCargoDescuento.Value = "false";
                //    oCargoDesc.CodigoMotivo.Value = "00";

                //    //sMonto = "0.00";
                //    sMonto = ((double)(oDocumentoSap.Descuento / oDocumentoSap.Total)).ToString("###0.00000");
                //    oCargoDesc.FactorCargo.Value = sMonto;

                //    sMonto = ((double)oDocumentoSap.Descuento).ToString("###0.00");
                //    oCargoDesc.Monto.Value = sMonto;

                //    oCargoDesc.TipoMoneda.Value = oDocumentoSap.IdMoneda;

                //    sMonto = ((double)oDocumentoSap.Total).ToString("###0.00");
                //    oCargoDesc.MontoBase.Value = sMonto;
                //    oCargoDesc.TipoMonedaMontoBase.Value = oDocumentoSap.IdMoneda;

                //    oDocumentoCarvajal.AddCargoDescuento(oCargoDesc);

                //}


                //Percepción
                if (oDocumentoSap.Percepcion > 0)
                {
                    var oCargo = new CYD();
                    oCargo.EsCargoDescuento.Value = "true";

                    if (oDocumentoSap.PorcPercepcion == 0)
                    {
                        var nCalculo = Math.Round((double)((oDocumentoSap.Percepcion / oDocumentoSap.Total)), 2) * 100;
                        oDocumentoSap.PorcPercepcion = nCalculo;
                    }

                    var sTipoPercepcion = "";
                    if (oDocumentoSap.PorcPercepcion == 1)
                        sTipoPercepcion = "52";
                    if (oDocumentoSap.PorcPercepcion == 2)
                        sTipoPercepcion = "51";
                    if (oDocumentoSap.PorcPercepcion == 0.50)
                    {
                        //oDocumentoSap.PorcPercepcion = 0.05;
                        sTipoPercepcion = "53";
                    }


                    oCargo.CodigoMotivo.Value = sTipoPercepcion;

                    sMonto = ((double)oDocumentoSap.PorcPercepcion / 100).ToString("###0.00000");
                    oCargo.FactorCargo.Value = sMonto;

                    Double? nMontoFactura;
                    Double? nMontoPercepcion;
                    Double? nMontoTotal;


                    nMontoFactura = oDocumentoSap.Total;
                    nMontoPercepcion = oDocumentoSap.Percepcion;
                    if (oDocumentoSap.IdMoneda.Equals("USD"))
                    {
                        nMontoTotal = nMontoFactura + (nMontoPercepcion / oDocumentoSap.TipoCambio);
                    }
                    else
                    {
                        nMontoTotal = nMontoFactura + nMontoPercepcion;
                    }

                    sMonto = ((double)nMontoPercepcion).ToString("###0.00");
                    oCargo.Monto.Value = sMonto;
                    oCargo.TipoMoneda.Value = "PEN";

                    sMonto = ((double)(oDocumentoSap.Total)).ToString("###0.00");
                    oCargo.MontoBase.Value = sMonto;
                    oCargo.TipoMonedaMontoBase.Value = oDocumentoSap.IdMoneda;

                    oCargo.Percepcion.Value = "Percepcion";
                    oCargo.ImporteTotal.Value = ((double)nMontoTotal).ToString("###0.00");
                    oCargo.TipoMonedaPercepcion.Value = "PEN";

                    oDocumentoCarvajal.AddCargoDescuento(oCargo);

                }


                #endregion

                #region ITE - Detalles

                var nItem = 0;
                var bAgregoDtr = false;

                foreach (var documentoDetInfo in oDocumentoSap.Detalle)
                {
                    var oItem = new ITE_Factura();

                    nItem += 1;

                    //1 - Número de Item
                    oItem.ITE.NumeroItem.Value = nItem.ToString();

                    //2 - Unidad de Medida
                    oItem.ITE.UnidadMedida.Value = documentoDetInfo.Articulo.UnidadMedida;

                    //3 - Cantidad
                    sMonto = ((double)documentoDetInfo.Cantidad).ToString("###0.00");
                    oItem.ITE.CantidadUnidad.Value = sMonto;

                    //4 - Valor Venta
                    sMonto = oDocumentoSap.MotivoVenta.Gratuito.Equals("1") ? ((double)(documentoDetInfo.MontoGratuito)).ToString("###0.00") : ((double)(documentoDetInfo.SubTotal)).ToString("###0.00");
                    oItem.ITE.ValorVenta.Value = sMonto;

                    //5 - Descripción del Producto
                    oItem.ITE.DescripcionVenta.Value = documentoDetInfo.Articulo.Descripcion.Replace(",", "");

                    if (documentoDetInfo.Api > 0 && documentoDetInfo.Temperatura > 0 && oDocumentoSap.IdTipoFacturacion.Equals("01"))
                    {
                        oItem.ITE.DescripcionVenta.Value += "~" + ((double)documentoDetInfo.Api).ToString("###0.00") + "~" +
                                                        ((double)documentoDetInfo.Temperatura).ToString("###0.00") + "~" + ((double)documentoDetInfo.Total).ToString("###0.00");
                    }
                    else
                    {
                        oItem.ITE.DescripcionVenta.Value += "~~~" +
                                                        ((double)documentoDetInfo.Total).ToString("###0.00");
                    }

                    //6 - Valor Unitario (Precio sin impuestos)

                    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("0"))
                    {
                        sMonto = ((double)documentoDetInfo.PrecioUnitario).ToString("###0.00");
                    }
                    else
                    {
                        if (documentoDetInfo.Igv == 0)
                        {
                            sMonto = ((double)documentoDetInfo.PrecioUnitarioIgv).ToString("###0.0000");
                        }
                        else
                        {
                            sMonto = "0.00";
                        }

                    }

                    oItem.ITE.ValorUnitario.Value = sMonto;

                    //7 - Código del Producto del Emisor
                    oItem.ITE.CodigoEmisor.Value = documentoDetInfo.IdArticulo;

                    //8 - Código del Producto SUNAT - CUBSO
                    if (!String.IsNullOrEmpty(documentoDetInfo.Articulo.CodigoCubso) && !documentoDetInfo.Articulo.CodigoCubso.Trim().Equals("0"))
                        oItem.ITE.CodigoProductoSUNAT.Value = documentoDetInfo.Articulo.CodigoCubso;

                    //9 - Código del Producto GS1
                    if (!String.IsNullOrEmpty(documentoDetInfo.Articulo.CodigoGs1) && !documentoDetInfo.Articulo.CodigoGs1.Trim().Equals("0"))
                        oItem.ITE.CodigoProductoGS1.Value = documentoDetInfo.Articulo.CodigoGs1;

                    //10 - Código del Producto GTIN
                    if (!String.IsNullOrEmpty(documentoDetInfo.Articulo.CodigoGtin) && !documentoDetInfo.Articulo.CodigoGtin.Trim().Equals("0"))
                        oItem.ITE.TipoEstructuraGTIN.Value = documentoDetInfo.Articulo.CodigoGtin;

                    //11 - Código de Precio
                    oItem.ITE.CodigoPrecio.Value = !oDocumentoSap.MotivoVenta.Gratuito.Equals("1") ? "01" : "02";

                    //12 - Valor Precio (Precio con impuestos)
                    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("0"))
                    {
                        sMonto = ((double)documentoDetInfo.PrecioUnitarioIgv).ToString("###0.0000");
                    }
                    else
                    {
                        sMonto = ((double)documentoDetInfo.PrecioUnitario).ToString("###0.00");
                    }

                    oItem.ITE.Precio.Value = sMonto;

                    #region IDE - Descuento del ITEM

                    if (documentoDetInfo.Descuento > 0 && oDocumentoSap.MotivoVenta.Gratuito.Equals("0"))
                    {
                        var oItemDescuento = new IDE();

                        //1 - Tipo de Cargo: true si es Cargo, false si es Descuento
                        oItemDescuento.TipoCargos.Value = "false";

                        //2 - Motivo del Descuento
                        oItemDescuento.MotivoDescuento.Value = "00";
                        oItemDescuento.DescripcionMotivoDescuento.Value = "Descuentos que afectan la base imponible del IGV";

                        //4 - Importe Final del descuento
                        //sMonto = ((double)documentoDetInfo.Descuento).ToString("###0.00");
                        sMonto = ((double)documentoDetInfo.Descuento).ToString("###0.00");
                        oItemDescuento.ImporteFinalDescuento.Value = sMonto;

                        //5 - Porcentaje de Descuento
                        //if (oDocumentoSap.IdTipoFacturacion == "05")
                        //{
                        //    sMonto = ((double)((documentoDetInfo.FactorDescuento) / 100)).ToString("###0.00");
                        //    oItemDescuento.PorcentajeDescuento.Value = sMonto;
                        //}
                        //else
                        //{
                        //    sMonto = ((double)((documentoDetInfo.Descuento / documentoDetInfo.Total) * 1000)).ToString("###0.00");
                        //    oItemDescuento.PorcentajeDescuento.Value = sMonto;
                        //}


                        //6 - Monto Base
                        if (oDocumentoSap.IdTipoFacturacion == "05")
                        {
                            sMonto = (((double)documentoDetInfo.PrecioUnitarioIgv / 1.18) * (double)documentoDetInfo.Cantidad).ToString("###0.00");
                            oItemDescuento.MontoBaseCargo.Value = sMonto;

                        }
                        else
                        {
                            sMonto = ((double)documentoDetInfo.Total).ToString("###0.00");
                            oItemDescuento.MontoBaseCargo.Value = sMonto;
                        }


                        //7 - Factor Cargo
                        if (oDocumentoSap.IdTipoFacturacion == "05")
                        {
                            var nDivisor = 0.0;
                            if (documentoDetInfo.FactorDescuento > 100)
                            {
                                nDivisor = 1000;
                            }

                            if (documentoDetInfo.FactorDescuento > 0 && documentoDetInfo.FactorDescuento <= 99)
                            {
                                nDivisor = 100;
                            }


                            sMonto = ((double)((documentoDetInfo.FactorDescuento) / nDivisor)).ToString("###0.00");
                            oItemDescuento.FactorCargo.Value = sMonto;
                        }
                        else
                        {
                            sMonto = ((double)((documentoDetInfo.Descuento / documentoDetInfo.Total) * 100)).ToString("###0.00");
                            oItemDescuento.FactorCargo.Value = sMonto;
                        }
                        //sMonto = ((double)(documentoDetInfo.Descuento / documentoDetInfo.Total)).ToString("###0.00000");
                        //oItemDescuento.FactorCargo.Value = sMonto;

                        oItem.AddIDE(oItemDescuento);
                    }

                    #endregion

                    #region IIM - Impuesto del ITEM

                    //IGV
                    if (documentoDetInfo.Igv == null)
                        documentoDetInfo.Igv = 0;

                    var oItemImpuesto = new IIM();

                    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("1"))
                    {
                        //5 - Tipo de Afectación IGV
                        oItemImpuesto.TipoAfectacionIGV.Value = "21";
                        sMonto = "0.00";
                    }
                    else
                    {
                        //5 - Tipo de Afectación IGV
                        oItemImpuesto.TipoAfectacionIGV.Value = documentoDetInfo.Igv == 0 ? "20" : "10";
                        sMonto = ((double)documentoDetInfo.Igv).ToString("###0.00");
                    }

                    //1 - Importe Total y 3 - Importe Explícito
                    oItemImpuesto.ImporteTotalTributo.Value = sMonto;
                    oItemImpuesto.ImporteExplicitoTributar.Value = sMonto;

                    //2 - Base Imponible sobre la que se aplica la tasa
                    sMonto = ((double)documentoDetInfo.SubTotal).ToString("###0.00");
                    oItemImpuesto.BaseImponible.Value = sMonto;

                    //4 - Porcentaje que se aplica a la base imponible
                    oItemImpuesto.PorcentajeAplicadoBaseImponible.Value = "18";

                    //7 - Categoría de Tributo
                    oItemImpuesto.CategoriaTributoSUNAT.Value = "1000";

                    //8 - Nombre del Tributo
                    oItemImpuesto.NombreTributo.Value = "IGV";
                    oItemImpuesto.CodigoTipoTributo.Value = "VAT";

                    //9 - Código del tipo de Tributo

                    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("1"))
                    {
                        //Transferencia Gratuita
                        //5 - Tipo de Afectación IGV
                        oItemImpuesto.CategoriaTributoSUNAT.Value = "9996";
                        oItemImpuesto.NombreTributo.Value = "GRA";
                        oItemImpuesto.TipoAfectacionIGV.Value = "32";
                        oItemImpuesto.CodigoTipoTributo.Value = "FRE";
                    }
                    else
                    {
                        //Exonerado
                        //5 - Tipo de Afectación IGV
                        if (documentoDetInfo.Igv == 0)
                        {
                            oItemImpuesto.CategoriaTributoSUNAT.Value = "9997";
                            oItemImpuesto.NombreTributo.Value = "EXO";
                            oItemImpuesto.CodigoTipoTributo.Value = "VAT";
                        }
                    }


                    oItemImpuesto.SistemaISC.Value = "";

                    oItem.AddIIM(oItemImpuesto);

                    //ISC
                    if (documentoDetInfo.Isc > 0)
                    {
                        var oItemImpuestoIsc = new IIM();

                        sMonto = ((double)documentoDetInfo.Isc).ToString("###0.00");

                        //1 - Importe Total y 3 - Importe Explícito
                        oItemImpuestoIsc.ImporteTotalTributo.Value = sMonto;
                        oItemImpuestoIsc.ImporteExplicitoTributar.Value = sMonto;

                        //2 - Base Imponible sobre la que se aplica la tasa
                        sMonto = ((double)documentoDetInfo.SubTotal).ToString("###0.00");
                        oItemImpuestoIsc.BaseImponible.Value = sMonto;

                        //5 - Código Sistema ISC
                        oItemImpuestoIsc.SistemaISC.Value = "01";

                        //7 - Categoría de Tributo
                        oItemImpuestoIsc.CategoriaTributoSUNAT.Value = "2000";

                        //8 - Nombre del Tributo
                        oItemImpuestoIsc.NombreTributo.Value = "ISC";

                        //9 - Código del tipo de Tributo
                        oItemImpuestoIsc.CodigoTipoTributo.Value = "EXC";

                        oItem.AddIIM(oItemImpuestoIsc);
                    }

                    //if (oDocumentoSap.MontoFise > 0)
                    //{
                    //    sMonto = ((double)(oDocumentoSap.MontoFise)).ToString("###0.00");
                    //    oItemImpuestoFise = new IIM();
                    //    oItemImpuestoFise.ImporteTotal.Value = sMonto;
                    //    oItemImpuestoFise.ImporteExplicito.Value = sMonto;
                    //    oItemImpuestoFise.TotalVenta.Value = oDocumentoCarvajal.CAB.TotalValorVenta.Value;
                    //    oItemImpuestoFise.CatalogoSunat.Value = "9999";
                    //    oItemImpuestoFise.NombreTributo.Value = "OTR";
                    //    oItemImpuestoFise.CodigoTipoTributo.Value = "OTH";

                    //    oDocumentoCarvajal.AddImpuesto(oImpuesto);
                    //}


                    #endregion

                    #region TRA - Placa de Atención de Venta de Combustibles

                    if (oEmisor.Ruc.Equals("20330033313"))
                    {
                        if (!String.IsNullOrEmpty(documentoDetInfo.PlacaAtencion) && !documentoDetInfo.PlacaAtencion.Equals("0"))
                        {
                            var oPlacaVehiculoPes = new TRA();

                            oPlacaVehiculoPes.NombreConcepto.Value = "Gastos Art. 37 Renta: Número de Placa";
                            oPlacaVehiculoPes.CodigoConcepto.Value = "7000";
                            oPlacaVehiculoPes.ValorConcepto.Value = documentoDetInfo.PlacaAtencion;

                            oItem.AddTRA(oPlacaVehiculoPes);
                        }

                    }

                    #endregion

                    oDocumentoCarvajal.AddItem(oItem);

                    if ((oDocumentoSap.IdTipoFacturacion.Equals("03") &&
                         oDocumentoSap.FlagDetraccionServ.Equals("1") &&
                         oDocumentoSap.Total > oDocumentoSap.MontoDetraccionServ)
                        || (oDocumentoSap.IdTipoFacturacion.Equals("01") && oDocumentoSap.Flete > 400))
                    {
                        if (bAgregoDtr == false)
                        {
                            var oDetraccion = new TRA();
                            oDetraccion.FormaPago.Value = "001";
                            oItem.AddTRA(oDetraccion);
                            bAgregoDtr = true;
                        }

                    }
                }


                //#region FISE - SISE

                //if (oDocumentoSap.MontoFise + oDocumentoSap.MontoSise > 0)
                //{
                //    //Detalle FISE
                //    if (oDocumentoSap.MontoFise > 0)
                //    {
                //        var oItemFise = new ITE_Factura();

                //        oItemFise.ITE.NumeroItem.Value = (nItem + 1).ToString(CultureInfo.InvariantCulture);
                //        oItemFise.ITE.UnidadMedida.Value = "NIU";

                //        //Cantidad
                //        oItemFise.ITE.CantidadUnidad.Value = "0.00";

                //        //Valor Precio
                //        sMonto = ((double) (oDocumentoSap.MontoFise)).ToString("###0.00");
                //        oItemFise.ITE.ValorVenta.Value = "0.00";
                //        oItemFise.ITE.DescripcionVenta.Value = "FISE - LEY 29852---" + sMonto;

                //        //Valor Unitario
                //        oItemFise.ITE.ValorUnitario.Value = "0.00";

                //        oItemFise.ITE.CodigoEmisor.Value = "0";
                //        oItemFise.ITE.CodigoPrecio.Value = !oDocumentoSap.MotivoVenta.Gratuito.Equals("1") ? "01" : "02";

                //        //Precio Unitario
                //        oItemFise.ITE.Precio.Value = "0.00";

                //        //Se crea el Impuesto de la línea de detalle

                //        var oItemImpuestoIgv = new IIM()
                //            {
                //                CategoriaTributoSUNAT = {Value = "1000"},
                //                NombreTributo = {Value = "IGV"},
                //                CodigoTipoTributo = {Value = "VAT"},
                //                PorcentajeAplicadoBaseImponible = {Value = "18"},
                //                TipoAfectacionIGV = {Value = "20"},
                //                ImporteTotalTributo = {Value = "0.00"},
                //                ImporteExplicitoTributar = {Value = "0.00"}
                //            };
                //        oItemFise.AddIIM(oItemImpuestoIgv);
                //        //}

                //        oDocumentoCarvajal.AddItem(oItemFise);
                //    }
                //}

                //    //Detalle SISE
                //    if (oDocumentoSap.MontoSise > 0)
                //    {
                //        var oItemSise = new ITE_Factura();

                //        oItemSise.ITE.NumeroItem.Value = (nItem + 2).ToString(CultureInfo.InvariantCulture);
                //        oItemSise.ITE.UnidadMedida.Value = "NIU";

                //        //Cantidad
                //        oItemSise.ITE.CantidadUnidad.Value = "0.00";

                //        //Valor Precio
                //        sMonto = ((double)(oDocumentoSap.MontoSise)).ToString("###0.00");
                //        oItemSise.ITE.ValorVenta.Value = "0.00";
                //        oItemSise.ITE.DescripcionVenta.Value = "SISE - LEY 29852---" + sMonto;

                //        //Valor Unitario
                //        oItemSise.ITE.ValorUnitario.Value = "0.00";

                //        oItemSise.ITE.CodigoEmisor.Value = "0";
                //        oItemSise.ITE.CodigoPrecio.Value = !oDocumentoSap.MotivoVenta.Gratuito.Equals("1") ? "01" : "02";

                //        //Precio Unitario
                //        oItemSise.ITE.Precio.Value = "0.00";

                //        //Se crea el Impuesto de la línea de detalle

                //        var oItemImpuestoIgv2 = new IIM()
                //            {
                //                CategoriaTributoSUNAT = { Value = "1000" },
                //                NombreTributo = { Value = "IGV" },
                //                CodigoTipoTributo = { Value = "VAT" },
                //                PorcentajeAplicadoBaseImponible = { Value = "18" },
                //                TipoAfectacionIGV = { Value = "20" },
                //                ImporteTotalTributo = { Value = "0.00" },
                //                ImporteExplicitoTributar = { Value = "0.00" }
                //            };
                //        oItemSise.AddIIM(oItemImpuestoIgv2);
                //        //}

                //        oDocumentoCarvajal.AddItem(oItemSise);
                //    }



                //#endregion


                //}

                #endregion

                #region DETRACCIÓN

                //Observaciones para Documentos de Servicios con Detracción
                if ((oDocumentoSap.IdTipoFacturacion.Equals("03") && oDocumentoSap.FlagDetraccionServ.Equals("1") &&
                        oDocumentoSap.Total > oDocumentoSap.MontoDetraccionServ)
                        || (oDocumentoSap.IdTipoFacturacion.Equals("01") && oDocumentoSap.Flete > 400))
                {

                    var oDetraccion = new DTR();

                    oDetraccion.CodigoBienServicio.Value = oDocumentoSap.CodDetraccion;
                    oDetraccion.NumeroCuenta.Value = oDocumentoSap.CtaBancoDetraccion;
                    oDetraccion.MontoDetraccion.Value = ((double)oDocumentoSap.MontoCalculoDtr).ToString("###0.00");
                    oDetraccion.PorcentajeDetraccion.Value = ((double)oDocumentoSap.PorcDetraccion).ToString("###0.00");

                    oDocumentoCarvajal.AddDetraccion(oDetraccion);

                }

                #endregion

                String sCadena = oDocumentoCarvajal.GetText();

                var oHashtag = oGeneradorHash.GetHashForInvoiceCdp(oDocumentoCarvajal);
                String sRutaFile = WebConfigurationManager.AppSettings["Ruta_Salida_XML_Firmado"];
                String sRutaDestino = "";

                if (oEmisor.Ruc.Equals("20259033072"))
                    sRutaDestino = WebConfigurationManager.AppSettings["RutaPecsa"];
                if (oEmisor.Ruc.Equals("20100132754"))
                    sRutaDestino = WebConfigurationManager.AppSettings["RutaPdp"];
                if (oEmisor.Ruc.Equals("20330033313"))
                    sRutaDestino = WebConfigurationManager.AppSettings["RutaPes"];

                String sTipo = "";
                switch (oDocumentoSap.IdTipoDoc)
                {
                    case "01":
                        sTipo = "01";
                        break;
                    case "03":
                        sTipo = "03";
                        break;
                    case "07":
                        sTipo = "07";
                        break;
                    case "08":
                        sTipo = "08";
                        break;
                }

                String sArchivoGenerado = oEmisor.Ruc + "_" + sTipo + "_" +
                                          oDocumentoSap.Serie + "_" + oDocumentoSap.NroDocumento.TrimStart('0') +
                                          ".xml";
                String sArchivoDestino = sArchivoGenerado.Replace('-', '_');
                File.Copy(sRutaFile + "\\" + sArchivoGenerado, sRutaDestino + "\\" + sArchivoDestino, true);

                return oHashtag;
            }
            catch (Exception ex)
            {
                return ex.Message + "-" + ex.StackTrace + "-" + ex.GetBaseException();
            }
        }

        private String GenerarXmlNotaAjuste21(CompaniaInfo oEmisor, DocumentoSapInfo21 oDocumentoSap)
        {
            try
            {
                var oDocumentoCarvajal = new FEPE_Nota();
                var oGeneradorHash = new GeneratorCdp21();
                String sMonto = "";

                #region CAB-Cabecera

                //3 - Tipo de Documento
                oDocumentoCarvajal.CAB.TipoDocumento.Value = oDocumentoSap.IdTipoDoc.Equals("07")
                                                                 ? FEPE_Document_Enums.Tipo_Documento.Nota_Credito_07
                                                                 : FEPE_Document_Enums.Tipo_Documento.Nota_Debito_08;

                //4 - Correlativo del Documento (Serie + Número)
                oDocumentoCarvajal.CAB.SerieCorrelativo.Value = oDocumentoSap.Serie + "-0" + oDocumentoSap.NroDocumento;

                //5 - Fecha de Emisión del Documento
                if (oDocumentoSap.FechaDocumento != null)
                    oDocumentoCarvajal.CAB.FechaEmision.Value = (DateTime)oDocumentoSap.FechaDocumento;

                //6 - Hora de Emisión del Documento
                oDocumentoCarvajal.CAB.HoraEmision.Value = DateTime.Now.ToString("hh:mm:ss");

                //7 - Moneda del Documento
                oDocumentoCarvajal.CAB.TipoMoneda.Value = oDocumentoSap.IdMoneda;

                //8 - Total Valor Venta
                if (oDocumentoSap.Subtotal != null)
                {
                    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("1"))
                    {
                        oDocumentoCarvajal.CAB.TotalValorVenta.Value = "0.00";
                    }
                    else
                    {
                        sMonto = ((double)(oDocumentoSap.Subtotal + oDocumentoSap.Flete)).ToString("###0.00");
                        oDocumentoCarvajal.CAB.TotalValorVenta.Value = sMonto;
                    }
                }

                //9 - Total Tributo
                //Pendiente
                if (oDocumentoSap.Igv != null)
                {
                    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("1"))
                    {
                        oDocumentoCarvajal.CAB.MontoTotalImpuestos.Value = "0.00";
                    }
                    else
                    {
                        sMonto = ((double)(oDocumentoSap.Igv + oDocumentoSap.Isc)).ToString("###0.00");
                        oDocumentoCarvajal.CAB.MontoTotalImpuestos.Value = sMonto;
                    }
                }

                //10 - Total Importe incluyendo Impuestos
                if (oDocumentoSap.Total != null)
                {
                    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("1"))
                    {
                        oDocumentoCarvajal.CAB.ImporteTotalCargos.Value = "0.00";
                    }
                    else
                    {
                        sMonto = ((double)oDocumentoSap.Total).ToString("###0.00");
                        oDocumentoCarvajal.CAB.ImporteTotalCargos.Value = sMonto;
                    }
                }

                //11 - Importe Total de Descuentos
                if (oDocumentoSap.Descuento != null)
                {
                    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("1"))
                    {
                        oDocumentoCarvajal.CAB.ImporteTotalDescuentos.Value = "0.00";
                    }
                    else
                    {
                        sMonto = ((double)oDocumentoSap.Descuento).ToString("###0.00");
                        oDocumentoCarvajal.CAB.ImporteTotalDescuentos.Value = sMonto;
                    }
                }

                //12 - Importe Total de Cargos
                if (oDocumentoSap.Percepcion > 0)
                {
                    if (oDocumentoSap.Percepcion != null)
                    {
                        sMonto = ((double)oDocumentoSap.Percepcion).ToString("###0.00");
                        oDocumentoCarvajal.CAB.ImporteTotalCargos.Value = sMonto;
                    }
                }


                //13 - Monto Redondeo Importe Total
                //if (oDocumentoSap.Total != null)
                //{
                //    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("1"))
                //    {
                //        oDocumentoCarvajal.CAB.MontoRedondeoImporteTotal.Value = "0.00";
                //    }
                //    else
                //    {
                //        sMonto = ((double)oDocumentoSap.Total).ToString("###0.00");
                //        oDocumentoCarvajal.CAB.MontoRedondeoImporteTotal.Value = sMonto;
                //    }
                //}
                oDocumentoCarvajal.CAB.MontoRedondeoImporteTotal.Value = "0.00";

                //14 - Importe Total a Pagar
                if (oDocumentoSap.Total != null)
                {
                    if (oDocumentoSap.MotivoVenta.Gratuito.Equals("1"))
                    {
                        oDocumentoCarvajal.CAB.ImporteTotalAPagar.Value = "0.00";
                    }
                    else
                    {
                        sMonto = ((double)oDocumentoSap.Total).ToString("###0.00");
                        oDocumentoCarvajal.CAB.ImporteTotalAPagar.Value = sMonto;
                    }
                }

                //15 - Código PDF
                var sPlantilla = "";
                var sResolucionSunat = "";
                if (oEmisor.Ruc.Equals("20259033072"))
                {
                    if (oDocumentoSap.IdTipoDoc.Equals("07"))
                        sPlantilla = WebConfigurationManager.AppSettings["PlantillaPecsaNC"];
                    else
                    {
                        if (oDocumentoSap.IdTipoDoc.Equals("08"))
                            sPlantilla = WebConfigurationManager.AppSettings["PlantillaPecsaND"];
                    }
                    sResolucionSunat = "0180050001319";
                }
                if (oEmisor.Ruc.Equals("20100132754"))
                {
                    if (oDocumentoSap.IdTipoDoc.Equals("07"))
                        sPlantilla = WebConfigurationManager.AppSettings["PlantillaPdpFA"];
                    else
                    {
                        if (oDocumentoSap.IdTipoDoc.Equals("08"))
                            sPlantilla = WebConfigurationManager.AppSettings["PlantillaPdpBO"];
                    }
                    sResolucionSunat = "0180050001318";
                }

                if (oEmisor.Ruc.Equals("20330033313"))
                {
                    if (oDocumentoSap.IdTipoDoc.Equals("07"))
                        sPlantilla = WebConfigurationManager.AppSettings["PlantillaPesNC"];
                    else
                    {
                        if (oDocumentoSap.IdTipoDoc.Equals("08"))
                            sPlantilla = WebConfigurationManager.AppSettings["PlantillaPesND"];
                    }
                    sResolucionSunat = "0180050001319";
                }

                oDocumentoCarvajal.CAB.CodigoPlantilla.Value = sPlantilla;

                //16 - Es Baja? -- 17 - Motivo Baja
                if (oDocumentoSap.IdEstado.Equals("2"))
                {
                    oDocumentoCarvajal.CAB.EsBaja.Value = true;
                    oDocumentoCarvajal.CAB.MotivoBaja.Value = "Baja";
                }
                else
                {
                    oDocumentoCarvajal.CAB.EsBaja.Value = false;
                }

                //18 - Orden de Compra
                oDocumentoCarvajal.CAB.OrdenCompra.Value = oDocumentoSap.OrdenCompra;

                //20 - Fecha Vencimiento
                //if (oDocumentoSap.FechaVencimiento != null)
                //    oDocumentoCarvajal.CAB.FechaVencimiento.Value = (DateTime)oDocumentoSap.FechaVencimiento;

                //21 - Resolución SUNAT
                oDocumentoCarvajal.CAB.NumeroResolucionSUNAT.Value = sResolucionSunat;


                //Otros
                oDocumentoCarvajal.CAB.TotalAnticipos.Value = "0.00";
                oDocumentoCarvajal.CAB.CantidadCopiasPDFImprimir.Value = "1";
                oDocumentoCarvajal.CAB.INCOTERM.Value = "";
                //oDocumentoCarvajal.CAB.CondicionesPago.Value = "";
                oDocumentoCarvajal.CAB.CodigoImpresora.Value = "IMPEC";

                #endregion

                #region EMI-Emisor

                var oDatosEmisor = new EMI();

                //1 - RUC Emisor
                oDatosEmisor.RUCEmisor.Value = oEmisor.Ruc;

                //2 - Tipo Documento Emisor
                oDatosEmisor.TipoDocumentoEmisor.Value = "6";

                //3 - Nombre Emisor
                oDatosEmisor.NombreEmisor.Value = oEmisor.Nombre;

                //4 - Razón Social Emisor
                oDatosEmisor.RazonSocialEmisor.Value = oEmisor.Nombre;

                //5 - Ubigeo
                oEmisor.Ubigeo = "150140";
                oDatosEmisor.CodigoUbigeoEmisor.Value = oEmisor.Ubigeo;

                //6 - Dirección
                oDatosEmisor.DireccionEmisor.Value = oEmisor.Direccion;

                //8 - Departamento -- 9 - Provincia -- 10 - Distrito
                oDatosEmisor.DepartamentoEmisor.Value = oEmisor.Ubigeo.Substring(0, 2);
                oDatosEmisor.ProvinciaEmisor.Value = oEmisor.Ubigeo.Substring(2, 2);
                oDatosEmisor.DistritoEmisor.Value = oEmisor.Ubigeo.Substring(4, 2);

                //11 - País
                oDatosEmisor.CodigoPaisEmisor.Value = "PE";

                oDocumentoCarvajal.AddEmisor(oDatosEmisor);

                #endregion

                #region REC-Cliente

                var oDatosCliente = new REC();

                //1 - ID Comprador
                oDatosCliente.IDReceptor.Value = oDocumentoSap.Cliente.NroDocumentoIdentidad;

                //2 - Documento de Identidad
                oDatosCliente.RUCReceptor.Value = oDocumentoSap.Cliente.NroDocumentoIdentidad;

                //3 - Tipo de Documento de Identidad
                oDatosCliente.TipoDocumentoReceptor.Value = oDocumentoSap.Cliente.IdTipoDoc.Equals("01") ? "1" : "6";

                //4 - Nombre Receptor
                oDatosCliente.NombreReceptor.Value = oDocumentoSap.Cliente.RazonSocial;

                //6 - Dirección
                oDatosCliente.DireccionReceptor.Value = oDocumentoSap.Cliente.Direccion;

                oDocumentoCarvajal.AddReceptor(oDatosCliente);

                #endregion

                #region MOT - Motivo Nota

                var oMotivoNota = new MOT();
                oMotivoNota.CodigoTipoNota.Value = oDocumentoSap.MotivoNotaAjuste.IdMotivoSunat;
                oMotivoNota.Motivo.Value = oDocumentoSap.MotivoNotaAjuste.Descripcion;

                oDocumentoCarvajal.AddMotivo(oMotivoNota);

                #endregion

                #region DOC - Documento Referencia

                var oDocumentoReferencia = new DOC();
                oDocumentoReferencia.SerieDocumento.Value = oDocumentoSap.SerieRef.ToUpper() + "-" + oDocumentoSap.NroDocRef;
                oDocumentoReferencia.TipoDocumento.Value = oDocumentoSap.IdTipoDocRef;

                oDocumentoCarvajal.AddReferencia(oDocumentoReferencia);

                #endregion

                #region NOT-Notas

                //NOTAS
                //GLOSA SAC
                var oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0004" },
                    Descripcion = { Value = @"0-800-20102 / 411-4696 / sac@pecsa.com.pe" }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);

                //Dirección Fiscal del Cliente
                oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0008" },
                    Descripcion = { Value = oDocumentoSap.Cliente.Direccion }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);

                //Dirección de Entrega
                oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0137" },
                    Descripcion = { Value = oDocumentoSap.DireccionEntrega }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);

                //Dirección de Emisión (Planta)
                oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0135" },
                    Descripcion = { Value = oDocumentoSap.Planta.Direccion }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);

                //Planta de Origen
                oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0002" },
                    Descripcion = { Value = oDocumentoSap.Planta.Descripcion }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);

                //Web de PECSA
                oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0006" },
                    Descripcion = { Value = @"www.pecsa.com.pe" }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);

                //Código del Cliente
                oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0007" },
                    Descripcion = { Value = oDocumentoSap.IdCliente }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);

                //Monto en Letras
                if (oDocumentoSap.Total != null)
                {
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "1000" },
                        Descripcion =
                        {
                            Value =
                                Util.NumeroALetras((double)oDocumentoSap.Total,
                                                   oDocumentoSap.IdMoneda)
                        }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }

                //Hora de Emisión
                oNotaDocumento = new Nota
                {
                    Codigo = { Value = "0142" },
                    Descripcion = { Value = DateTime.Now.ToString("hh:mm tt") }
                };
                oDocumentoCarvajal.AddNota(oNotaDocumento);

                //Fecha de Vencimiento
                if (oDocumentoSap.FechaVencimiento != null)
                {
                    oNotaDocumento = new Nota
                    {
                        Codigo = { Value = "0143" },
                        Descripcion = { Value = ((DateTime)oDocumentoSap.FechaVencimiento).ToShortDateString().Replace('/', '-') }
                    };
                    oDocumentoCarvajal.AddNota(oNotaDocumento);
                }


                #endregion

                #region LEYENDAS

                //TOTAL EN LETRAS
                if (oDocumentoSap.Total != null)
                {
                    var oLeyenda = new Leyenda();
                    oLeyenda.Codigo.Value = "1000";
                    oLeyenda.Descripcion.Value = Util.NumeroALetras((double)oDocumentoSap.Total,
                                                                    oDocumentoSap.IdMoneda);

                    oDocumentoCarvajal.AddLeyenda(oLeyenda);
                }

                #endregion

                #region IMP-Impuestos

                //IGV
                sMonto = oDocumentoSap.MotivoVenta.Gratuito.Equals("1")
                             ? "0.00"
                             : ((double)oDocumentoSap.Igv).ToString("###0.00");
                var oImpuesto = new IMP();
                oImpuesto.ImporteTotal.Value = sMonto;
                oImpuesto.ImporteExplicito.Value = sMonto;
                oImpuesto.TotalVenta.Value = oDocumentoCarvajal.CAB.TotalValorVenta.Value;
                oImpuesto.CatalogoSunat.Value = "1000";
                oImpuesto.NombreTributo.Value = "IGV";
                oImpuesto.CodigoTipoTributo.Value = "VAT";

                oDocumentoCarvajal.AddImpuesto(oImpuesto);

                #endregion

                #region ITE - Detalles

                var nItem = 0;

                foreach (var documentoDetInfo in oDocumentoSap.Detalle)
                {
                    var oItem = new ITE_Nota();
                    nItem += 1;

                    //1 - Número de Item
                    oItem.ITE.NumeroItem.Value = nItem.ToString();

                    //2 - Unidad de Medida
                    oItem.ITE.UnidadMedida.Value = documentoDetInfo.Articulo.UnidadMedida;

                    //3 - Cantidad
                    sMonto = ((double)documentoDetInfo.Cantidad).ToString("###0.00");
                    oItem.ITE.CantidadUnidad.Value = sMonto;

                    //4 - Valor Venta
                    sMonto = ((double)(documentoDetInfo.SubTotal)).ToString("###0.00");
                    oItem.ITE.ValorVenta.Value = sMonto;

                    //5 - Descripción del Producto
                    oItem.ITE.DescripcionVenta.Value = documentoDetInfo.Articulo.Descripcion.Replace(",", "") + "~~~" +
                                                        ((double)documentoDetInfo.Total).ToString("###0.00");

                    //6 - Valor Unitario (Precio sin impuestos)
                    sMonto = ((double)documentoDetInfo.PrecioUnitario).ToString("###0.00");
                    oItem.ITE.ValorUnitario.Value = sMonto;

                    //7 - Código del Producto del Emisor
                    oItem.ITE.CodigoEmisor.Value = documentoDetInfo.IdArticulo;

                    //8 - Código del Producto SUNAT - CUBSO
                    if (!String.IsNullOrEmpty(documentoDetInfo.Articulo.CodigoCubso) && !documentoDetInfo.Articulo.CodigoCubso.Trim().Equals("0"))
                        oItem.ITE.CodigoProductoSUNAT.Value = documentoDetInfo.Articulo.CodigoCubso;

                    //9 - Código del Producto GS1
                    if (!String.IsNullOrEmpty(documentoDetInfo.Articulo.CodigoGs1) && !documentoDetInfo.Articulo.CodigoGs1.Trim().Equals("0"))
                        oItem.ITE.CodigoProductoGS1.Value = documentoDetInfo.Articulo.CodigoGs1;

                    //10 - Código del Producto GTIN
                    if (!String.IsNullOrEmpty(documentoDetInfo.Articulo.CodigoGtin) && !documentoDetInfo.Articulo.CodigoGtin.Trim().Equals("0"))
                        oItem.ITE.TipoEstructuraGTIN.Value = documentoDetInfo.Articulo.CodigoGtin;

                    //11 - Código de Precio
                    oItem.ITE.CodigoPrecio.Value = "01";

                    //12 - Valor Precio (Precio con impuestos)
                    sMonto = ((double)documentoDetInfo.PrecioUnitarioIgv).ToString("###0.0000");
                    oItem.ITE.Precio.Value = sMonto;

                    #region IIM - Impuesto del ITEM

                    //IGV
                    if (documentoDetInfo.Igv == null)
                        documentoDetInfo.Igv = 0;



                    //if (documentoDetInfo.Igv > 0)
                    //{
                    var oItemImpuesto = new IIM();
                    //5 - Tipo de Afectación IGV
                    //oItemImpuesto.TipoAfectacionIGV.Value = documentoDetInfo.Igv == 0 ? "20" : "10";
                    oItemImpuesto.TipoAfectacionIGV.Value = "10";
                    sMonto = ((double)documentoDetInfo.Igv).ToString("###0.00");


                    //1 - Importe Total y 3 - Importe Explícito
                    oItemImpuesto.ImporteTotalTributo.Value = sMonto;
                    oItemImpuesto.ImporteExplicitoTributar.Value = sMonto;

                    //2 - Base Imponible sobre la que se aplica la tasa
                    sMonto = ((double)documentoDetInfo.SubTotal).ToString("###0.00");
                    oItemImpuesto.BaseImponible.Value = sMonto;

                    //4 - Porcentaje que se aplica a la base imponible
                    oItemImpuesto.PorcentajeAplicadoBaseImponible.Value = "18";

                    //7 - Categoría de Tributo
                    oItemImpuesto.CategoriaTributoSUNAT.Value = "1000";

                    //8 - Nombre del Tributo
                    oItemImpuesto.NombreTributo.Value = "IGV";

                    //9 - Código del tipo de Tributo
                    oItemImpuesto.CodigoTipoTributo.Value = "VAT";

                    if (documentoDetInfo.Igv == 0)
                    {
                        if (oDocumentoSap.IdTipoDoc == "08" && oDocumentoSap.IdTipoFacturacion == "03")
                        {
                            oItemImpuesto.TipoAfectacionIGV.Value = "20";
                            oItemImpuesto.PorcentajeAplicadoBaseImponible.Value = "0.00";
                        }
                        oItemImpuesto.CategoriaTributoSUNAT.Value = "9997";
                        oItemImpuesto.NombreTributo.Value = "EXO";
                        oItemImpuesto.CodigoTipoTributo.Value = "VAT";
                    }

                    oItem.AddIIM(oItemImpuesto);
                    //}
                    //else
                    //{
                    //    //5 - Tipo de Afectación IGV
                    //    oItemImpuesto.TipoAfectacionIGV.Value = documentoDetInfo.Igv == 0 ? "20" : "10";
                    //    sMonto = ((double)documentoDetInfo.Igv).ToString("###0.00");


                    //    //1 - Importe Total y 3 - Importe Explícito
                    //    oItemImpuesto.ImporteTotalTributo.Value = sMonto;
                    //    oItemImpuesto.ImporteExplicitoTributar.Value = sMonto;

                    //    //2 - Base Imponible sobre la que se aplica la tasa
                    //    sMonto = ((double)documentoDetInfo.SubTotal).ToString("###0.00");
                    //    oItemImpuesto.BaseImponible.Value = sMonto;

                    //    //4 - Porcentaje que se aplica a la base imponible
                    //    oItemImpuesto.PorcentajeAplicadoBaseImponible.Value = "18";

                    //    //7 - Categoría de Tributo
                    //    oItemImpuesto.CategoriaTributoSUNAT.Value = "9998";

                    //    //8 - Nombre del Tributo
                    //    oItemImpuesto.NombreTributo.Value = "INA";

                    //    //9 - Código del tipo de Tributo
                    //    oItemImpuesto.CodigoTipoTributo.Value = "VAT";
                    //}




                    #endregion


                    oDocumentoCarvajal.AddItem(oItem);
                }


                #region FISE - SISE

                if (oDocumentoSap.MontoFise + oDocumentoSap.MontoSise > 0)
                {
                    //Detalle FISE
                    //if (oDocumentoSap.MontoFise > 0)
                    //{
                    //    var oItemFise = new ITE_Nota();

                    //    oItemFise.ITE.NumeroItem.Value = (nItem + 1).ToString(CultureInfo.InvariantCulture);
                    //    oItemFise.ITE.UnidadMedida.Value = "NIU";

                    //    //Cantidad
                    //    oItemFise.ITE.CantidadUnidad.Value = "0.00";

                    //    //Valor Precio
                    //    sMonto = ((double)(oDocumentoSap.MontoFise)).ToString("###0.00");
                    //    oItemFise.ITE.ValorVenta.Value = "0.00";
                    //    oItemFise.ITE.DescripcionVenta.Value = "FISE - LEY 29852~~~" + sMonto;

                    //    //Valor Unitario
                    //    oItemFise.ITE.ValorUnitario.Value = "0.00";

                    //    oItemFise.ITE.CodigoEmisor.Value = "0";
                    //    oItemFise.ITE.CodigoPrecio.Value = !oDocumentoSap.MotivoVenta.Gratuito.Equals("1") ? "01" : "02";

                    //    //Precio Unitario
                    //    oItemFise.ITE.Precio.Value = "0.00";

                    //    //Se crea el Impuesto de la línea de detalle

                    //    var oItemImpuestoIgv = new IIM()
                    //    {
                    //        CategoriaTributoSUNAT = { Value = "1000" },
                    //        NombreTributo = { Value = "IGV" },
                    //        CodigoTipoTributo = { Value = "VAT" },
                    //        PorcentajeAplicadoBaseImponible = { Value = "18" },
                    //        TipoAfectacionIGV = { Value = "20" },
                    //        ImporteTotalTributo = { Value = "0.00" },
                    //        ImporteExplicitoTributar = { Value = "0.00" }
                    //    };
                    //    oItemFise.AddIIM(oItemImpuestoIgv);
                    //    //}

                    //    oDocumentoCarvajal.AddItem(oItemFise);
                    //}


                    //Detalle SISE
                    //if (oDocumentoSap.MontoSise > 0)
                    //{
                    //    var oItemSise = new ITE_Nota();

                    //    oItemSise.ITE.NumeroItem.Value = (nItem + 2).ToString(CultureInfo.InvariantCulture);
                    //    oItemSise.ITE.UnidadMedida.Value = "NIU";

                    //    //Cantidad
                    //    oItemSise.ITE.CantidadUnidad.Value = "0.00";

                    //    //Valor Precio
                    //    sMonto = ((double)(oDocumentoSap.MontoSise)).ToString("###0.00");
                    //    oItemSise.ITE.ValorVenta.Value = "0.00";
                    //    oItemSise.ITE.DescripcionVenta.Value = "SISE - LEY 29852---" + sMonto;

                    //    //Valor Unitario
                    //    oItemSise.ITE.ValorUnitario.Value = "0.00";

                    //    oItemSise.ITE.CodigoEmisor.Value = "0";
                    //    oItemSise.ITE.CodigoPrecio.Value = !oDocumentoSap.MotivoVenta.Gratuito.Equals("1") ? "01" : "02";

                    //    //Precio Unitario
                    //    oItemSise.ITE.Precio.Value = "0.00";

                    //    //Se crea el Impuesto de la línea de detalle

                    //    var oItemImpuestoIgv2 = new IIM()
                    //    {
                    //        CategoriaTributoSUNAT = { Value = "1000" },
                    //        NombreTributo = { Value = "IGV" },
                    //        CodigoTipoTributo = { Value = "VAT" },
                    //        PorcentajeAplicadoBaseImponible = { Value = "18" },
                    //        TipoAfectacionIGV = { Value = "20" },
                    //        ImporteTotalTributo = { Value = "0.00" },
                    //        ImporteExplicitoTributar = { Value = "0.00" }
                    //    };
                    //    oItemSise.AddIIM(oItemImpuestoIgv2);
                    //    //}

                    //    oDocumentoCarvajal.AddItem(oItemSise);
                    //}

                    #endregion


                }

                #endregion


                String sCadena = oDocumentoCarvajal.GetText();

                var oHashtag = oGeneradorHash.GetHashForNoteCdp(oDocumentoCarvajal.GetText());
                String sRutaFile = WebConfigurationManager.AppSettings["Ruta_Salida_XML_Firmado"];
                String sRutaDestino = "";

                if (oEmisor.Ruc.Equals("20259033072"))
                    sRutaDestino = WebConfigurationManager.AppSettings["RutaPecsa"];
                if (oEmisor.Ruc.Equals("20100132754"))
                    sRutaDestino = WebConfigurationManager.AppSettings["RutaPdp"];
                if (oEmisor.Ruc.Equals("20330033313"))
                    sRutaDestino = WebConfigurationManager.AppSettings["RutaPes"];

                String sTipo = "";
                switch (oDocumentoSap.IdTipoDoc)
                {
                    case "01":
                        sTipo = "01";
                        break;
                    case "03":
                        sTipo = "03";
                        break;
                    case "07":
                        sTipo = "07";
                        break;
                    case "08":
                        sTipo = "08";
                        break;
                }

                String sArchivoGenerado = oEmisor.Ruc + "_" + sTipo + "_" +
                                          oDocumentoSap.Serie + "_" + oDocumentoSap.NroDocumento.TrimStart('0') +
                                          ".xml";
                String sArchivoDestino = sArchivoGenerado.Replace('-', '_');
                File.Copy(sRutaFile + "\\" + sArchivoGenerado, sRutaDestino + "\\" + sArchivoDestino, true);

                return oHashtag;

            }
            catch (Exception ex)
            {
                return ex.Message + "-" + ex.StackTrace + "-" + ex.GetBaseException();
            }
        }

        [WebMethod]
        public EstadoInfo ConsultarEstadoDocumento(String sCompania, String sIdTipoDoc, String sSerie,
                                                   String sNroDocumento)
        {
            try
            {
                var oEstado = new Estado().Consultar(sCompania, sIdTipoDoc, sSerie, sNroDocumento);
                return oEstado;
            }
            catch (Exception ex)
            {
                return new EstadoInfo { IdEstado = "ER", Descripcion = ex.Message };
            }
        }

        [WebMethod]
        public String GenerarXmlRetencion(CompaniaInfo oEmisor, DocumentoRetencionInfo oDocumentoRetencion)
        {
            try
            {
                var oRetencion = new FEPE_Retention();
                var oGeneradorHash = new GeneratorCdp21();
                var sNumeroResolucion = "";


                if (oEmisor.Ruc.Equals("20259033072"))
                    sNumeroResolucion = "0180050001319";
                if (oEmisor.Ruc.Equals("20100132754"))
                    sNumeroResolucion = "0180050001318";
                if (oEmisor.Ruc.Equals("20330033313"))
                    sNumeroResolucion = "0180050001319";

                var sNumeroLetras = "";
                if (oDocumentoRetencion.MontoTotalRetenido != null)
                {
                    sNumeroLetras = Util.NumeroALetras((double)oDocumentoRetencion.MontoTotalRetenido,
                                                           oDocumentoRetencion.MonedaMontoRetenido);
                }

                DateTime dFechaComprobante = new DateTime(Int32.Parse(oDocumentoRetencion.FechaComprobante.Substring(0, 4)),
                    Int32.Parse(oDocumentoRetencion.FechaComprobante.Substring(5, 2)),
                    Int32.Parse(oDocumentoRetencion.FechaComprobante.Substring(8, 2)));

                //Segmento: ENCABEZADO
                var oEncabezado = new Encabezado(1, ",")
                {
                    TipoDocumento = { Value = FEPE_Document_Enums.Tipo_Documento.Retencion_20 },
                    IdentificacionEmpresaCompradora = { Value = oEmisor.Ruc },
                    IdentificacionEmpresaProveedora = { Value = oDocumentoRetencion.Proveedor.NroDocumentoIdentidad },
                    TipoAccion = { Value = "0" },
                    NumeroUnicoComprobante = { Value = oDocumentoRetencion.Serie.Trim() + "-" + oDocumentoRetencion.NumeroRetencion.Trim().TrimStart('0').PadLeft(8, '0') },
                    FechaEmisionComprobante = { Value = dFechaComprobante },
                    NumeroResolucionSunat = { Value = sNumeroResolucion },
                    ImporteTotalEnLetras = { Value = sNumeroLetras },
                    CodigoPlantilla = { Value = "CP001" },
                    FirmaDigital = {Value = "--"}
                };
                oRetencion.Encabezado = oEncabezado;

                //Segmento: INFORMACIÓN EMISOR
                var oInformacionEmisor = new InformacionEmisor(2, ",")
                {
                    NumeroDocumentoIdentificacion = { Value = oEmisor.Ruc },
                    TipoDocumentoIdentificacion = { Value = "6" },
                    RazonSocial = { Value = oEmisor.Nombre },
                    NombreComercial = { Value = oEmisor.Nombre },
                    Direccion = { Value = oEmisor.Direccion },
                    CodigoUbigeo = { Value = oEmisor.Ubigeo },
                    Departamento = { Value = oEmisor.Departamento },
                    Provincia = { Value = oEmisor.Provincia },
                    Distrito = { Value = oEmisor.Distrito },
                    PaisCodigoPostal = { Value = "PE" }
                };
                oRetencion.InformacionEmisor = oInformacionEmisor;

                //Segmento: INFORMACIÓN PROVEEDOR
                var oInformacionProveedor = new InformacionProveedor(3, ",")
                {
                    NumeroDocumentoIdentificacion = { Value = oDocumentoRetencion.Proveedor.NroDocumentoIdentidad },
                    TipoDocumentoIdentificacion = { Value = "6" },
                    RazonSocial = { Value = oDocumentoRetencion.Proveedor.RazonSocial },
                    NombreComercial = { Value = oDocumentoRetencion.Proveedor.RazonSocial },
                    Direccion = { Value = oDocumentoRetencion.Proveedor.Direccion },
                    CodigoUbigeo = { Value = oDocumentoRetencion.Proveedor.Ubigeo },
                    Departamento = { Value = oDocumentoRetencion.Proveedor.Departamento },
                    Provincia = { Value = oDocumentoRetencion.Proveedor.Provincia },
                    Distrito = { Value = oDocumentoRetencion.Proveedor.Distrito },
                    PaisCodigoPostal = { Value = "PE" }
                };
                oRetencion.InformacionProveedor = oInformacionProveedor;

                //Segmento: DATOS RETENCIÓN COMPROBANTE
                if (oDocumentoRetencion.ValorTasaRetencion != null && oDocumentoRetencion.MontoTotalRetenido != null && oDocumentoRetencion.MontoTotalPagado != null)
                {
                    var oDatosRetencionComprobante = new DatosRetencionComprobante(4, ",")
                    {
                        RegimenRetencion = { Value = oDocumentoRetencion.RegimenRetencion },
                        ValorTasaRetencion = { Value = ((double)oDocumentoRetencion.ValorTasaRetencion).ToString("###0.00") },
                        ImporteTotalRetenido = { Value = ((double)oDocumentoRetencion.MontoTotalRetenido).ToString("###0.00") },
                        MonedaImporteTotalRetenido = { Value = oDocumentoRetencion.MonedaMontoRetenido },
                        ImporteTotalPagado = { Value = ((double)oDocumentoRetencion.MontoTotalPagado).ToString("###0.00") },
                        MonedaImporteTotalPagado = { Value = oDocumentoRetencion.MonedaMontoPagado }
                    };
                    oRetencion.DatosRetencionComprobante = oDatosRetencionComprobante;
                }

                //Detalles
                foreach (var oDetalleRetencion in oDocumentoRetencion.Detalle)
                {
                    //Detalle del Comprobante
                    var oItem = new Item_Retencion();

                    var oTipoDocumento = FEPE_Document_Enums.Tipo_Documento.Factura_01;

                    //Comprobante Relacionado
                    if (oDetalleRetencion.DocumentoRelacionado.FechaEmision != null && oDetalleRetencion.DocumentoRelacionado.ImporteTotal != null)
                    {
                        DateTime dFechaDocRelacionado = new DateTime(Int32.Parse(oDetalleRetencion.DocumentoRelacionado.FechaEmision.Substring(0, 4)),
                            Int32.Parse(oDetalleRetencion.DocumentoRelacionado.FechaEmision.Substring(5, 2)),
                            Int32.Parse(oDetalleRetencion.DocumentoRelacionado.FechaEmision.Substring(8, 2)));

                        //var oTipoDocumento = FEPE_Document_Enums.Tipo_Documento.Factura_01;

                        switch (oDetalleRetencion.DocumentoRelacionado.IdTipoDocumento)
                        {
                            case "01":
                                oTipoDocumento = FEPE_Document_Enums.Tipo_Documento.Factura_01;
                                break;
                            case "07":
                                oTipoDocumento = FEPE_Document_Enums.Tipo_Documento.Nota_Credito_07;
                                break;
                            case "08":
                                oTipoDocumento = FEPE_Document_Enums.Tipo_Documento.Nota_Credito_07;
                                break;
                        }

                        var oDocumentoRelacionado = new DatosComprobanteRelacionado(1, ",")
                        {
                            TipoDocumento = { Value = oTipoDocumento },
                            NumeroDocumento = { Value = oDetalleRetencion.DocumentoRelacionado.NumeroDocumento },
                            FechaEmision = { Value = dFechaDocRelacionado },
                            ImporteTotal = { Value = ((Double)oDetalleRetencion.DocumentoRelacionado.ImporteTotal).ToString("###0.00") },
                            MonedaImporteTotal = { Value = oDetalleRetencion.DocumentoRelacionado.MonedaImporteTotal }
                        };
                        oItem.DatosComprobanteRelacionado = oDocumentoRelacionado;
                    }

                    //if (oTipoDocumento == FEPE_Document_Enums.Tipo_Documento.Factura_01)
                    //{
                    //Datos del Pago
                    if (oDetalleRetencion.DatosPago.FechaPago != null && oDetalleRetencion.DatosPago.ImportePago != null)
                    {
                        DateTime dFechaPago = new DateTime(Int32.Parse(oDetalleRetencion.DatosPago.FechaPago.Substring(0, 4)),
                            Int32.Parse(oDetalleRetencion.DatosPago.FechaPago.Substring(5, 2)),
                            Int32.Parse(oDetalleRetencion.DatosPago.FechaPago.Substring(8, 2)));


                        Double nMontoRetenido = 0.0;

                        if (oDetalleRetencion.DatosPago.MonedaPago.Equals("PEN"))
                        {
                            nMontoRetenido = (Double)oDetalleRetencion.DatosRetencion.ImporteRetenido;
                        }
                        else
                        {
                            nMontoRetenido = (Double)oDetalleRetencion.DatosRetencion.ImporteRetenido /
                                             (Double)oDetalleRetencion.TipoCambio.TipoCambio;
                        }

                        Double nMontoPago = 0.0;


                        var oDatosPago = new DatosPago(2, ",")
                        {
                            Fecha = { Value = dFechaPago },
                            Numero = { Value = oDetalleRetencion.DatosPago.NumeroPago },
                            ImporteSinRetencion = { Value = ((Double)oDetalleRetencion.DatosPago.ImportePago + nMontoRetenido).ToString("###0.00") },
                            CodigoMoneda = { Value = oDetalleRetencion.DatosPago.MonedaPago }
                        };
                        oItem.DatosPago = oDatosPago;
                    }

                    //Datos de la Retención
                    DateTime dFechaRetencion = new DateTime(Int32.Parse(oDetalleRetencion.DatosRetencion.FechaRetencion.Substring(0, 4)),
                        Int32.Parse(oDetalleRetencion.DatosRetencion.FechaRetencion.Substring(5, 2)),
                        Int32.Parse(oDetalleRetencion.DatosRetencion.FechaRetencion.Substring(8, 2)));

                    var oDatosRetencion = new DatosRetencion(3, ",")
                    {
                        ImporteRetenido = { Value = ((Double)oDetalleRetencion.DatosRetencion.ImporteRetenido).ToString("###0.00") },
                        CodigoMonedaImporteRetenido = { Value = oDetalleRetencion.DatosRetencion.MonedaImporteRetenido },
                        FechaRetenido = { Value = dFechaRetencion },
                        ImportePagado = { Value = ((Double)oDetalleRetencion.DatosRetencion.ImporteTotal).ToString("###0.00") },
                        CodigoMonedaImportePagado = { Value = "PEN" }
                    };
                    oItem.DatosRetencion = oDatosRetencion;
                    //}


                    if (oDetalleRetencion.DocumentoRelacionado.MonedaImporteTotal != "PEN")
                    {
                        DateTime dFechaTipoCambio = new DateTime(Int32.Parse(oDetalleRetencion.TipoCambio.FechaCambio.Substring(0, 4)),
                            Int32.Parse(oDetalleRetencion.TipoCambio.FechaCambio.Substring(5, 2)),
                            Int32.Parse(oDetalleRetencion.TipoCambio.FechaCambio.Substring(8, 2)));

                        //Tipo de Cambio
                        var oTipoCambio = new TipoCambio(4, ",")
                        {
                            CodigoMonedaOrigen = { Value = oDetalleRetencion.TipoCambio.MonedaOrigen },
                            CodigoMonedaDestino = { Value = oDetalleRetencion.TipoCambio.MonedaDestino },
                            ValorAplicado = { Value = ((Double)oDetalleRetencion.TipoCambio.TipoCambio).ToString("###0.000") },
                            FechaDeCambio = { Value = dFechaTipoCambio }
                        };
                        oItem.TipoCambio = oTipoCambio;
                    }
                    oRetencion.Items.AddElement(oItem);
                }

                var oHashtag = oGeneradorHash.GetHashForRetentionCdp(oRetencion);

                String sRutaFile = WebConfigurationManager.AppSettings["Ruta_Salida_XML_Firmado"];
                String sRutaDestino = "";

                if (oEmisor.Ruc.Equals("20259033072"))
                    sRutaDestino = WebConfigurationManager.AppSettings["RutaPecsaRet"];
                if (oEmisor.Ruc.Equals("20100132754"))
                    sRutaDestino = WebConfigurationManager.AppSettings["RutaPdpRet"];
                if (oEmisor.Ruc.Equals("20330033313"))
                    sRutaDestino = WebConfigurationManager.AppSettings["RutaPesRet"];

                const String sTipo = "20";

                var sArchivoGenerado = oEmisor.Ruc + "_" + sTipo + "_" +
                                          oDocumentoRetencion.Serie + "_" + oDocumentoRetencion.NumeroRetencion.TrimStart('0') +
                                          ".xml";
                File.Copy(sRutaFile + "\\" + sArchivoGenerado, sRutaDestino + "\\" + sArchivoGenerado, true);

                return oHashtag;
            }
            catch (Exception ex)
            {
                return ex.Message + "-" + ex.StackTrace + "-" + ex.GetBaseException();
            }

        }

        [WebMethod]
        public String GenerarXmlAnulados(CompaniaInfo oEmisor, List<DocumentoSapInfo> oDocumentoPecsa)
        {
            try
            {

                var oGeneradorCancel = new GeneratorCancel(oEmisor.Ruc);
                foreach (var documentoInfo in oDocumentoPecsa)
                {
                    var oTipoDocumento = new Constantes.CDP_TIPOS();
                    if (documentoInfo.IdTipoDoc.Equals("01"))
                        oTipoDocumento = Constantes.CDP_TIPOS.FACTURA;
                    if (documentoInfo.IdTipoDoc.Equals("03"))
                        oTipoDocumento = Constantes.CDP_TIPOS.BOLETA;
                    if (documentoInfo.IdTipoDoc.Equals("07"))
                        oTipoDocumento = Constantes.CDP_TIPOS.NOTA_DE_CREDITO;
                    if (documentoInfo.IdTipoDoc.Equals("08"))
                        oTipoDocumento = Constantes.CDP_TIPOS.NOTA_DE_DEBITO;

                    oGeneradorCancel.AddCancel(oTipoDocumento, documentoInfo.Serie, Int32.Parse(documentoInfo.NroDocumento), documentoInfo.Observacion);
                }

                if (oDocumentoPecsa.Count > 0)
                {

                    String sRutaDestino = "";

                    if (oEmisor.Ruc.Equals("20259033072"))
                        sRutaDestino = WebConfigurationManager.AppSettings["RutaPecsaNa"];
                    if (oEmisor.Ruc.Equals("20100132754"))
                        sRutaDestino = WebConfigurationManager.AppSettings["RutaPdpNa"];
                    if (oEmisor.Ruc.Equals("20330033313"))
                        sRutaDestino = WebConfigurationManager.AppSettings["RutaPesNa"];

                    oGeneradorCancel.GenerateXml(sRutaDestino);
                }

                return "Archivo de Bajas generado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public String GenerarXmlPercepcion(CompaniaInfo oEmisor, PercepcionInfo oPercepcion)
        {
            try
            {
                var oPercepcionCarvajal = new FEPE_Perception();
                var oGeneradorHash = new GeneratorCdp();
                var sNumeroResolucion = "";
                var sMonto = "";


                if (oEmisor.Ruc.Equals("20259033072") || oEmisor.Ruc.Equals("20554545743") || oEmisor.Ruc.Equals("20127765279"))
                    sNumeroResolucion = "0180050001319";
                if (oEmisor.Ruc.Equals("20100132754"))
                    sNumeroResolucion = "0180050001318";
                if (oEmisor.Ruc.Equals("20330033313"))
                    sNumeroResolucion = "0180050001319";

                var sNumeroLetras = "";
                if (oPercepcion.ImporteTotalPercibido != null)
                {
                    sNumeroLetras = Util.NumeroALetras((double)oPercepcion.ImporteTotalPercibido,
                                                           oPercepcion.MonedaImporteTotalPercibido);
                }

                #region Encabezado

                var oEncabezado = new Encabezado(1, ",");
                oEncabezado.TipoDocumento.Value = FEPE_Document_Enums.Tipo_Documento.Percepcion_40;
                oEncabezado.IdentificacionEmpresaCompradora.Value = oPercepcion.Cliente.NroDocumentoIdentidad;
                oEncabezado.IdentificacionEmpresaProveedora.Value = oEmisor.Ruc;
                oEncabezado.TipoAccion.Value = "0";
                oEncabezado.NumeroUnicoComprobante.Value = oPercepcion.SeriePercepcion.Trim() + "-" +
                                                           oPercepcion.NumeroPercepcion.Trim()
                                                                      .TrimStart('0')
                                                                      .PadLeft(8, '0');

                var dFechaComprobante = new DateTime(Int32.Parse(oPercepcion.FechaPercepcion.Substring(0, 4)),
                                                     Int32.Parse(oPercepcion.FechaPercepcion.Substring(5, 2)),
                                                     Int32.Parse(oPercepcion.FechaPercepcion.Substring(8, 2)));

                oEncabezado.FechaEmisionComprobante.Value = dFechaComprobante;
                oEncabezado.NumeroResolucionSunat.Value = sNumeroResolucion;

                //Validar Código de Plantilla
                oEncabezado.CodigoPlantilla.Value = "CP001";
                oEncabezado.FirmaDigital.Value = "--";

                oEncabezado.CodigoNota.Value = "P040";
                oEncabezado.ImporteTotalEnLetras.Value = sNumeroLetras;
                //oEncabezado.HoraEmision.Value = DateTime.Now.ToString("hh:mm:ss");

                oPercepcionCarvajal.Encabezado = oEncabezado;

                #endregion

                #region Emisor

                var oInformacionEmisor = new InformacionEmisor(2, ",");
                oInformacionEmisor.NumeroDocumentoIdentificacion.Value = oEmisor.Ruc;
                oInformacionEmisor.TipoDocumentoIdentificacion.Value = "6";
                oInformacionEmisor.RazonSocial.Value = oEmisor.Nombre;
                oInformacionEmisor.NombreComercial.Value = oEmisor.Nombre;
                oInformacionEmisor.CodigoUbigeo.Value = oEmisor.Ubigeo;
                oInformacionEmisor.Direccion.Value = oEmisor.Direccion;
                oInformacionEmisor.Departamento.Value = oEmisor.Departamento;
                oInformacionEmisor.Provincia.Value = oEmisor.Provincia;
                oInformacionEmisor.Distrito.Value = oEmisor.Distrito;
                oInformacionEmisor.PaisCodigoPostal.Value = oEmisor.Pais;

                oPercepcionCarvajal.InformacionEmisor = oInformacionEmisor;

                #endregion

                #region Cliente

                var oInformacionCliente = new InformacionCliente(3, ",");
                oInformacionCliente.NumeroDocumentoIdentificacion.Value = oPercepcion.Cliente.NroDocumentoIdentidad;
                oInformacionCliente.TipoDocumentoIdentificacion.Value = "6";
                oInformacionCliente.RazonSocial.Value = oPercepcion.Cliente.RazonSocial;
                oInformacionCliente.NombreComercial.Value = oPercepcion.Cliente.RazonSocial;
                oInformacionCliente.Direccion.Value = oPercepcion.Cliente.Direccion;
                oInformacionCliente.Departamento.Value = oPercepcion.Cliente.IdDepartamento;
                oInformacionCliente.Provincia.Value = oPercepcion.Cliente.IdProvincia;
                oInformacionCliente.Distrito.Value = oPercepcion.Cliente.IdDistrito;
                oInformacionCliente.PaisCodigoPostal.Value = "PE";

                oPercepcionCarvajal.InformacionCliente = oInformacionCliente;

                #endregion

                #region Datos Comprobante Percepción

                var oDatosPercepcionComprobante = new DatosPercepcionComprobante(4, ",");
                oDatosPercepcionComprobante.RegimenPercepcion.Value = oPercepcion.RegimenPercepcion;

                if (oPercepcion.ValorTasaPercepcion != null)
                {
                    sMonto = ((double)oPercepcion.ValorTasaPercepcion).ToString("###0.00");
                }

                oDatosPercepcionComprobante.ValorTasaPercepcion.Value = sMonto;

                if (oPercepcion.ImporteTotalPercibido != null)
                    sMonto = ((double)oPercepcion.ImporteTotalPercibido).ToString("###0.00");

                oDatosPercepcionComprobante.ImporteTotalPercibido.Value = sMonto;
                oDatosPercepcionComprobante.MonedaImporteTotalPercibido.Value = oPercepcion.MonedaImporteTotalPercibido;

                if (oPercepcion.ImporteTotalCobrado != null)
                    sMonto = ((double)oPercepcion.ImporteTotalCobrado).ToString("###0.00");

                oDatosPercepcionComprobante.ImporteTotalCobrado.Value = sMonto;
                oDatosPercepcionComprobante.MonedaImporteTotalCobrado.Value = oPercepcion.MonedaImporteTotalCobrado;

                oDatosPercepcionComprobante.Observaciones.Value = oPercepcion.Observaciones;
                //oDatosPercepcionComprobante.MontoRedondeoImporteTotal.Value = "0.00";
                //oDatosPercepcionComprobante.MonedaMontoRedondeoImporteTotal.Value =
                //    oPercepcion.MonedaImporteTotalCobrado;

                oPercepcionCarvajal.DatosPercepcionComprobante = oDatosPercepcionComprobante;

                #endregion


                foreach (var detallePercepcionInfo in oPercepcion.Detalle)
                {
                    var oItemPercepcion = new Item_Percepcion();

                    #region Comprobante Relacionado

                    var oDocumentoRelacionado = new DatosComprobanteRelacionado(1, ",");
                    oDocumentoRelacionado.TipoDocumento.Value = FEPE_Document_Enums.Tipo_Documento.Factura_01;
                    oDocumentoRelacionado.NumeroDocumento.Value =
                        detallePercepcionInfo.ComprobanteRelacionado.SerieDocumento.Trim() + "-" +
                        detallePercepcionInfo.ComprobanteRelacionado.NumeroDocumento.Trim()
                                             .TrimStart('0')
                                             .PadLeft(8, '0');

                    var dFechaDocumento =
                        new DateTime(
                            Int32.Parse(detallePercepcionInfo.ComprobanteRelacionado.FechaEmision.Substring(0, 4)),
                            Int32.Parse(detallePercepcionInfo.ComprobanteRelacionado.FechaEmision.Substring(5, 2)),
                            Int32.Parse(detallePercepcionInfo.ComprobanteRelacionado.FechaEmision.Substring(8, 2)));

                    oDocumentoRelacionado.FechaEmision.Value = dFechaDocumento;

                    if (detallePercepcionInfo.ComprobanteRelacionado.ImporteTotal != null)
                        sMonto = ((double)detallePercepcionInfo.ComprobanteRelacionado.ImporteTotal).ToString("###0.00");
                    oDocumentoRelacionado.ImporteTotal.Value = sMonto;
                    oDocumentoRelacionado.MonedaImporteTotal.Value =
                        detallePercepcionInfo.ComprobanteRelacionado.MonedaDocumento;

                    oItemPercepcion.DatosComprobanteRelacionado = oDocumentoRelacionado;

                    #endregion

                    #region Cobro

                    var oDatosCobro = new DatosCobro(2, ",");

                    var dFechaCobro =
                        new DateTime(
                            Int32.Parse(detallePercepcionInfo.CobroPercepcion.FechaCobro.Substring(0, 4)),
                            Int32.Parse(detallePercepcionInfo.CobroPercepcion.FechaCobro.Substring(5, 2)),
                            Int32.Parse(detallePercepcionInfo.CobroPercepcion.FechaCobro.Substring(8, 2)));

                    oDatosCobro.Fecha.Value = dFechaCobro;
                    oDatosCobro.Numero.Value = detallePercepcionInfo.CobroPercepcion.NumeroCobro;

                    if (detallePercepcionInfo.CobroPercepcion.ImporteCobroSinPerc != null)
                        sMonto = ((double)detallePercepcionInfo.CobroPercepcion.ImporteCobroSinPerc).ToString("###0.00");

                    oDatosCobro.ImporteSinPercepcion.Value = sMonto;
                    oDatosCobro.CodigoMoneda.Value = detallePercepcionInfo.CobroPercepcion.MonedaCobro;

                    oItemPercepcion.DatosCobro = oDatosCobro;

                    #endregion

                    #region Datos de Percepción

                    var oDatosPercepcion = new DatosPercepcion(3, ",");

                    if (detallePercepcionInfo.DatosPercepcion.ImportePercibido != null)
                        sMonto = ((double)detallePercepcionInfo.DatosPercepcion.ImportePercibido).ToString("###0.00");

                    oDatosPercepcion.ImportePercibido.Value = sMonto;
                    oDatosPercepcion.CodigoMonedaImportePercibido.Value =
                        detallePercepcionInfo.DatosPercepcion.MonedaImportePercibido;

                    var dFechaPercepcion =
                        new DateTime(
                            Int32.Parse(detallePercepcionInfo.DatosPercepcion.FechaPercepcion.Substring(0, 4)),
                            Int32.Parse(detallePercepcionInfo.DatosPercepcion.FechaPercepcion.Substring(5, 2)),
                            Int32.Parse(detallePercepcionInfo.DatosPercepcion.FechaPercepcion.Substring(8, 2)));

                    oDatosPercepcion.FechaPercepcion.Value = dFechaPercepcion;


                    if (detallePercepcionInfo.DatosPercepcion.ImporteTotalCobrado != null)
                        sMonto = ((double)detallePercepcionInfo.DatosPercepcion.ImporteTotalCobrado).ToString("###0.00");
                    oDatosPercepcion.ImporteCobrar.Value = sMonto;
                    oDatosPercepcion.CodigoMonedaImportePercibido.Value =
                        detallePercepcionInfo.DatosPercepcion.MonedaImporteTotalCobrado;

                    oItemPercepcion.DatosPercepcion = oDatosPercepcion;

                    #endregion


                    if (detallePercepcionInfo.ComprobanteRelacionado.MonedaDocumento != "PEN")
                    {
                        DateTime dFechaTipoCambio = new DateTime(Int32.Parse(detallePercepcionInfo.TipoCambio.FechaCambio.Substring(0, 4)),
                            Int32.Parse(detallePercepcionInfo.TipoCambio.FechaCambio.Substring(5, 2)),
                            Int32.Parse(detallePercepcionInfo.TipoCambio.FechaCambio.Substring(8, 2)));

                        //Tipo de Cambio
                        var oTipoCambio = new TipoCambio(4, ",")
                        {
                            CodigoMonedaOrigen = { Value = detallePercepcionInfo.TipoCambio.MonedaOrigen },
                            CodigoMonedaDestino = { Value = detallePercepcionInfo.TipoCambio.MonedaDestino },
                            ValorAplicado = { Value = ((Double)detallePercepcionInfo.TipoCambio.TipoCambio).ToString("###0.000") },
                            FechaDeCambio = { Value = dFechaTipoCambio }
                        };
                        oItemPercepcion.TipoCambio = oTipoCambio;
                    }

                    oPercepcionCarvajal.Items.AddElement(oItemPercepcion);

                }

                var oHashtag = oGeneradorHash.GetHashForPerceptionCdp(oPercepcionCarvajal);

                String sRutaFile = WebConfigurationManager.AppSettings["Ruta_Salida_XML_Firmado"];
                String sRutaDestino = "";

                if (oEmisor.Ruc.Equals("20259033072"))
                    sRutaDestino = WebConfigurationManager.AppSettings["RutaPecsaPer"];
                if (oEmisor.Ruc.Equals("20554545743")) //PRIMAX
                    sRutaDestino = WebConfigurationManager.AppSettings["RutaPecsaPer"];
                if (oEmisor.Ruc.Equals("20127765279")) //COESTI
                    sRutaDestino = WebConfigurationManager.AppSettings["RutaPecsaPer"];
                if (oEmisor.Ruc.Equals("20100132754"))
                    sRutaDestino = WebConfigurationManager.AppSettings["RutaPdpPer"];
                if (oEmisor.Ruc.Equals("20330033313"))
                    sRutaDestino = WebConfigurationManager.AppSettings["RutaPesPer"];

                const String sTipo = "40";

                var sArchivoGenerado = oEmisor.Ruc + "_" + sTipo + "_" +
                                          oPercepcion.SeriePercepcion + "_" + oPercepcion.NumeroPercepcion.TrimStart('0') +
                                          ".xml";
                File.Copy(sRutaFile + "\\" + sArchivoGenerado, sRutaDestino + "\\" + sArchivoGenerado, true);

                return oHashtag;

            }
            catch (Exception ex)
            {
                return ex.Message + "-" + ex.StackTrace + "-" + ex.GetBaseException();
            }
        }
    }
}
