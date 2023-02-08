using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1A_1C_2020_AsistenciaViajero
{
    class AppAsistencia
    {
        const int CantCasos = 10000;
        const int CantClases = 10;
        const int CantConceptos = 200;
        const int CantAsocs = 100000;

        const string OpcNCa = "A";
        const string OpcNPr = "B";
        const string OpcCPr = "C";
        const string OpcLCa = "D";
        const string OpcCer = "E";
        const string OpcSal = "F";

        private Caso[] casos = new Caso[CantCasos];
        private Clase[] clases = new Clase[CantClases];
        private Concepto[] conceptos = new Concepto[CantConceptos];
        private Asociado[] asociados = new Asociado[CantAsocs];

        public AppAsistencia()
        {
            //Inicializa todas las posiciones de sus arreglos con valores 0 o ""
            for (int pos = 0; pos < CantCasos; pos++)
            {
                casos[pos] = new Caso(0,0,"","","", "", "");
            }

            for (int pos = 0; pos < CantClases; pos++)
            {
                clases[pos] = new Clase("", "");
            }

            for (int pos = 0; pos < CantConceptos; pos++)
            {
                conceptos[pos] = new Concepto("", "", "");
            }

            for (int pos = 0; pos < CantAsocs; pos++)
            {
                asociados[pos] = new Asociado(0, 0, "");
            }
        }

        public void Iniciar()
        {
            string opcion = "";

            CargaInicial();

            do
            {
                opcion = ServValidac.PedirStrNoVac("Menu Principal:\n"
                    + OpcNCa + ": Nuevo Caso\n"
                    + OpcNPr + ": Nueva Prestación\n"
                    + OpcCPr + ": Cumplir Prestación\n"
                    + OpcLCa + ": Listar casos abiertos\n"
                    + OpcCer + ": Cerrar casos\n"
                    + OpcSal + ": Salir");
                switch (opcion)
                {
                    case OpcNCa:
                        NuevoCaso();
                        break;
                    case OpcNPr:
                        NuevaPrestacion();
                        break;
                    case OpcCPr:
                        //CumplirPrestacion();
                        break;
                    case OpcLCa:
                        //ListarCasosAbiertos();
                        break;
                    case OpcCer:
                        CerrarCasos();
                        break;
                    case OpcSal:
                        break;
                    default:
                        Console.WriteLine("Opción inválida");
                        break;
                }
            } while (opcion != OpcSal);

            GuardadoFinal();
        }

        private int BuscarCasoXId(int idReq)
        {
            //devuelve la posición del caso con id = idReq en el
            //arreglo casos y -1 si no lo encuentra
            int pos = 0;

            while (casos[pos].Id != idReq && pos < CantCasos - 1)
            {
                pos++;
            }

            if (casos[pos].Id != idReq)
            {
                pos = -1;
            }
            return pos;
        }

        private String ObtenerCasosAbiertos()
        {
            //devuelve el método resumir de todos los
            //casos abiertos, agregando una cabecera
            //y "" si ningún caso está abierto
            String retorno = "";
            for (int i = 0; i < CantCasos; i++)
            {
                if (casos[i].Estado == Caso.EstAbie)
                {
                    retorno = retorno + casos[i].Resumir() + "\n";
                }
            }
            if (retorno != "")
            {
                retorno = "ID\tNRO ASOC.\tNRO POLIZA\tCIUDAD\tPAIS\tCLASE\tDESCRIPCION\n"
                    + retorno;
            }
            return (retorno);
        }

        //SOLUCION - INICIO

        public string NuevaPrestacion()
        {

            string lista = "";
            int idCaso = 0;
            int idPosicion = 0;
            idPosicion = BuscarCasoXId(idCaso);
    
            if (ObtenerCasosAbiertos() == "")
            {
                Console.WriteLine("No existen casos abiertos");
            }

            else
            {
                Console.WriteLine(lista + ObtenerCasosAbiertos());
                idCaso = ServValidac.PedirInt("Ingrese ID de caso: ", Caso.IdMin, Caso.IdMax);

                if (BuscarCasoXId(idCaso) == -1)
                {
                    Console.WriteLine("El caso ingresado no existe ");
                }
                else
                {
                    Console.WriteLine("Existe");
                }



            }


            return lista;


        }


        //SOLUCION - FIN

        //INICIO NO DESARROLLAR
        private void NuevoCaso()
        {
            int nroAsoc;
            string codClase;
            int posicion = BuscarCasoXId(0);
            if (posicion == -1)
            {
                Console.WriteLine("No hay lugar para mas casos");
            }
            else
            {
                nroAsoc = ServValidac.PedirInt("Ingrese ID del asociado", Asociado.NumMin, Asociado.NumMax);
                if (BuscarAsocXNro(nroAsoc)==-1)
                {
                    Console.WriteLine("No hay asociados con ese número");
                } else
                {
                    codClase = ServValidac.PedirStrNoVac("Ingrese código de clase:\n" + ListarClases());
                    if (BuscarClaseXCod(codClase)==-1)
                    {
                        Console.WriteLine("No hay clases con ese código");
                    } else
                    {
                        casos[posicion] = new Caso(
                            -1,
                            nroAsoc,
                            ServValidac.PedirStrNoVac("Ingrese poliza"),
                            ServValidac.PedirStrNoVac("Ingrese ciudad"),
                            ServValidac.PedirStrNoVac("Ingrese pais"),
                            ServValidac.PedirStrNoVac("Ingrese descripción"),
                            codClase
                            );
                    }
                } 
            }
        }

        private int BuscarAsocXNro(int numReq)
        {
            int pos = 0;

            while (asociados[pos].Numero != numReq && pos < CantAsocs - 1)
            {
                pos++;
            }

            if (asociados[pos].Numero != numReq)
            {
                pos = -1;
            }
            return pos;
        }

        private int BuscarClaseXCod(string codReq)
        {
            int pos = 0;

            while (clases[pos].Codigo != codReq && pos < CantClases - 1)
            {
                pos++;
            }

            if (clases[pos].Codigo != codReq)
            {
                pos = -1;
            }
            return pos;
        }

        private string ListarClases()
        {
            string lista = "";
            for (int pos = 0; pos < CantClases; pos++)
            {
                if (clases[pos].Codigo != "")
                {
                    lista = lista + clases[pos].Resumir()+"\n";
                }
            }
            return lista;
        }

        private void CerrarCasos()
        {
            int idCaso;
            int posicionCaso;
            string mensajeCasosAbiertos = this.ObtenerCasosAbiertos();
            string retorno;

            if (mensajeCasosAbiertos == "")
            {
                Console.WriteLine("No hay casos abiertos");
            }
            else
            {
                idCaso = ServValidac.PedirInt("Ingrese ID de caso\n"
                    + mensajeCasosAbiertos, Caso.IdMin, Caso.IdMax);
                posicionCaso = BuscarCasoXId(idCaso);
                if (posicionCaso == -1)
                {
                    Console.WriteLine("El caso ingresado no existe");
                }
                else
                {
                    retorno = casos[posicionCaso].CerrarCaso();
                    if (retorno != "")
                    {
                        Console.WriteLine("No se hizo el cierre: "+retorno);
                    }
                    else
                    {
                        Console.WriteLine("Se cerró el caso");
                    }
                }
            }
        }
        
        private void CargaInicial()
        {
            asociados[0] = new Asociado(22222, 22222222, "GONZALEZ, LIDIA");
            asociados[1] = new Asociado(33333, 333222111, "CARVALHO, PEDRO");
            asociados[2] = new Asociado(44444, 123123, "ATKINS, JEREMY");
            clases[0] = new Clase("SAN", "SANITARIO");
            clases[1] = new Clase("PEN", "PENAL");
            clases[2] = new Clase("CON", "CONTRACTUAL");
            conceptos[0] = new Concepto("HOS", "GASTOS HOSPITALARIOS", "SAN");
            conceptos[8] = new Concepto("HME", "HONORARIOS MEDICOS", "SAN");
            conceptos[9] = new Concepto("MED", "MEDICAMENTOS", "SAN");
            conceptos[10] = new Concepto("TRA", "TRASLADO SANITARIO", "SAN");
            conceptos[1] = new Concepto("HAB", "HONORARIOS ABOGADOS", "PEN");
            conceptos[2] = new Concepto("HIN", "HONORARIOS INVESTIGACION", "PEN");
            conceptos[3] = new Concepto("FIA", "PAGO DE FIANZA", "PEN");

            casos[0] = new Caso(111111111, 33333, "BR3213", "SIDNEY", "AUSTRALIA"
                , "ACCIDENTE SKY ACUATICO", "SAN");
            casos[1] = new Caso(111111114, 33333, "BR3213", "SIDNEY", "AUSTRALIA"
                , "INCUMPLIMIENTO DEL SERVICIO EN HOTEL", "CON");
            casos[2] = new Caso(111111117, 44444, "IR3213", "MOSCU", "RUSIA"
                , "PELEA EN BAR", "PEN");

        }

        private void GuardadoFinal()
        {
            //NO DESARROLLADO
        }
    }
}
