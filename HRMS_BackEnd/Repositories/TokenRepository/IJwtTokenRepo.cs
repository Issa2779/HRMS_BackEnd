using Microsoft.AspNetCore.Identity;

namespace HRMS_BackEnd.Repositories.TokenRepository
{
    public interface IJwtTokenRepo
    {

        string CreateJWTToken(IdentityUser user);

    }
}
