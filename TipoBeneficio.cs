using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class TipoBeneficio
    {
        [Required]
        public int Id { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public double Percent { get; set; }
        
        public IEnumerable<Beneficio> Beneficios { get; set; }
    }
}
