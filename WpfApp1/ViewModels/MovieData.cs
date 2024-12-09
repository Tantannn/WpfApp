using System.Text.Json.Serialization;

namespace WpfApp1.ViewModels;

public class MovieData
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("movie")] public string? MovieName { get; set; }
    [JsonPropertyName("rating")] public float Rating { get; set; }
    [JsonPropertyName("image")] public string? Image { get; set; }
    [JsonPropertyName("imdb_url")] public string? ImbdUrl { get; set; }
}