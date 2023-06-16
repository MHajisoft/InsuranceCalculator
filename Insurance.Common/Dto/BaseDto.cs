namespace Insurance.Common.Dto;

public class BaseDto
{
    public long Id { get; set; }

    public string CreateUser { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsTransient() => Id == 0;
}