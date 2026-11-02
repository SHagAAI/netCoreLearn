using EFCoreMovieCRUD.Models;

namespace netCore.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }

    public int DirectorId { get; set; }
    public virtual Director? Director { get; set; }

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
