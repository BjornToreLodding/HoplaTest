using System.Text.Json.Serialization;

namespace UsersApi.Models;

public class User
{
    public int Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;

    [JsonIgnore] // Ignorer under serialisering
    public ICollection<Horse>? Horses { get; set; }
}
