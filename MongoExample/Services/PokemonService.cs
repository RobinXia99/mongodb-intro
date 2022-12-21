using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;

namespace MongoExample.Services;

public class PokemonService : ControllerBase
{

    private readonly IMongoCollection<Pokemon> _pokemonCollection;

    public PokemonService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _pokemonCollection = database.GetCollection<Pokemon>(mongoDBSettings.Value.PokemonsCollectionName);
    }

    public async Task<List<Pokemon>> getPokemons()
    {
        return await _pokemonCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Pokemon?> getPokemon(string id)
    {

        return await _pokemonCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    }


    public async Task createPokemon(Pokemon pokemon)
    {
        await _pokemonCollection.InsertOneAsync(pokemon);
        return;
    }

    public async Task addMoveToPokemon(string id, string move)
    {
        FilterDefinition<Pokemon> filter = Builders<Pokemon>.Filter.Eq("Id", id);
        UpdateDefinition<Pokemon> update = Builders<Pokemon>.Update.AddToSet<string>("moves", move);
        await _pokemonCollection.UpdateOneAsync(filter, update);
        return;
    }
    public async Task deletePokemon(string id)
    {
        FilterDefinition<Pokemon> filter = Builders<Pokemon>.Filter.Eq("Id", id);
        await _pokemonCollection.DeleteOneAsync(filter);
        return;
    }



}