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
    
    }
}
