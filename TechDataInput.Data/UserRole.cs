namespace TechDataInput.Data
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Name { get; set; } // "Оператор", "Технолог", "Инженер"

        public ICollection<ParameterDefinition> ParameterDefinitions { get; set; }
    }

}
