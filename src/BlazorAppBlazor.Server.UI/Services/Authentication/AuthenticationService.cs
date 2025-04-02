using BlazorAppBlazor.Server.UI.Providers;
using BlazorAppBlazor.Server.UI.Service.Base;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorAppBlazor.Server.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient client, 
            ILocalStorageService localStorageService,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _client = client;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(LoginUserDto user)
        {
            // Call login endpoint
            var response = await _client.LoginAsync(user);

            // Store token
            await _localStorageService.SetItemAsync("AccessToken", response.Token);

            // Change auth state for the app
            await ((APIAuthenticationProvider)_authenticationStateProvider).LoggedIn();

            return true;
        }
    }
}
