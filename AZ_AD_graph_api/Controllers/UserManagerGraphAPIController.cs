using AZ_AD_graph_api.helpers;
using AZ_AD_graph_api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace AZ_AD_graph_api.Controllers
{
    [ApiController]
    [Route("UserManagerGraphAPI")]
    public class UserManagerGraphAPIController : Controller
    {
        // GET: UserManagerGraphAPI
        [HttpGet]
        [Route("Users")]
        public JsonResult Index()
        {
            var userreq = GraphApiClient.GetGraphClient().Users.Request().GetAsync();

            var userlist = new List<dynamic>();
            foreach (var user in userreq.Result)
            {
                userlist.Add(new
                {
                    username = user.UserPrincipalName,
                    id = user.Id,
                    displayName = user.DisplayName,
                    Mobile = user.MobilePhone,
                    location = user.OfficeLocation
                });
            }
            return Json(userlist);
        }

        // GET: UserManagerGraphAPI/Details/5
        [HttpGet]
        [Route("Detail/{userid}")]
        public async Task<JsonResult> DetailsAsync(string userId)
        {
            var userreq = GraphApiClient.GetGraphClient().Users[userId].Request().GetAsync();
                        
            var userAppRoles = await GraphApiClient.GetGraphClient().Users[userId].AppRoleAssignments
                 .Request()
                 .GetAsync();

            var roleList = new List<dynamic>();
            foreach (var role in userAppRoles.ToList())
            {
                roleList.Add(new
                {
                    name = role.PrincipalDisplayName,
                    id = role.Id,
                    principleid = role.PrincipalId,
                    appRoleid = role.AppRoleId

                });
            }

            var userDetail = userreq.Result;

            var user = new
            {
                username = userDetail.UserPrincipalName,
                id = userDetail.Id,
                displayName = userDetail.DisplayName,
                Mobile = userDetail.MobilePhone,
                location = userDetail.OfficeLocation,
                userAppRoles = roleList
            };

            return Json(user);
        }

        // POST: UserManagerGraphAPI/Create
        [HttpPost]

        [Route("Create")]
        public JsonResult Create([FromBody]CreateNewUserRequest userRequest)
        {
            var user = new User
            {
                AccountEnabled = userRequest.AccountEnabled,
                DisplayName = userRequest.DisplayName,
                MailNickname = userRequest.MailNickname,
                UserPrincipalName = userRequest.UserPrincipalName, //"skj@sunitkanyanhotmail.onmicrosoft.com",
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = userRequest.ForceChangePasswordNextSignIn,
                    Password = userRequest.Password// "test@123456@23"
                }
            };

            var user1 = GraphApiClient.GetGraphClient().Users.Request().AddAsync(user);
            string userId = user1.Result.Id;

            return Json(new { message = "user created.", userId = userId });
        }

        
        // POST: UserManagerGraphAPI/updateMobile
        [HttpPost]
        [Route("UpdateMobile")]
        public JsonResult UpdateMobile([FromBody]UpdateUserRequest userRequest)
        { 
            var user = new User
            {
                MobilePhone = userRequest.Mobile,
                BusinessPhones = new List<String>()
                    {
                       userRequest.Mobile
                    },
                OfficeLocation = userRequest.OfficeLocation
            };

            var user1 = GraphApiClient.GetGraphClient().Users[userRequest.UserId].Request().UpdateAsync(user);
            
            return Json(user1);
        }

        [HttpPost]

        [Route("AddRoleAssignmentToUser")]
        public async Task<JsonResult> AddRoleAssignmentToUserAsync([FromBody]RoleAssignmentUserRequest request)
        {

            var userAppRoles = await GraphApiClient.GetGraphClient().Users[request.UserId].AppRoleAssignments
                .Request()
                .GetAsync();
            Guid requestedRoleId = Guid.Parse(request.RoleId);

            if (userAppRoles.ToList().Where(c => c.AppRoleId == requestedRoleId).Count() > 0)
                return Json("this role assigned to user.");
            

            var appRoleAssignment = new AppRoleAssignment
            {
                PrincipalId = Guid.Parse(request.UserId),
                ResourceId = Guid.Parse(request.ObjectIdApp),//"15216253-b588-4cd1-a2f3-d89f007c1f4c"), //evaluated from assigned role object
                AppRoleId = Guid.Parse(request.RoleId)
            };

           var res = await GraphApiClient.GetGraphClient().Users[request.UserId].AppRoleAssignments
                .Request()
                .AddAsync(appRoleAssignment);

          
            return Json(res);
        }
        [HttpGet]
        [Route("getapproles")]
        public JsonResult GetAppRoles()
        {
            var app = GraphApiClient.GetGraphClient().Applications
                .Request()
                .GetAsync().Result;
            return Json(app.Select(s => s.AppRoles.Select(m => new { Role = m.DisplayName,Id = m.Id })));
            //response [[{"role":"Role4","id":"05350556-2ca4-45e1-ae5d-03727565f5d3"},{"role":"Role3","id":"377515e3-9ec7-46c6-a7f0-0d4b928e2cc5"},{"role":"Role2","id":"2dc11550-e0ca-42d4-b536-03622f91a629"},{"role":"Role1","id":"b0d99097-eaa1-4472-8988-88be0d14b0ac"}]]
        }

    }
}
