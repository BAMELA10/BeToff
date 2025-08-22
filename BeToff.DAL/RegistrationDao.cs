using BeToff.DAL.Interface;
using BeToff.Entities;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Registration> GetRegistrationByFamillyAndUser(Guid FamillyId, Guid MemberId)
        {
            var registration = await _db.Registration
                .Where(x => x.FamillyId.Equals(FamillyId))
                .Where(s => s.UserId.Equals(MemberId))
                .Include(x => x.Familly)
                .Include(y => y.User)
                .FirstOrDefaultAsync();

            return registration;
        }

        public async Task<List<Registration>> GetRegistrationByFamilly(Guid FamillyId)
        {
            var ListRegis = await _db.Registration
                .Where(x => x.FamillyId.Equals(FamillyId))
                .Include(x => x.Familly)
                .Include(y => y.User)
                .ToListAsync();
            return ListRegis;
        }

        public async Task<Task> DeleteRegistration(Guid FamillyId, Guid MemberId)
        {
            var Regis = await _db.Registration
                .Where(x => x.FamillyId.Equals(FamillyId))
                .Where(s => s.UserId.Equals(MemberId))
                .ExecuteDeleteAsync(); 

            return Task.CompletedTask;
        }

        public async Task<List<Registration>> GetRegistrationByUser(Guid UserId)
        {
            var ListRegis = await _db.Registration
                .Where(x => x.UserId.Equals(UserId))
                .Include(x => x.Familly)
                .Include(y => y.User)
                .ToListAsync();
            return ListRegis;
        }
    }
}
