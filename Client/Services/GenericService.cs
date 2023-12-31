﻿using Client.Exceptions;
using Client.Models.API;
using Clinet.Models.API;
using Clinet.Services.Interfaces;
using Newtonsoft.Json;

namespace Clinet.Services
{
    public class GenericService<T> : BaseService, IGenericService<T>
    {
        private readonly string APIUrl;
        public GenericService(string URL, IHttpClientFactory _clientFactory, IConfiguration configuration) : base(_clientFactory)
        {
            APIUrl = configuration.GetValue<string>("ServiceUrls:BookLibrary") + URL;
        }

        public async Task AddAsync(object entity, string token)
        {
            await SendAsync(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = APIUrl,
                Data = entity,
                Token = token
            });
        }

        public async Task DeleteAsync(int Id, string token)
        {
            var apiResponse = await SendAsync(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = APIUrl + "/" + Id,
                Token = token
            });

            GenericService<T>.CheckAPIResponse(apiResponse);
        }

        public async Task<List<T>> GetAllSelectorItemsAsync(string token)
        {
            var apiResponse = await SendAsync(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = APIUrl + "/" + "SelectorItems",
                Token = token
            });

            return JsonConvert.DeserializeObject<List<T>>(Convert.ToString(apiResponse.Result)!)!;
        }

        public async Task<List<T>> GetAllAsync(string? searchString, string token)
        {
            var apiResponse = await SendAsync(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = APIUrl,
                Token = token,
                Data = new SearchString { Search = searchString }
            });

            return JsonConvert.DeserializeObject<List<T>>(Convert.ToString(apiResponse.Result)!)!;
        }

        public async Task<T> GetByIdAsync(int Id, string token)
        {
            var apiResponse = await SendAsync(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = APIUrl + "/" + Id,
                Token = token
            });

            return JsonConvert.DeserializeObject<T>(Convert.ToString(apiResponse.Result)!)!;  
        }

        public async Task UpdateAsync(int Id, object entity, string token)
        {
            var apiResponse = await SendAsync(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = APIUrl + "/" + Id,
                Data = entity,
                Token = token
            });

            GenericService<T>.CheckAPIResponse(apiResponse);
        }

        private static void CheckAPIResponse(APIResponse apiResponse)
        {
            if (apiResponse.IsSuccess == false)
            {
                throw new APIException(apiResponse.ErrorMessages.FirstOrDefault());
            }
        }
    }
}
