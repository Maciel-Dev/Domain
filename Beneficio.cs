using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Beneficio
    {
        public int Id { get; set; }
        public Nivel Nivel {get; set;}
        public int NivelID { get; set; }

        public TipoBeneficio TipoBeneficio { get; set; }
        public int TipoBeneficioId { get; set; }
        
    }
}
