using Microsoft.AspNetCore.Identity;

namespace Module.User.Core.Entities;

public class ApplicationRole : IdentityRole<long>
{
    public ApplicationRole()
    {

    }
    public ApplicationRole(string RoleName) : base(RoleName)
    {

    }
}
