using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    /// <summary>
    /// Http请求帮助类
    /// </summary>
    public static class HttpClientHelper
    {
        private static readonly ServiceProvider _serviceProvider =
            new ServiceCollection().AddHttpClient().BuildServiceProvider();

        private static async Task HttpClientFactoryTest()
        {
            var httpClientFactory = _serviceProvider.GetService<IHttpClientFactory>();
            var client = httpClientFactory.CreateClient();
            var response = await client.SendAsync(new HttpRequestMessage(System.Net.Http.HttpMethod.Get, "https://www.aliyun.com/"));
            var content = await response.Content.ReadAsStringAsync();

            string tmp = string.Empty;
        }
    }
}
