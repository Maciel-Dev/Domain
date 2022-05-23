using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ModalidadeCargo
    {
        [Required]
        public int Id { get; set; }
        //public ModalidadeDeContrato ModalidadeDeContrato {get; set;}
        [ForeignKey("ModalidadeContratoID")]
        public virtual ModalidadeDeContrato ModalidadeDeContrato { get; set; }
        public int? ModalidadeContratoID { get; set; }
        [ForeignKey("CargoID")]
        public virtual Cargo Cargo {get; set;}
        public int CargoID { get; set; }
        [ForeignKey("NivelID")]
        public virtual Nivel Nivel {get; set;}
        public int? NivelID { get; set; }

        public IEnumerable<Funcionario> Funcionarios { get; set; }

        
    }
}
