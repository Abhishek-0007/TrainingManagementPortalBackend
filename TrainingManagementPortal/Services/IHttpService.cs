using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using TrainingManagementPortal.Models.RequestViewModels;

namespace TrainingManagementPortal.Services
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> SendAsync(HttpCallRequestModel vm);
    }

    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(IServiceProvider service)
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            _httpClient = new HttpClient(handler);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpCallRequestModel vm)
        {
            HttpResponseMessage response = await _httpClient.SendAsync(new HttpRequestMessage
            {
                RequestUri = new Uri(vm.Url),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(vm.Body), Encoding.UTF8, MediaTypeNames.Application.Json)
            });
            response.EnsureSuccessStatusCode(); 
            return response;
        }
    }
}
