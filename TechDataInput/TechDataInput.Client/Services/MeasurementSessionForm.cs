namespace TechDataInput.Client.Services
{
    public class MeasurementSessionForm
    {
        public int EquipmentId { get; set; }
        public string? EnteredBy { get; set; } // или ID пользователя
        public int UserRoleId { get; set; }

        public List<ParameterInput> Values { get; set; } = new();
        public void Reset()
        {
            EnteredBy = null;
            EquipmentId = 0;
            UserRoleId = 0;
            Values.Clear();
        }
    }

    public class ParameterInput
    {
        public int ParameterDefinitionId { get; set; }
        public string Value { get; set; }

    }
}
