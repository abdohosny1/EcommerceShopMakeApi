using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcommerceShop.EF.Repositories
{
    public class ResponseCasheService : IResponseCasheService
    {

        private readonly IDatabase _dataBase;

        public ResponseCasheService(IConnectionMultiplexer redis)
        {
            _dataBase = redis.GetDatabase();
        }

        public async Task CasheResponseAsync(string casheKey, object response, TimeSpan timeToLive)
        {
            if(response== null)
            {
                return ;
            }

            var option = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var serlizeReadResponse=JsonSerializer.Serialize(response,option);

            await _dataBase.StringSetAsync(casheKey, serlizeReadResponse, timeToLive);


        }

        public async Task<string> GetCasheResponseAsync(string casheKey)
        {
            var cashedResponse= await _dataBase.StringGetAsync(casheKey);

            if (cashedResponse.IsNullOrEmpty) return null;

            return cashedResponse;
        }
    }
}
