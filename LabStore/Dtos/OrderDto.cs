using LabStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LabStore.Dtos
{
    public class OrderDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateFor { get; set; }

        public string Grade { get; set; }

        public string Comment { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string Content { get; set; }

        public byte StatusId { get; set; }

        public Status Status { get; set; }
    }
}