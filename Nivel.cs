using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Nivel
    {
        [Required]
        public int Id { get; set; }
        public string Tipo { get; set; }

    }
}
