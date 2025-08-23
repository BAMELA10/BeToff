using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeToff.Entities;


namespace BeToff.BLL.Interface
{
    public interface IFamillyBc : IBeToffBc<Familly>
    {
        public Task<Familly> SelectFamilly(string FamillyId);
        public Task<Guid> SaveFamilly(string NameOfFamilly, string CurrentUserId);
        public Task<List<Familly>> SelectFamillyByHead(string IdHead);

        public Task<Task> ChangeHeadOfFamilly(string FamillyId, string UserId);
    }
}
