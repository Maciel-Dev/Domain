using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ModalidadeDeContrato
    {
        [Required]
        public int Id { get; set; }
        public int Hour { get; set; }
        public string Description { get; set; }
        
    }
}
