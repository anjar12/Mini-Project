using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Shared.Data.Model;

namespace Shared.Utils
{
    public class Helpers
    {
        public class HTTPService
        {
            public async Task<T> PostWithHeaderCustoms<T>(string uri, IList<HeadersRequest> headersRequests)
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, uri);
                    foreach (HeadersRequest data in headersRequests)
                    {
                        request.Headers.Add(data.headerName, data.headerValue);
                    }
                    using var httpResponse = await client.SendAsync(request);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        //string result = httpResponse.Content.ReadAsStringAsync().Result;
                        //return JsonConvert.DeserializeObject<T>(result);
                        return await httpResponse.Content.ReadFromJsonAsync<T>();
                    }
                    else
                    {
                        return default;
                    }
                }
            }
            public async Task<Tuple<HttpResponse, BaseResponse>> PostWithTokenResult<tPost>(string uri, string token, BaseRequest<tPost> postData)
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, uri);
                    var strTestForPostman = JsonSerializer.Serialize(postData);//// for debuging programmer only
                    request.Content = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    using var httpResponse = await client.SendAsync(request);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        return new Tuple<HttpResponse, BaseResponse>(new HttpResponse(), await httpResponse.Content.ReadFromJsonAsync<BaseResponse>());
                    }
                    else
                    {
                        HttpResponse httpRes = new HttpResponse();
                        httpRes.StatusCode = httpResponse.StatusCode.ToString();
                        httpRes.ReasonPhrase = httpResponse.ReasonPhrase;
                        return new Tuple<HttpResponse, BaseResponse>(httpRes, null);
                    }
                }
            }
            public async Task<Tuple<HttpResponse, BaseResponseValue<tResult>>> PostWithTokenResultValue<tPost, tResult>(string uri, string token, BaseRequest<tPost> postData)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.Timeout = TimeSpan.FromMinutes(3);
                        var request = new HttpRequestMessage(HttpMethod.Post, uri);
                        var strTestForPostman = JsonSerializer.Serialize(postData);//// for debuging programmer only
                        request.Content = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        using var httpResponse = await client.SendAsync(request);
                        httpResponse.EnsureSuccessStatusCode();
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            return new Tuple<HttpResponse, BaseResponseValue<tResult>>(new HttpResponse(), await httpResponse.Content.ReadFromJsonAsync<BaseResponseValue<tResult>>());
                        }
                        else
                        {
                            HttpResponse httpRes = new HttpResponse();
                            httpRes.StatusCode = httpResponse.StatusCode.ToString();
                            httpRes.ReasonPhrase = httpResponse.ReasonPhrase;
                            return new Tuple<HttpResponse, BaseResponseValue<tResult>>(httpRes, null);
                        }
                    }
                }
                catch (Exception e)
                {
                    HttpResponse httpRes = new HttpResponse();
                    //httpRes.StatusCode = httpResponse.StatusCode.ToString();
                    //httpRes.ReasonPhrase = httpResponse.ReasonPhrase;
                    return new Tuple<HttpResponse, BaseResponseValue<tResult>>(httpRes, null);
                }
            }
        }
        public async Task<JwtToken> RequestToken(string uri, string key, string value)
        {
            JwtToken tokenRequest = new JwtToken();
            ///////////// Get token
            try
            {
                HeadersRequest headersRequest = new HeadersRequest();
                headersRequest.headerName = key;
                headersRequest.headerValue = value;

                IList<HeadersRequest> listHeadersRequests = new List<HeadersRequest>();
                listHeadersRequests.Add(headersRequest);

                string address = uri;
                tokenRequest = await new HTTPService().PostWithHeaderCustoms<JwtToken>(uri, listHeadersRequests);
            }
            catch (Exception ex)
            {

            }
            return tokenRequest;
        }

    }
}

