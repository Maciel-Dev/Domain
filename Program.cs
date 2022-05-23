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
            FileInfo caminho = new FileInfo(@"C:\Users\jvvia\Desktop\Programação\TrabalhoProg2\teste\Infosis\InfosisDb\Domain\Infosis.xlsx");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using(ExcelPackage package = new ExcelPackage(caminho))
            {

                

                ExcelWorksheet planilha = package.Workbook.Worksheets[0];
                int maxRows = planilha.Dimension.End.Row;
                using(FuncionarioContext dbConnection = new FuncionarioContext())
                {
                    for(int linha = 2; linha <= maxRows; linha++)
                    {
                        var option = Convert.ToString(planilha.Cells[linha, 1].Value);


                        
                            if(option.Contains("I"))
                            {
                                
                            
                            var checkCargo = dbConnection.Cargos.FirstOrDefault(x => x.Tipo == Convert.ToString(planilha.Cells[linha, 12].Value));
                            if(checkCargo == null)
                            {
                                Cargo cargo = new Cargo();
                                cargo.Tipo = Convert.ToString(planilha.Cells[linha, 12].Value);
                                dbConnection.Cargos.Add(cargo);
                                dbConnection.SaveChanges();
                            }

                            var checkHourMC = dbConnection.ModalidadeDeContratos.FirstOrDefault(x => x.Hour == (Convert.ToInt16(planilha.Cells[linha, 7].Value)));
                            var checkDescMC = dbConnection.ModalidadeDeContratos.FirstOrDefault(x => x.Description == Convert.ToString((planilha.Cells[linha, 8].Value)));
                            if(checkDescMC == null || checkHourMC == null) 
                            {
                                if(int.Parse(planilha.Cells[linha, 7].Value.ToString()) > 0)
                                {
                                    ModalidadeDeContrato modalidadeContrato = new ModalidadeDeContrato();
                                    modalidadeContrato.Hour = Convert.ToInt16((planilha.Cells[linha, 7].Value));
                                    modalidadeContrato.Description = Convert.ToString(planilha.Cells[linha, 8].Value);
                                    dbConnection.ModalidadeDeContratos.Add(modalidadeContrato);
                                    dbConnection.SaveChanges();
                                }
                            };

                            
                            var checkNivel = dbConnection.NiveisFuncionario.FirstOrDefault(x => x.Tipo == Convert.ToString(planilha.Cells[linha, 6].Value));
                            if(checkNivel == null)
                            {
                                Nivel nivel = new Nivel();
                                nivel.Tipo = Convert.ToString(planilha.Cells[linha, 6].Value);
                                dbConnection.NiveisFuncionario.Add(nivel);
                                dbConnection.SaveChanges();
                            };
                        
                        
                            var nivelID = dbConnection.NiveisFuncionario.FirstOrDefault(x => x.Tipo == Convert.ToString(planilha.Cells[linha, 6].Value)).Id;
                            var cargoId = dbConnection.Cargos.FirstOrDefault(x => x.Tipo == Convert.ToString(planilha.Cells[linha, 12].Value)).Id;
                            var modalidadeContratoId = dbConnection.ModalidadeDeContratos.FirstOrDefault(x => x.Description == Convert.ToString(planilha.Cells[linha, 8].Value)).Id;
                            
                            

                            ModalidadeCargo modalidadeCargo = new ModalidadeCargo();
                            
                            modalidadeCargo.CargoID = cargoId;
                            modalidadeCargo.NivelID = nivelID;
                            modalidadeCargo.ModalidadeContratoID = modalidadeContratoId;

                            dbConnection.ModalidadeCargos.Add(modalidadeCargo);
                            dbConnection.SaveChanges();


                            var modalidadeCargoId = dbConnection.ModalidadeCargos.FirstOrDefault(x => x.CargoID == cargoId && x.NivelID == nivelID && x.ModalidadeContratoID == modalidadeContratoId).Id;
                            
                            var checkFuncionario = dbConnection.Funcionarios.FirstOrDefault(x => x.Cpf == Convert.ToString(planilha.Cells[linha, 5].Value));
                            if(checkFuncionario == null)
                            {
                                Funcionario funcionario = new Funcionario();
                                funcionario.Nome = Convert.ToString(planilha.Cells[linha, 2].Value);
                                funcionario.Sobrenome = Convert.ToString(planilha.Cells[linha, 3].Value);
                                funcionario.Endereco = Convert.ToString(planilha.Cells[linha, 4].Value);
                                funcionario.Cpf = Convert.ToString(planilha.Cells[linha, 5].Value);
                                funcionario.modalidadeCargoId = modalidadeCargoId;
                                dbConnection.Funcionarios.Add(funcionario);
                                dbConnection.SaveChanges();
                            }

                            var checkTipoBeneficio = dbConnection.TipoBeneficios.FirstOrDefault(x => x.Description == Convert.ToString(planilha.Cells[linha, 9].Value));
                            //if(conn.TipoBeneficios.Any(o => o.Description == planilha.Cells[linha, 5].Value.ToString())) return;
                            if(checkTipoBeneficio == null)
                            {
                                TipoBeneficio TBeneficio = new TipoBeneficio();
                                TBeneficio.Description = Convert.ToString(planilha.Cells[linha, 9].Value);
                                TBeneficio.Value = Convert.ToInt16((planilha.Cells[linha, 10].Value));
                                TBeneficio.Percent = Convert.ToDouble(planilha.Cells[linha, 11].Value);
                                dbConnection.TipoBeneficios.Add(TBeneficio);
                                dbConnection.SaveChanges();
                            }
                            
                            var tipoBeneficioId = dbConnection.TipoBeneficios.FirstOrDefault(x => x.Description == Convert.ToString(planilha.Cells[linha, 9].Value)).Id;
                            object a = planilha.Cells[linha, 9].Value;

                            if(checkTipoBeneficio == null || checkNivel == null)
                            {
                                Beneficio beneficio = new Beneficio();
                                beneficio.NivelID = nivelID;
                                beneficio.TipoBeneficioId = tipoBeneficioId;
                                dbConnection.Beneficios.Add(beneficio);
                                dbConnection.SaveChanges();
                            }

                            var checkValor = dbConnection.DepositosBeneficios.FirstOrDefault(x => x.Value == Convert.ToDouble(planilha.Cells[linha, 18].Value));
                            var checkVencimento = dbConnection.DepositosBeneficios.FirstOrDefault(x => x.Vencimento == Convert.ToDateTime(planilha.Cells[linha, 19].Value));
                            var funcionarioId = dbConnection.Funcionarios.FirstOrDefault(x => x.Cpf == Convert.ToString(planilha.Cells[linha, 5].Value)).Id;
                        
                            var beneficioId = dbConnection.Beneficios.FirstOrDefault(x => x.TipoBeneficioId == tipoBeneficioId && x.NivelID == nivelID).Id;

                            if(checkValor == null && checkVencimento == null)
                            {
                                DepositoBeneficio DBeneficio = new DepositoBeneficio();
                                DBeneficio.Value = Convert.ToDouble(planilha.Cells[linha, 18].Value);
                                DBeneficio.Vencimento = Convert.ToDateTime(planilha.Cells[linha, 19].Value);
                                DBeneficio.BeneficioId = beneficioId;
                                DBeneficio.FuncionarioId = funcionarioId;
                                dbConnection.DepositosBeneficios.Add(DBeneficio);
                                dbConnection.SaveChanges();
                            }

                        }

                        else if(option.Contains("A"))
                        {
                            
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
        public DbSet<Endereco> Enderecos { get; set;}
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-SRU8BTNN;Database=EFCore.Demo;Trusted_Connection=True;");
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

