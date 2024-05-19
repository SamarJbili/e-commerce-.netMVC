using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class commande : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commandes",
                columns: table => new
                {
                    CommandeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilisateurId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCommande = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstPayee = table.Column<bool>(type: "bit", nullable: false),
                    EstLivree = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commandes", x => x.CommandeId);
                    table.ForeignKey(
                        name: "FK_Commandes_AspNetUsers_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticlesCommandes",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    CommandeId = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    PrixUnitaire = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlesCommandes", x => new { x.ArticleId, x.CommandeId });
                    table.ForeignKey(
                        name: "FK_ArticlesCommandes_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticlesCommandes_Commandes_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commandes",
                        principalColumn: "CommandeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesCommandes_CommandeId",
                table: "ArticlesCommandes",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_UtilisateurId",
                table: "Commandes",
                column: "UtilisateurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticlesCommandes");

            migrationBuilder.DropTable(
                name: "Commandes");
        }
    }
}
