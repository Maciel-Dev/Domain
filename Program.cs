using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Domain;
using static Domain.Funcionario;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;
using System.Linq;

//Incrementar Chaves Estrangeiras
//Ler como input Excel
//Fazer relações para pesquisa no BD

namespace Domain
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new FuncionarioContext())
            {
                db.Database.EnsureCreated(); 

            }
        
        LerArquivo();

        }

        public static void LerArquivo()
        { 
            FileInfo caminho = new FileInfo(@"Infosis.xlsx");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using(ExcelPackage package = new ExcelPackage(caminho))
            {

                ExcelWorksheet planilha = package.Workbook.Worksheets[0];
                int maxRows = planilha.Dimension.End.Row;
                using(FuncionarioContext dbConnection = new FuncionarioContext())
                {
                    for(int linha = 2; linha <= maxRows; linha++)
                    {
                        //object a = planilha.Cells[linha, 17].Value;
                        //Console.WriteLine(a.GetType());
                        
                        var checkCargo = dbConnection.Cargos.FirstOrDefault(x => x.Tipo == planilha.Cells[linha, 11].Value.ToString());
                        if(checkCargo == null)
                        {
                            Cargo cargo = new Cargo();
                            cargo.Tipo = planilha.Cells[linha, 11].Value.ToString();
                            dbConnection.Cargos.Add(cargo);
                            dbConnection.SaveChanges();
                        }

                        var checkHourMC = dbConnection.ModalidadeDeContratos.FirstOrDefault(x => x.Hour == int.Parse(planilha.Cells[linha, 6].Value.ToString()));
                        var checkDescMC = dbConnection.ModalidadeDeContratos.FirstOrDefault(x => x.Description == planilha.Cells[linha, 7].Value.ToString());
                        if(checkDescMC == null || checkHourMC == null)
                        {
                            ModalidadeDeContrato modalidadeContrato = new ModalidadeDeContrato();
                            modalidadeContrato.Hour = int.Parse(planilha.Cells[linha, 6].Value.ToString());
                            modalidadeContrato.Description = planilha.Cells[linha, 7].Value.ToString();
                            dbConnection.ModalidadeDeContratos.Add(modalidadeContrato);
                            dbConnection.SaveChanges();
                        };

                        
                        var checkNivel = dbConnection.NiveisFuncionario.FirstOrDefault(x => x.Tipo == planilha.Cells[linha, 5].Value.ToString());
                        if(checkNivel == null)
                        {
                            Nivel nivel = new Nivel();
                            nivel.Tipo = planilha.Cells[linha, 5].Value.ToString();
                            dbConnection.NiveisFuncionario.Add(nivel);
                            dbConnection.SaveChanges();
                        };
                    
                       
                        var nivelID = dbConnection.NiveisFuncionario.FirstOrDefault(x => x.Tipo == planilha.Cells[linha, 5].Value.ToString()).Id;
                        var cargoId = dbConnection.Cargos.FirstOrDefault(x => x.Tipo == planilha.Cells[linha, 11].Value.ToString()).Id;
                        var modalidadeContratoId = dbConnection.ModalidadeDeContratos.FirstOrDefault(x => x.Description == planilha.Cells[linha, 7].Value.ToString()).Id;
                        
                        
                        if(checkNivel == null || checkCargo == null || checkDescMC == null || checkHourMC == null)
                        {

                        ModalidadeCargo modalidadeCargo = new ModalidadeCargo();
                        
                        modalidadeCargo.CargoID = cargoId;
                        modalidadeCargo.NivelID = nivelID;
                        modalidadeCargo.ModalidadeContratoID = modalidadeContratoId;

                        dbConnection.ModalidadeCargos.Add(modalidadeCargo);
                        dbConnection.SaveChanges();
                        }

                        var checkCEP = dbConnection.Enderecos.FirstOrDefault(x => x.CEP == int.Parse(planilha.Cells[linha, 21].Value.ToString()));
                        var checkNumero = dbConnection.Enderecos.FirstOrDefault(x => x.Numero == int.Parse(planilha.Cells[linha, 20].Value.ToString()));

                        if(checkCEP == null || checkNumero == null)
                        {
                            Endereco endereco = new Endereco();

                            endereco.CEP = int.Parse(planilha.Cells[linha, 21].Value.ToString());
                            endereco.Logradouro = planilha.Cells[linha, 19].Value.ToString();
                            endereco.Numero = int.Parse(planilha.Cells[linha, 20].Value.ToString());
                            endereco.Complemento = planilha.Cells[linha, 22].Value.ToString();

                            dbConnection.Enderecos.Add(endereco);
                            dbConnection.SaveChanges();



                        }

                        var modalidadeCargoId = dbConnection.ModalidadeCargos.FirstOrDefault(x => x.CargoID == cargoId && x.NivelID == nivelID && x.ModalidadeContratoID == modalidadeContratoId).Id;
                        var enderecoId = dbConnection.Enderecos.FirstOrDefault(x => x.CEP == int.Parse(planilha.Cells[linha, 21].Value.ToString())).Id;
                        var checkFuncionario = dbConnection.Funcionarios.FirstOrDefault(x => x.Cpf == Convert.ToString(planilha.Cells[linha, 4].Value.ToString()));
                        if(checkFuncionario == null)
                        {
                            Funcionario funcionario = new Funcionario();
                            funcionario.Nome = planilha.Cells[linha, 1].Value.ToString();
                            funcionario.Sobrenome = planilha.Cells[linha, 2].Value.ToString();
                            funcionario.enderecoId = enderecoId;
                            funcionario.Cpf = Convert.ToString(planilha.Cells[linha, 4].Value.ToString());
                            funcionario.modalidadeCargoId = modalidadeCargoId;
                            dbConnection.Funcionarios.Add(funcionario);
                            dbConnection.SaveChanges();
                        }

                        var checkTipoBeneficio = dbConnection.TipoBeneficios.FirstOrDefault(x => x.Description == planilha.Cells[linha, 8].Value.ToString());
                        //if(conn.TipoBeneficios.Any(o => o.Description == planilha.Cells[linha, 5].Value.ToString())) return;
                        if(checkTipoBeneficio == null)
                        {
                            TipoBeneficio TBeneficio = new TipoBeneficio();
                            TBeneficio.Description = planilha.Cells[linha, 8].Value.ToString();
                            TBeneficio.Value = float.Parse(planilha.Cells[linha, 9].Value.ToString());
                            TBeneficio.Percent = float.Parse(planilha.Cells[linha, 10].Value.ToString());
                            dbConnection.TipoBeneficios.Add(TBeneficio);
                            dbConnection.SaveChanges();
                        }
                        var tipoBeneficioId = dbConnection.TipoBeneficios.FirstOrDefault(x => x.Description == planilha.Cells[linha, 8].Value.ToString()).Id;



                        if(checkTipoBeneficio == null || checkNivel == null)
                        {
                            Beneficio beneficio = new Beneficio();
                            beneficio.NivelID = nivelID;
                            beneficio.TipoBeneficioId = tipoBeneficioId;
                            dbConnection.Beneficios.Add(beneficio);
                            dbConnection.SaveChanges();
                        }
                        var checkValor = dbConnection.DepositosBeneficios.FirstOrDefault(a => a.Value == double.Parse(planilha.Cells[linha, 17].Value.ToString()));
                        var checkVencimento = dbConnection.DepositosBeneficios.FirstOrDefault(x => x.Vencimento == Convert.ToDateTime(planilha.Cells[linha, 18].Value.ToString()));
                        var funcionarioId = dbConnection.Funcionarios.FirstOrDefault(y => y.Cpf == Convert.ToString(planilha.Cells[linha, 4].Value.ToString())).Id;
                       
                        

                        var beneficioId = dbConnection.Beneficios.FirstOrDefault(x => x.TipoBeneficioId == tipoBeneficioId && x.NivelID == nivelID).Id;

                        if(checkValor == null || checkVencimento == null)
                        {
                            DepositoBeneficio DBeneficio = new DepositoBeneficio();
                            DBeneficio.Value = double.Parse(planilha.Cells[linha, 17].Value.ToString());
                            DBeneficio.Vencimento = Convert.ToDateTime(planilha.Cells[linha, 18].Value.ToString());
                            DBeneficio.BeneficioId = beneficioId;
                            DBeneficio.FuncionarioId = funcionarioId;
                            dbConnection.DepositosBeneficios.Add(DBeneficio);
                            dbConnection.SaveChanges();
                        }

                    }
                }
            }
        }
    }


    public class FuncionarioContext : DbContext
    {
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<ModalidadeCargo> ModalidadeCargos { get; set; }
        public DbSet<Nivel> NiveisFuncionario { get; set; }
        public DbSet<ModalidadeDeContrato> ModalidadeDeContratos { get; set; }
        public DbSet<TipoBeneficio> TipoBeneficios { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Deposito> Depositos { get; set; }
        public DbSet<DepositoBeneficio> DepositosBeneficios { get; set; }
        public DbSet<Beneficio> Beneficios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EFCore.Demo;Trusted_Connection=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beneficio>(entity => 
            {

            //Beneficio
           entity.HasKey(x => x.Id);

            entity.HasOne(x=>x.TipoBeneficio)
            .WithMany(d => d.Beneficios)
            .OnDelete(DeleteBehavior.Restrict);

           
            entity.HasOne(x => x.Nivel)
            .WithMany(d => d.Beneficios)
            .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DepositoBeneficio>(entity => 
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Beneficio)
                .WithMany(d => d.DepositoBeneficios)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Funcionario)
                .WithMany(d => d.DepositoBeneficios)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Funcionario>(entity => {

                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.ModalidadeCargo)
                .WithMany(d => d.Funcionarios)
                .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<ModalidadeCargo>(entity => 
            {
                
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Cargo)
                .WithMany(d => d.ModalidadeCargos)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Nivel)
                .WithMany(d =>d.ModalidadeCargos)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.ModalidadeDeContrato)
                .WithMany(d => d.ModalidadeCargos)
                .OnDelete(DeleteBehavior.Restrict);
                
            });


        }

        }
    }

