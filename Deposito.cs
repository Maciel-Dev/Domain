using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain
{
    public class Deposito
    {
        public int Id { get; set; }
        public double ValorDepositoFuncionario { get; set; }

        [Column(TypeName = "Datetime")]
        public DateTime Data { get; set; }

    }
}
