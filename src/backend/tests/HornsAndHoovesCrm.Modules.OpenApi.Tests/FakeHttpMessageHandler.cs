using System.Net;
using System.Text;

namespace HornsAndHoovesCrm.Modules.OpenApi.Tests;

public class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly Dictionary<string, string> _responses;

    public FakeHttpMessageHandler(Dictionary<string, string> responses)
    {
        _responses = responses;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var uri = request.RequestUri!.ToString();

        if (_responses.TryGetValue(uri, out var content))
        {
            return Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            });
        }

        return Task.FromResult(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound
        });
    }
}