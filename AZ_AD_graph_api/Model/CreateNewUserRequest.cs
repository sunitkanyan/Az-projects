namespace AZ_AD_graph_api.Model
{
    public class CreateNewUserRequest
    {
        public Boolean AccountEnabled { get; set; }
        public string DisplayName { get; set; }
        public string MailNickname { get; set; }
        public string UserPrincipalName { get; set; }
        public Boolean ForceChangePasswordNextSignIn { get; set; }
        public string Password { get; set; }
    }
}
