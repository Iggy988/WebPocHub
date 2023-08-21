using Models;

namespace WebApi.Jwt
{
    public interface ITokenManager
    {
        string GenerateToken(User user, string roleName);
    }
}
