using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDataInput.Data
{
    public class MeasurementSessionDto
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public List<ParameterValueDto> Values { get; set; } = new();
    }

    public class ParameterValueDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string ParameterName { get; set; }
        public int PageNumber { get; set; }
        public int RowOnPage { get; set; }
        public int ColumnInLine { get; set; }
        public int ParameterDefinitionId { get; set; }
        public string? ParameterAddInfo { get; set; }

    }
}
