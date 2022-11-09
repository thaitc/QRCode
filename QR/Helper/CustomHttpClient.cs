using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System;
using RestSharp;
using Newtonsoft.Json;

namespace QR.Helper
{
    public class CustomHttpClient
    {
        private static RestRequest InitRequest(string tokenName, string token, Method method, string contentType, Dictionary<string, string> queries, Dictionary<string, string> headerQueries)
        {
            var request = new RestRequest("/", method);
            request.AddHeader("Content-Type", contentType);
            if (!string.IsNullOrEmpty(token))
                request.AddHeader(tokenName, token);
            if (queries != null)
            {
                foreach (var item in queries)
                {
                    request.AddParameter(item.Key, item.Value, ParameterType.QueryString);
                }
            }
            if (headerQueries != null)
            {
                foreach (var item in headerQueries)
                {
                    request.AddHeader(item.Key, item.Value);
                }
            }
            return request;
        }

        public static void SetRequestBody(RestRequest request, string body, string contentType)
        {
            if (!string.IsNullOrEmpty(body))
                request.AddParameter(contentType, body, ParameterType.RequestBody);
        }

        public static T PreprocessResponse<T>(RestResponse response)
        {
            try
            {
                return response.StatusCode switch
                {
                    HttpStatusCode.OK => JsonConvert.DeserializeObject<T>(response.Content),
                    _ => JsonConvert.DeserializeObject<T>(response.Content)
                };
            }
            catch (Exception)
            {
                return default;
            }
        }

        #region Resful

        public static T Post<T>(string path, string body, string tokenName, string token = "", string contentType = "application/json",
            Dictionary<string, string> queries = null, Dictionary<string, string> headerQueries = null)
        {
            try
            {
                var client = new RestClient(path);
                var request = InitRequest(tokenName, token, Method.Post, contentType, queries, headerQueries);
                SetRequestBody(request, body, contentType);
                var response = client.Execute(request);
                return PreprocessResponse<T>(response);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static T Get<T>(string path, string tokenName, string token = "", string contentType = "application/json", Dictionary<string, string> queries = null, Dictionary<string, string> headerQueries = null)
        {
            try
            {
                var client = new RestClient(path);
                var request = InitRequest(tokenName, token, Method.Get, contentType, queries, headerQueries);
                var response = client.Execute(request);
                return PreprocessResponse<T>(response);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static T UploadFile<T>(string path, Dictionary<string, IFormFile> files, string tokenName, string token = "", string contentType = "multipart/form-data", Dictionary<string, string> queries = null, Dictionary<string, string> headerQueries = null)
        {
            try
            {
                var client = new RestClient(path);
                var request = InitRequest(tokenName, token, Method.Post, contentType, queries, headerQueries);
                SetRequestBody(request, files);
                var response = client.Execute(request);
                return PreprocessResponse<T>(response);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static void SetRequestBody(RestRequest request, Dictionary<string, IFormFile> files)
        {
            using var ms = new MemoryStream();

            if (files != null)
            {
                foreach (var file in files)
                {
                    file.Value.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    request.AddFile(file.Key, fileBytes, file.Value.FileName, file.Value.ContentType);
                }
            }
        }

        #endregion Resful
    }
}
