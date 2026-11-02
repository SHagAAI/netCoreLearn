namespace netCore.DTOs;

public class CreateMovieDto
{
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int DirectorId { get; set; }
    public List<int> GenreIds { get; set; } = new();
}

public class UpdateMovieDto
{
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int DirectorId { get; set; }
    public List<int> GenreIds { get; set; } = new();
}

public class MovieSummaryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
}