using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace PolicyAPI.Configurations
{
    public class VaultConfiguration(IConfiguration configuration)         
    {
        private IConfiguration _configuration =configuration;

        public async Task<IDictionary<string,object>> GetSecrets(string RootKey, 
            string Url)
        {
           // var Url = _configuration["Url"];
           // var RootKey = _configuration["Root_Key"];
            TokenAuthMethodInfo tokenAuthMethodInfo= 
                new TokenAuthMethodInfo(RootKey);
          VaultClientSettings vaultClientSettings=
                new VaultClientSettings(Url, tokenAuthMethodInfo);

            IVaultClient vaultClient = new VaultClient(vaultClientSettings);
            Console.WriteLine(vaultClient.V1.Secrets);

            //var result = await vaultClient.V1.Secrets.KeyValue.V1.
            //  ReadSecretAsync("sqlserver2019",
            //"secret", null);

            var result = vaultClient.V1.Secrets.KeyValue.
               V2.ReadSecretAsync(path: "sqlserver2019", mountPoint: "secret").Result.Data.Data;


            return result;




        }

        public async Task<IDictionary<string, object>> GetJWTSecrets(string RootKey,
            string Url)
        {
            // var Url = _configuration["Url"];
            // var RootKey = _configuration["Root_Key"];
            TokenAuthMethodInfo tokenAuthMethodInfo =
                new TokenAuthMethodInfo(RootKey);
            VaultClientSettings vaultClientSettings =
                  new VaultClientSettings(Url, tokenAuthMethodInfo);

            IVaultClient vaultClient = new VaultClient(vaultClientSettings);
            Console.WriteLine(vaultClient.V1.Secrets);

            //var result = await vaultClient.V1.Secrets.KeyValue.V1.
            //  ReadSecretAsync("sqlserver2019",
            //"secret", null);

            var result = vaultClient.V1.Secrets.KeyValue.
               V2.ReadSecretAsync(path: "trainerjwtsecret", mountPoint: "secret").Result.Data.Data;


            return result;




        }

    }
}
