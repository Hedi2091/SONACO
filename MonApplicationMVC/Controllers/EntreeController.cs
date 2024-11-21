using Microsoft.AspNetCore.Mvc;
using MonApplicationMVC.Data;
using MonApplicationMVC.Models;

namespace MonApplicationMVC.Controllers
{
    public class EntreeController : Controller
    {
        private readonly AppDbContext _context;

        public EntreeController(AppDbContext context)
        {
            _context = context;
        }

        // Afficher le formulaire et la liste des entrées
        public IActionResult Index(string searchNumEntree, DateTime? searchDateArr)
        {
            // Récupérer les entrées avec des filtres si spécifiés
            var entrees = _context.Entree.AsQueryable();

            // Filtre par NumEntree
            if (!string.IsNullOrEmpty(searchNumEntree))
            {
                entrees = entrees.Where(e => e.NumEntree.Contains(searchNumEntree));
            }

            // Filtre par DateArr
            if (searchDateArr.HasValue)
            {
                entrees = entrees.Where(e => e.DateArr.Date == searchDateArr.Value.Date);
            }

            // Retourner les entrées filtrées à la vue
            return View(entrees.ToList());
        }


        [HttpGet]
        public JsonResult SearchEntrees(string searchNumEntree, string searchDateArr)
        {
            // Filtrer les entrées
            var entrees = _context.Entree.AsQueryable();

            if (!string.IsNullOrEmpty(searchNumEntree))
            {
                entrees = entrees.Where(e => e.NumEntree.Contains(searchNumEntree));
            }

            if (DateTime.TryParse(searchDateArr, out var date))
            {
                entrees = entrees.Where(e => e.DateArr.Date == date.Date);
            }

            // Renvoie les données avec les bons noms de propriétés
            var result = entrees.Select(e => new
            {
                e.NumEntree, // Numéro d'entrée
                DateArr = e.DateArr.ToString("dd/MM/yyyy"), // Format de la date
                e.CodeFour, // Code fournisseur
                e.CodeClient, // Code client
                e.NbRouleau, // Nombre de rouleaux
                e.Largeur // Largeur
            }).ToList();

            return Json(result);
        }


        // Ajouter une nouvelle entrée
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Entree entree)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entree);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Si le modèle est invalide, rechargez la vue Index avec les entrées existantes
            var entrees = _context.Entree.ToList();
            return View("Index", entrees);
        }
    }
}
