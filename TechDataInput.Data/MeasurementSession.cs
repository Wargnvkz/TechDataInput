namespace TechDataInput.Data
{
    public class MeasurementSession
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }

        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        public int UserRoleId { get; set; }
        public string EnteredBy { get; set; }

        public ICollection<ParameterValue>? Values { get; set; }
    }

}
