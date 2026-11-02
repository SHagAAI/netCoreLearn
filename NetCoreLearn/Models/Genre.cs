namespace netCore.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Navigation property: Many-to-Many with Movie
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
