namespace Insurance.Common.Dto;

public class CollectionApiDto
{
    public long PersonId { get; set; }

    public string Title { get; set; }

    public List<KeyValuePair<long, long>> TypeList { get; set; }
}