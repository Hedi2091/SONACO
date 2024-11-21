using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonApplicationMVC.Models
{
    public class ProdEnroul_Rouleau
    {
        [Key]
        public string NumLot { get; set; } = string.Empty; // Numéro de lot

        public int NumEnsouple { get; set; } // Numéro d'ensouple

        public int NumRouleau { get; set; } // Numéro de rouleau

        public decimal Poid { get; set; } // Poids

        public int Metrage { get; set; } // Métrage inscrit

        public ProdEnroul_Enete? Enete { get; set; } // La propriété peut être null
    }
}
