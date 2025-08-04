namespace TechDataInput.Data
{
    public class ParameterValue
    {
        public int Id { get; set; }

        public int MeasurementSessionId { get; set; }
        public MeasurementSession MeasurementSession { get; set; }

        public int ParameterDefinitionId { get; set; }
        public ParameterDefinition ParameterDefinition { get; set; }

        public string Value { get; set; }
    }

}
