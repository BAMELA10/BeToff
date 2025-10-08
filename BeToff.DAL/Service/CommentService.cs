using BeToff.Entities;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using BeToff.DAL.Interface;

namespace BeToff.DAL.Service;

public class CommentService : ICommentService
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
            commentDatabaseSettings.Value.CommentCollectionName
            );
    }

    public async Task<List<Comment>> GetCommentsByFamilyPicture(string IdPictureFamily)
    {
        var ListCommentForPicture = await _commentCollection
            .Find<Comment>(x => x.PhotoFamily == IdPictureFamily)
            .ToListAsync();
        return ListCommentForPicture;
    }

    public async Task InsertComment(Comment comment)
    {
        await _commentCollection.InsertOneAsync(comment);
    }

    public async Task<Comment> DeleteCommentById(string Id)
    {
        return await _commentCollection.FindOneAndDeleteAsync(x => x.Id == Id);
    }
}