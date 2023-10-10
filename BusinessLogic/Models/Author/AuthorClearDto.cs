namespace BusinessLogic.Models.Author;

public class AuthorClearDto
{
    public string Name { get; set; }
    public DateTime? Birth { get; set; }
    public DateTime? Death { get; set; }
    public string? Description { get; set; }
}