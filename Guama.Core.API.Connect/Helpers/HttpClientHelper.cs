using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MOROPO.Framework.Guama.Core.Presentation.API.Connect.Helpers
{
    public class HttpClientHelper
    {
        private static readonly string CONTENT_TYPE = "APPLICATION/JSON";

        private readonly HttpClient client;

        private readonly System.Func<KeyValuePair<string, string>> authorizationTokenGetter;
        private readonly System.Func<KeyValuePair<string, string>[]> extraHeadersGetter;

        public HttpClientHelper(string baseUrl, System.Func<KeyValuePair<string, string>> authorizationTokenGetter = null, System.Func<KeyValuePair<string, string>[]> extraHeadersGetter = null, System.TimeSpan timeout = default)
        {
            this.authorizationTokenGetter = authorizationTokenGetter;

            this.extraHeadersGetter = extraHeadersGetter;

            client = new HttpClient { BaseAddress = new System.Uri(baseUrl), Timeout = timeout == default ? System.TimeSpan.FromSeconds(30) : timeout };
        }

        public HttpClientHelper(HttpClientHandler handler, string baseUrl, System.Func<KeyValuePair<string, string>> authorizationTokenGetter = null, System.Func<KeyValuePair<string, string>[]> extraHeadersGetter = null, System.TimeSpan timeout = default)
        {
            this.authorizationTokenGetter = authorizationTokenGetter;

            this.extraHeadersGetter = extraHeadersGetter;

            client = new HttpClient(handler) { BaseAddress = new System.Uri(baseUrl), Timeout = timeout == default ? System.TimeSpan.FromSeconds(30) : timeout };
        }

        public async Task DeleteAsync(string url)
        {
            EnsureRequiredHeadersInclution();

            var response = await client.DeleteAsync(url);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }
        }

        public async Task<TResponse> GetAsync<TResponse>(string url)
        {
            EnsureRequiredHeadersInclution();

            var response = await client.GetAsync(url);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }

        public async Task PostAsync<TRequest>(string urlOrResource, TRequest request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, CONTENT_TYPE);

            EnsureRequiredHeadersInclution();

            var response = await client.PostAsync(urlOrResource, content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string urlOrResource, TRequest request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, CONTENT_TYPE);

            EnsureRequiredHeadersInclution();

            var response = await client.PostAsync(urlOrResource, content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }

        public async Task PostFormAsync(string urlOrResource, MultipartFormDataContent multipartFormDataContent)
        {
            EnsureRequiredHeadersInclution();

            var response = await client.PostAsync(urlOrResource, multipartFormDataContent);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }
        }

        public async Task PutFormAsync(string urlOrResource, MultipartFormDataContent multipartFormDataContent)
        {
            EnsureRequiredHeadersInclution();

            var response = await client.PutAsync(urlOrResource, multipartFormDataContent);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string urlOrResource, TRequest request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, CONTENT_TYPE);

            EnsureRequiredHeadersInclution();

            var response = await client.PutAsync(urlOrResource, content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }

        public async Task PutAsync<TRequest>(string urlOrResource, TRequest request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, CONTENT_TYPE);

            EnsureRequiredHeadersInclution();

            var response = await client.PutAsync(urlOrResource, content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }
        }

        public async Task PutAsync(string urlOrResource)
        {
            var content = new StringContent(string.Empty, System.Text.Encoding.UTF8, CONTENT_TYPE);

            EnsureRequiredHeadersInclution();

            var response = await client.PutAsync(urlOrResource, content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new UnspectedHttpStatusException { HttpStatusCode = response.StatusCode };
            }
        }

        private void EnsureRequiredHeadersInclution()
        {
            if (authorizationTokenGetter != null)
            {

                var authorizationToken = authorizationTokenGetter.Invoke();

                if (string.IsNullOrWhiteSpace(authorizationToken.Key) || string.IsNullOrWhiteSpace(authorizationToken.Value))
                {
                    return;
                }

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authorizationToken.Key, authorizationToken.Value);
            }

            if (extraHeadersGetter != null)
            {
                var extraHeaders = extraHeadersGetter.Invoke();

                foreach (var header in extraHeaders)
                {
                    if (client.DefaultRequestHeaders.Contains(header.Key))
                    {
                        client.DefaultRequestHeaders.Remove(header.Key);
                    }

                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }
    }
}
