﻿using Marvelous.Contracts.Autentificator;
using Marvelous.Contracts.Endpoints;
using Marvelous.Contracts.Enums;
using Marvelous.Contracts.ResponseModels;
using MarvelousReportMicroService.BLL.Exceptions;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;

namespace MarvelousReportMicroService.BLL.Helpers
{
    public class RequestHelper : IRequestHelper
    {
        public async Task<RestResponse<IdentityResponseModel>> SendRequestCheckValidateToken(
            string url,
            string path,
            string jwtToken)
        {
            var request = new RestRequest(path);
            var client = new RestClient(url);
            client.Authenticator = new MarvelousAuthenticator(jwtToken);
            client.AddDefaultHeader(nameof(Microservice), Microservice.MarvelousTransactionStore.ToString());
            var response = await client.ExecuteAsync<IdentityResponseModel>(request);
            CheckTransactionError(response);

            return response;
        }

        public async Task<RestResponse<T>> SendRequestForConfigs<T>(string url, string path, string jwtToken = "null")
        {
            var request = new RestRequest(path);
            var client = new RestClient(url);
            client.Authenticator = new JwtAuthenticator(jwtToken);
            client.AddDefaultHeader(nameof(Microservice), Microservice.MarvelousTransactionStore.ToString());
            var response = await client.ExecuteAsync<T>(request);
            CheckTransactionError(response);

            return response;
        }

        private static void CheckTransactionError(RestResponse response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    break;
                default:
                    throw new Exception($"Error. {response.ErrorException!.Message}");
            }
            if (response.Content is null)
                throw new Exception($"Content equal's null {response.ErrorException!.Message}");
        }
    }
}
