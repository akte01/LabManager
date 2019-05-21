using LabStore.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace LabStoreTest.Mock
{
    public class MockUserIdentity
    {

        public ClaimsPrincipal CreateLoggedInUser(string userID)
        {
            var identity = new GenericIdentity("TestUser");
            identity.AddClaims(new List<Claim> {
            new Claim(ClaimTypes.Sid, userID),
        });
            return new ClaimsPrincipal(identity);
        }

        public ClaimsPrincipal CreateLoggedInUserWithRoleCanManage(string userID)
        {
            var roles = new[] { RoleName.CanManage };
            var identity = new GenericIdentity("TestUser");
            identity.AddClaims(new List<Claim> {
            new Claim(ClaimTypes.Sid, userID),
        });
            var principal = new GenericPrincipal(identity, roles);
            return new ClaimsPrincipal(principal);
        }
    }
}
