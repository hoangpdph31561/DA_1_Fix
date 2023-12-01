using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BaseSolution.BlazorServer.Pages.Manager
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _session;
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ISessionStorageService session)
        {
            _session = session;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                List<ClaimSimplifyModel> lstClaimVM = await _session.GetItemAsync<List<ClaimSimplifyModel>>("UserClaims");
                List<Claim> lstClaim = lstClaimVM.Select(c => new Claim(c.Key, c.Value)).ToList();

                if (lstClaim == null || lstClaim.Count == 0)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(lstClaim, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState(List<Claim>? claims)
        {
            ClaimsPrincipal claimsPrincipal;

            if (claims != null)
            {
                List<ClaimSimplifyModel> lstClaimVM = claims.Select(c => new ClaimSimplifyModel()
                {
                    Key = c.Type,
                    Value = c.Value
                }).ToList();

                await _session.SetItemAsync("UserClaims", lstClaimVM);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
            }
            else
            {
                await _session.RemoveItemAsync("UserClaims");
                claimsPrincipal = _anonymous;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
