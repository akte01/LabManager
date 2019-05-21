using System.ComponentModel.DataAnnotations.Schema;

namespace LabStore.Models
{
    public class SolidOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public byte UnitId { get; set; }
    }
}