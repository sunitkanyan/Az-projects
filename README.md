# Az-projects
Azure learning and samples.

**This project "AZ_AD_graph_api" explain the way to manager users in active directory using Microsoft Graph API
**
What need first --

1. A azure Active Directory Tentant
2. Register an App
 <img width="613" alt="image" src="https://user-images.githubusercontent.com/12130799/211207182-081999ab-a16b-4fd9-a3f0-a7ab204632e7.png">

3. Add app role <img width="791" alt="image" src="https://user-images.githubusercontent.com/12130799/211207244-0b3f9859-f8a2-4a0b-850d-1a3c271e6422.png">

4. Create client seret <img width="770" alt="image" src="https://user-images.githubusercontent.com/12130799/211207279-bcb63ace-a622-424c-aa3a-ed7bf09cc31e.png">

5. Add permission "api permission". Permission needed 
User.ReadWrite.All
AppRoleAssignment.ReadWrite.All <img width="767" alt="image" src="https://user-images.githubusercontent.com/12130799/211207326-13c3ff74-09a3-47a8-951f-a6c6db790fd7.png">


6. Now user AZ_AD_graph_api to perform user operation on AD Using Active Directory. 


**
Curl and response for AZ_AD_graph_api**

**1.Create new account
**
curl --location --request POST 'http://localhost:5219/UserManagerGraphAPI/Create' \
--header 'Content-Type: application/json' \
--data-raw '{
    "AccountEnabled": true,
    "DisplayName":"MyPostTestUser",
    "MailNickname":"super",
    "UserPrincipalName": "skanyan@*******.onmicrosoft.com",
    "ForceChangePasswordNextSignIn":true,
    "Password": "skumar@12345"
}'

response

{
    "message": "user created.",
    "userId": "88503cf8-2612-4743-83dd-ae752b87c870"
}

2.** list users in ad
**<img width="859" alt="image" src="https://user-images.githubusercontent.com/12130799/211207096-ea4def18-c05a-4237-aa5c-03746bb8f52c.png">

curl --location --request GET 'http://localhost:5219/UserManagerGraphAPI/Users'


response
[
    {
        "username": "skj@*******..onmicrosoft.com",
        "id": "fffb9b1f-854d-4612-a862-2abcae49a7b1",
        "displayName": "sumit",
        "mobile": "9000000000",
        "location": "India"
    },
    {
        "username": "skk@*******.onmicrosoft.com",
        "id": "37803835-dff3-490b-b92a-8076ab83c58e",
        "displayName": "sunit",
        "mobile": null,
        "location": null
    },
    {
        "username": "sunitkanyan_#EXT#@****.onmicrosoft.com",
        "id": "39c7e1d9-e872-4ce4-b786-3c65ad8cf5ce",
        "displayName": "Sunit Ror",
        "mobile": null,
        "location": null
    },
    {
        "username": "testuser1@****.onmicrosoft.com",
        "id": "e05cd5e7-8f68-4f3e-9a65-652fc9e19f54",
        "displayName": "MyPostTestUser",
        "mobile": null,
        "location": null
    }
]

3.**  get single user
**
curl --location --request GET 'http://localhost:5219/UserManagerGraphAPI/Detail/fffb9b1f-854d-4612-a862-2abcae49a7b1'

{
    "username": "skj@*******.onmicrosoft.com",
    "id": "fffb9b1f-854d-4612-a862-2abcae49a7b1",
    "displayName": "sumit",
    "mobile": "9000000000",
    "location": "India",
    "userAppRoles": [
        {
            "name": "sumit",
            "id": "H5v7_02FEkaoYiq8rkmnsXcWPTM759tJihvr3JqaodE",
            "principleid": "fffb9b1f-854d-4612-a862-2abcae49a7b1",
            "appRoleid": "8b4f9e27-d9dc-4075-8a46-223b6b3c1236"
        },
        {
            "name": "sumit",
            "id": "H5v7_02FEkaoYiq8rkmnsfW3Q2b0v4lCkx9Q-s_9_Ag",
            "principleid": "fffb9b1f-854d-4612-a862-2abcae49a7b1",
            "appRoleid": "9fe4636c-679b-4405-a343-f5d6a5ac6fc3"
        }
    ]
}



4.**update detail
**
curl --location --request POST 'http://localhost:5219/UserManagerGraphAPI/UpdateMobile' \
--header 'Content-Type: application/json' \
--data-raw '{
    "UserId":"fffb9b1f-854d-4612-a862-2abcae49a7b1",
    "Mobile":"9000000000",
    "OfficeLocation":"India"
}'

5.** add app role to user
**
curl --location --request POST 'http://localhost:5219/UserManagerGraphAPI/AddRoleAssignmentToUser' \
--header 'Content-Type: application/json' \
--data-raw '{
    "UserId":"e05cd5e7-8f68-4f3e-9a65-652fc9e19f54",
    "RoleId":"9fe4636c-679b-4405-a343-f5d6a5ac6fc3",
    "ObjectIdApp":"15216253-b588-4cd1-a2f3-d89f007c1f4c"
}'


response
{
    "UserId":"e05cd5e7-8f68-4f3e-9a65-652fc9e19f54",
    "RoleId":"9fe4636c-679b-4405-a343-f5d6a5ac6fc3",
    "ObjectIdApp":"15216253-b588-4cd1-a2f3-d89f007c1f4c"
}



