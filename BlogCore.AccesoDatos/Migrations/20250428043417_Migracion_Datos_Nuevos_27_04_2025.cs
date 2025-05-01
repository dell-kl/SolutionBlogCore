using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migracion_Datos_Nuevos_27_04_2025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaProducto",
                columns: table => new
                {
                    categoriaProducto_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoriaProducto_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    categoriaProducto_fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    categoriaProducto_fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaProducto", x => x.categoriaProducto_id);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    producto_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    producto_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    producto_precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    producto_stock = table.Column<int>(type: "int", nullable: false),
                    producto_disponiblidad = table.Column<bool>(type: "bit", nullable: false),
                    producto_descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    producto_Esdescuento = table.Column<bool>(type: "bit", nullable: false),
                    producto_descuento = table.Column<int>(type: "int", nullable: false),
                    producto_fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    producto_fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    producto_categoriaProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.producto_id);
                    table.ForeignKey(
                        name: "FK_Producto_CategoriaProducto_producto_categoriaProductoId",
                        column: x => x.producto_categoriaProductoId,
                        principalTable: "CategoriaProducto",
                        principalColumn: "categoriaProducto_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImagenesProducto",
                columns: table => new
                {
                    imagenesProducto_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imagenesProducto_ruta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imagenesProducto_estado = table.Column<bool>(type: "bit", nullable: false),
                    imagenesProducto_fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    imagenesProducto_fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    imagenesProducto_ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagenesProducto", x => x.imagenesProducto_id);
                    table.ForeignKey(
                        name: "FK_ImagenesProducto_Producto_imagenesProducto_ProductoId",
                        column: x => x.imagenesProducto_ProductoId,
                        principalTable: "Producto",
                        principalColumn: "producto_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpinionesProducto",
                columns: table => new
                {
                    opinionesProducto_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    opinionesProducto_tema = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opinionesProducto_descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opinionesProducto_recursoRuta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opinionesProducto_puntuacion = table.Column<int>(type: "int", nullable: false),
                    opinionesProducto_fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    opinionesProducto_fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    opinionesProducto_productoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpinionesProducto", x => x.opinionesProducto_id);
                    table.ForeignKey(
                        name: "FK_OpinionesProducto_Producto_opinionesProducto_productoId",
                        column: x => x.opinionesProducto_productoId,
                        principalTable: "Producto",
                        principalColumn: "producto_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideosProducto",
                columns: table => new
                {
                    videosProducto_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    videosProducto_ruta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    videosProducto_estado = table.Column<bool>(type: "bit", nullable: false),
                    videosProducto_fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    videosProducto_fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    videosProducto_ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideosProducto", x => x.videosProducto_id);
                    table.ForeignKey(
                        name: "FK_VideosProducto_Producto_videosProducto_ProductoId",
                        column: x => x.videosProducto_ProductoId,
                        principalTable: "Producto",
                        principalColumn: "producto_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagenesProducto_imagenesProducto_ProductoId",
                table: "ImagenesProducto",
                column: "imagenesProducto_ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_OpinionesProducto_opinionesProducto_productoId",
                table: "OpinionesProducto",
                column: "opinionesProducto_productoId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_producto_categoriaProductoId",
                table: "Producto",
                column: "producto_categoriaProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_VideosProducto_videosProducto_ProductoId",
                table: "VideosProducto",
                column: "videosProducto_ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagenesProducto");

            migrationBuilder.DropTable(
                name: "OpinionesProducto");

            migrationBuilder.DropTable(
                name: "VideosProducto");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "CategoriaProducto");
        }
    }
}
