using CarsCatalog.Api.Args;

namespace CarsCatalog.Api.Managers
{
    public interface IAuthManager
    {
        bool AuthenticateUser(AuthenticationArgs authenticationArgs);
    }
}