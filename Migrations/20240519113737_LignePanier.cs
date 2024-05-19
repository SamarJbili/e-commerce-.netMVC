using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class LignePanier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Panier_PanierId",
                table: "Article");

            migrationBuilder.DropTable(
                name: "ArticlesPaniers");

            migrationBuilder.DropTable(
                name: "Panier");

            migrationBuilder.DropIndex(
                name: "IX_Article_PanierId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "PanierId",
                table: "Article");

            migrationBuilder.CreateTable(
                name: "LignePanier",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleID = table.Column<int>(type: "int", nullable: false),
                    Qte = table.Column<int>(type: "int", nullable: false),
                    PanierId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LignePanier", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LignePanier_Article_ArticleID",
                        column: x => x.ArticleID,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LignePanier_ArticleID",
                table: "LignePanier",
                column: "ArticleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LignePanier");

            migrationBuilder.AddColumn<int>(
                name: "PanierId",
                table: "Article",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Panier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstValide = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticlesPaniers",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    PanierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlesPaniers", x => new { x.ArticleId, x.PanierId });
                    table.ForeignKey(
                        name: "FK_ArticlesPaniers_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticlesPaniers_Panier_PanierId",
                        column: x => x.PanierId,
                        principalTable: "Panier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_PanierId",
                table: "Article",
                column: "PanierId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesPaniers_PanierId",
                table: "ArticlesPaniers",
                column: "PanierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Panier_PanierId",
                table: "Article",
                column: "PanierId",
                principalTable: "Panier",
                principalColumn: "Id");
        }
    }
}
