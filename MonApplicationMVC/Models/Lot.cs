using System;
using System.ComponentModel.DataAnnotations;
using MonApplicationMVC.Models;  // Importez l'espace de noms contenant la classe Lot


namespace MonApplicationMVC.Models
{
    public class Lot
    {
        [Key]
        public string NumLot { get; set; } // Numéro du Lot (clé primaire)

        public string LotNumEntree { get; set; } // Numéro d'entrée (peut-être une référence à une autre table)

        public DateTime LotDateProd { get; set; } // Date de production

        public string LotClient { get; set; } // Code client

        public string LotFournisseur { get; set; } // Code fournisseur

        public string LotOperateurEnr { get; set; } // Opérateur de l'enregistrement

        public TimeSpan LotDebutEnr { get; set; } // Début de l'enregistrement
    }
}
