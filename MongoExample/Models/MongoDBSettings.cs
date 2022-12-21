namespace MongoExample.Models;

public class MongoDBSettings
{

    public string ConnectionURI { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string PokemonsCollectionName { get; set; } = null!;
    public string TrainersCollectionName { get; set; } = null!;

}