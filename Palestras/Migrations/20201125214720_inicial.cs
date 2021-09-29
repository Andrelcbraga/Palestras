using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Palestras.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Palestrantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Qualificacao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Experiencia = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    DataPalestra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraPalestra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Local = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palestrantes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Palestrantes");
        }
    }
}
