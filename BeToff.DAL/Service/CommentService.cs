using BeToff.Entities;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Query.Internal;


namespace BeToff.DAL.Service;

public class CommentService
{
    private readonly IMongoCollection<Comment> _commentCollection;

    public CommentService(IOptions<CommentDatabaseSettings> commentDatabaseSettings)
    {
        var Client = new MongoClient(
            commentDatabaseSettings.Value.ConnectionString
            );

        var DatabaseName = Client.GetDatabase(
            commentDatabaseSettings.Value.DatabaseName
            );
        _commentCollection = DatabaseName.GetCollection<Comment>(
            commentDatabaseSettings.Value.CommentDatabaseName
            );
    }



}