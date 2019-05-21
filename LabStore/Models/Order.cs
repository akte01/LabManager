using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabStore.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime DateFor { get; set; }

        public string Grade { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; } 

        public string Content { get; set; }

        public byte StatusId { get; set; }

        public Status Status { get; set; }
    }
}