using Microsoft.AspNetCore.Authorization;

namespace FAMS.Api.Policies
{
    public class RoleRequirement :IAuthorizationRequirement
    {

        public string Rolename { get; set; }
        
        public RoleRequirement(string rolename) 
        { 
            Rolename= rolename;
        }    
    }
}
