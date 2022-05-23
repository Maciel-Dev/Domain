using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Crud
{
    public class FuncionarioCrud
    {
        
        private readonly FuncionarioContext connect;

        public FuncionarioCrud(FuncionarioContext connect)
        {
            this.connect = connect;
        }

        public void insertFunc(Funcionario funcionario)
        {
            connect.Funcionarios.Add(funcionario); //Adiciona um funcionario usando a conexão já estabelecida
            connect.SaveChanges();
        }

        public void changeFunc(Funcionario funcionario)
        {
            connect.Funcionarios.Update(funcionario);
            connect.SaveChanges();
        }

        public void deleteFunc(Funcionario funcionario)
        {
            connect.Funcionarios.Remove(funcionario);
            connect.SaveChanges();
        }
        public Funcionario search(string CPF)
        {
            return connect.Funcionarios.FirstOrDefault(x => x.Cpf == CPF); 
        }

        public Funcionario idSearch(int Id)
        {
            return connect.Funcionarios.FirstOrDefault(x => x.Id == Id);
        }
    }
}