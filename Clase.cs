using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1A_1C_2020_AsistenciaViajero
{
    class Clase
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }

        public Clase(string elCod, string laDesc)
        {
            //devuelve una Clase con datos "" si
            //recibe elCod o laDesc con valor nulo o vacío 
            //y una Clase con todos los datos recibidos 
            //en caso contrario
            if (string.IsNullOrEmpty(elCod) || string.IsNullOrEmpty(laDesc))
            {
                Codigo = "";
                Descripcion = "";
            } else
            {
                Codigo = elCod;
                Descripcion = laDesc;
            }
        }

        public string Resumir()
        {
            return Codigo + "\t" + Descripcion;
        }
    }
}
