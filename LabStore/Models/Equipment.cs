namespace LabStore.Models
{
    public class Equipment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        public EquipmentLocation EquipmentLocation { get; set; }

        public byte EquipmentLocationId { get; set; }

        public string Comment { get; set; }
    }
}
