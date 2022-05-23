using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class DepositoBeneficio
    {
        [Required]
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime Vencimento { get; set; }
        [ForeignKey("BeneficioId")]
        public virtual Beneficio Beneficio { get; set; }
        public int? BeneficioId { get; set; }
        [ForeignKey("FuncionarioId")]
        public virtual Funcionario Funcionario { get; set; }
        public int FuncionarioId { get; set; }


    }
}

