namespace TechDataInput.Client.Services
{
    public class MeasurementSessionForm
    {
        public int EquipmentId { get; set; }
        public string? EnteredBy { get; set; } // или ID пользователя
        public int UserRoleId { get; set; }

        public List<ParameterInput> Values { get; set; } = new();
    }

    public class ParameterInput
    {
        public int ParameterDefinitionId { get; set; }
        public string Value { get; set; }

    }
}
