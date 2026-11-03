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

// displaying movie inside director operation
public class MovieSummaryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
}

public class MovieResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int DirectorId { get; set; }
    public string DirectorName { get; set; } = string.Empty;
    public List<GenreSummaryDto> Genres { get; set; } = new();
}
