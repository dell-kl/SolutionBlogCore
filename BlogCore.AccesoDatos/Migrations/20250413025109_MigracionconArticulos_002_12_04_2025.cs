using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracionconArticulos_002_12_04_2025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "categoria_orden",
                table: "Categoria",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Articulo",
                columns: table => new
                {
                    articulo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    articulo_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    articulo_descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    articulo_fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    articulo_rutaImagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    articulo_categoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulo", x => x.articulo_id);
                    table.ForeignKey(
                        name: "FK_Articulo_Categoria_articulo_categoriaId",
                        column: x => x.articulo_categoriaId,
                        principalTable: "Categoria",
                        principalColumn: "categoria_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_articulo_categoriaId",
                table: "Articulo",
                column: "articulo_categoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articulo");

            migrationBuilder.AlterColumn<int>(
                name: "categoria_orden",
                table: "Categoria",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
