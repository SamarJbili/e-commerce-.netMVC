using e_commerce.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e_commerce.ViewModels
{
    public class EditViewModel :CreateViewModel
    {
        public int Id { get; set; }
        public List<UserRoleViewModel> Users { get; set; }
        public string ExistingImagePath { get; set; }
    }
}
