using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoExample.Models;

public class Trainer
{

    // The [BsonId] attribute is used to annotate a property in a class that represents the _id in a collection.
    // It specifies that the property should be mapped to the _id field when the class is serialized to or deserialized from a BSON document.
    // For example one of the trainers is mapped as _id: ObjectId('63a291e1c255c7bdcd02e826') in mongodb atlas.
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    // Here we define what attributes a trainer should have. Each attribute has a get and set method. Get is used to retrieve,
    // and set is used to assign a new value.
    // The main difference between Id and name is that Id can be left as null when for example creating a trainer because an Id is
    // automatically generated. Name, region and pokemons cannot be null.
    public string? Id { get; set; }

    public string name { get; set; } = null!;

    public string region { get; set; } = null!;

    // Here we specify that "pokemons" should be the name of the BsonElement when it is converted to and from a bson document.
    // JsonPropertyName specifies the name of the property when in a Json format. The array is thus named pokemons.
    [BsonElement("pokemons")]
    [JsonPropertyName("pokemons")]
    public List<string> pokemons { get; set; } = null!;

}