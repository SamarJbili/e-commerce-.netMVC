using e_commerce.Models;

namespace e_commerce.ViewModels

{
    public class PanierIndexViewModel
    {
        public IEnumerable<LignePanier> LignesPanier { get; set; }
        public float Total { get; set; }
    }
}
