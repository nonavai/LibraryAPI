namespace LibraryAPI.Requests.Author;

public record AuthorRequest
{
    public string Name { get; set; }
    public DateTime? Birth { get; set; }
    public DateTime? Death { get; set; }
    public string? Description { get; set; }
}