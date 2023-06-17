namespace Insurance.Common.Dto;

public class RequestApiDto : BaseApiDto
{
    public long PersonId { get; set; }

    public long TypeId { get; set; }
    
    public long Investment { get; set; }
}