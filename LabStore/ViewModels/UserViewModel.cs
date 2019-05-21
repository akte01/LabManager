using System.ComponentModel.DataAnnotations;

namespace LabStore.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

    }
}