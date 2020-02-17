using System;
using System.Collections.Generic;

namespace wsFacturacionElectronica
{
    public class Util
    {
        public static Boolean EsNumerico(string sValor)
        {
            const string strRegex = @"^[+-]?\d+\.?\d*$";
            return System.Text.RegularExpressions.Regex.IsMatch(sValor, strRegex);
        }

        public static bool EsEnteroPositivo(string sValor)
        {
            const string strRegex = "[^0-9]";
            return !System.Text.RegularExpressions.Regex.IsMatch(sValor, strRegex);
        }

        //public static void MarcarDesmarcarCheckedBoxList(CheckedListBox chklb, Boolean bMarca)
        //{
        //    for (int i = 0; i <= chklb.Items.Count - 1; i++)
        //    {
        //        chklb.SetItemChecked(i, bMarca);
        //    }
        //}

        public static IEnumerable<DateTime> DiasEntreFechas(DateTime dFechaInicio, DateTime dFechaFin)
        {
            var oFechas = new List<DateTime>();
            for (var day = dFechaInicio.Date; day <= dFechaFin; day = day.AddDays(1))
                oFechas.Add(day);

            return oFechas;
        }

        public static DateTime MaxHora(DateTime dFecha)
        {
            return new DateTime(dFecha.Year, dFecha.Month, dFecha.Day, 23, 59, 59);
        }

        public static DateTime MinHora(DateTime dFecha)
        {
            return new DateTime(dFecha.Year, dFecha.Month, dFecha.Day, 0, 0, 0);
        }

        public static String NumeroALetras(Double nMonto, String sMoneda)
        {
            var sMonedaLetras = "";
            if (sMoneda.Equals("S/.") || sMoneda.Equals("PEN"))
                sMonedaLetras = "SOLES";
            if (sMoneda.Equals("$") || sMoneda.Equals("USD"))
                sMonedaLetras = "DÓLARES AMERICANOS";

            var sLetras = "";
            var sEntero = "";
            var sDecimal = "";
            var sFlag = "N";
            var sMonto = nMonto.ToString("###0.00");

            //********Declara variables de tipo sEntero***********
            Int32 y;

            //**********Número Negativo***********
            if (sMonto.Substring(1, 1).Equals("-"))
            {
                sMonto = sMonto.Substring(2, sMonto.Length - 1);
                sLetras = "MENOS";
            }

            //**********Si tiene ceros a la izquierda*************
            sMonto = sMonto.TrimStart('0');
            if (sMonto.Equals("0"))
                sLetras = "";

            //*********Dividir parte entera y decimal************
            for (y = 0; y < sMonto.Length; y++)
            {
                if (sMonto.Substring(y, 1).Equals("."))
                {
                    sFlag = "S";
                }
                else
                {
                    if (sFlag.Equals("N"))
                    {
                        sEntero += sMonto.Substring(y, 1);
                    }
                    else
                    {
                        sDecimal += sMonto.Substring(y, 1);
                    }
                }
            }

            if (sDecimal.Length == 1)
            {
                sDecimal += "0";
            }

            //**********Proceso de conversión***********
            sFlag = "N";
            if (nMonto < 999999999)
            {
                for (y = sEntero.Length; y > 0; y--)
                {
                    var num = sEntero.Length - y;
                    switch (y)
                    {
                        //**********Asigna las palabras para las centenas***********
                        case 9:
                        case 6:
                        case 3:
                            switch (sEntero.Substring(num, 1))
                            {
                                case "1":
                                    if (sEntero.Substring(num + 1, 1).Equals("0") &&
                                        sEntero.Substring(num + 2, 1).Equals("0"))
                                    {
                                        sLetras += "CIEN ";
                                    }
                                    else
                                    {
                                        sLetras += "CIENTO ";
                                    }
                                    break;
                                case "2":
                                    sLetras += "DOSCIENTOS ";
                                    break;
                                case "3":
                                    sLetras += "TRESCIENTOS ";
                                    break;
                                case "4":
                                    sLetras += "CUATROCIENTOS ";
                                    break;
                                case "5":
                                    sLetras += "QUINIENTOS ";
                                    break;
                                case "6":
                                    sLetras += "SEISCIENTOS ";
                                    break;
                                case "7":
                                    sLetras += "SETECIENTOS ";
                                    break;
                                case "8":
                                    sLetras += "OCHOCIENTOS ";
                                    break;
                                case "9":
                                    sLetras += "NOVECIENTOS ";
                                    break;
                            }
                            break;
                        //*********Asigna las palabras para las decenas************
                        case 2:
                        case 5:
                        case 8:
                            switch (sEntero.Substring(num, 1))
                            {
                                case "0":
                                    sFlag = "N";
                                    break;
                                case "1":
                                    if (sEntero.Substring(num + 1, 1).Equals("0"))
                                    {
                                        sFlag = "S";
                                        sLetras += "DIEZ ";
                                    }
                                    if (sEntero.Substring(num + 1, 1).Equals("1"))
                                    {
                                        sFlag = "S";
                                        sLetras += "ONCE ";
                                    }
                                    if (sEntero.Substring(num + 1, 1).Equals("2"))
                                    {
                                        sFlag = "S";
                                        sLetras += "DOCE ";
                                    }
                                    if (sEntero.Substring(num + 1, 1).Equals("3"))
                                    {
                                        sFlag = "S";
                                        sLetras += "TRECE ";
                                    }
                                    if (sEntero.Substring(num + 1, 1).Equals("4"))
                                    {
                                        sFlag = "S";
                                        sLetras += "CATORCE ";
                                    }
                                    if (sEntero.Substring(num + 1, 1).Equals("5"))
                                    {
                                        sFlag = "S";
                                        sLetras += "QUINCE ";
                                    }
                                    if (int.Parse(sEntero.Substring(num + 1, 1)) > 5)
                                    {
                                        sFlag = "N";
                                        sLetras += "DIECI";
                                    }
                                    break;
                                case "2":
                                    if (sEntero.Substring(num + 1, 1).Equals("0"))
                                    {
                                        sFlag = "S";
                                        sLetras += "VEINTE ";
                                    }
                                    else
                                    {
                                        sFlag = "N";
                                        sLetras += "VEINTI";
                                    }
                                    break;
                                case "3":
                                    if (sEntero.Substring(num + 1, 1).Equals("0"))
                                    {
                                        sFlag = "S";
                                        sLetras += "TREINTA ";
                                    }
                                    else
                                    {
                                        sFlag = "N";
                                        sLetras += "TREINTI";
                                    }
                                    break;
                                case "4":
                                    if (sEntero.Substring(num + 1, 1).Equals("0"))
                                    {
                                        sFlag = "S";
                                        sLetras += "CUARENTA ";
                                    }
                                    else
                                    {
                                        sFlag = "N";
                                        sLetras += "CUARENTI";
                                    }
                                    break;
                                case "5":
                                    if (sEntero.Substring(num + 1, 1).Equals("0"))
                                    {
                                        sFlag = "S";
                                        sLetras += "CINCUENTA ";
                                    }
                                    else
                                    {
                                        sFlag = "N";
                                        sLetras += "CINCUENTI";
                                    }
                                    break;
                                case "6":
                                    if (sEntero.Substring(num + 1, 1).Equals("0"))
                                    {
                                        sFlag = "S";
                                        sLetras += "SESENTA ";
                                    }
                                    else
                                    {
                                        sFlag = "N";
                                        sLetras += "SESENTI";
                                    }
                                    break;
                                case "7":
                                    if (sEntero.Substring(num + 1, 1).Equals("0"))
                                    {
                                        sFlag = "S";
                                        sLetras += "SETENTA ";
                                    }
                                    else
                                    {
                                        sFlag = "N";
                                        sLetras += "SETENTI";
                                    }
                                    break;
                                case "8":
                                    if (sEntero.Substring(num + 1, 1).Equals("0"))
                                    {
                                        sFlag = "S";
                                        sLetras += "OCHENTA ";
                                    }
                                    else
                                    {
                                        sFlag = "N";
                                        sLetras += "OCHENTI";
                                    }
                                    break;
                                case "9":
                                    if (sEntero.Substring(num + 1, 1).Equals("0"))
                                    {
                                        sFlag = "S";
                                        sLetras += "NOVENTA ";
                                    }
                                    else
                                    {
                                        sFlag = "N";
                                        sLetras += "NOVENTI";
                                    }
                                    break;
                            }
                            break;
                        //*********Asigna las palabras para las unidades************
                        case 1:
                        case 4:
                        case 7:
                            switch (sEntero.Substring(num, 1))
                            {
                                case "1":
                                    if (sFlag.Equals("N"))
                                    {
                                        if (y == 1)
                                        {
                                            sLetras += "UNO ";
                                        }
                                        else
                                        {
                                            sLetras += "UN ";
                                        }
                                    }
                                    break;
                                case "2":
                                    if (sFlag.Equals("N"))
                                        sLetras += "DOS ";
                                    break;
                                case "3":
                                    if (sFlag.Equals("N"))
                                        sLetras += "TRES ";
                                    break;
                                case "4":
                                    if (sFlag.Equals("N"))
                                        sLetras += "CUATRO ";
                                    break;
                                case "5":
                                    if (sFlag.Equals("N"))
                                        sLetras += "CINCO ";
                                    break;
                                case "6":
                                    if (sFlag.Equals("N"))
                                        sLetras += "SEIS ";
                                    break;
                                case "7":
                                    if (sFlag.Equals("N"))
                                        sLetras += "SIETE ";
                                    break;
                                case "8":
                                    if (sFlag.Equals("N"))
                                        sLetras += "OCHO ";
                                    break;
                                case "9":
                                    if (sFlag.Equals("N"))
                                        sLetras += "NUEVE ";
                                    break;
                            }
                            break;
                    }

                    //***********Asigna la palabra mil***************
                    if (y == 4)
                    {
                        if (sEntero.Length == y)
                            sLetras += "MIL ";
                        else
                        {
                            if (!sEntero.Substring(y, 1).Equals("0") || (sEntero.Substring(y, 1).Equals("0") && sEntero.Length <= y))
                                sLetras += "MIL ";
                            else
                            {
                                if (sEntero.Substring(y, 1).Equals("0") && sEntero.Length == 5)
                                {
                                    sLetras += "MIL ";
                                }
                            }
                        }
                    }


                    //'**********Asigna la palabra millón*************
                    if (y != 7) continue;
                    if (sEntero.Length == 7 && sEntero.Substring(0, 1).Equals("1"))
                        sLetras += "MILLÓN ";
                    else
                        sLetras += "MILLONES ";
                }

                //**********Une la parte entera y la parte decimal*************

                if (String.IsNullOrEmpty(sEntero))
                {
                    sLetras += "CERO ";
                }

                if (!String.IsNullOrEmpty(sDecimal))
                    sLetras += "Y " + sDecimal + "/100 " + sMonedaLetras;
                else
                    sLetras += " " + sMonedaLetras;
            }


            return sLetras;

        }

        public static IEnumerable<String> PartirCadena(String sCadena, Int32 nTamano)
        {
            if (sCadena == null)
            {
                throw new ArgumentNullException(sCadena);
            }
            if (nTamano <= 0)
            {
                throw new ArgumentException("Tamaño debe ser mayor a cero");
            }

            for (var i = 0; i < sCadena.Length; i += nTamano)
            {
                yield return sCadena.Substring(i, Math.Min(nTamano, sCadena.Length - i));
            }
        }
    }
}