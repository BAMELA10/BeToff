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
        Task<Familly> SelectFamilly(string FamillyId);
        Task SaveFamilly(string NameOfFamilly, string CurrentUserId);
        Task<List<Familly>> SelectFamillyByHead(string IdHead);
    }
}
