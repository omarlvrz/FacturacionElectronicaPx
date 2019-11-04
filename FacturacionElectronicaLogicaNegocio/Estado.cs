using FacturacionElectronicaEntidades;

namespace FacturacionElectronicaLogicaNegocio
{
    public class Estado
    {
        private static readonly FacturacionElectronicaInterfaces.IEstado DalEstado = new FacturacionElectronicaAccesoDatos.Estado();

        public EstadoInfo Consultar(string sEmisor, string sIdTipoDoc, string sSerie, string sNumero)
        {
            return DalEstado.Consultar(sEmisor, sIdTipoDoc, sSerie, sNumero);
        }
    }
}
