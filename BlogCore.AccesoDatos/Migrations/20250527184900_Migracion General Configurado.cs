using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogCore.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class MigracionGeneralConfigurado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    categoria_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoria_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    categoria_orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.categoria_id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaProducto",
                columns: table => new
                {
                    categoriaProducto_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoriaProducto_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    categoriaProducto_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    categoriaProducto_fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    categoriaProducto_fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaProducto", x => x.categoriaProducto_id);
                });

            migrationBuilder.CreateTable(
                name: "DataProtectionKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Xml = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProtectionKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Etiqueta",
                columns: table => new
                {
                    etiqueta_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    etiqueta_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    etiqueta_color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    etiqueta_disponiblidad = table.Column<bool>(type: "bit", nullable: false),
                    etiqueta_fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    etiqueta_fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiqueta", x => x.etiqueta_id);
                });

            migrationBuilder.CreateTable(
                name: "Slider",
                columns: table => new
                {
                    slider_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    slider_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    slider_estado = table.Column<bool>(type: "bit", nullable: false),
                    slider_rutaImagen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.slider_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articulo",
                columns: table => new
                {
                    articulo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    articulo_nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    articulo_descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    articulo_fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    articulo_rutaImagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    producto_precioDescuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                name: "ComentarioArticulo",
                columns: table => new
                {
                    comentarioArticulo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comentarioArticulo_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    comentarioArticulo_nombrePublicador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    comentarioArticulo_descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    comentarioArticulo_fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    comentarioArticulo_fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    comentarioArticulo_articuloId = table.Column<int>(type: "int", nullable: false),
                    comentarioArticulo_CmtArticulofkID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentarioArticulo", x => x.comentarioArticulo_id);
                    table.ForeignKey(
                        name: "FK_ComentarioArticulo_Articulo_comentarioArticulo_articuloId",
                        column: x => x.comentarioArticulo_articuloId,
                        principalTable: "Articulo",
                        principalColumn: "articulo_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Comentari__comen__4AB81AF0",
                        column: x => x.comentarioArticulo_CmtArticulofkID,
                        principalTable: "ComentarioArticulo",
                        principalColumn: "comentarioArticulo_id");
                });

            migrationBuilder.CreateTable(
                name: "EtiquetaArticulo",
                columns: table => new
                {
                    EtiquetaArticulo_articuloId = table.Column<int>(type: "int", nullable: false),
                    EtiquetaArticulo_etiquetaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_EtiquetaArticulo_Articulo_EtiquetaArticulo_articuloId",
                        column: x => x.EtiquetaArticulo_articuloId,
                        principalTable: "Articulo",
                        principalColumn: "articulo_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EtiquetaArticulo_Etiqueta_EtiquetaArticulo_etiquetaId",
                        column: x => x.EtiquetaArticulo_etiquetaId,
                        principalTable: "Etiqueta",
                        principalColumn: "etiqueta_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReaccionArticulo",
                columns: table => new
                {
                    ReaccionComentario_articuloId = table.Column<int>(type: "int", nullable: false),
                    ReaccionComentario_aspNetUserId = table.Column<int>(type: "int", nullable: false),
                    applicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ReaccionArticulo_Articulo_ReaccionComentario_articuloId",
                        column: x => x.ReaccionComentario_articuloId,
                        principalTable: "Articulo",
                        principalColumn: "articulo_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReaccionArticulo_AspNetUsers_applicationUserId",
                        column: x => x.applicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComentarioProducto",
                columns: table => new
                {
                    comentarioProducto_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comentarioProducto_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    comentarioProducto_nombrePublicador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    comentarioProducto_descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    comentarioProducto_fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    comentarioProducto_fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    comentarioProducto_productoId = table.Column<int>(type: "int", nullable: false),
                    comentarioProducto_CmtProductFkID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentarioProducto", x => x.comentarioProducto_id);
                    table.ForeignKey(
                        name: "FK_ComentarioProducto_Producto_comentarioProducto_productoId",
                        column: x => x.comentarioProducto_productoId,
                        principalTable: "Producto",
                        principalColumn: "producto_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentario_product_4AB81AF0",
                        column: x => x.comentarioProducto_CmtProductFkID,
                        principalTable: "ComentarioProducto",
                        principalColumn: "comentarioProducto_id");
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
                    videosProducto_rutaFotoVideo = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "IX_Articulo_articulo_categoriaId",
                table: "Articulo",
                column: "articulo_categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioArticulo_comentarioArticulo_articuloId",
                table: "ComentarioArticulo",
                column: "comentarioArticulo_articuloId");

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioArticulo_comentarioArticulo_CmtArticulofkID",
                table: "ComentarioArticulo",
                column: "comentarioArticulo_CmtArticulofkID");

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioProducto_comentarioProducto_CmtProductFkID",
                table: "ComentarioProducto",
                column: "comentarioProducto_CmtProductFkID");

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioProducto_comentarioProducto_productoId",
                table: "ComentarioProducto",
                column: "comentarioProducto_productoId");

            migrationBuilder.CreateIndex(
                name: "IX_EtiquetaArticulo_EtiquetaArticulo_articuloId",
                table: "EtiquetaArticulo",
                column: "EtiquetaArticulo_articuloId");

            migrationBuilder.CreateIndex(
                name: "IX_EtiquetaArticulo_EtiquetaArticulo_etiquetaId",
                table: "EtiquetaArticulo",
                column: "EtiquetaArticulo_etiquetaId");

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
                name: "IX_ReaccionArticulo_applicationUserId",
                table: "ReaccionArticulo",
                column: "applicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReaccionArticulo_ReaccionComentario_articuloId",
                table: "ReaccionArticulo",
                column: "ReaccionComentario_articuloId");

            migrationBuilder.CreateIndex(
                name: "IX_VideosProducto_videosProducto_ProductoId",
                table: "VideosProducto",
                column: "videosProducto_ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ComentarioArticulo");

            migrationBuilder.DropTable(
                name: "ComentarioProducto");

            migrationBuilder.DropTable(
                name: "DataProtectionKeys");

            migrationBuilder.DropTable(
                name: "EtiquetaArticulo");

            migrationBuilder.DropTable(
                name: "ImagenesProducto");

            migrationBuilder.DropTable(
                name: "OpinionesProducto");

            migrationBuilder.DropTable(
                name: "ReaccionArticulo");

            migrationBuilder.DropTable(
                name: "Slider");

            migrationBuilder.DropTable(
                name: "VideosProducto");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Etiqueta");

            migrationBuilder.DropTable(
                name: "Articulo");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "CategoriaProducto");
        }
    }
}
