using System.ComponentModel.DataAnnotations.Schema;

namespace LabStore.Models
{
    public class EquipmentOrder
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }
    }
}