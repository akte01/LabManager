using System.ComponentModel.DataAnnotations.Schema;

namespace LabStore.ViewModels
{
    public class ReagentAddConsumptionViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public decimal ConsumedAmount { get; set; }
    }
}