using Microsoft.AspNetCore.Mvc;
using MonApplicationMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace MonApplicationMVC.Controllers
{
    public class ProductionController : Controller
    {
        private static List<ProdEnroul_Enete> productions = new List<ProdEnroul_Enete>();
        private static List<ProdEnroul_Rouleau> rouleaux = new List<ProdEnroul_Rouleau>();

        // Action pour afficher la liste des productions
        public IActionResult Index()
        {
            return View(productions);
        }

        // Action pour créer une nouvelle production (POST)
        [HttpPost]
        public IActionResult Create(ProdEnroul_Enete enete, List<ProdEnroul_Rouleau> rouleauxList)
        {
            if (ModelState.IsValid)
            {
                // Ajouter l'en-tête de production
                productions.Add(enete);

                // Ajouter les rouleaux associés
                foreach (var rouleau in rouleauxList)
                {
                    rouleau.NumLot = enete.NumLot; // Associer les rouleaux au numéro du lot
                    rouleaux.Add(rouleau);
                }

                return RedirectToAction(nameof(Index));
            }

            return View("Index", productions);
        }

        // Action pour effectuer une recherche dynamique
        public IActionResult Search(string searchNumLot, string searchDateDebut)
        {
            // Rechercher dans les productions
            var filteredProductions = productions.AsQueryable();

            if (!string.IsNullOrEmpty(searchNumLot))
            {
                filteredProductions = filteredProductions.Where(p => p.NumLot.Contains(searchNumLot));
            }

            if (!string.IsNullOrEmpty(searchDateDebut) && DateTime.TryParse(searchDateDebut, out DateTime dateDebut))
            {
                filteredProductions = filteredProductions.Where(p => p.DateDebut.Date == dateDebut.Date);
            }

            return Json(filteredProductions);
        }
    }
}
