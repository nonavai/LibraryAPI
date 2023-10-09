namespace BusinessLogic.Models.Author;

public class AuthorClearDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? Birth { get; set; }
    public DateTime? Death { get; set; }
    public string? Description { get; set; }
}