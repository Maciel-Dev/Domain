using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Depositos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorDepositoFuncionario = table.Column<double>(type: "float", nullable: false),
                    Data = table.Column<DateTime>(type: "Datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depositos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logradouro = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    CEP = table.Column<int>(type: "int", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModalidadeDeContratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModalidadeDeContratos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NiveisFuncionario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NiveisFuncionario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoBeneficios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Percent = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoBeneficios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModalidadeCargos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModalidadeContratoID = table.Column<int>(type: "int", nullable: true),
                    CargoID = table.Column<int>(type: "int", nullable: true),
                    NivelID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModalidadeCargos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModalidadeCargos_Cargos_CargoID",
                        column: x => x.CargoID,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModalidadeCargos_ModalidadeDeContratos_ModalidadeContratoID",
                        column: x => x.ModalidadeContratoID,
                        principalTable: "ModalidadeDeContratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModalidadeCargos_NiveisFuncionario_NivelID",
                        column: x => x.NivelID,
                        principalTable: "NiveisFuncionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Beneficios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NivelID = table.Column<int>(type: "int", nullable: true),
                    TipoBeneficioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beneficios_NiveisFuncionario_NivelID",
                        column: x => x.NivelID,
                        principalTable: "NiveisFuncionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Beneficios_TipoBeneficios_TipoBeneficioId",
                        column: x => x.TipoBeneficioId,
                        principalTable: "TipoBeneficios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modalidadeCargoId = table.Column<int>(type: "int", nullable: true),
                    enderecoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Enderecos_enderecoId",
                        column: x => x.enderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionarios_ModalidadeCargos_modalidadeCargoId",
                        column: x => x.modalidadeCargoId,
                        principalTable: "ModalidadeCargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepositosBeneficios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Vencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BeneficioId = table.Column<int>(type: "int", nullable: true),
                    FuncionarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositosBeneficios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepositosBeneficios_Beneficios_BeneficioId",
                        column: x => x.BeneficioId,
                        principalTable: "Beneficios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositosBeneficios_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beneficios_NivelID",
                table: "Beneficios",
                column: "NivelID");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficios_TipoBeneficioId",
                table: "Beneficios",
                column: "TipoBeneficioId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositosBeneficios_BeneficioId",
                table: "DepositosBeneficios",
                column: "BeneficioId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositosBeneficios_FuncionarioId",
                table: "DepositosBeneficios",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_enderecoId",
                table: "Funcionarios",
                column: "enderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_modalidadeCargoId",
                table: "Funcionarios",
                column: "modalidadeCargoId");

            migrationBuilder.CreateIndex(
                name: "IX_ModalidadeCargos_CargoID",
                table: "ModalidadeCargos",
                column: "CargoID");

            migrationBuilder.CreateIndex(
                name: "IX_ModalidadeCargos_ModalidadeContratoID",
                table: "ModalidadeCargos",
                column: "ModalidadeContratoID");

            migrationBuilder.CreateIndex(
                name: "IX_ModalidadeCargos_NivelID",
                table: "ModalidadeCargos",
                column: "NivelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Depositos");

            migrationBuilder.DropTable(
                name: "DepositosBeneficios");

            migrationBuilder.DropTable(
                name: "Beneficios");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "TipoBeneficios");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "ModalidadeCargos");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "ModalidadeDeContratos");

            migrationBuilder.DropTable(
                name: "NiveisFuncionario");
        }
    }
}
