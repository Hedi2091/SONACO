using System;
using System.ComponentModel.DataAnnotations;

namespace MonApplicationMVC.Models
{
    public class ProdEnroul_Enete
    {
        public string NumLot { get; set; } = string.Empty; // Numéro de lot (clé primaire)

        public DateTime DateDebut { get; set; } = DateTime.Now; // Date de début

        public TimeSpan HeureDeb { get; set; } = DateTime.Now.TimeOfDay; // Heure de début

        public int LongeurTotale { get; set; } // Longueur totale

        public string Operateur { get; set; } = string.Empty; // Opérateur
    }
}
