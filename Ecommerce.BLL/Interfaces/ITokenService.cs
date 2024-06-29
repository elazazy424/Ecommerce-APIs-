using Ecommerce.DAL.Identity;

namespace Ecommerce.BLL.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);

    }
}
