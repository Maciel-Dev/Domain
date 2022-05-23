using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Funcionario
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Endereco { get; set; }
        public string Cpf { get; set; }
        [ForeignKey("modalidadeCargoId")]
        public virtual ModalidadeCargo ModalidadeCargo {get; set;}
        public int modalidadeCargoId { get; set; }
        public IEnumerable<DepositoBeneficio> DepositoBeneficios { get; set; }


    }
}
