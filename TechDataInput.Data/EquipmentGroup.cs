namespace TechDataInput.Data
{
    public class EquipmentGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }  // например "Компрессоры", "Насосы"

        public ICollection<Equipment> Equipments { get; set; }
        public ICollection<ParameterDefinition> ParameterDefinitions { get; set; }
    }

}
