using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string name { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Prix en dinar :")]
        public float Prix { get; set; }
        [ForeignKey("Categorie")]
        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }
        [Required]
        [Display(Name = "Image :")]
        public string ImagePath { get; set; }
    }
}
