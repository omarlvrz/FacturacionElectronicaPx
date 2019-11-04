using System;
using System.Data;
using System.Data.SqlClient;
using FacturacionElectronicaBdUtil;
using FacturacionElectronicaEntidades;
using FacturacionElectronicaInterfaces;
using System.Configuration;

namespace FacturacionElectronicaAccesoDatos
{
    public class Estado : IEstado
    {
        public EstadoInfo Consultar(string sEmisor, string sIdTipoDoc, string sSerie, string sNumero)
        {
            string sConexion;
            sConexion = sEmisor.Equals("20330033313")
                            ? (ConfigurationManager.AppSettings["FlagProduccion"].Equals("1")
                                   ? SqlHelper.ConnectionFepePes
                                   : SqlHelper.ConnectionFepedev)
                            : (ConfigurationManager.AppSettings["FlagProduccion"].Equals("1")
                                   ? SqlHelper.ConnectionFepePecsa
                                   : SqlHelper.ConnectionFepedev);

            var sqlParm = new SqlParameter[4];
            var oEstadoDoc = new EstadoInfo();

            sqlParm[0] = new SqlParameter("@Ruc_Emisor", SqlDbType.VarChar) { Value = sEmisor };
            sqlParm[1] = new SqlParameter("@Id_Tipo_Doc", SqlDbType.VarChar) { Value = sIdTipoDoc };
            sqlParm[2] = new SqlParameter("@Serie", SqlDbType.VarChar) { Value = sSerie };
            sqlParm[3] = new SqlParameter("@Numero", SqlDbType.VarChar) { Value = sNumero };

            using (var drd = SqlHelper.ExecuteReader(sConexion, CommandType.StoredProcedure, "dbo.SAP_SP_FEPE_CONSULTAR_DOCUMENTO", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        if (drd.Read())
                        {
                            oEstadoDoc.IdEstado = drd.GetValue(0).ToString();
                            oEstadoDoc.Descripcion = drd.GetString(1).Trim();
                        }
                    }
                }
            }

            if (String.IsNullOrEmpty(oEstadoDoc.IdEstado))
            {
                oEstadoDoc = new EstadoInfo { IdEstado = "0", Descripcion = "Documento no encontrado en Carvajal" };
            }

            return oEstadoDoc;
        }
    }
}
