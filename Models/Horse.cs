using System.Text.Json.Serialization;

namespace UsersApi.Models;

public class Horse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Owner { get; set; }

    [JsonIgnore] // Ignorer under serialisering
    public User? User { get; set; }
}
