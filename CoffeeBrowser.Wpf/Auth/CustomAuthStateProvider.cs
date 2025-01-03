﻿namespace CoffeeBrowser.Wpf.Auth;

using CoffeeBrowser.Library.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Security.Claims;
using System.Security.Principal;

public class CustomAuthStateProvider : AuthenticationStateProvider, ICustomAuthStateProvider
{
    private ClaimsPrincipal currentUser = GetWindowsPricipal();

    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
        Task.FromResult(new AuthenticationState(currentUser));

    public Task LogInAsync()
    {
        var loginTask = LogInAsyncCore();
        NotifyAuthenticationStateChanged(loginTask);

        return loginTask;

        async Task<AuthenticationState> LogInAsyncCore()
        {
            var user = await LoginWithExternalProviderAsync();
            currentUser = user;

            return new AuthenticationState(currentUser);
        }
    }

    private Task<ClaimsPrincipal> LoginWithExternalProviderAsync()
    {
        var authenticatedUser = GetWindowsPricipal();
        return Task.FromResult(authenticatedUser);
    }

    private static ClaimsPrincipal GetWindowsPricipal()
    {
        var identity = WindowsIdentity.GetCurrent();

        return new WindowsPrincipal(identity);
    }

    public void Logout()
    {
        currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(currentUser)));
    }
}