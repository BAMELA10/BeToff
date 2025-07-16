using BeToff.DAL.Interface;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.DAL
{
    public class RegistrationDao : BeToffDao, IRegistrationDao
    {
        private readonly BeToffDbContext _db;
        public RegistrationDao( BeToffDbContext db)
        {
            _db = db;
        }
        public async Task AddRegistration (Guid FamillyId, Guid MemberId)
        {
            Registration registration = new Registration()
            {
                UserId = MemberId,
                FamillyId = FamillyId,
                DateOfregistation = DateTime.Now,
            };

            await _db.AddAsync(registration);
            await _db.SaveChangesAsync();
            
        }
    }
}
