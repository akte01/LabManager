using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabStore.Models
{
    public class OrderBase 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateFor { get; set; }

        public string Grade { get; set; }

        public string Comment { get; set; }
    }
}