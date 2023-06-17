namespace Insurance.Common.Dto;

public class BaseApiDto
{
    public long Id { get; set; }

    public bool IsTransient() => Id == 0;
}