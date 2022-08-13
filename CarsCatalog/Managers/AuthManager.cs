using CarsCatalog.Api.Args;
using CarsCatalog.Api.Helpers;

namespace CarsCatalog.Api.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly ILogger<AuthManager> _logger;

        public AuthManager(ILogger<AuthManager> logger)
        {
            _logger = logger;
        }
        public bool AuthenticateUser(AuthenticationArgs authenticationArgs)
        {
            var result = false;

            try
            {
                if (authenticationArgs.UserName == "string" && authenticationArgs.Password != null
                    && EncryptionHelper.Encrypt(authenticationArgs.Password) == "v+mspNjY9tKoq4mLnw8xYA==")
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToShortTimeString()} : Exception at loggin - {ex}");
            }

            return result;
        }
    }
}
