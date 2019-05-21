using System.ComponentModel.DataAnnotations.Schema;

namespace LabStore.Models
{
    public class SolutionOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public decimal Concentration { get; set; }

        public byte ConcentrationUnitId { get; set; }

        public int Volume{ get; set; }

    }
}