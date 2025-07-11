using BeToff.DAL.Interface;
using BeToff.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeToff.DAL;

public class UserDao : BeToffDao, IUserDao
{
    protected readonly BeToffDbContext _UserDB;
    public UserDao(BeToffDbContext UserDB)
    {
        _UserDB = UserDB;
    }
    public async Task<bool> Insert(User user)
    {
        await _UserDB.AddAsync(user);
        _UserDB.SaveChanges();
        return true;

    }

    public async Task<User> GetUserByEmail(string Email)
    {
        var user = await _UserDB.Users.SingleOrDefaultAsync(item =>item.Email == Email);
        if(user == null)
        {
            return null;
        }
        return user;
    }
}