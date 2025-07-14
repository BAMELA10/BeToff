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
            .FirstAsync();
    }

    public async Task<List<Familly>> GetFamillyByIdHead(Guid IdHead)
    {
        return await _FamillyDb.Famillies
             .Where(items => items.IdHead == IdHead)
             .ToListAsync();
    }


    public async Task CreateFamilly(string NameOfFamilly, Guid UserId)
    {
        Familly familly = new Familly();

        familly.Name = NameOfFamilly;
        familly.IdCreator = UserId;
        familly.IdHead = UserId;
        familly.DateCreation = DateOnly.FromDateTime(DateTime.Now);
        
        await _FamillyDb.AddAsync(familly);
        _FamillyDb.SaveChanges();
    }
}
