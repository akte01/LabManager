using LabStore.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabStore.ViewModels
{
    public class EquipmentViewModel
    {
        public IEnumerable<EquipmentLocation> EquipmentLocations { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Proszę wprowadzić nazwę sprzętu.")]
        [StringLength(255)]
        [Display(Name = "Nazwa sprzętu")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proszę wprowadzić ilość.")]
        [Display(Name = "Ilość (szt.)")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Proszę wprowadzić miejsce przechowywania.")]
        [Display(Name = "Miejsce przechowywania")]
        public byte EquipmentLocationId { get; set; }

        [StringLength(255)]
        [Display(Name = "Uwagi")]
        public string Comment { get; set; }

        public EquipmentViewModel()
        {
            Id = 0;
        }

        public string Title
        {
            get
            {
                return Id != 0 ? "Edytuj sprzęt" : "Dodaj sprzęt";
            }
        }

        public string SaveButtonText
        {
            get
            {
                return Id != 0 ? "Zmień dane" : "Dodaj sprzęt";
            }
        }

        public string Subtitle
        {
            get
            {
                return Id != 0 ? "Formularz edycji sprzętu" : "Formularz wprowadzania nowego sprzętu";
            }
        }

        public EquipmentViewModel(Equipment equipment)
        {
            Id = equipment.Id;
            Name = equipment.Name;
            Amount = equipment.Amount;
            EquipmentLocationId = equipment.EquipmentLocationId;
            Comment = equipment.Comment;
        }
    }
}