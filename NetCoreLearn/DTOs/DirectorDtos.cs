namespace netCore.DTOs;

public class CreateDirectorDto
{
    public string Name { get; set; } = string.Empty;
}

public class UpdateDirectorDto
{
    public string Name { get; set; } = string.Empty;
}


public class DirectorResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<MovieSummaryDto> Movies { get; set; } = new();
}