using System.Threading.Tasks;

namespace TLMS.Infrastructure
{
    public interface ITokenAuthenticator
    {
        Task<AuthenticatorResult> Authenticate(string authorizationHeaderValue, string[] scopes);

        Task<string> GetUserId(string authorizationHeaderValue);
    }

    public class AuthenticatorResult
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public AuthenticatorResultError AuthenticatorResultError { get; set; }
    }

    public enum AuthenticatorResultError
    {
        MissingAuthorizationHeader,
        NullValue,
        Invalid,
    }
}