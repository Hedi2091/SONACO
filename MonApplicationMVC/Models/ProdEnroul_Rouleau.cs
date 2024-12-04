using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonApplicationMVC.Models
{
    public class ProdEnroul_Rouleau
    {
        [Key]
        public int Id { get; set; }

        public int NumEnsouple { get; set; } // Numéro d'ensouple

        public int NumRouleau { get; set; } // Numéro de rouleau

        public decimal Poid { get; set; } // Poids

        public int Metrage { get; set; } // Métrage inscrit

        // Clé étrangère vers ProdEnroul_Enete
        [ForeignKey("ProdEnroul_Enete")]
        public string NumLot { get; set; }

        public ProdEnroul_Enete ProdEnroul_Enete { get; set; } // Navigation vers l'entité parent
    }

}
