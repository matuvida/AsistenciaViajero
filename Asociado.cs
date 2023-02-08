using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1A_1C_2020_AsistenciaViajero
{
    class Asociado
    {
        public const int NumMin = 1;
        public const int NumMax = 99999;
        public const int DocMin = 1;
        public const int DocMax = 999999999;

        public int Numero { get; private set; }
        public int Documento { get; private set; }
        public string NombreyAp { get; private set; }

        public Asociado (int elNum, int elDoc, string nomYAp)
        {
            //devuelve un Asociado con datos 0 o "" si
            //recibe elNum = 0 o algún dato inválido
            //devuelve un Asociado con todos los datos recibidos
            //si son válidos
            if (elNum<NumMin || elNum >NumMax
                || elDoc < DocMin || elDoc > DocMax 
                || string.IsNullOrEmpty(nomYAp))
            {
                Numero = 0;
                Documento = 0;
                NombreyAp = "";
            } else
            {
                Numero = elNum;
                Documento = elDoc;
                NombreyAp = nomYAp;
            }
        }
    }
}
