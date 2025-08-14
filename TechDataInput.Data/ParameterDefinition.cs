namespace TechDataInput.Data
{
    public class ParameterDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int PageNumber { get; set; }
        public int OrderOnPage { get; set; }

        public int EquipmentGroupId { get; set; }
        public EquipmentGroup? EquipmentGroup { get; set; }

        public int UserRoleId { get; set; } // кто отвечает за этот параметр
        public UserRole? UserRole { get; set; }

        public ICollection<ParameterValue>? Values { get; set; }
    }

}
