using BeToff.BLL.Interface;
using BeToff.Entities;
using BeToff.DAL;
using BeToff.DAL.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
namespace BeToff.BLL;
using BCrypt.Net;
using BeToff.BLL.Dto.Response;
using BeToff.BLL.Mapping;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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

    public async Task<User> ComparePassword(Credentials credentials)
    {
        User PotentialTrueUser = await _userDao.GetUserByEmail(credentials.Email);
        if (PotentialTrueUser == null)
        {
            return null;
        } 
        bool Match = BCrypt.Verify(credentials.Password, PotentialTrueUser.Password);
        if(Match)
        {
            return PotentialTrueUser;
        }
        else
        {
            return null;
        }
        //if (Match) {
        //    var TokenHandler = new JsonWebTokenHandler();
        //    var Descriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //            new Claim("Email", PotentialTrueUser.Email),
        //            new Claim("UserId", PotentialTrueUser.Id.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(1),
        //        IssuedAt = DateTime.UtcNow,
        //        SigningCredentials = new SigningCredentials(
        //            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ma-cle-secrete")),
        //            SecurityAlgorithms.HmacSha256)
        //    };
        //    string Token = TokenHandler.CreateToken(Descriptor);
        //    return Token;
        //}
        //else
        //{
        //    return String.Empty;
        //}
    }

    public async Task<List<UserResponseDto>> AllUser()
    {
        var result =  await _userDao.GetAll();
        var list = new List<UserResponseDto>();
        if (result.Count == 0) 
        {
            return list;
        }
        else
        {
            foreach (var user in result)
            {
                var item = UserResponseMapper.ToDto(user);
                list.Add(item);
            }
            return list;
        }
    }

    public async Task<UserResponseDto> GetSpecificuser(string Id)
    {
        var user = await _userDao.GetUserById(Id);
        if (user == null)
        {
            return null;
        }
        var result = UserResponseMapper.ToDto(user);
        return result;
    }
}
