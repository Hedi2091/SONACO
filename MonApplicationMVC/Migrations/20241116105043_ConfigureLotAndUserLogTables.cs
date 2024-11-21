using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonApplicationMVC.Migrations
{
    public partial class ConfigureLotAndUserLogTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ajouter une vérification si la table existe déjà
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS `Lots` (
                    `NumLot` varchar(255) NOT NULL,
                    `LotNumEntree` longtext NOT NULL,
                    `LotDateProd` datetime(6) NOT NULL,
                    `LotClient` longtext NOT NULL,
                    `LotFournisseur` longtext NOT NULL,
                    `LotOperateurEnr` longtext NOT NULL,
                    `LotDebutEnr` time(6) NOT NULL,
                    PRIMARY KEY (`NumLot`)
                ) CHARACTER SET=utf8mb4;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Supprimer la table lors de l'annulation de la migration
            migrationBuilder.Sql("DROP TABLE IF EXISTS `Lots`;");
        }
    }
}
