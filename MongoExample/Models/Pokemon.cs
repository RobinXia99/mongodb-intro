using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoExample.Models;

public class Pokemon
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string name { get; set; } = null!;

    [BsonElement("types")]
    [JsonPropertyName("types")]
    public List<string> types { get; set; } = null!;

    [BsonElement("moves")]
    [JsonPropertyName("moves")]
    public List<string> moves { get; set; } = null!;

}