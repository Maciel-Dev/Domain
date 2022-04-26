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
        public int ModalidadeContratoID { get; set; }
        public Cargo Cargo {get; set;}
        public int CargoID { get; set; }
        public Nivel Nivel {get; set;}
        public int NivelID { get; set; }

        
    }
}
