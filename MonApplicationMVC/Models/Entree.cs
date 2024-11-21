using System.ComponentModel.DataAnnotations;

namespace MonApplicationMVC.Models
{
    public class Entree
    {
        [Key]
        public string NumEntree { get; set; } = string.Empty; // Numéro d'entrée (clé primaire)
        public DateTime DateArr { get; set; } // Date d'arrivée
        public string CodeFour { get; set; } = string.Empty; // Code fournisseur
        public string CodeClient { get; set; } = string.Empty; // Code client
        public int NbRouleau { get; set; } // Nombre de rouleaux
        public double Largeur { get; set; } // Largeur
    }
}
