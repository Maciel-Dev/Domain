using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class V1 : Migration
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
                    ModalidadeContratoID = table.Column<int>(type: "int", nullable: false),
                    CargoID = table.Column<int>(type: "int", nullable: false),
                    NivelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModalidadeCargos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModalidadeCargos_Cargos_CargoID",
                        column: x => x.CargoID,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModalidadeCargos_NiveisFuncionario_NivelID",
                        column: x => x.NivelID,
                        principalTable: "NiveisFuncionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beneficios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NivelID = table.Column<int>(type: "int", nullable: false),
                    TipoBeneficioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beneficios_NiveisFuncionario_NivelID",
                        column: x => x.NivelID,
                        principalTable: "NiveisFuncionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beneficios_TipoBeneficios_TipoBeneficioId",
                        column: x => x.TipoBeneficioId,
                        principalTable: "TipoBeneficios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cpf = table.Column<int>(type: "int", nullable: false),
                    ModalidadeCargoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionario_ModalidadeCargos_ModalidadeCargoId",
                        column: x => x.ModalidadeCargoId,
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
                    Vencimento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeneficioId = table.Column<int>(type: "int", nullable: false),
                    FuncionarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositosBeneficios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepositosBeneficios_Beneficios_BeneficioId",
                        column: x => x.BeneficioId,
                        principalTable: "Beneficios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepositosBeneficios_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beneficios_NivelID",
                table: "Beneficios",
                column: "NivelID");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficios_TipoBeneficioId",
                table: "Beneficios",
                column: "TipoBeneficioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepositosBeneficios_BeneficioId",
                table: "DepositosBeneficios",
                column: "BeneficioId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositosBeneficios_FuncionarioId",
                table: "DepositosBeneficios",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_ModalidadeCargoId",
                table: "Funcionario",
                column: "ModalidadeCargoId");

            migrationBuilder.CreateIndex(
                name: "IX_ModalidadeCargos_CargoID",
                table: "ModalidadeCargos",
                column: "CargoID");

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
                name: "ModalidadeDeContratos");

            migrationBuilder.DropTable(
                name: "Beneficios");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "TipoBeneficios");

            migrationBuilder.DropTable(
                name: "ModalidadeCargos");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "NiveisFuncionario");
        }
    }
}
