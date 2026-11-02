namespace netCore.DTOs;

public class CreateGenreDto
{
    public string Name { get; set; } = string.Empty;
}

public class UpdateGenreDto
{
    public string Name { get; set; } = string.Empty;
}

public class GenreResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<MovieSummaryDto> Movies { get; set; } = new();
}

public class GenreSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
