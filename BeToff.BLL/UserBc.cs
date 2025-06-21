using BeToff.BLL.Interface;
using BeToff.Entities;
using BeToff.DAL;
using BeToff.DAL.Interface;
namespace BeToff.BLL;
using BCrypt.Net;


public class UserBc : BeToffBc<User, UserDao>, IUserBc
{
    protected readonly IUserDao _userDao;

    public UserBc(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public async Task HashPasswordAndInsertUser(User user)
    {
        var HahedPassword = BCrypt.HashPassword(user.Password);
        user.Password = HahedPassword;
        bool IsAdd = await _userDao.Insert(user);
        if (!IsAdd) {
            throw new Exception("Internal Error");
        }

        
    }

}
