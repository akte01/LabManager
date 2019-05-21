using LabStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabStore.Dtos
{
    public class EquipmentDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public int Amount { get; set; }

        public EquipmentLocation EquipmentLocation { get; set; }

        [Required]
        public byte EquipmentLocationId { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }
    }
}