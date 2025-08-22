using BeToff.BLL.Interface;
using BeToff.DAL.Interface;
using BeToff.Entities;
using BeToff.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL
{
    public class FamillyBc : BeToffBc<Familly, FamillyDao>, IFamillyBc
    {
        private readonly IFamillyDao _famillyDao;

        public FamillyBc(IFamillyDao famillyDao)
        {
            _famillyDao = famillyDao;
        }

        public async Task<Familly> SelectFamilly(string FamillyId)
        {
            Console.WriteLine(FamillyId);
            Guid Id = Guid.Parse(FamillyId);
            if (Id == Guid.Empty) {
                throw new Exception("Vide");
            }
            return await _famillyDao.GetFamillyById(Id);
        }

        public async Task<List<Familly>> SelectFamillyByHead(string IdHead)
        {
            Guid Id = Guid.Parse(IdHead);
            return await _famillyDao.GetFamillyByIdHead(Id);
        }

        public async Task<Guid> SaveFamilly(string NameOfFamilly, string CurrentUserId)
        {
            Guid UserId = Guid.Parse(CurrentUserId);
            Guid Familly = await _famillyDao.CreateFamilly(NameOfFamilly, UserId);
            return Familly;

        }

        public Task<Task> ChangeHeadOfFamilly(Guid FamillyId, Guid UserId)
        {
            throw new NotImplementedException();
        }
    }
}
