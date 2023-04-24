using Day12_JWT_Authenticate_Authorize.Data;
using Day12_JWT_Authenticate_Authorize.Models;

namespace Day12_JWT_Authenticate_Authorize.Repository
{
    public interface IUserService
    {
        User AddUser(UserRegister reg);
        bool CheckEmail(UserRegister reg);
        User CheckUser(UserLogin login);
        string EncryptPassword(string password);
        string GenerateJWT(User user);
    }
}