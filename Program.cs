﻿using Microsoft.EntityFrameworkCore;
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
            FileInfo caminho = new FileInfo(@"C:\Users\Joao Maciel\Documents\Infosis\Infosis.xlsx");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using(ExcelPackage package = new ExcelPackage(caminho))
            {

                ExcelWorksheet planilha = package.Workbook.Worksheets[0];
                int maxRows = planilha.Dimension.End.Row;
                using(FuncionarioContext dbConnection = new FuncionarioContext())
                {
                    for(int linha = 2; linha <= maxRows; linha++)
                    {

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
                        var nivelID = dbConnection.NiveisFuncionario.FirstOrDefault(x => x.Tipo == planilha.Cells[linha, 5].Value.ToString()).Id;
                        var checkBeneficio = dbConnection.Beneficios.FirstOrDefault(x => x.NivelID == nivelID);

                        
                        Beneficio beneficio = new Beneficio();
                        beneficio.NivelID = nivelID;
                        beneficio.TipoBeneficioId = tipoBeneficioId;
                        dbConnection.Beneficios.Add(beneficio);
                        dbConnection.SaveChanges();
                        

                        
                        var beneficioId = dbConnection.Beneficios.FirstOrDefault(x => x.NivelID == nivelID).Id;

                        var cargoId = dbConnection.Cargos.FirstOrDefault(x => x.Tipo == planilha.Cells[linha, 11].Value.ToString()).Id;
                        var modalidadeContratoId = dbConnection.ModalidadeDeContratos.FirstOrDefault(x => x.Description == planilha.Cells[linha, 7].Value.ToString()).Id;

                        ModalidadeCargo modalidadeCargo = new ModalidadeCargo();
                        
                        modalidadeCargo.CargoID = cargoId;
                        modalidadeCargo.NivelID = nivelID;
                        modalidadeCargo.ModalidadeContratoID = modalidadeContratoId;

                        dbConnection.ModalidadeCargos.Add(modalidadeCargo);
                        dbConnection.SaveChanges();

                        
                        var checkFuncionario = dbConnection.Funcionarios.FirstOrDefault(x => x.Cpf == long.Parse(planilha.Cells[linha, 4].Value.ToString()));
                        if(checkFuncionario == null)
                        {
                            Funcionario funcionario = new Funcionario();
                            funcionario.Nome = planilha.Cells[linha, 1].Value.ToString();
                            funcionario.Sobrenome = planilha.Cells[linha, 2].Value.ToString();
                            funcionario.Endereco = planilha.Cells[linha, 3].Value.ToString();
                            funcionario.Cpf = int.Parse(planilha.Cells[linha, 4].Value.ToString());
                            dbConnection.Funcionarios.Add(funcionario);
                            dbConnection.SaveChanges();
                        }

                        var checkValor = dbConnection.DepositosBeneficios.FirstOrDefault(x => x.Value == float.Parse(planilha.Cells[linha, 17].Value.ToString()));
                        var checkVencimento = dbConnection.DepositosBeneficios.FirstOrDefault(x => x.Vencimento == planilha.Cells[linha, 18].Value.ToString());
                        var funcionarioId = dbConnection.Funcionarios.FirstOrDefault(x => x.Cpf == long.Parse(planilha.Cells[linha, 4].Value.ToString())).Id;


                        if(checkValor == null && checkVencimento == null)
                        {
                            DepositoBeneficio DBeneficio = new DepositoBeneficio();
                            DBeneficio.Value = float.Parse(planilha.Cells[linha, 17].Value.ToString());
                            DBeneficio.Vencimento = planilha.Cells[linha, 18].Value.ToString();
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
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EFCore.Demo;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Funcionario>().ToTable("Funcionario")
                .HasKey(p => p.Id);

            modelBuilder.Entity<ModalidadeCargo>().HasKey(p => p.Id);

            modelBuilder.Entity<Cargo>().HasKey(p => p.Id);

            modelBuilder.Entity<ModalidadeDeContrato>().HasKey(p => p.Id);

            modelBuilder.Entity<Nivel>().HasKey(p => p.Id);

            modelBuilder.Entity<Deposito>().HasKey(p => p.Id);

            modelBuilder.Entity<TipoBeneficio>().HasKey(p => p.Id);

        }
    }
}

