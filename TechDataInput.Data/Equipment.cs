namespace TechDataInput.Data
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int EquipmentGroupId { get; set; }
        public EquipmentGroup EquipmentGroup { get; set; }
    }

}
