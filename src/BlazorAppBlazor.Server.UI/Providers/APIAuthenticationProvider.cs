using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorAppBlazor.Server.UI.Providers
{
    public class APIAuthenticationProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public APIAuthenticationProvider(ILocalStorageService localStorageService, JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _localStorageService = localStorageService;
            jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await _localStorageService.GetItemAsync<string>("AccessToken");
            var user = new ClaimsPrincipal(new ClaimsIdentity());

            if (string.IsNullOrEmpty(savedToken))
            {
                return new AuthenticationState(user);
            }

            // Reading the token content
            var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);

            // check if token has expired
            if(tokenContent.ValidTo < DateTime.Now)
            {
                return new AuthenticationState(user);
            }

            var claims = tokenContent.Claims;

            user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

            return new AuthenticationState(user);
        }

        public async Task LoggedIn()
        {
            // Get token from local storage
            var savedToken = await _localStorageService.GetItemAsync<string>("AccessToken");
            // Read token content
            var tokenContent = _jwtSecurityTokenHandler?.ReadJwtToken(savedToken);
            // Get claims from the token
            var claims = tokenContent.Claims;
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        public void LoggedOut()
        {
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
