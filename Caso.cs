using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1A_1C_2020_AsistenciaViajero
{
    class Caso
    {
        public const int IdMin = 100000000;
        public const int IdMax = 999999999;
        public const int CantPrest = 50;
        public const string EstAbie = "A";
        public const string EstCerr = "C";

        public int Id { get; private set; }
        public int NumAsoc { get; private set; }
        public string CodPoliza { get; private set; }
        public string Ciudad { get; private set; }
        public string Pais { get; private set; }
        public string Descripcion { get; private set; }
        public string Clase { get; private set; }
        public string Estado { get; private set; }
        private Prestacion[] prestaciones = new Prestacion[CantPrest];

        public Caso (int elId, int elNumAsoc, string elCodPoliza
            , string laCiudad, string elPais, string laDescrip, string laClase)
        {
            //inicializa todos los elementos del arreglo prestaciones
            //devuelve un Caso con datos 0 o "" si
            //recibe elId inválido (salvo -1), elCodPoliza,  
            //laCiudad, elPais, laClase o laDescrip
            //con valor nulo o vacío, o elNumAsoc fuera del
            //rango permitido
            //y un Caso con todos los datos recibidos 
            //en caso contrario (generando un Id automáticamente
            //si recibe elId = -1)

            for (int posPres = 0; posPres < CantPrest; posPres++)
            {
                prestaciones[posPres] = new Prestacion(0,"", 0);
            }

            if ((elId != -1 && elId < IdMin || elId> IdMax)
                || elNumAsoc < Asociado.NumMin || elNumAsoc > Asociado.NumMax
                || string.IsNullOrEmpty(elCodPoliza)
                || string.IsNullOrEmpty(laCiudad)
                || string.IsNullOrEmpty(elPais)
                || string.IsNullOrEmpty(laClase)
                || string.IsNullOrEmpty(laDescrip))
            {
                Id = 0;
                NumAsoc = 0;
                CodPoliza = "";
                Ciudad = "";
                Pais = "";
                Descripcion = "";
                Clase = "";
                Estado = "";
            } else
            {
                if (elId == -1)
                {
                    Id = (int) Math.Truncate(DateTime.Now.ToOADate() * 10000);
                } else {
                    Id = elId;
                }
                NumAsoc = elNumAsoc;
                CodPoliza = elCodPoliza;
                Ciudad = laCiudad;
                Pais = elPais;
                Descripcion = laDescrip;
                Clase = laClase;
                Estado = EstAbie;
            }
        }

        public String Resumir()
        {
            return Id + "\t" + NumAsoc + "\t" + CodPoliza + "\t" + Ciudad + "\t" + Pais
                + "\t" + Clase + "\t" + Descripcion;
        }

        public String ResumirPrestacionesPendientes()
        {
            //devuelve el método resumir de todas las
            //prestaciones pendientes, agregando una cabecera
            //y "" si ninguna prestación está pendiente
            String retorno = "";
            for (int i = 0; i < CantPrest; i++)
            {
                if (prestaciones[i].Estado == Prestacion.EstPend)
                {
                    retorno = retorno + prestaciones[i].Resumir() + "\n";
                }
            }
            if (retorno != "")
            {
                retorno = "ID\tCONCEPTO\tIMPORTE\tESTADO\n"
                    + retorno;
            }

            return (retorno);
        }

        public string CerrarCaso()
        {
            //valida que el caso esté abierto y no tenga prestaciones pendientes
            //antes de cambiar el estado a Cerrado
            if (Estado != EstAbie)
            {
                return "El caso no está abierto";
            }
            else
            {
                if (!string.IsNullOrEmpty(ResumirPrestacionesPendientes()))
                {
                    return "El caso tiene prestaciones pendientes";
                }
                else
                {
                    Estado = EstCerr;
                    return "";
                }
            }
        }

        public int BuscarPrestacionXId(int idReq)
        {
            //devuelve la posición de la prestación 
            //con id = idReq en el arreglo prestaciones
            //y -1 si no lo encuentra
            int pos = 0;

            while (prestaciones[pos].Id != idReq && pos < CantPrest-1)
            {
                pos++;
            }

            if (prestaciones[pos].Id != idReq)
            {
                pos = -1;
            }
            return pos;
        }

        public string AgregarPrestacion(int id, string codigoConcepto, int importe)
        {
            //Agrega la prestación al arreglo prestaciones
            //previo revisar que exista lugar allí
            //Devuelve "" si la nueva prestación fue creada 
            //o un mensaje de error en caso contrario

            int posicion = BuscarPrestacionXId(0);
            if (posicion != -1)
            {
                prestaciones[posicion] = new Prestacion(id, codigoConcepto,  importe);
                if (prestaciones[posicion].Id == 0)
                {
                    return "No se agregó la prestación. Verifique los datos.";
                } else
                {
                    return "";
                }
            }
            else
            {
                return "No hay lugar para más prestaciones";
            }
        }

        public string GetEstadoPrest(int posPrest, int idPrest)
        {
            //devuelve prestaciones[posPrest].Estado 
            //previo revisar que posPrest sea una posición
            //válida cuyo Id coincida con idPrest
            //devuelve "" o un mensaje si hubo error

            //toda la validación de la posición recibida por rango y contra el id podrían
            //ponerse en un método separado para no duplicar el código en cada uno de los
            //métodos que, como éste, busquen dar un acceso controlado a los arreglos internos
            //desde clases externas
            if (posPrest>= 0 && posPrest < CantPrest)
            {
                if (prestaciones[posPrest].Id != idPrest)
                {
                    return "Falló la verificación del Id";
                } else
                {
                    return prestaciones[posPrest].Estado;
                }
            } else
            {
                return "Posición Invalida";
            }    
        }

        public string EjecutarPrest(int posPrest, int idPrest, string numFact)
        {
            //devuelve prestaciones[posPrest].IngFactProv(numFact)
            //previo revisar que posPrest sea una posición
            //válida cuyo Id coincida con idPrest
            //devuelve "" o un mensaje si hubo error

            string retorno;
            if (posPrest >= 0 && posPrest < CantPrest)
            {
                if (prestaciones[posPrest].Id != idPrest)
                {
                    return "Falló la verificación del Id";
                }
                else
                {
                    retorno = prestaciones[posPrest].IngFactProv(numFact);
                    return retorno;
                }
            }
            else
            {
                return "Posición Invalida";
            }
        }
    }
}
