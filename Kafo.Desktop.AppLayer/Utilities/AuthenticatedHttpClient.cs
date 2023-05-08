using System.Net.Http.Headers;
using System.Text.Json;
using Kafo.Desktop.AppLayer.Authorization;
using Kafo.Desktop.AppLayer.Utilities.Models;
using Microsoft.Extensions.Options;

namespace Kafo.Desktop.AppLayer.Utilities;
public class AuthenticatedHttpClient : HttpClient
{
    private readonly UserInfo _userInfo;

    public AuthenticatedHttpClient(UserInfo userInfo, IOptions<AuthenticatedHttpClientOptions> options)
    {
        _userInfo = userInfo;
        BaseAddress = new Uri(options.Value.ApiAddress);
    }

    /// <summary>
    /// Посылает авторизованный GET запрос по указанной ссылке с содержимым <param name="content"></param>
    /// </summary>
    /// <typeparam name="TRequest">Тип объекта запроса</typeparam>
    /// <typeparam name="TResult">Тип объекта ответа</typeparam>
    /// <param name="requestUri">URL запроса</param>
    /// <param name="content">Содержимое запроса</param>
    /// <returns>Результат запроса</returns>
    public async Task<TResult> GetAuthenticatedAsync<TRequest, TResult>(string? requestUri, TRequest? content) where TRequest : class where TResult : class
    {
        if (string.IsNullOrEmpty(requestUri))
            throw new ArgumentNullException(nameof(requestUri));
        if (!_userInfo.IsLoggedIn)
            throw new InvalidOperationException("Not Authenticated");

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri))
        {
            requestMessage.Headers.Add("Authorization", "Bearer " + _userInfo.JwtToken);
            if (content != null)
                requestMessage.Content = new StringContent(JsonSerializer.Serialize(content), new MediaTypeHeaderValue("application/json"));

            var result = await SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();

            var resultContent = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResult>(resultContent, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        };

    }

    /// <summary>
    /// Посылает авторизованный GET запрос по указанной ссылке
    /// </summary>
    /// <typeparam name="TRequest">Тип объекта запроса</typeparam>
    /// <typeparam name="TResult">Тип объекта ответа</typeparam>
    /// <param name="requestUri">URL запроса</param>
    /// <param name="content">Содержимое запроса</param>
    /// <returns>Результат запроса</returns>
    public async Task<TResult> GetAuthenticatedAsync<TResult>(string? requestUri) where TResult : class
    {
        if (string.IsNullOrEmpty(requestUri))
            throw new ArgumentNullException(nameof(requestUri));
        if (!_userInfo.IsLoggedIn)
            throw new InvalidOperationException("Not Authenticated");

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri))
        {
            requestMessage.Headers.Add("Authorization", "Bearer " + _userInfo.JwtToken);
            var result = await SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();

            var resultContent = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResult>(resultContent, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }


    /// <summary>
    /// Посылает авторизованный POST запрос по указанной ссылке
    /// </summary>
    /// <typeparam name="TRequest">Тип объекта запроса</typeparam>
    /// <typeparam name="TResult">Тип объекта ответа</typeparam>
    /// <param name="requestUri">URL запроса</param>
    /// <param name="content">Содержимое запроса</param>
    /// <returns>Результат запроса</returns>

    public async Task<TResult> PostAuthenticatedAsync<TRequest, TResult>(string? requestUri, TRequest? content) where TRequest : class where TResult : class
    {
        if (string.IsNullOrEmpty(requestUri))
            throw new ArgumentNullException(nameof(requestUri));
        if (!_userInfo.IsLoggedIn)
            throw new InvalidOperationException("Not Authenticated");

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri))
        {
            requestMessage.Headers.Add("Authorization", "Bearer " + _userInfo.JwtToken);
            requestMessage.Content = new StringContent(JsonSerializer.Serialize(content), new MediaTypeHeaderValue("application/json"));


            var result = await SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();

            var resultContent = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResult>(resultContent, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        };
    }
    
    /// <summary>
    /// Посылает авторизованный POST запрос по указанной ссылке 
    /// </summary>
    /// <typeparam name="TRequest">Тип объекта запроса</typeparam>
    /// <typeparam name="TResult">Тип объекта ответа</typeparam>
    /// <param name="requestUri">URL запроса</param>
    /// <param name="content">Содержимое запроса</param>

    public async Task PostAuthenticatedAsync<TRequest>(string? requestUri, TRequest? content) where TRequest : class
    {
        if (string.IsNullOrEmpty(requestUri))
            throw new ArgumentNullException(nameof(requestUri));
        if (!_userInfo.IsLoggedIn)
            throw new InvalidOperationException("Not Authenticated");

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri))
        {
            requestMessage.Headers.Add("Authorization", "Bearer " + _userInfo.JwtToken);
            requestMessage.Content = new StringContent(JsonSerializer.Serialize(content), new MediaTypeHeaderValue("application/json"));


            var result = await SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();
        };
    }

}
