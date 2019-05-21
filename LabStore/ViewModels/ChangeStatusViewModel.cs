using System.ComponentModel.DataAnnotations.Schema;

namespace LabStore.ViewModels
{
    public class ChangeStatusViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public byte StatusId { get; set; }
    }
}