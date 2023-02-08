using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1A_1C_2020_AsistenciaViajero
{
    class Concepto
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }
        public string CodClase { get; private set; }

        public Concepto(string elCod, string laDesc, string elCodClase)
        {
            //devuelve un Concepto con datos "" si
            //recibe elCod, laDesc, o elCodClase con valor nulo o vacío 
            //y una Clase con todos los datos recibidos 
            //en caso contrario
            if (string.IsNullOrEmpty(elCod)
                || string.IsNullOrEmpty(laDesc)
                || string.IsNullOrEmpty(elCodClase))
            {
                Codigo = "";
                Descripcion = "";
                CodClase = "";
            }
            else
            {
                Codigo = elCod;
                Descripcion = laDesc;
                CodClase = elCodClase;
            }
        }

        public string Resumir()
        {
            return Codigo + "\t" + Descripcion;
        }
    }
}
