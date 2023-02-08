using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1A_1C_2020_AsistenciaViajero
{
    class Prestacion
    {
        public const int IdMin = 100000000;
        public const int IdMax = 999999999;
        public const int ImpteMin = 1;
        public const int ImpteMax = 999999;
        public const string EstPend = "P";
        public const string EstEjec = "E";

        public int Id { get; private set; }
        public string Concepto { get; private set; }
        public int Importe { get; private set; }
        public string Estado { get; private set; }
        public string Factura { get; private set; }

        public Prestacion(int elId, string elConcepto, int elImpte)
        {
            //devuelve una Prestación con datos 0 o "" si
            //recibe elId inválido (salvo -1), elConcepto 
            //con valor nulo o vacío, o elImpte fuera del
            //rango permitido
            //y una Prestacion con todos los datos recibidos 
            //en caso contrario (generando un Id automáticamente
            //si recibe elId = -1)
            if ( elId != -1 && (elId < IdMin || elId > IdMax)
                || string.IsNullOrEmpty( elConcepto)
                || elImpte < ImpteMin || elImpte > ImpteMax
                )
            {
                Concepto = "";
                Importe = 0;
                Estado = "";
                Factura = "";
            } else
            {
                if (elId == -1)
                {
                    Id = (int) Math.Truncate(DateTime.Now.ToOADate() * 10000);
                } else
                {
                    Id = elId;
                }
                Concepto = elConcepto.ToUpper();
                Importe = elImpte;
                Estado = EstPend;
            }
        }

        public string IngFactProv (string factProv)
        {
            //cambia el estado de la prestación a
            //ejecutada previo revisar que la prestación esté
            //pendiente y que factProv no sea nulo o
            //vacío. Devuelve "" o un texto con el error
            if (Estado != EstPend)
            {
                return "La prestación no está pendiente";
            } else
            {
                if (string.IsNullOrEmpty(factProv))
                {
                    return "Debe indicarse el número de factura";
                } else
                {
                    Factura = factProv.ToUpper();
                    Estado = EstEjec;
                    return "";
                }
            }
        }

        public string Resumir()
        {
            return (Id + "\t" + Concepto + "\t" + Importe + "\t" + Estado);
        }
    }
}
