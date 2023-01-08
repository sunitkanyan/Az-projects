using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace AZ_AD_graph_api.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("create")]
        public  string Create()
        {
            // The client credentials flow requires that you request the
            // /.default scope, and preconfigure your permissions on the
            // app registration in Azure. An administrator must grant consent
            // to those permissions beforehand.
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = "819a8625-9fb0-44ff-b7bc-50da330ca0b1";

            // Values from app registration
            var clientId = "a499bef8-038e-400c-8160-2be4d108c32f";
            var clientSecret = "5pr8Q~QETrlsWMI.aCMRqeoqkqiiGu6j~Eo4JdtM";

            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            // https://learn.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
           // GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var user = new User
            {
                AccountEnabled = true,
                DisplayName = "sumit",
                MailNickname = "J",
                UserPrincipalName = "skj@sunitkanyanhotmail.onmicrosoft.com",
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = true,
                    Password = "test@123456@23"
                }
            };

           var user1=  graphClient.Users.Request().AddAsync(user);
           string userid = user1.Result.Id;

            //get list of users
            var users = graphClient.Users.Request();

            users.GetAsync().Wait();
          

            //assign user
            //var appRoleAssignment = new AppRoleAssignment
            //{
            //    PrincipalId = Guid.Parse(userid),
            //    ResourceId = Guid.Parse("819a8625-9fb0-44ff-b7bc-50da330ca0b1"),
            //    AppRoleId = Guid.Parse("8b4f9e27-d9dc-4075-8a46-223b6b3c1236")
            //};

            //await graphClient.Users[userid].AppRoleAssignments
            //    .Request()
            //    .AddAsync(appRoleAssignment);
            ////

            return user1.Result.DisplayName;
        }

        [HttpGet]
        [Route("userList")]
        public async Task<dynamic> getusers()
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = "819a8625-9fb0-44ff-b7bc-50da330ca0b1";

            // Values from app registration
            var clientId = "a499bef8-038e-400c-8160-2be4d108c32f";
            var clientSecret = "5pr8Q~QETrlsWMI.aCMRqeoqkqiiGu6j~Eo4JdtM";

            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            // https://learn.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

            var a = graphClient.Users.Request().GetAsync();

            var userlist = new List<dynamic>();
            foreach (var user in a.Result)
            {
                userlist.Add(new
                {
                    username = user.UserPrincipalName,
                    id = user.Id,
                    displayName = user.DisplayName,
                    nickname = user.MailNickname
                });


            }


            return userlist;
        }


        [HttpGet]
        [Route("GetassignRole")]
        public async Task<dynamic> GetassignRole(string userid)
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = "819a8625-9fb0-44ff-b7bc-50da330ca0b1";

            // Values from app registration
            var clientId = "a499bef8-038e-400c-8160-2be4d108c32f";
            var clientSecret = "5pr8Q~QETrlsWMI.aCMRqeoqkqiiGu6j~Eo4JdtM";

            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            // https://learn.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

            var appRoleAssignments = await graphClient.Users[userid].AppRoleAssignments
                .Request()
                .GetAsync();

           

            var roleList = new List<dynamic>();
            foreach (var role in appRoleAssignments.ToList())
            {
                roleList.Add(new
                {
                    name = role.PrincipalDisplayName,
                    id=role.Id,
                    principleid=  role.PrincipalId,
                    appRoleid= role.AppRoleId

                });


            }


            return roleList;
        }
        [HttpGet]
        [Route("AssignNewRole")]
        public async Task<dynamic> AssignNewRole(string userid,string appRoleId)
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = "819a8625-9fb0-44ff-b7bc-50da330ca0b1";

            // Values from app registration
            var clientId = "a499bef8-038e-400c-8160-2be4d108c32f";
            var clientSecret = "5pr8Q~QETrlsWMI.aCMRqeoqkqiiGu6j~Eo4JdtM";

            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            // https://learn.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

          
            var appRoleAssignment = new AppRoleAssignment
            {
                PrincipalId = Guid.Parse(userid),
                ResourceId = Guid.Parse("15216253-b588-4cd1-a2f3-d89f007c1f4c"), //evaluated from assigned role object
                AppRoleId = Guid.Parse(appRoleId)
            };

            await graphClient.Users[userid].AppRoleAssignments
                .Request()
                .AddAsync(appRoleAssignment);


            var appRoleAssignments = await graphClient.Users[userid].AppRoleAssignments
                .Request()
                .GetAsync();



            var roleList = new List<dynamic>();
            foreach (var role in appRoleAssignments.ToList())
            {
                roleList.Add(new
                {
                    name = role.PrincipalDisplayName,
                    id = role.Id,
                    principleid = role.PrincipalId,
                    appRoleid = role.AppRoleId

                });


            }


            return roleList;
        }


    }
}
