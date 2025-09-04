using BeToff.Entities;
using Microsoft.Identity.Client;

namespace BeToff.DAL.Interface;

public interface IUserDao : IBetoff<User> 
{
    public Task<bool> Insert(User user);
    public Task<User> GetUserByEmail(string Email);
    public Task<User> GetUserById(string Id);
    //public bool UpdateUser(User User);
    //public bool OffUser(User User);



}