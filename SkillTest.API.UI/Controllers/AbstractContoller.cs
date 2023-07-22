using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace SkillTest.API.UI.Controllers
{
    public abstract class AbstractContoller : ControllerBase
    {

        [ApiExplorerSettings(IgnoreApi = true)]
        public JwtSecurityToken GetToken()
        {
            string jwt = HttpContext.Request.Headers["Authorization"].ToString().Replace("bearer ", "").Replace("Bearer ", "");
            var token = new JwtSecurityToken(jwt);
            return token;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetClaim(string claimType)
        {
            var claim = GetToken().Claims.First(claim => claim.Type == claimType).Value;
            return claim;
        }
    }
}
