using NLog;
using RestSharp;
using Testiny.Helpers.Configuration;
using System.Text.Json;

namespace Testiny.Clients
{
    public sealed class RestClientExtended
    {
        private readonly RestClient _client;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public RestClientExtended(bool isActualKey = true)
        {
            var options = new RestClientOptions(Configurator.AppSettings.URI ?? throw new InvalidOperationException());

            _client = new RestClient(options);
            _client.AddDefaultHeader("accept", "application/json");

            if (isActualKey)
            {
                _client.AddDefaultHeader("X-Api-Key", Configurator.AppSettings.XApiKey);
            }
            else
            {
                _client.AddDefaultHeader("X-Api-Key", Configurator.AppSettings.XApiKeyDeleted);
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }

        private void LogRequest(RestRequest request)
        {
            _logger.Debug($"{request.Method} request to: {request.Resource}");

            var body = request.Parameters
                .FirstOrDefault(p => p.Type == ParameterType.RequestBody)?.Value;

            if (body != null)
            {
                _logger.Debug($"body: {JsonSerializer.Serialize(body)}");
            }
        }

        private void LogResponse(RestResponse response)
        {
            if (response.ErrorException != null)
            {
                _logger.Error(
                    $"Error retrieving response. Check inner details for more info. \n{response.ErrorException.Message}");
            }

            _logger.Debug($"Request finished with status code: {response.StatusCode}");

            if (!string.IsNullOrEmpty(response.Content))
            {
                _logger.Debug($"Responce: {response.Content}");
            }
        }

        public async Task<RestResponse> ExecuteAsync(RestRequest request)
        {
            LogRequest(request);
            var response = await _client.ExecuteAsync(request);
            LogResponse(response);

            return response;
        }

        public async Task<T> ExecuteAsync<T>(RestRequest request)
        {
            LogRequest(request);
            var response = await _client.ExecuteAsync<T>(request);
            LogResponse(response);

            return response.Data ?? throw new InvalidOperationException();
        }
    }
}