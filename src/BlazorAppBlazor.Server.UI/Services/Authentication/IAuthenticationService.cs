using BlazorAppBlazor.Server.UI.Service.Base;

namespace BlazorAppBlazor.Server.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto user);
    }
}
