using BeToff.Entities;
namespace BeToff.DAL.Interface;

public interface IFamillyDao: IBetoff<Familly>
{
    Task<Guid> CreateFamilly(string NameOfFamilly, Guid UserId);
    Task<List<Familly>> GetFamillyByIdHead(Guid IdHead);
    Task<Familly> GetFamillyById(Guid FamillyId);
    Task DeleteFamillyById(Guid FamillyId);
    Task<Task> UpateIdHeadOfFamilly(Guid FamillyId, Guid UserId);


}