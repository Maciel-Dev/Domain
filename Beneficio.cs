using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Beneficio
    {
        public int Id { get; set; }
        [ForeignKey("NivelID")]
        public virtual Nivel Nivel {get; set;}
        public int NivelID { get; set; }

        [ForeignKey("TipoBeneficioId")]
        public virtual TipoBeneficio TipoBeneficio { get; set; }
        public int TipoBeneficioId { get; set; }
        public IEnumerable<DepositoBeneficio> DepositoBeneficios { get; set; }
        
    }
}
