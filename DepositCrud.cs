using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Crud
{
    public class DepositCrud
    {
        
        private readonly FuncionarioContext connect;

        public DepositCrud(FuncionarioContext connect)
        {
            this.connect = connect;
        }

        public void insertDepos(Deposito deposito)
        {
            connect.Depositos.Add(deposito); 
            connect.SaveChanges();
        }

        public void changeDepos(Deposito deposito)
        {
            connect.Depositos.Update(deposito);
            connect.SaveChanges();
        }

        public void deleteDepos(Deposito deposito)
        {
            connect.Depositos.Remove(deposito);
            connect.SaveChanges();
        }

        public Deposito depositoIdSearch(int DepositoId)
        {
            return connect.Depositos.FirstOrDefault(x => x.Id == DepositoId);
        }

        public Deposito search(int Id)
        {
            return connect.Depositos.FirstOrDefault(x => x.DepositoBeneficioId == Id);
        }

    }
}