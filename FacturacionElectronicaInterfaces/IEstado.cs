using System;
using FacturacionElectronicaEntidades;

namespace FacturacionElectronicaInterfaces
{
    public interface IEstado
    {
        EstadoInfo Consultar(String sEmisor, String sIdTipoDoc, String sSerie, String sNumero);
    }
}
