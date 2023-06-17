namespace Insurance.Common.Dto;

public class RequestDto : BaseDto
{
    public string Title { get; set; }
    
    public string PersonFullName { get; set; }
    public long PersonId { get; set; }

    public string TypeTitle { get; set; }
    public long TypeId { get; set; }
    
    public long Investment { get; set; }
    
    public long Payment { get; set; }
}