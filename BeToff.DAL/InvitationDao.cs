using BeToff.DAL.Interface;
using BeToff.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.DAL
{
    public class InvitationDao : BeToffDao, IInvitationDao
    {
        private readonly BeToffDbContext _Db;
        public InvitationDao(BeToffDbContext db)
        {
            _Db = db;
        }
        

        public async Task<Task> CreateInvitation(Guid SenderId, Guid TargetId, Guid FamillyId)
        {
            Invitation NewInvitation = new Invitation
            {
                SenderId = SenderId,
                ReceiverId = TargetId,
                FamillyId = FamillyId,
                Status = StatusInvitation.Pending,
                SendAt = DateOnly.FromDateTime(DateTime.Now),
                ExpireAt = DateOnly.FromDateTime(DateTime.Now.AddDays(7))
            };
            
            await _Db.Invitations.AddAsync(NewInvitation);
            await _Db.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task<Task> CancelInvitation(Guid Id)
        {
            var Item = await _Db.Invitations.FirstOrDefaultAsync(x=> x.Id == Id);
            Item.Status = StatusInvitation.Refused;
            _Db.Invitations.Update(Item);
            await _Db.SaveChangesAsync();

            return Task.CompletedTask;

        }

        public async Task<Task> AcceptInvitation(Guid Id)
        {
            var Item = await _Db.Invitations.FirstOrDefaultAsync(x => x.Id == Id);
            Item.Status = StatusInvitation.Accepted;
            _Db.Invitations.Update(Item);
            await _Db.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task<List<Invitation>> GetInvitationBySender(Guid SenderId)
        {
            List<Invitation> result = await _Db.Invitations
                .Where(x => x.SenderId == SenderId)
                .Include(s => s.Sender)
                .Include(r => r.Receiver)
                .Include(f => f.FamillyItem)
                .ToListAsync();
            return result;
        }

        public async Task<List<Invitation>> GetInvitationByReceiver(Guid ReceiverId)
        {
            List<Invitation> result = await _Db.Invitations
                .Where(x => x.ReceiverId == ReceiverId)
                .Include(s => s.Sender)
                .Include(r => r.Receiver)
                .Include(f => f.FamillyItem)
                .ToListAsync();

            return result;
        }

        public async Task<Task> DeleteInvitation(Guid Id)
        {
            await _Db.Invitations
                .Where(x => x.Id == Id)
                .ExecuteDeleteAsync();
            return Task.CompletedTask;
        }

        public async Task<Invitation> GetSpecificInvitation(Guid Id)
        {
            var Item = await _Db.Invitations
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();
            return Item;
        }

        public async Task<List<Invitation>> GetAllInvitation()
        {
            List<Invitation> Items = await _Db.Invitations
                .Include(s => s.Sender)
                .Include(r => r.Receiver)
                .Include(f => f.FamillyItem)
                .ToListAsync();
            return Items;
        }
    }
}
