using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Endereco
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public int CEP { get; set; }
        [MaxLength(255)]
        public string Complemento { get; set; }
        public IEnumerable<Funcionario> Funcionarios { get; set; }
    }
}
