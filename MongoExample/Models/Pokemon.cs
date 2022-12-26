using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoExample.Models;

public class Pokemon
{

    /// <summary>
    /// The pokemons ID (not pokedex ID)
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    /// <summary>
    /// Name of the pokemon
    /// </summary>
    public string name { get; set; } = null!;


    /// <summary>
    /// Types of the pokemon
    /// </summary>
    [BsonElement("types")]
    [JsonPropertyName("types")]
    public List<string> types { get; set; } = null!;


    /// <summary>
    /// List of moves known by the pokemon
    /// </summary>
    [BsonElement("moves")]
    [JsonPropertyName("moves")]
    public List<string> moves { get; set; } = null!;

}