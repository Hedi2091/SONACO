using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonApplicationMVC.Migrations
{
    public partial class CreateEntreeAndUserLogTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Vérifier si la table 'Entree' existe déjà avant de la créer
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS Entree (
                    NumEntree varchar(20) NOT NULL,
                    DateArr datetime NOT NULL,
                    CodeFour varchar(10) NOT NULL,
                    CodeClient varchar(10) NOT NULL,
                    NbRouleur int NOT NULL,
                    Largeur double NOT NULL,
                    PRIMARY KEY (NumEntree)
                );
            ");

            // Vérifier si la table 'User_log' existe déjà avant de la créer
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS User_log (
                    idUser_log int NOT NULL AUTO_INCREMENT,
                    user longtext NOT NULL,
                    password longtext NOT NULL,
                    PRIMARY KEY (idUser_log)
                );
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Supprimer les tables 'Entree' et 'User_log' lors de la réversion de la migration
            migrationBuilder.DropTable(name: "Entree");
            migrationBuilder.DropTable(name: "User_log");
        }
    }
}
