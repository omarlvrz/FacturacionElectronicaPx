using System;
using System.Web.Services;
using FacturacionElectronicaEntidades;
using CarvajalSrv;
using System.ServiceModel;
using System.Web.Configuration;

namespace wsConsultaFepe
{
    /// <summary>
    /// Descripción breve de wsConsultaFepe
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsConsultaFepe : System.Web.Services.WebService
    {

        [WebMethod]
        public EstadoInfo ConsultarEstadoDocumento(string sCompania, string sIdTipoDoc, string sSerie,
                                                   string sNroDocumento)
        {
            try
            {

                var sUsuarioWs = "";
                var sPwdWs = "";
                String sTipoDocCrv = "";

                //Usuarios de Web Service
                switch (sCompania)
                {
                    //PRIMAX
                    case "20554545743":
                        sUsuarioWs = WebConfigurationManager.AppSettings["UsuarioPrimax"];
                        sPwdWs = WebConfigurationManager.AppSettings["PwdPrimax"];
                        break;
                    //COESTI
                    case "20127765279":
                        sUsuarioWs = WebConfigurationManager.AppSettings["UsuarioCoesti"];
                        sPwdWs = WebConfigurationManager.AppSettings["PwdCoesti"];
                        break;
                    //JOVEME
                    case "20512767011":
                        sUsuarioWs = WebConfigurationManager.AppSettings["UsuarioJoveme"];
                        sPwdWs = WebConfigurationManager.AppSettings["PwdJoveme"];
                        break;
                    //CODESA
                    case "20602544002":
                        sUsuarioWs = WebConfigurationManager.AppSettings["UsuarioCodesa"];
                        sPwdWs = WebConfigurationManager.AppSettings["PwdCodesa"];
                        break;
                }

                //Tipo de Documento
                if (sIdTipoDoc.Equals("01"))
                {
                    sTipoDocCrv = "FA";
                }
                else
                {
                    if (sIdTipoDoc.Equals("03"))
                    {
                        sTipoDocCrv = "BO";
                    }
                    else
                    {
                        if (sIdTipoDoc.Equals("07"))
                        {
                            sTipoDocCrv = "NC";
                        }
                        else
                        {
                            if (sIdTipoDoc.Equals("08"))
                            {
                                sTipoDocCrv = "ND";
                            }
                            else
                            {
                                if (sIdTipoDoc.Equals("20"))
                                {
                                    sTipoDocCrv = "20";
                                }
                                else
                                {
                                    if (sIdTipoDoc.Equals("40"))
                                    {
                                        sTipoDocCrv = "40";
                                    }
                                }
                            }
                        }
                                    
                    }
                }

                invoiceServiceClient _proxyConsultas = new invoiceServiceClient();
                var behavior = new PasswordDigestBehavior(sUsuarioWs, sPwdWs);
                _proxyConsultas.Endpoint.EndpointBehaviors.Add(behavior);

                var res = _proxyConsultas.DocumentStatusByNumber(new DocumentStatusByNumberRequest()
                {
                    companyId = sCompania,
                    accountId = "ACCOUNT000",
                    documentType = sTipoDocCrv,
                    documentPrefix = sSerie,
                    documentNumber = sNroDocumento
                });

                var oEstadoDocumento = new EstadoInfo();


                if (res != null)
                {

                    switch (res.legalStatus)
                    {
                        case "ACCEPTED":
                            oEstadoDocumento.IdEstado = "ACC";
                            break;
                        case "ACCEPTED_WITH_OBSERVATIONS":
                            oEstadoDocumento.IdEstado = "ACCWO";
                            break;
                        case "INCIDENT":
                            oEstadoDocumento.IdEstado = "INC";
                            break;
                        case "EXCEPTION":
                            oEstadoDocumento.IdEstado = "EXC";
                            break;

                    }

                    //oEstadoDocumento.IdEstado = res.legalStatus;
                    oEstadoDocumento.Descripcion = res.governmentResponseDescription;
                    return oEstadoDocumento;
                }
                oEstadoDocumento.IdEstado = "NF";
                oEstadoDocumento.Descripcion = "Documento no encontrado en Carvajal";

                return oEstadoDocumento;



            }
            catch (FaultException<InvoiceServiceFault> ex)
            {
                return new EstadoInfo { IdEstado = "ER", Descripcion = ex.Detail.statusCode.ToString() + " - " + ex.Detail.errorMessage.ToString() };
            }
            catch (Exception ex)
            {
                return new EstadoInfo { IdEstado = "ER", Descripcion = ex.Message };
            }




        }
    }
}
