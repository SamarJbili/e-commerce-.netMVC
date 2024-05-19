using System.ComponentModel.DataAnnotations;

namespace e_commerce.Models
{
    public class Categorie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de la catégorie est requis")]
        public string Nom { get; set; }
    }
}
