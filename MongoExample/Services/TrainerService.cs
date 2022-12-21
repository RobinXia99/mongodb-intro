using MongoExample.Models;
// Microsoft.Extensions.Options provides us with a way to configure options for our application. Our options follows
// our MongoDBSettings model.
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;

namespace MongoExample.Services;

public class TrainerService : ControllerBase
{

    // This variable contains all the newly configured options from our TrainerService configuration down below.
    private readonly IMongoCollection<Trainer> _trainerCollection;

    public TrainerService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        // Here we specify the keys and names for our client and database.
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _trainerCollection = database.GetCollection<Trainer>(mongoDBSettings.Value.TrainersCollectionName);
    }

    // An asynchronous function that retrieves all trainers from our collection. Find is used to used to retrieve all documents
    // and ToListAsync() converts the retrieves data into an array of trainers.
    public async Task<List<Trainer>> getTrainers()
    {
        return await _trainerCollection.Find(new BsonDocument()).ToListAsync();
    }

    // This function takes in a trainer id from the controller and passes it into the Find function. It retrieves the first matching
    // object where the id is the same as the input id. It returns a trainer if found, else it returns null.
    public async Task<Trainer?> getTrainer(string id)
    {

        return await _trainerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    }

    // A new trainer retrieved [FromBody] is inserted into our trainers collection.
    public async Task createTrainer(Trainer trainer)
    {
        await _trainerCollection.InsertOneAsync(trainer);
        return;
    }

    // A function that adds a pokemonId to selected trainer. The Filter.Eq function finds the document that has an Id equal to our input id.
    // Then through Update.AddToSet, we add our pokemonId to the pokemons array of that found object(trainer).
    // Lastly the UpdateOneAsync function takes in the found object, and the requested update and then updates the document.
    public async Task addPokemonToTrainer(string id, string pokemonId)
    {
        FilterDefinition<Trainer> filter = Builders<Trainer>.Filter.Eq("Id", id);
        UpdateDefinition<Trainer> update = Builders<Trainer>.Update.AddToSet<string>("pokemons", pokemonId);
        await _trainerCollection.UpdateOneAsync(filter, update);
        return;
    }

    // A function that finds the selected trainer and deletes it.
    public async Task deleteTrainer(string id)
    {
        FilterDefinition<Trainer> filter = Builders<Trainer>.Filter.Eq("Id", id);
        await _trainerCollection.DeleteOneAsync(filter);
        return;
    }

}