using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Crud
{
    public class BenefitDepositCrud
    {
        
        private readonly FuncionarioContext connect;

        public BenefitDepositCrud(FuncionarioContext connect)
        {
            this.connect = connect;
        }

        public void insertBenefitDepos(DepositoBeneficio depositoBeneficio)
        {
            connect.DepositosBeneficios.Add(depositoBeneficio); 
            connect.SaveChanges();
        }

        public void changeBenefitDepos(DepositoBeneficio depositoBeneficio)
        {
            connect.DepositosBeneficios.Update(depositoBeneficio);
            connect.SaveChanges();
        }

        public void deleteBenefitDepos(DepositoBeneficio depositoBeneficio)
        {
            connect.DepositosBeneficios.Remove(depositoBeneficio);
            connect.SaveChanges();
        }

        public void benefitExcludeId(int FuncionarioId)
        {
            var search = connect.DepositosBeneficios.Where(x => x.Id == FuncionarioId);
            
            connect.DepositosBeneficios.RemoveRange(search);
            connect.SaveChanges();
        }

        public DepositoBeneficio search(int Id)
        {
            return connect.DepositosBeneficios.FirstOrDefault(x => x.FuncionarioId == Id);
        }

    }
}