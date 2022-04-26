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
        public float Value { get; set; }
        public string Vencimento { get; set; }

        public Beneficio Beneficio { get; set; }
        public int BeneficioId { get; set; }

        public Funcionario Funcionario { get; set; }
        public int FuncionarioId { get; set; }


    }
}
