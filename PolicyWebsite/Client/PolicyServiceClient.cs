using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Algorithms;
using JWT.Builder;
using Newtonsoft.Json;
using Website.PolicyWebsite.Models;

namespace Website.PolicyWebsite.Client
{
    public class PolicyServiceClient : IPolicyServiceClient, IDisposable
    {
        public PolicyServiceClient(HttpClient client)
        {
            this.Client = client;
            this.Client.BaseAddress = new Uri(Config.PolicyServiceUrl);
            this.Client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("jwt", this.BuildJwt());
        }

        private HttpClient Client { get; }

        public async Task<IEnumerable<DisplayPolicy>> FetchAll()
        {
            var results = await this.Client.GetStringAsync("api/Policy/fetchall");
            return JsonConvert.DeserializeObject<IEnumerable<InsurancePolicy>>(results).Select(p => new DisplayPolicy
            {
                EffectiveDate = p.EffectiveDate,
                Expiration = p.Expiration,
                PolicyNumber = int.Parse(p.PolicyNumber),
                PrimaryInsuredFirstName = p.PrimaryInsured?.PrimaryInsuredFirstName,
                PrimaryInsuredLastName = p.PrimaryInsured?.PrimaryInsuredLastName
            });
        }

        public async Task<string> Save(AddPolicy addPolicy)
        { 
            var insurancePolicy = Mapper.Map<InsurancePolicy>(addPolicy);
            var serialized = JsonConvert.SerializeObject(insurancePolicy);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            var response = await this.Client.PostAsync("api/Policy/add", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<IEnumerable<BuildingType>> GetConstructionTypes()
        {
            var results = await this.Client.GetStringAsync("api/constructiontype/fetchall");
            var buildingTypes = JsonConvert.DeserializeObject<IEnumerable<BuildingType>>(results);
            return buildingTypes;
        }

        public void Dispose()
        {
            this.Client.Dispose();
        }

        private string BuildJwt()
        {
            var token = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(Config.PolicyServiceSecret)
                .AddClaim(ClaimName.ExpirationTime.ToString(), DateTime.UtcNow.AddMinutes(2))
                .AddClaim(ClaimName.NotBefore.ToString(), DateTime.UtcNow.AddSeconds(-2))
                .Build();

            return token;
        }
    }
}