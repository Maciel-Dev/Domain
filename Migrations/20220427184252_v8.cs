using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoBeneficios_Beneficios_Beneficio",
                table: "TipoBeneficios");

            migrationBuilder.DropIndex(
                name: "IX_TipoBeneficios_Beneficio",
                table: "TipoBeneficios");

            migrationBuilder.DropColumn(
                name: "Beneficio",
                table: "TipoBeneficios");

            migrationBuilder.DropColumn(
                name: "BeneficioId",
                table: "TipoBeneficios");

            migrationBuilder.AlterColumn<int>(
                name: "ModalidadeContratoID",
                table: "ModalidadeCargos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ModalidadeCargos_ModalidadeContratoID",
                table: "ModalidadeCargos",
                column: "ModalidadeContratoID");

            migrationBuilder.AddForeignKey(
                name: "FK_ModalidadeCargos_ModalidadeDeContratos_ModalidadeContratoID",
                table: "ModalidadeCargos",
                column: "ModalidadeContratoID",
                principalTable: "ModalidadeDeContratos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModalidadeCargos_ModalidadeDeContratos_ModalidadeContratoID",
                table: "ModalidadeCargos");

            migrationBuilder.DropIndex(
                name: "IX_ModalidadeCargos_ModalidadeContratoID",
                table: "ModalidadeCargos");

            migrationBuilder.AddColumn<int>(
                name: "Beneficio",
                table: "TipoBeneficios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BeneficioId",
                table: "TipoBeneficios",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModalidadeContratoID",
                table: "ModalidadeCargos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoBeneficios_Beneficio",
                table: "TipoBeneficios",
                column: "Beneficio");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoBeneficios_Beneficios_Beneficio",
                table: "TipoBeneficios",
                column: "Beneficio",
                principalTable: "Beneficios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
