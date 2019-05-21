using LabStore.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabStore.ViewModels
{
    public class ReagentViewModel
    {
        public IEnumerable<StorageLocation> StorageLocations { get; set; }
        public IEnumerable<Unit> Units { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Proszę wprowadzić nazwę odczynnika chemicznego.")]
        [StringLength(255)]
        [Display(Name = "Nazwa odczynnika")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proszę wprowadzić ilość początkową.")]
        [Display(Name = "Ilość początkowa (g, mL, szt.)")]
        public decimal InitialAmount { get; set; }

        [Display(Name = "Ilość zużyta (g, mL, szt.)")]
        public decimal ConsumedAmount { get; set; }

        [Display(Name = "Ilość końcowa (g, mL, szt.)")]
        public decimal FinalAmount { get; set; }

        [Required(ErrorMessage = "Proszę wprowadzić miejsce przechowywania.")]
        [Display(Name = "Miejsce przechowywania")]
        public byte StorageLocationId { get; set; }

        [Required(ErrorMessage = "Proszę wprowadzić jednostkę.")]
        [Display(Name = "Jednostka")]
        public byte UnitId { get; set; }

        [StringLength(255)]
        [Display(Name = "Uwagi")]
        public string Comment { get; set; }

        public ReagentViewModel()
        {
            Id = 0;
        }

        public string Title
        {
            get
            {
                return Id != 0 ? "Edytuj odczynnik" : "Dodaj odczynnik";
            }
        }

        public string SaveButtonText
        {
            get
            {
                return Id != 0 ? "Zmień dane" : "Dodaj odczynnik";
            }
        }

        public string Subtitle
        {
            get
            {
                return Id != 0 ? "Formularz edycji odczynnika" : "Formularz wprowadzania nowego odczynnika";
            }
        }

        public ReagentViewModel(Reagent reagent)
        {
            Id = reagent.Id;
            Name = reagent.Name;
            InitialAmount = reagent.InitialAmount;
            ConsumedAmount = reagent.ConsumedAmount;
            FinalAmount = reagent.FinalAmount;
            StorageLocationId = reagent.StorageLocationId;
            UnitId = reagent.UnitId;
            Comment = reagent.Comment;
        }
    }
}