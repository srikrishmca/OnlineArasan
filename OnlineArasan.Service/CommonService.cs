using Newtonsoft.Json;
using OnlineArasan.Service.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnlineArasan.Service
{
    public class CommonService
    {
        private static readonly CommonService instance = new CommonService();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static CommonService()
        {
        }
        public static CommonService Instance
        {
            get => instance;
        }
        public async Task<ApiResponse<T>> GetResponseAsync<T>(string url)
        {
            try
            {
                Uri uri = new Uri(string.Format(ServiceConstants.ApiBaseURL + url));
                HttpClient _client = ServiceConfig.GetHttpClient(uri);
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);
                HttpResponseMessage response = _client.SendAsync(message).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var responseResult = JsonConvert.DeserializeObject<T>(result);
                    ApiResponse<T> res = new ApiResponse<T>
                    {
                        IsError = false,
                        Message = "Success",
                        Version = null,
                        Result = responseResult
                    };
                    return res;
                }

                else
                {
                    ApiResponse<T> res = new ApiResponse<T>
                    {
                        IsError = true,
                        Message = "Some thing went wrong",
                        Version = null
                    };
                    return res;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default(ApiResponse<T>);
            }
        }
        public async Task<ApiResponse<T1>> PostResponseAsync<T1, T2>(string url, T2 model)
        {
            try
            {

                Uri uri = new Uri(string.Format(ServiceConstants.ApiBaseURL + url));
                HttpClient _client = ServiceConfig.GetHttpClient(uri);
                string json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(uri, content);
                ApiResponse<T1> responseResult = new ApiResponse<T1>();

                if (response.IsSuccessStatusCode)
                {
                    using (Stream s = await response.Content.ReadAsStreamAsync())
                    using (StreamReader sr = new StreamReader(s))
                    {
                        using (JsonReader reader = new JsonTextReader(sr))
                        {
                            JsonSerializer serializer = new JsonSerializer();

                            // read the json from a stream
                            // json size doesn't matter because only a small piece is read at a time from the HTTP request
                            var result = serializer.Deserialize<T1>(reader);

                            responseResult.Result = result;
                            responseResult.Version = "1.0";
                            responseResult.IsError = false;
                            responseResult.Message = "Success";
                        }
                    }
                }
                else
                {
                    responseResult.Version = "1.0";
                    responseResult.IsError = true;
                    responseResult.Message = "Some thing went wrong";
                }
                return responseResult;
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default(ApiResponse<T1>);
            }
        }
    }
}
