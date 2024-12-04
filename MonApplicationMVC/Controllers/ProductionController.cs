using Microsoft.AspNetCore.Mvc;
using MonApplicationMVC.Data;
using MonApplicationMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonApplicationMVC.Controllers
{
    public class ProductionController : Controller
    {
        private readonly AppDbContext _context;

        public ProductionController(AppDbContext context)
        {
            _context = context;
        }

        // Action pour afficher la page principale
        public IActionResult Index()
        {
            return View();
        }

        // API pour récupérer les lots dynamiquement
        [HttpGet]
        public IActionResult GetLotNumbers()
        {
            var lots = _context.Lots
                .Where(l => !string.IsNullOrEmpty(l.NumLot)) // Filtrer les numéros non nuls ou vides
                .Select(l => new
                {
                    NumLot = l.NumLot // Récupérer uniquement la colonne NumLot
                }).ToList();

            return Json(lots); // Retourner le JSON au frontend
        }

        [HttpGet]
        public IActionResult GetCloture()
        {
            var lots = _context.ProdEnroul_Enete
                .Select(l => new
                {
                    NumLot = l.NumLot, // Include NumLot
                    Cloture = l.Cloture
                })
                .ToList();

            return Json(lots);
        }




        [HttpGet]
        public IActionResult GetProductionDetails(string numLot)
        {
            // Vérifiez si numLot est fourni
            if (string.IsNullOrEmpty(numLot))
            {
                return BadRequest(new { success = false, message = "Le numéro de lot est requis." });
            }

            // Recherchez l'entête correspondant au numéro de lot
            var enete = _context.ProdEnroul_Enete.FirstOrDefault(e => e.NumLot == numLot);

            // Récupérez les rouleaux associés
            var rouleaux = _context.ProdEnroul_Rouleau
                .Where(r => r.NumLot == numLot)
                .ToList();

            // Préparez la réponse même si aucune donnée n'est trouvée
            var response = new
            {
                numLot = enete?.NumLot ?? numLot, // Utilisez numLot si enete est null
                dateDebut = enete?.DateDebut.ToString("yyyy-MM-dd") ?? string.Empty,
                heureDeb = enete?.HeureDeb.ToString(@"hh\:mm") ?? string.Empty,
                longeurTotale = enete?.LongeurTotale ?? 0,
                operateur = enete?.Operateur ?? string.Empty,
                rouleaux = rouleaux.Select(r => new
                {
                    numEnsouple = r.NumEnsouple,
                    numRouleau = r.NumRouleau,
                    poid = r.Poid,
                    metrage = r.Metrage
                }).ToList()
            };

            return Json(response);
        }

        [HttpPost]
        public IActionResult CloturerProduction(string numLot)
        {
            if (string.IsNullOrEmpty(numLot))
            {
                return BadRequest(new { success = false, message = "Le numéro de lot est requis." });
            }

            try
            {
                // Rechercher la production associée au numéro de lot
                var enete = _context.ProdEnroul_Enete.FirstOrDefault(e => e.NumLot == numLot);
                if (enete == null)
                {
                    return NotFound(new { success = false, message = "Aucune production trouvée pour ce numéro de lot." });
                }

                // Modifier uniquement la valeur de Cloture
                enete.Cloture = 1;

                // Sauvegarder les modifications
                _context.SaveChanges();

                return Ok(new { success = true, message = "Production clôturée avec succès." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Une erreur s'est produite lors de la clôture.", error = ex.Message });
            }
        }


        [HttpGet]
        public IActionResult ViewProduction(string numLot)
        {
            if (string.IsNullOrEmpty(numLot))
            {
                return BadRequest(new { success = false, message = "Le numéro de lot est requis." });
            }

            try
            {
                var enete = _context.ProdEnroul_Enete.FirstOrDefault(e => e.NumLot == numLot);
                if (enete == null)
                {
                    return NotFound(new { success = false, message = "Aucune production trouvée pour ce numéro de lot." });
                }

                var rouleaux = _context.ProdEnroul_Rouleau
                    .Where(r => r.NumLot == numLot)
                    .ToList();

                var response = new
                {
                    numLot = enete.NumLot,
                    dateDebut = enete.DateDebut.ToString("yyyy-MM-dd"),
                    heureDeb = enete.HeureDeb.ToString(@"hh\:mm"),
                    longeurTotale = enete.LongeurTotale,
                    operateur = enete.Operateur,
                    rouleaux = rouleaux.Select(r => new
                    {
                        numEnsouple = r.NumEnsouple,
                        numRouleau = r.NumRouleau,
                        poid = r.Poid,
                        metrage = r.Metrage
                    })
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Une erreur s'est produite.", error = ex.Message });
            }
        }



        // Enregistrer les données de production (POST)
        [HttpPost]
        public IActionResult SaveProduction([FromBody] ProductionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Requête invalide." });
            }

            try
            {
                // Vérifier si NumLot existe déjà dans ProdEnroul_Enete
                var existingEnete = _context.ProdEnroul_Enete.FirstOrDefault(e => e.NumLot == request.NumLot);
                if (existingEnete == null)
                {
                    // Ajouter une nouvelle entrée dans ProdEnroul_Enete
                    var productionEnete = new ProdEnroul_Enete
                    {
                        NumLot = request.NumLot,
                        DateDebut = request.DateDebut,
                        HeureDeb = request.HeureDeb,
                        LongeurTotale = request.LongeurTotale,
                        Operateur = request.Operateur,
                        Cloture = 0
                    };
                    _context.ProdEnroul_Enete.Add(productionEnete);
                    _context.SaveChanges();
                }

                // Associer les rouleaux avec ProdEnroul_Enete
                foreach (var rouleau in request.Rouleaux)
                {
                    var existingRouleau = _context.ProdEnroul_Rouleau.FirstOrDefault(r =>
                        r.NumEnsouple == rouleau.NumEnsouple &&
                        r.NumRouleau == rouleau.NumRouleau &&
                        r.NumLot == request.NumLot);

                    if (existingRouleau == null)
                    {
                        var rouleauEntity = new ProdEnroul_Rouleau
                        {
                            NumEnsouple = rouleau.NumEnsouple,
                            NumRouleau = rouleau.NumRouleau,
                            Poid = rouleau.Poid,
                            Metrage = rouleau.Metrage,
                            NumLot = request.NumLot
                        };
                        _context.ProdEnroul_Rouleau.Add(rouleauEntity);
                    }
                }

                _context.SaveChanges();

                return Ok(new { success = true, message = "La production a été enregistrée avec succès." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Une erreur s'est produite.", error = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult SearchByLot(string lotNumber)
        {
            if (string.IsNullOrWhiteSpace(lotNumber))
            {
                return Json(new { success = false, message = "Numéro de lot non fourni." });
            }

            var lots = _context.ProdEnroul_Enete
                .Where(l => l.NumLot.Contains(lotNumber))
                .Select(l => new
                {
                    l.NumLot
                }).ToList();

            return Json(new { success = true, data = lots });
        }



        public class ProductionRequest
        {
            public string NumLot { get; set; }
            public DateTime DateDebut { get; set; }
            public TimeSpan HeureDeb { get; set; }
            public int LongeurTotale { get; set; }
            public string Operateur { get; set; }
            public List<RouleauRequest> Rouleaux { get; set; }
        }

        public class RouleauRequest
        {
            public int NumEnsouple { get; set; }
            public int NumRouleau { get; set; }
            public decimal Poid { get; set; }
            public int Metrage { get; set; }
        }

        public class ProductionDetailsViewModel
        {
            public string NumLot { get; set; }
            public DateTime DateDebut { get; set; }
            public TimeSpan HeureDeb { get; set; }
            public int LongeurTotale { get; set; }
            public string Operateur { get; set; }
            public List<RouleauDetailsViewModel> Rouleaux { get; set; }
        }

        public class RouleauDetailsViewModel
        {
            public int NumEnsouple { get; set; }
            public int NumRouleau { get; set; }
            public decimal Poid { get; set; }
            public int Metrage { get; set; }
        }


    }
}
