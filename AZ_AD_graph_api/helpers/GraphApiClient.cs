using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Extensions.Configuration;

namespace AZ_AD_graph_api.helpers
{
    public static class GraphApiClient
    {
        public static GraphServiceClient GetGraphClient()
        {
            ConfigReader configReader = new();

            var scopes = new[] { "https://graph.microsoft.com/.default" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = configReader.TenantId;
            var clientId = configReader.ClientId; //client id
            var clientSecret = configReader.ClientSecret; //secret

            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            // https://learn.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            return new GraphServiceClient(clientSecretCredential, scopes);
        }


    }

}
