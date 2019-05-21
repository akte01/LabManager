using LabStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabStore.Dtos
{
    public class ReagentDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public decimal InitialAmount { get; set; }

        public decimal ConsumedAmount { get; set; }

        public decimal FinalAmount { get; set; }

        public StorageLocation StorageLocation { get; set; }

        [Required]
        public byte StorageLocationId { get; set; }

        public Unit Unit{ get; set; }

        [Required]
        public byte UnitId { get; set; }

        [StringLength(255)]

        public string Comment { get; set; }
    }
}