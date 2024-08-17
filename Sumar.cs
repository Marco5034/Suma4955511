using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suma4955511
{
   public class Sumar
    {
        public int Numero1 {  get; set; }    
        public int Numero2 { get; set; }


        public int RealizarSuma()
        {
            var resultado = Numero1 + Numero2;
            return resultado;

        }
    }
}
