namespace netCore.DTOs;

public class CreateDirectorDto
{
    public string DirectorName { get; set; } = string.Empty;
}

public class UpdateDirectorDto
{
    public string DirectorName { get; set; } = string.Empty;
}


public class DirectorResponseDto
{
    public int Id { get; set; }
    public string DirectorName { get; set; } = string.Empty;
    public List<MovieSummaryDto> Movies { get; set; } = new();
}