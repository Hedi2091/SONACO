using Microsoft.AspNetCore.Mvc;
using MonApplicationMVC.Data;
using MonApplicationMVC.Models;

namespace MonApplicationMVC.Controllers
{
    public class LotController : Controller
    {
        private readonly AppDbContext _context;

        public LotController(AppDbContext context)
        {
            _context = context;
        }

        // Afficher le formulaire et la liste des lots
        public IActionResult Index(string searchNumLot, DateTime? searchDateProd)
        {
            // Récupérer les lots avec des filtres si spécifiés
            var lots = _context.Lots.AsQueryable();

            // Filtre par NumLot
            if (!string.IsNullOrEmpty(searchNumLot))
            {
                lots = lots.Where(l => l.NumLot.Contains(searchNumLot));
            }

            // Filtre par LotDateProd
            if (searchDateProd.HasValue)
            {
                lots = lots.Where(l => l.LotDateProd.Date == searchDateProd.Value.Date);
            }

            // Retourner les lots filtrés à la vue
            return View(lots.ToList());
        }

        [HttpGet]
        public JsonResult SearchLots(string searchNumLot, string searchDateProd)
        {
            // Filtrer les lots
            var lots = _context.Lots.AsQueryable();

            if (!string.IsNullOrEmpty(searchNumLot))
            {
                lots = lots.Where(l => l.NumLot.Contains(searchNumLot));
            }

            if (DateTime.TryParse(searchDateProd, out var date))
            {
                lots = lots.Where(l => l.LotDateProd.Date == date.Date);
            }

            // Renvoie les données avec les bons noms de propriétés
            var result = lots.Select(l => new
            {
                l.NumLot, // Numéro du Lot
                LotDateProd = l.LotDateProd.ToString("dd/MM/yyyy"), // Format de la date
                l.LotNumEntree, // Numéro d'entrée
                l.LotClient, // Client
                l.LotFournisseur, // Fournisseur
                l.LotOperateurEnr // Opérateur
            }).ToList();

            return Json(result);
        }



        // Ajouter un nouveau lot
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lot lot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lot);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Si le modèle est invalide, rechargez la vue Index avec les lots existants
            var lots = _context.Lots.ToList();
            return View("Index", lots);
        }

        [HttpDelete]
        [Route("Lots/Delete")]
        public IActionResult Delete(string NumLot)
        {
            if (string.IsNullOrEmpty(NumLot))
            {
                return BadRequest(new { success = false, message = "Identifiant non spécifié." });
            }

            var lot = _context.Lots.FirstOrDefault(l => l.NumLot == NumLot);
            if (lot == null)
            {
                return NotFound(new { success = false, message = "Lot non trouvé." });
            }

            _context.Lots.Remove(lot);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Lot supprimé avec succès." });
        }

        [HttpGet]
        [Route("Lots/Edit")]
        public IActionResult Edit(string NumLot)
        {
            if (string.IsNullOrEmpty(NumLot))
            {
                return BadRequest(new { success = false, message = "Numéro de lot non spécifié." });
            }

            var lot = _context.Lots.FirstOrDefault(l => l.NumLot == NumLot);
            if (lot == null)
            {
                return NotFound(new { success = false, message = "Lot non trouvé." });
            }

            return Json(new
            {
                numLot = lot.NumLot,
                dateProd = lot.LotDateProd.ToString("yyyy-MM-dd"),
                numEntree = lot.LotNumEntree,
                client = lot.LotClient,
                fournisseur = lot.LotFournisseur,
                operateur = lot.LotOperateurEnr
            });
        }

        [HttpPost]
        [Route("Lots/Edit")]
        public IActionResult Edit([FromBody] Lot updatedLot)
        {
            if (updatedLot == null || string.IsNullOrEmpty(updatedLot.NumLot))
            {
                return BadRequest(new { success = false, message = "Données invalides." });
            }

            var lot = _context.Lots.FirstOrDefault(l => l.NumLot == updatedLot.NumLot);
            if (lot == null)
            {
                return NotFound(new { success = false, message = "Lot non trouvé." });
            }

            // Mise à jour des champs
            lot.LotDateProd = updatedLot.LotDateProd;
            lot.LotNumEntree = updatedLot.LotNumEntree;
            lot.LotClient = updatedLot.LotClient;
            lot.LotFournisseur = updatedLot.LotFournisseur;
            lot.LotOperateurEnr = updatedLot.LotOperateurEnr;

            _context.SaveChanges();

            return Ok(new { success = true, message = "Lot modifié avec succès." });
        }
    }
}
