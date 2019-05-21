using LabStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabStore.ViewModels
{
    public class OrderViewModel
    {
        public IEnumerable<Unit> Units { get; set; }
        public IEnumerable<ConcentrationUnit> ConcentrationUnits { get; set; }
        public IEnumerable<SolidOrder> SolidOrders { get; set; }
        public IEnumerable<SolutionOrder> SolutionOrders { get; set; }
        public IEnumerable<EquipmentOrder> EquipmentOrders { get; set; }
        public IEnumerable<OrderBase> OrderBaseList { get; set; }

        public string UserId { get; set; }

        [Required(ErrorMessage = "Proszę wprowdzić datę na kiedy przygotować zamówienie")]
        [DataType(DataType.Date)]
        [Display(Name = "Na kiedy?")]
        public DateTime DateFor { get; set; }

        [Required(ErrorMessage = "Proszę wprowdzić klasę")]
        [StringLength(3)]
        [Display(Name = "Klasa")]
        public string Grade { get; set; }

        [StringLength(255)]
        [Display(Name = "Uwagi")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Proszę zaznaczyć kategorię zamówienia")]
        public bool IsEquipment { get; set; }

        [Display(Name = "Nazwa sprzętu")]
        public string EquipmentName { get; set; }

        [Display(Name = "Ilość")]
        public int EquipmentAmount { get; set; }

        [Display(Name = "Nazwa odczynnika")]
        public string ReagentName { get; set; }

        public bool IsSolid { get; set; }

        [Display(Name = "Ilość")]
        public decimal ReagentAmount { get; set; }

        [Display(Name = "Jednostka")]
        public byte UnitId { get; set; }

        [Display(Name = "Stężenie")]
        public decimal Concentration { get; set; }

        [Display(Name = "Jednostka stężenia")]
        public byte ConcentrationUnitId { get; set; }

        [Display(Name = "Objętość w mL")]
        public int Volume { get; set; }   
    }
}