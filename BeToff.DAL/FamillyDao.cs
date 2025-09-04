using BeToff.DAL.Interface;
using BeToff.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace BeToff.DAL;

public class FamillyDao : BeToffDao, IFamillyDao
{
    private readonly BeToffDbContext _FamillyDb;

    public FamillyDao(BeToffDbContext famillyDb)
    {
        _FamillyDb = famillyDb;
    }


    public async Task<Familly> GetFamillyById(Guid FamillyId)
    {
       return await _FamillyDb.Famillies
            .Where(items => items.Id == FamillyId)
            .Include(a => a.Headof)
            .Include(b => b.CreatedBy)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Familly>> GetFamillyByIdHead(Guid IdHead)
    {
        return await _FamillyDb.Famillies
             .Where(items => items.IdHead == IdHead)
             .ToListAsync();
    }


    public async Task<Guid> CreateFamilly(string NameOfFamilly, Guid UserId)
    {
        Familly familly = new Familly();

        familly.Name = NameOfFamilly;
        familly.IdCreator = UserId;
        familly.IdHead = UserId;
        familly.DateCreation = DateOnly.FromDateTime(DateTime.Now);
        
        await _FamillyDb.AddAsync(familly);
        await _FamillyDb.SaveChangesAsync();

        Guid generetedId = familly.Id;

        return generetedId;


    }

    public async Task<Task> UpateIdHeadOfFamilly(Guid FamillyId, Guid UserId)
    {
        var item = await _FamillyDb.Famillies
            .Where(x => x.Id.Equals(FamillyId))
            .FirstOrDefaultAsync();

        if(item.IdHead == UserId)
        {
            return Task.CompletedTask;
        }

        item.IdHead = UserId;

        _FamillyDb.Famillies.Update(item);
        await _FamillyDb.SaveChangesAsync();

        return Task.CompletedTask;
    }

    public async Task DeleteFamillyById(Guid FamillyId)
    {
        await _FamillyDb.Famillies
            .Where(x => x.Id.Equals(FamillyId))
            .ExecuteDeleteAsync();
    }
}
