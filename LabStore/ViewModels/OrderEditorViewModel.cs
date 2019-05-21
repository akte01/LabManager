using LabStore.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabStore.ViewModels
{
    public class OrderEditorViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Proszę wprowdzić datę na kiedy przygotować zamówienie")]
        [DataType(DataType.Date)]
        [Display(Name = "Na kiedy?")]
        public DateTime DateFor { get; set; }

        [Required(ErrorMessage = "Proszę wprowdzić klasę")]
        [StringLength(3)]
        [Display(Name = "Klasa")]
        public string Grade { get; set; }

        [Required(ErrorMessage = "Proszę wprowdzić treść zamówienia")]
        [Display(Name = "Zamówienie")]
        public string Content { get; set; }

        [StringLength(255)]
        [Display(Name = "Uwagi")]
        public string Comment { get; set; }

        public OrderEditorViewModel(Order order)
        {
            Id = order.Id;
            Grade = order.Grade;
            DateFor = order.Date;
            Comment = order.Comment;
            Content = order.Content;
        }
    }
}