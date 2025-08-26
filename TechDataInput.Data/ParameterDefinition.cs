namespace TechDataInput.Data
{
    public class ParameterDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? AddInfo { get; set; }
        public int PageNumber { get; set; }
        public int RowOnPage { get; set; }
        public int ColumnInLine { get; set; }
        public string? ListOfValues { get; set; }

        public int EquipmentGroupId { get; set; }
        public EquipmentGroup? EquipmentGroup { get; set; }

        public int UserRoleId { get; set; } // кто отвечает за этот параметр
        public UserRole? UserRole { get; set; }

        public ICollection<ParameterValue>? Values { get; set; }

        public void TakeFromData(ParameterDefinition pdFrom)
        {
            Name = pdFrom.Name;
            AddInfo = pdFrom.AddInfo;
            EquipmentGroupId = pdFrom.EquipmentGroupId;
            UserRoleId = pdFrom.UserRoleId;
            PageNumber = pdFrom.PageNumber;
            RowOnPage = pdFrom.RowOnPage;
            ColumnInLine = pdFrom.ColumnInLine;
            ListOfValues = pdFrom.ListOfValues;
        }
    }

}
