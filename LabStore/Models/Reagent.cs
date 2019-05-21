namespace LabStore.Models
{
    public class Reagent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal InitialAmount { get; set; }

        public decimal ConsumedAmount { get; set; }

        public decimal FinalAmount { get; set; }

        public StorageLocation StorageLocation { get; set; }

        public byte StorageLocationId { get; set; }

        public Unit Unit { get; set; }

        public byte UnitId { get; set; }

        public string Comment { get; set; }
    }
}