using BeToff.Entities;
using Microsoft.Identity.Client;

namespace BeToff.DAL.Interface;

public interface IUserDao : IBetoff<User> 
{
    public Task<bool> Insert(User user);
    //public bool UpdateUser(User User);
    //public bool OffUser(User User);

  
    
}