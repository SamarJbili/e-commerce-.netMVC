using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace e_commerce.ViewModels
{
    public class CreateRoleViewModel
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Role")]

        public string RoleName { get; set; }
    }
}
